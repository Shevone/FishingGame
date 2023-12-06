namespace FishingGame.Fishes;

// Класс премимум рыба
public class PremiumFish : Fish
{
    private bool IsRear;
    public PremiumFish(string name, double weight, double pricePerKilo, bool isRear) : base(name, weight, pricePerKilo)
    {
        IsRear = isRear;
    }
    // Переопределяем абстракнтый метд
    public override string DisplayInfo()
    {
        return "Рыба премиум " + base.ToString() +$", Редкая ли {IsRear}";
    }
}