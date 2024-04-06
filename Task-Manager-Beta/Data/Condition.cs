using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Condition
{
    public int Idcondition { get; set; }

    public int Idpermission { get; set; }

    public int Idworkflow { get; set; }

    public virtual Permisssion IdpermissionNavigation { get; set; } = null!;

    public virtual Workflow IdworkflowNavigation { get; set; } = null!;
}
