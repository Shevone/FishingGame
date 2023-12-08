namespace FishingGame.Logging;

// Класс, содержащий метод печати в консоль

public class ConsoleHandler
{
    // Метод печати лога в консоль
    public void ConsoleLog(string message)
    {
        Console.WriteLine("Сообщение:");
        Console.WriteLine(message);
        
        Console.WriteLine("\nНажмите любую клавишу чтобы продолжить");
        Console.ReadKey();
    }
}