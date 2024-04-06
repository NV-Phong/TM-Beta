using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Workflow
{
    public int Idworkflow { get; set; }

    public int Idstatus { get; set; }

    public string? Transition { get; set; }

    public virtual ICollection<Condition> Conditions { get; set; } = new List<Condition>();

    public virtual Status IdstatusNavigation { get; set; } = null!;
}
