namespace WebApiWorkshop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User(string firstName, string lastName, int id)
        {
            // This constructor makes it so string props can not be null. As u can see we dont get any warnings anymore
            FirstName = firstName;
            LastName = lastName;

            Id = id; // int can not be null and would be auto incremented in a database, this is simply for demonstration purposes.

        }

    }
}
