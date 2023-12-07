using FishingGame.Collection;
using FishingGame.Fishes;
using FishingGame.Person;

namespace FishingGame;

static class Program
{
    // Объявем наше рыбацкое местечко
    private static FisherPlace FisherPlace = new FisherPlace();
    
    public static void Main(string[] args)
    {
        Covariance();
        Console.ReadKey();
        // Метод демонстрации ковариантности
        // Вызовем метод чтоб заполнить данные
        FillPlace();
        
        bool exitFlag = false;
        while (!exitFlag)
        {
            Console.Clear();
            // Выводим меню и считываем с консоли
            Console.WriteLine("Главное меню\n\n" +
                              "1 - Добавить рыбу\n" +
                              "2 - Информация о рыбах\n" +
                              "3 - Новый рыбак\n" +
                              "4 - Информация о рыбаках\n" +
                              "5 - Симулиорвать рыбалку\n" +
                              "6 - Информация о водоемах\n" +
                              "7 - Выход\n");
            string readLine = Console.ReadLine() ?? "";
            string message; // Сообщение, куда запишется результат текущей итерации
            switch (readLine)
            {
                case "1":
                    // Создание рыбы
                    message = CreateFish();
                    break;
                case "2":
                    // Информация о рыбах
                    message = FisherPlace.FishesInfo();
                    break;
                case "3":
                    // Новый рыбак
                    message = CreateFisherMan();
                    break;
                case "4":
                    // Информация о рыбаках
                    message = FisherPlace.FisherMansInfo();
                    break;
                case "5":
                    // Симулятор рыбалки
                    message = Fishing();
                    break;
                case "6":
                    // Информация о водоемах
                    message = FisherPlace.PondInfo();
                    break;
                case "7":
                    message = "Выход";
                    exitFlag = true;
                    break;
                default:
                    // Если введена какая - то другая строка
                    message = "Введено не корректное значение";
                    break;
            }
            Console.WriteLine(message+"\n\nЧтобы продолжить нажмите любую клавишу\n");
            Console.ReadKey();
        }
    }
    // Метод - вызывающий симуляцию рыбалки
    private static string Fishing()
    {
         // Выводим всех рыбаков и предлгаем пользавателю выбрать текущего рыбака для рыбалк
         Console.WriteLine(FisherPlace.FisherMansInfo()
                           +"\nВведите номер рыбака, который будет рыбачить");
         // Считываме номер и пытаемся перевести в число, если перевести не удастся, то вернем
         string readLine = Console.ReadLine() ?? "";
         bool parseResult = int.TryParse(readLine, out int index);
         index -= 1;
         if (parseResult == false )
         {
             return "Вы ввели что то не то";
         }
         if (!FisherPlace.IndexValid(index))
         {
             return "Нет такого рыбака";
         }
         // красивенько рисуем
         string message = "Забрасываем удочку";
         for (int i = 0; i < 10; i++)
         {
             Console.Clear();
             message += ".";
             if (message.Length == 22)
             {
                 message = "Забрасываем удочку";
             }
             Console.WriteLine(message);
             Thread.Sleep(150);
         }
         Console.WriteLine("\nВыберите тип водоема где хотите рыбачить\n" +
                           "1- Бюджетный\n" +
                           "2 - Стандартный\n" +
                           "3 - Премиум\n");
         string pondIndex = Console.ReadLine() ?? "";
         string fishResult = FisherPlace.DoFishing(index,pondIndex);
         return fishResult;
    }
    // метод который обрабатывает элемент меню по созданию нового рыбака
    private static string CreateFisherMan()
    {
        Console.WriteLine("Введите имя новго рыбака");
        string name = Console.ReadLine() ?? "";
        Console.WriteLine("Введите фамилию нового рыбака");
        string surname = Console.ReadLine() ?? "";

        if (surname == "" || name == "")
        {
            return "Вы оставили пустые строки при вводе данных о рыбаке. Рыбак не создан";
        }

        FisherMan fisherMan = new FisherMan(name, surname);
        FisherPlace.AddNewFisherMen(fisherMan);
        return $"Рыбак {name} {surname} успешно создан";
    }
    // Метод который обрабатывает элемент меню по созданию новой рыбы
    private static string CreateFish()
    {
        Console.WriteLine("Введите название рыбы");
        string name = Console.ReadLine() ?? "";
        
        Console.WriteLine("Введите вес рыбы");
        // 1. Считывается строка с консоли
        // 2. Елси оставили её пустой то ввелось ""
        // 3. Пытаемся привести считанное значени с консоли к типу double
        // если не получается то в переменную weightParse записывается false
        // если получается то true, и приведенное значение типа double  
        // Запишется в переменную weight
        bool weightParse = double.TryParse(Console.ReadLine() ?? "", out double weight);
        
        Console.WriteLine("Введите цену за килограм");
        // 1. Считывается строка с консоли
        // 2. Елси оставили её пустой то ввелось ""
        // 3. Пытаемся привести считанное значени с консоли к типу double
        // если не получается то в переменную costParse записывается false
        // если получается то true, и приведенное значение типа double  
        // Запишется в переменную cost
        bool costParse = double.TryParse(Console.ReadLine() ?? "", out double cost);
        
        // Проверям правильность того, как введены значения
        if (name == "" || weightParse == false || costParse == false)
        {
            return "Базовые значения введены не корректно";
        }
        
        // Далее в зависимости от того, какая была введена цена мы создаём нашу рыбу под определенным типом
        // Если нужно, то запрашиваем доп параметры
        Fish fish;
        switch (cost)
        {
            case  < 10:
                // Если цена меньше 10 - то рыба считается не дорогой
                fish = new CheapFish(name, weight, cost);
                break;
            case >= 10 and <40 :
                Console.WriteLine("Введите 1 - ели рыба цветастая");
                string colorful = Console.ReadLine() ?? "";
                fish = new StandardFish(name, weight, cost,colorful == "1");
                break;
            // >=40
            default:
                Console.WriteLine("Введите 1 - ели рыба редкая");
                string rear = Console.ReadLine() ?? "";
                fish = new PremiumFish(name, weight, cost,rear == "1");
                break;
        }
        // Считываем то, сколько рыбы добавляем(нужно для вобоёма)
        Console.WriteLine("Введите количество(для добавления в водоём)");
        bool countParseResult = int.TryParse(Console.ReadLine() ?? "1", out int count);
        if (countParseResult == false) count = 1;
        FisherPlace.AddNewFish(fish,count);
        return $"Создана рыба\n{fish.DisplayInfo()}\nДобавлена в водоём количестве {count}";
    }
    
    // Метод в коотором заполним данные для удобвства демонстрации возможности программы
    private static void FillPlace()
    {
        // Создаём рыбаков
        FisherMan fisherMan1 = new FisherMan("Вася", "Пупкин");
        FisherMan fisherMan2 = new FisherMan("Петя", "Пирожков");
        FisherMan fisherMan3 = new FisherMan("Коля", "Булавочкин");
        
        // Создадим рыб
        StandardFish standardFish1 = new StandardFish("Сом", 5, 14, false);
        StandardFish standardFish2 = new StandardFish("Язь", 0.4, 10, false);

        CheapFish cheapFish1 = new CheapFish("Карась", 1, 5);
        CheapFish cheapFish2 = new CheapFish("Окунь", 0.2, 5);
        CheapFish cheapFish3 = new CheapFish("Плотва", 0.1, 5);

        PremiumFish premiumFish1 = new PremiumFish("Бестер", 1, 40, true);
        //Добавим рыб
        FisherPlace.AddNewFisherMen(fisherMan1);
        FisherPlace.AddNewFisherMen(fisherMan2);
        FisherPlace.AddNewFisherMen(fisherMan3);
        
        FisherPlace.AddNewFish(standardFish1, 5);
        FisherPlace.AddNewFish(standardFish2, 5);
        FisherPlace.AddNewFish(cheapFish1, 10);
        FisherPlace.AddNewFish(cheapFish2, 7);
        FisherPlace.AddNewFish(cheapFish3, 13);
        FisherPlace.AddNewFish(premiumFish1, 2);
    }

    private static void Covariance()
    {
        // Создадим рыб
        StandardFish standardFish1 = new StandardFish("Сом", 5, 14, false);
        StandardFish standardFish2 = new StandardFish("Язь", 0.4, 10, false);
        
        // Создадим водоём
        Pond<StandardFish> standardFishesPond = new Pond<StandardFish>("Стандарт");
        
        // Добавим туда рыб
        standardFishesPond.Add(standardFish1, 2);
        standardFishesPond.Add(standardFish2, 5);
        
        // --------------------------
        // Теперь перейдем к демонстрации возможностей ковариантности
        // Поместим наш объект типа Pond<StandardFish> под тип интерфейса
        IPondCovariance<StandardFish> standardFishesInteface = standardFishesPond;
        
        // Продемонстрируем какой тип вернет сейчас ковариантный метод
        StandardFish[] standardFishesArray = standardFishesInteface.ExampleMethod();
        StandardFish standardFish = standardFishesInteface.GetItem(1);
        Console.WriteLine(standardFish?.DisplayInfo());
        
        // Теперь приведем к более обобщенносму типу и покажем что верентся
        IPondCovariance<Fish> fishes1 = standardFishesPond;
        IPondCovariance<Fish> fishes = standardFishesInteface;
        // StandardFish[] standardFishesArray2 = fishes.ExampleMethod(); Так не  получится тепепрь сделать
        Fish[] fish = fishes.ExampleMethod(); // Теперь метод возращает болле обобщенный тип
        Fish fish2 = fishes.GetItem(1);
        Console.WriteLine(fish2?.DisplayInfo());
        
        // Вся суть в том, что мы можем присвоить более обобщенному типу IPondCovariance<Fish> объект боллее конктреного типа
        // Pond<StandartFish> или IPondCovariance<StandardFish>
        
        // ЕЩЕ РАЗ САМОЕ ВАЖНОЕ
        IPondCovariance<Fish> fishes2 = new Pond<StandardFish>("d");
        IPondCovariance<StandardFish> standardFishes = new Pond<StandardFish>("a");
        IPondCovariance<Fish> fishes3 = standardFishes;

    }
}