using System.ComponentModel.DataAnnotations;

namespace Trackio_API.Controllers.Entities
{
    public class Jobs
    {

        public int Id { get; set; }
        public string jTitle { get; set; } = string.Empty;
        public string jCompany { get; set; } = string.Empty; //drop down list of countries
        public string jLocation { get; set; } = string.Empty;
        public string jDescription { get; set; } = string.Empty;
        public string jPostedDate { get; set; } = string.Empty;
        public string JPostURL { get; set; } = string.Empty;
        public int jSalary { get; set; } = 0; 
    }
}
