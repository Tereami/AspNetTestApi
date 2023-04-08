namespace AspNetTestApi.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public int FailedAccessCount { get; set; }

        public string Role { get; set; }

        public string PasswordHash { get; set; }
    }
}
