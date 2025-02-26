using System.ComponentModel.DataAnnotations;

namespace SimpleCrudApp.Models.Entities
{
    public class ProjectTask
    {
        [Key]
        public int TaskId { get; set; }

        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTime? DueDate { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
