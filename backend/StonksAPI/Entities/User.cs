namespace StonksAPI.Entities
{
    /*
     * Entity Framework class which represents the User
     */
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public virtual List<Holding> Holdings { get; set; }
    }
}
