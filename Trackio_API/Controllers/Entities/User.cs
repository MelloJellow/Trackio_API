namespace Trackio_API.Controllers.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }//Validate and join first+last on frontend
        public string Email { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty; //drop down list of countries
        public string Password { get; set; } = string.Empty;

     

    }
}
