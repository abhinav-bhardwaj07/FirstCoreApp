using System;
using System.Collections.Generic;

namespace FirstCoreApp.ModelsNew;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int? Age { get; set; }

    public int DeptId { get; set; }

    public string Email { get; set; }

    public virtual Department Dept { get; set; }
}
