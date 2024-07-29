namespace ExcellenceQuest.Models.Badge
{
    using ExcellenceQuest.Models.Common;
    using System.ComponentModel.DataAnnotations;

    public class BadgeModel:BaseObject
    {
       
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage ="Numbers are not allowed.")]
        [StringLength(20,ErrorMessage ="BadgeName must be less tha 20 characters")]

        public string Name { get; set; }

        
        public string Description { get; set; }
    }
}
