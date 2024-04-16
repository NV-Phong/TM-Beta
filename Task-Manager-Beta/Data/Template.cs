using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class Template
{
    public int Idtemplate { get; set; }

    public string? TemplateName { get; set; }

    public virtual ICollection<Listtemplate> Listtemplates { get; set; } = new List<Listtemplate>();
}
