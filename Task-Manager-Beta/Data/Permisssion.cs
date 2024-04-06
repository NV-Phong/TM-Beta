using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Permisssion
{
    public int Idpermission { get; set; }

    public int Iduser { get; set; }

    public int Idproject { get; set; }

    public string? Role { get; set; }

    public string? Object { get; set; }

    public string? Privilege { get; set; }

    public virtual ICollection<Condition> Conditions { get; set; } = new List<Condition>();

    public virtual Project IdprojectNavigation { get; set; } = null!;

    public virtual User IduserNavigation { get; set; } = null!;
}
