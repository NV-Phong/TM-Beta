using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Taskdetail
{
    public int Idtask { get; set; }

    public string? Description { get; set; }

    public string? Attachments { get; set; }

    public virtual Task IdtaskNavigation { get; set; } = null!;
}
