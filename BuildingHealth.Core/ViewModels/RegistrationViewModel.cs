namespace BuildingHealth.Core.ViewModels
{
    public class RegistrationViewModel : LoginViewModel
    {
        public string PasswordConfirm { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}
