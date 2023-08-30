using System.Collections.Generic;

namespace FirstCoreApp.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);

        List<FirstCoreApp.ModelsNew.Employee> GetAll();
        Employee Add(Employee employee);
        FirstCoreApp.ModelsNew.Employee Update(FirstCoreApp.ModelsNew.Employee emp);

        int Delete(int id);

    }
}
