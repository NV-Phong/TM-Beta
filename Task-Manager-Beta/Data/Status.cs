using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Status
{
    public int Idstatus { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();
}
