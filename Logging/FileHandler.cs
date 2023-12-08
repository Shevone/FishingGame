namespace FishingGame.Logging;

// Класс, содержащий метод печати в файл
public class FileHandler
{
    // Поле - путь до файла логирования
    private string _logFilePath;

    public FileHandler(string logFilePath)
    {
        // Конструктор класса - записив файл
        // Проверям, существует ли файл по указаному адресу
        // Нам приходи относительный пыть до файла
        // Формируем полный путь до файла и проверяем его
        string fullFilePath = Directory.GetCurrentDirectory()[..^16] + logFilePath;
        if (!Directory.Exists(fullFilePath))
        {
            // Если файл не сущствует, то создаем и закрываем 
            File.Create(fullFilePath).Close();
        }
        // Далее просто записываем в переменную 
        _logFilePath = fullFilePath;
    }
    // Метод печати сообщения в файл
    public void FileLog(string message)
    {
        string messageString = DateTime.Now + " | " + message + "\n"; 
        File.AppendAllText(_logFilePath,messageString);
    }
    
    
}