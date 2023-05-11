public class Person
{
    public string Name { get; set; }
    public DateTime Birthdate { get; set; }
    public string DocumentNum { get; set; }

    public Person(string name, DateTime birthdate, string documentNum)
    {
        Name = name;
        Birthdate = birthdate;
        DocumentNum = documentNum;
    }
}