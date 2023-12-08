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
    private IPond<CheapFish> CheapFishes;
    private IPond<StandardFish> StandardFishes;
    private IPond<PremiumFish> PremiumFishes;

    // ----------------------------------

    public FisherPlace()
    {
        CheapFishes = new Pond<CheapFish>("Недорогие рыбы");
        StandardFishes = new Pond<StandardFish>("Cтандарт");
        PremiumFishes = new Pond<PremiumFish>("Премиум");
        
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
        // Нам приходит рыба с более обобщенным типом, мы смотрим, каким более конкретным типом она является и исходя из этого
        // добавляем в новый класс
        switch (fish)
        {
            // Пишем: <тип> <Если fish является этим типом то приводим и засовываем в переменную тут> 
            case CheapFish cheapFish:
                CheapFishes.Add(cheapFish, count);
                break;
            case StandardFish standardFish:
                StandardFishes.Add(standardFish, count);
                break;
            case PremiumFish premiumFish:
                PremiumFishes.Add(premiumFish, count);
                break;
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
    public string DoFishing(int fisherIndex, string pondIndex)
    {
        // Нам приходит индекс рыбака и индекс типа водоема
        // В зависимости от индекса выбираем тот водоем в котором будут происходить рыбалка
        
        FisherMan fisherMan = FisherMens[fisherIndex];
        string result;
        switch (pondIndex)
        {
            case "1" :
                // не дорогие
                result = FishingExcute(CheapFishes, fisherMan);
                break;
            case "2" :
                // средние
                result = FishingExcute(StandardFishes, fisherMan);
                break;
            case "3" :
                // ПРемиум
                result = FishingExcute(PremiumFishes, fisherMan);
                break;
            default:
                result = "Нет такого водоёма...";
                break;
        }

        return result;
    }
    // Вынесли в отдельный обобщенный метод - то как производится рыбалка
    // Это нужно чтобы у нас не было потворения кода, а мы могли просто передать необходимый нам водоём
    private string FishingExcute<T>(IPond<T> pond, FisherMan fisherMan) where T : Fish
    {
        // Генерируем рандомное число, чтобы мы могли собрать по индексу нашу рыбу
        // Число генирурется в диапазоне от -Количество рыб в водоеме, до Количество рыб в водоёме
        // -Количество рыб в водоеме - необходимо чтобы был шанс не поймать рыбу
        Random rnd = new Random();
        int fishIndex = rnd.Next(0-(pond.Count*2), pond.Count);
        T caughtFish = null;
        int i = 0;
        // Пытаемся найти рыбу с таким индексом
        foreach (T fish in pond)
        {
            if (i == fishIndex)
            {
                caughtFish = fish;
            }
            i++;
        }
        
        if (caughtFish != null)
        {
            pond.Remove(caughtFish);
            fisherMan.CaughtFish(caughtFish);

            return $"{fisherMan.FisherManNameSurname()}\n |  поймал : {caughtFish.DisplayInfo()}";
        }
        return $"{fisherMan.FisherManNameSurname()}\n | ничего не поймал(";
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
        // Формируем информацию о каждом водоеме и выводим в сумме
        string cheapInfo = CurPondInfo(CheapFishes, 1);
        string standeartInfo = CurPondInfo(StandardFishes, 2);
        string premiumInfo = CurPondInfo(PremiumFishes, 3);
        return cheapInfo + standeartInfo + premiumInfo;

    }
    // Вынесли метод чтобы не было повторения кода
    private string CurPondInfo<T>(IPond<T> pond, int numer) where T : Fish
    {
        // Информация о водоёме
        StringBuilder stringBuilder = new StringBuilder();
        
        // Первый водоем инфа
        // Записываем в словарь - рыба, количество в водоеме, а потом переводим в строку
        Dictionary<T, int> fishes1 = new Dictionary<T, int>();
        stringBuilder.Append($"Водоём {numer} : {pond.Name}, количество выловленных рыб {pond.CaughtFish}\nРыбы в Водоёме 1\n");
        foreach (T fish in pond)
        {
            if (!fishes1.ContainsKey(fish))
            {
                fishes1.Add(fish,0);
            }
            fishes1[fish]++;
        }

        foreach (KeyValuePair<T,int> keyValuePair in fishes1)
        {
            stringBuilder.Append(keyValuePair.Key + $" | Количество : {keyValuePair.Value} \n");
        }

        stringBuilder.Append("\n\n");
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
    // -----------------------------------------------
    // Ниже описан метод вызывающий сортировку
     public string SortFishesInPonds(string typeSort)
    {
        // Func - делеагт( переменная которая хранит в себе метод)
        
        // Суть этого делегата в том, чтобы в перемнную func мы поместил метод,
        // который сравнивает элементы типа Fish по какому - либо признаку

        string message;
        Func<Fish, Fish, int> orderFunc;

        // В зависимости от того какой индекс был выбра, то записывамем в делеаг определенную анонимную функцию
        // Метод Compare и CompareTo возращают int
        // < 0 - когда второй больше первого
        // == 0 когда одинкаовы
        // > 0  когда первый больше второго
        switch (typeSort)
        {
            case "1":
                
                orderFunc = (fish1, fish2) => String.Compare(fish1.Name, fish2.Name, StringComparison.Ordinal);
                message = "Произведена сортировка по названиям";
                break;
            case "2":
                
                orderFunc = (fish1, fish2) => fish1.PricePerKilo.CompareTo(fish2.PricePerKilo);
                message = "Произведена сортировка цене за кг";
                break;
           
            case "3":
                orderFunc = (fish1, fish2) => fish1.Weight.CompareTo(fish2.Weight);
                message = "Произведена сортировка по весу";
                break;
            default:
                return "Ничего не отсортированно";
        }
        // Передаем делегат сравнения в методы выполнения сортирвоки внутри коллекций
        // То есть в метод сортировки, определенный в коллекции мы передаем то, как мы будем сравнитвать элемнты
        // Перейдем в клас Pond
        CheapFishes.InvokeSort(orderFunc);
        StandardFishes.InvokeSort(orderFunc);
        PremiumFishes.InvokeSort(orderFunc);
        return message;
    }
    
}
