using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Status
{
    public int Idstatus { get; set; }

    public int Idproject { get; set; }

    public string? StatusName { get; set; }

    public virtual Project IdprojectNavigation { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();
}
