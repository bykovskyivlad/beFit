using System.ComponentModel.DataAnnotations;

namespace beFit.Models
{
    public class TrainingSession
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        


        [Display(Name = "Start Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
    }
}
