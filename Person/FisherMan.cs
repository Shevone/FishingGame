using System.Text;
using FishingGame.Fishes;

namespace FishingGame.Person;
// Класс рыбак
public class FisherMan
{
    // Список пойманнх рыб
    private List<Fish> _fishesCaught;
    private Passport _passportData;

    // Свойсто - цена выкупа всех рыб
    private double _redemptionPrice
    {
        // Суммируем ценц всех пойманных рыб
        get
        {
            double sum = 0;
            foreach (Fish fish in _fishesCaught)
            {
                sum += fish.PricePerKilo * fish.Weight;
            }
            return sum;
        }
    }
    // Конструтор
    public FisherMan(string name, string surname)
    {
        _passportData = new Passport(name, surname);
        _fishesCaught = new List<Fish>();
    }

    public void CaughtFish(Fish fish)
    {
        _fishesCaught.Add(fish);
    }
    // Переопределям то как выглядит в строковом эквиваленте
    public override string ToString()
    {
        string info = $"Рыбак {_passportData}, Количество пойманных рыб : {_fishesCaught.Count}, Цена выкупа : {_redemptionPrice}\n---\n";
        StringBuilder stringBuilder = new StringBuilder(info);
        // Записываем в информацию для вывода все пойманные рыбы
        foreach (Fish fish in _fishesCaught)
        {
            // Полиморфный вызов
            stringBuilder.Append(fish.DisplayInfo() + "\n");
        }

        stringBuilder.Append("---");
        return stringBuilder.ToString();
    }
}