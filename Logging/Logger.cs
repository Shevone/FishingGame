namespace FishingGame.Logging;

public class Logger
{
    public delegate void LogHandler(string message); // делегат(шаблон того, какие методы мы сможем записать в наш Event)

    public event LogHandler? LogMethods; // event, куда записаны методы логирования
    
    // Метод добавления методов в event
    public void AddHandler(LogHandler handler)
    {
        // Записываем все передаенные метожы логгирования в переменную
        LogMethods += handler;
    }
    
    // Метод вызвова лога
    // Обобщенный, принимает на вход любой объект и выщывает у него метод ToString()
    // Вызыват все методы, помещенный в event LogMethods
    public void LogMessage<T>(T obj)
    {
        string message = obj.ToString();
        LogMethods?.Invoke(message);
    }
}