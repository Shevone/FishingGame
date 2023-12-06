namespace FishingGame.Fishes;

// Абстрактный клас - рыба
public abstract class Fish
{
    // Название рыбы
    private string _name;
    
    // ----------------------------
    // Вес рыбы
    private double _weight;
    
    public double Weight
    {
        get => _weight;
        init
        {
            if (value <= 0.1)
            {
                value = 0.1;
            }

            _weight = value;
        }
    }
    // ----------------------------
    
    // Цена за килограм
    private double _pricePerKilo;

    public double PricePerKilo
    {
        get => _pricePerKilo;
        init
        {
            if (value <= 1)
            {
                value = 1;
            }
            _pricePerKilo = value;
        }
    }
    // ----------------------------

    public Fish(string name, double weight, double pricePerKilo)
    {
        _name = name;
        Weight = weight;
        _pricePerKilo = pricePerKilo;
    }
    public abstract string DisplayInfo();

    public override string ToString()
    {
        return $"Название : {_name}, Вес(кг.) : {Weight}, Цена за кг. : {PricePerKilo}";
    }
}