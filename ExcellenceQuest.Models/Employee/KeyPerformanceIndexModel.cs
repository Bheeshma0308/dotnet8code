namespace ExcellenceQuest.Models.Employee
{
    using ExcellenceQuest.Models.Common;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Text.Json.Serialization;

    public class KeyPerformanceIndexModel :BaseObject
    {
        [Required(ErrorMessage="Name is Required")]

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only characters and spaces are allowed.")]
        [StringLength(350, ErrorMessage = " Kpi name should be less than 35")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Description is Required")]

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only characters and spaces are allowed.")]
        [StringLength(350, ErrorMessage = " Kpi description should be less than 350")]
        public string Description { get; set; }
        [Required]
        [Range(1,3,ErrorMessage ="StreamId must be between 1 to 3")]
        public int StreamId { get; set; }
        [JsonIgnore]
        public StreamModel Stream { get; set; }
        [Required]
        [Range(1, 3, ErrorMessage = "SubCompetencyId must be between 1 to 3")]
        public int SubCompetencyId { get; set; }
        [JsonIgnore]
        public SubCompetencyModel SubCompetency { get; set; }
    }
}
