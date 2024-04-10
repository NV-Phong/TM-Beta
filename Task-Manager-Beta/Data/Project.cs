using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Project
{
    public int Idproject { get; set; }

    public string? ProjectName { get; set; }

    public int? Idleader { get; set; }

    public DateTime? DayCreate { get; set; }

    public string? Image { get; set; }

    public int? Hide { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Permisssion> Permisssions { get; set; } = new List<Permisssion>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();
}
