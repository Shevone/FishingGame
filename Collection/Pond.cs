using System.Text;
using FishingGame.Fishes;

namespace FishingGame.Collection;

// Класс - водоем
public class Pond
{
    public Pond(string name)
    {
        _fishes = new List<Fish>();
        _caughtFish = 0;
        Name = name;
    }

    public string Name { get; }
    // Список рыб в водоеме
    private List<Fish> _fishes;
    // Возвращаем копию
    public List<Fish> Fishes => new(_fishes);

    // Число выловленных рыб
    private int _caughtFish;
    public int CaughtFish => _caughtFish;
    // Метод добавления
    public void Add(Fish fish)
    {
        _fishes.Add(fish);
    }
    // Метод удаления
    public bool Remove(Fish fish)
    {
        if (_fishes.Remove(fish))
        {
            _caughtFish++;
            return true;
        }
        return false;
    }
    
}