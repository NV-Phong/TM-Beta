using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Task
{
    public int Idtask { get; set; }

    public int Idproject { get; set; }

    public int Idstatus { get; set; }

    public string? TaskName { get; set; }

    public DateTime? DayCreate { get; set; }

    public DateTime? DayStart { get; set; }

    public DateTime? Deadline { get; set; }

    public int? Hide { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual Project IdprojectNavigation { get; set; } = null!;

    public virtual Status IdstatusNavigation { get; set; } = null!;

    public virtual Taskdetail? Taskdetail { get; set; }
}
