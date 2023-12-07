namespace FishingGame.Collection;

// Ковариантный инетерфейс
public interface IPondCovariance<out T> : IEnumerable<T>
{
    // Ковариантный метод
    // Возвращает объект с типом обобщения(T)
    public T[] ExampleMethod();

    public T GetItem(int index);
}