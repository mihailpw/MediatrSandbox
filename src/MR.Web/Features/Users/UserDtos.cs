namespace MR.Web.Features.Users
{
    public static class UserDtos
    {
        public class Basic
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public class Full : Basic
        {
            public string Description { get; set; }
        }
    }
}