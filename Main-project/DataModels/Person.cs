using System.Globalization;

public class Person
{
    public string Name { get; set; }
    public string BirthdateString { get; set; }
    public string DocumentNum { get; set; }

    public DateTime Birthdate
    {
        get
        {
            return DateTime.ParseExact(BirthdateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        }
    }

    public Person(string name, string birthdateString, string documentNum)
    {
        Name = name;
        BirthdateString = birthdateString;
        DocumentNum = documentNum;
    }
}