using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Member
{
    public int Idmember { get; set; }

    public int Iduser { get; set; }

    public int Idproject { get; set; }

    public virtual Project IdprojectNavigation { get; set; } = null!;

    public virtual User IduserNavigation { get; set; } = null!;
}
