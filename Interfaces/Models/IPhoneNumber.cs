namespace Domain.Interfaces
{
    public interface IPhoneNumber
    {
        string Number { get; set; }
        bool IsActive { get; set; }
    }
}
