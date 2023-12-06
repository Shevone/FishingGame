using System.Text;
using FishingGame.Collection;
using FishingGame.Fishes;
using FishingGame.Person;

namespace FishingGame;

// Класс - наша арена для ловли рыб
public class FisherPlace
{
    // Список рыбаков
    private List<FisherMan> FisherMens;
    // HashSet рыб(содержит только уникальные значения)
    private HashSet<Fish> Fishes;

    // ----------------------------------
    // Водоёмы
    private Pond Pond;
    
    // ----------------------------------

    public FisherPlace()
    {
        Pond = new Pond("Водоём");
        Fishes = new HashSet<Fish>();
        FisherMens = new List<FisherMan>();
    }

    // Метод добавления новго рыбака
    public void AddNewFisherMen(FisherMan fisherMan)
    {
        FisherMens.Add(fisherMan);
    }
    
    // Метод добавления новой рыбы
    public void AddNewFish(Fish fish, int count)
    {
        // Добавляем нашу рыбу в общий сет, для того чтобы просто понимать какие рыбы вобще есть в проге
        Fishes.Add(fish);
        
        // Добавляем в водоем то количество рыб, какое захотим
        for (int i = 0; i < count; i++)
        {
            Pond.Add(fish);
        }
        
    }
    
    // Симулятор рыбалки
    // Перадем индекс рыбака. 
    // Достаем слуйчайную рыбу из водоема и записываем её в пойманные рыбаку
    public bool IndexValid(int index)
    {
        // Проверяем что выбранный индекс рыбака, корректный
        if (index < 0 || index >= FisherMens.Count) return false;
        return true;
    }
    public string DoFishing(int fisherIndex)
    {
        
        FisherMan fisherMan = FisherMens[fisherIndex];
        
        
        // Генерируем рандомное число, чтобы мы могли собрать по индексу нашу рыбу
        // Число генирурется в диапазоне от -Количество рыб в водоеме, до Количество рыб в водоёме
        // -Количество рыб в водоеме - необходимо чтобы был шанс не поймать рыбу
        Random rnd = new Random();
        int fishIndex = rnd.Next(-(Pond.Fishes.Count/2), Pond.Fishes.Count);
        if (fisherIndex >= 0)
        {
            Fish caughtFish = Pond.Fishes[fishIndex];
            Pond.Remove(caughtFish);
            fisherMan.CaughtFish(caughtFish);

            return $"Поймана рыба : \n{caughtFish.DisplayInfo()}";
        }
        return "Неудача ;(";
    }
    // ------------------------------------------------------------------------------------------
    // Ниже описаны методы, которые формируют текстовую информацию об объектах в нашем "рыбацком месте"
    public string FisherMansInfo()
    {
        // Информаци о рыбаках
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Рыбаки\n");
        int i = 1;
        foreach (FisherMan fisherMan in FisherMens)
        {
            stringBuilder.Append(i.ToString() + " - " +fisherMan + "\n");
            i++;
        }

        return stringBuilder.ToString();
    }

    public string PondInfo()
    {
        // Информация о водоёмах
        StringBuilder stringBuilder = new StringBuilder();
        
        // Первый водоем инфа
        // Записываем в словарь - рыба, количество в водоеме, а потом переводим в строку
        Dictionary<Fish, int> fishes1 = new Dictionary<Fish, int>();
        stringBuilder.Append($"Водоём 1 : {Pond.Name}, количество выловленных рыб {Pond.CaughtFish}\nРыбы в Водоёме 1\n");
        foreach (Fish fish in Pond.Fishes)
        {
            if (!fishes1.ContainsKey(fish))
            {
                fishes1.Add(fish,0);
            }
            fishes1[fish]++;
        }

        foreach (KeyValuePair<Fish,int> keyValuePair in fishes1)
        {
            stringBuilder.Append(keyValuePair.Key + $" | Количество : {keyValuePair.Value} \n");
        }

        return stringBuilder.ToString();
    }

    public string FishesInfo()
    {
        // ИНформация о рыбах
        StringBuilder stringBuilder = new StringBuilder();
        
        // Первый водоем инфа
        foreach (Fish fish in Fishes)
        {
            // Полиморфный вызов
            // Мы проходимся по списку Fish, и fish - именно тип Fish/
            // Однако, когда мы вызываем метод DisplayInfo, то вызовется этот метод, переопределнный именно в классе населднике
            stringBuilder.Append(fish.DisplayInfo() + "\n");
        }

        return stringBuilder.ToString();
    }
}