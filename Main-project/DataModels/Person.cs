using System.Globalization;

namespace Main_project.DataModels;

public class Person
{
	public string Name { get; set; }
	public string DocumentNum { get; set; }
	public DateTime DateOfBirth { get; set; }

	public Person(string name, DateTime dateOfBirth, string documentNum)
	{
		Name = name;
		DateOfBirth = dateOfBirth;
		DocumentNum = documentNum;
	}
}