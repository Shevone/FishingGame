namespace FishingGame.Fishes;

// Не дорогая рыба
public class CheapFish : Fish
{
    public CheapFish(string name, double weight, double pricePerKilo) : base(name, weight, pricePerKilo)
    {
    }
    // Переопределяем абстрактный метод
    public override string DisplayInfo()
    {
        // Добавлем свою подпись, а потом вызываем метод To String У базого класса, там определено как выглядит общая информация о рыбе
        return "Рыба не дорогая, " + base.ToString();
    }
}