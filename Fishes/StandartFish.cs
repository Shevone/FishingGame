namespace FishingGame.Fishes;

// Стандартная рыба
public class StandardFish : Fish
{
    private bool IsColorFul;
    public StandardFish(string name, double weight, double pricePerKilo, bool isColor) : base(name, weight, pricePerKilo)
    {
        IsColorFul = isColor;
    }
    // Переопределяем абстрактный метод
    public override string DisplayInfo()
    {
        return "Рыба стандартная, " + base.ToString() + $", Цветная ли {IsColorFul}";
    }
}