using FishingGame.Fishes;

namespace FishingGame.Collection;

// Интерфейс для коллекции
public interface IPond<T> : IEnumerable<T> where T : Fish
{
    // Свойство - название водоёма
    public string Name { get; }
    // Свойство - число выловленных рыб 
    public int CaughtFish { get; }
    
    // Свойство, количество рыб в водоеме
    public int Count { get; }
    // Метод добавления
    public void Add(T fish, int count);
    // Метод удаления рыбы
    public bool Remove(T removingFish);
    
    // Метод сортировки
    public void InvokeSort(Func<T,T,int> order);
}