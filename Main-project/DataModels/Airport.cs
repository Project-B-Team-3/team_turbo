namespace Main_project.DataModels;

public class Airport
{
    public string PhoneNumber { get; }
    public string Address { get; }
    public string Email { get; }

    public Airport(string phoneNumber, string address, string email)
    {
        PhoneNumber = phoneNumber;
        Address = address;
        Email = email;
    }
}
