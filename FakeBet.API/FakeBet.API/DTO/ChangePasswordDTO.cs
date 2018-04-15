namespace FakeBet.API.DTO
{
    public class ChangePasswordDTO
    {
        public string NickName { get; set; }

        public string NewPassword { get; set; }

        public string CurrentPassword { get; set; }
    }
}