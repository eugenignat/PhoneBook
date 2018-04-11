using Domain.Interfaces;


namespace Domain
{
    public class PhoneNumber : IPhoneNumber
    {
        public bool IsActive { get; set; }

        public string Number { get; set; }
    }
}

