using System.Collections;
using System.Text;
using FishingGame.Fishes;

namespace FishingGame.Collection;

// Класс - водоем
public class Pond<T> : IPond<T> where T : Fish
{
    // Список, хранящий рыб в водоёме
    private List<T> _fishes;
    public Pond(string name)
    {
        _fishes = new List<T>();
        _caughtFish = 0;
        Name = name;
    }
    // ----------------------------------------------------------------------
    public string Name { get; }

    // Число выловленных рыб
    private int _caughtFish;
    public int CaughtFish => _caughtFish;
    
    // Текущее число рыб
    public int Count => _fishes.Count;
    // ----------------------------------------------------------------------
    // Метод GetEnumerator необходим нам для того чтобы мы могли итерироваться по объекту класса Pond
    // Например мы создадим Pond<PremiumFish> premiumPond = new(name);
    // И мы сможем засунуть этот объект в цикл foreach
    // foreach(PremiumFish fish in premiumPond)
    public IEnumerator<T> GetEnumerator()
    {
        return _fishes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    // =========================
    // Далее 2 метода, прописанных в IPond
    // Метод добавления
    public void Add(T fish, int count)
    {
        for (int i = 0; i < count; i++)
        {
            _fishes.Add(fish);
        }
    }
    // Метод удаления
    public bool Remove(T removingFish)
    {
        if (_fishes.Remove(removingFish))
        {
            _caughtFish++;
            return true;
        }
        return false;
    }
}