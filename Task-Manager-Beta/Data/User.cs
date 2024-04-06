using System;
using System.Collections.Generic;

namespace Task_Manager_Beta.Data;

public partial class User
{
    public int Iduser { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? Hide { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Permisssion> Permisssions { get; set; } = new List<Permisssion>();
}
