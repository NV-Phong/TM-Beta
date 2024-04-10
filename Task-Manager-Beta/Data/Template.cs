using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Template
{
    public int Idtemplate { get; set; }

    public int Idproject { get; set; }

    public int Idstatus { get; set; }

    public virtual Project IdprojectNavigation { get; set; } = null!;

    public virtual Status IdstatusNavigation { get; set; } = null!;
}
