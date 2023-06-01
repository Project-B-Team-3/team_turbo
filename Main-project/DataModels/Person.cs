namespace Main_project.DataModels;

public class Person
{
	public string Name { get; set; }
	public DateTime Birthdate { get; set; }
	public string DocumentNum { get; set; }

	public Person(string name, DateTime birthDate, string documentNum)
	{
		Name = name;
		Birthdate = birthDate;
		DocumentNum = documentNum;
	}

	public override string ToString()
	{
		return $"{Name}, {Birthdate:dd/MM/yyyy}, {DocumentNum}";
	}
}