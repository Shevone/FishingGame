namespace FishingGame.Person;

// Просто класс паспортных данных который хранит фамилию и имя рыбака
public class Passport
{
    private string Name;
    private string Surname;

    public Passport(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public override string ToString()
    {
        return $"Фамилия : {Surname}, Имя : {Name}";
    }
}