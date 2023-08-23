using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;

namespace FirstCoreApp.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        List<Employee> _ListEmp;
        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Employee Add(Employee employee)
        {
            if(_ListEmp.Where(x=> x.Age == employee.Age).Count() > 0)
            {

            }
            else
            {
                employee.Id = _ListEmp.Max(x => x.Id) + 1;
                _ListEmp.Add(employee);
            }
          
            return employee;
        }

        public List<Employee> GetAll()
        {
            _ListEmp = new List<Employee>();
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    SqlCommand cmd = new SqlCommand("select * from Employee", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                   
                    while(rdr.Read())
                    {
                        int id = rdr.GetInt32("ID");
                        string name = rdr.GetString("Name");
                        string Email = rdr.GetString("Email");
                        int age = rdr.GetInt32("Age");

                        _ListEmp.Add(new Employee
                        {
                            Id = id,
                            Name = name,
                            Email = Email,
                            Age = age
                        });
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return _ListEmp;
        }

        public Employee GetEmployee(int id)
        {
            return _ListEmp.FirstOrDefault(x => x.Id == id);
        }
    }
}
