namespace Main_project.DataModels;

public class Person
{
    public string Name { get; set; }
    public string Birthdate { get; set; }
    public string DocumentNum { get; set; }

    public Person(string name, string birthDate, string documentNum)
    {
        Name = name;
        Birthdate = birthDate;
        DocumentNum = documentNum;
    }
}