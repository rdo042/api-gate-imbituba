namespace GateAPI.Domain.Entities.Integracao
{
    public class Driver
    {
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsValidDriver { get; set; }
        public object? Documents { get; set; }
        public string? Base64FacialBiometrics { get; set; }
        public string? Base64Document { get; set; }
    }

}
