namespace FakeBet.API.DTO
{
    public class ChangePasswordDTO
    {
        public string Nickname { get; set; }

        public string NewPassword { get; set; }

        public string CurrentPassword { get; set; }
    }
}