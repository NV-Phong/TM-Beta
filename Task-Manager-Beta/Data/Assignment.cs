using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Assignment
{
    public int Idassignment { get; set; }

    public int Iduser { get; set; }

    public int Idtask { get; set; }

    public virtual Task IdtaskNavigation { get; set; } = null!;

    public virtual User IduserNavigation { get; set; } = null!;
}
