using System;
using System.Collections.Generic;

namespace FirstCoreApp.ModelsNew;

public partial class Department
{
    public int DeptId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
