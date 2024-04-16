using Task_Manager_Beta.Data;

namespace Task_Manager_Beta.ViewModels
{
    public class DashBoardViewModel
    {
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Condition> Condition { get; set; } = new List<Condition>();
        public List<Listtemplate> Listtemplate { get; set; } = new List<Listtemplate>(); 
        public List<Member> Member { get; set; } = new List<Member>(); 
        public List<Permisssion> Permisssions { get; set; } = new List<Permisssion>();
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Status> Status { get; set; } = new List<Status>();
        public List<Data.Task> Tasks { get; set; } = new List<Data.Task>();
        public List<Taskdetail> Taskdetail { get; set; } = new List<Taskdetail>();
        public List<Template> Templates { get; set; } = new List<Template>();
        public List<Workflow> Workflows { get; set; } = new List<Workflow>();
    }
}
