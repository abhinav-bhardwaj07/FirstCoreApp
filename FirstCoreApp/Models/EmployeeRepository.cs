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
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    SqlCommand cmd = new SqlCommand("Insert into Employee(Name,Age,DeptId,Email) VALUES (@Name, @Age, @DeptId, @Email)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Age", employee.Age);
                    cmd.Parameters.AddWithValue("@DeptId", 1);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                   
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return employee;
        }


        public Employee Update(Employee emp)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    SqlCommand cmd = new SqlCommand("Update Employee SET Name = @Name, Age = @Age, Email = @Email where ID = @Id ;", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", emp.Name);
                    cmd.Parameters.AddWithValue("@Age", emp.Age);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@Id", emp.Id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return emp;
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
            Employee emp = new Employee();
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    SqlCommand cmd = new SqlCommand("select * from Employee where Id = @Id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        int ID = rdr.GetInt32("ID");
                        string name = rdr.GetString("Name");
                        string Email = rdr.GetString("Email");
                        int age = rdr.GetInt32("Age");

                        emp.Id = ID; emp.Name = name; emp.Email = Email;
                        emp.Age = age;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return emp;
        }

        public int Delete(int id)
        {
            int count = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    SqlCommand cmd = new SqlCommand("delete from Employee where Id = @Id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    count = cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return count;
        }
    }
}
