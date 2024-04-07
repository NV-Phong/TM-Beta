using Task_Manager_Beta.Data;

namespace Task_Manager_Beta.ViewModels
{
    public class ProjectViewModel
    {
        public List<Template> Templates { get; set; }
        public List<Data.Task> Tasks { get; set; }
    }
}
