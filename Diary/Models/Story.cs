using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Diary.Models
{
    public class Story
    {
        public int storyId { get; set; }
        [Required]
        [Display (Name="Story")]
        public string storyTitle { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime dateStory { get ;set; }
        [Display(Name = "Rate")]
        [Range(1,5)]
        public int rateOfStory { get; set; }
        [Required]
        [Display(Name = "Text")]
        public string textStory { get; set; }
        [Display(Name = "Image")]
        public string imageStory { get; set; }
    }

    public class StoryDBContext : DbContext
    {
        public DbSet<Story> Stories { get; set; }
    }
}
