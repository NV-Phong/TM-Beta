using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Listtemplate
{
    public int IdlistTemplate { get; set; }

    public int Idtemplate { get; set; }

    public string? StatusName { get; set; }

    public virtual Template IdtemplateNavigation { get; set; } = null!;
}
