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
        private AppDbContext _AppDbContext;
        List< Employee> _ListEmp;
        public EmployeeRepository(IConfiguration configuration, AppDbContext AppDbContext)
        {
            _configuration = configuration;
            _AppDbContext = AppDbContext;
        }

        public  Employee Add( Employee employee)
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


        public  Employee Update( Employee emp)
        {
            try
            {
                var record = _AppDbContext.Employees.Where(x => x.Id == emp.Id).FirstOrDefault();
                if(record != null)
                {
                    record.Age = emp.Age;
                    record.Name = emp.Name;
                    record.Email = emp.Email;
                    _AppDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return emp;
        }
        public List< Employee> GetAll()
        {
            _ListEmp = new List< Employee>();
            try
            {
                //Method Sytnax of linq 
                _ListEmp = _AppDbContext.Employees.Where(x => x.Id > 2).OrderByDescending(o => o.Name).ToList();

                //Query syntax

                var result = from emp in _AppDbContext.Employees
                             where emp.Id > 2
                             orderby emp.Name descending                           
                             select emp;

                _ListEmp = result.ToList();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return _ListEmp;
        }

        public  Employee GetEmployee(int id)
        {
             Employee emp = new  Employee();
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
                var record = _AppDbContext.Employees.Where(x => x.Id == id).FirstOrDefault();
                if(record != null)
                {
                    _AppDbContext.Remove(record);
                    _AppDbContext.SaveChanges();
                }
                //using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Default")))
                //{
                //    SqlCommand cmd = new SqlCommand("delete from Employee where Id = @Id", con);
                //    cmd.CommandType = CommandType.Text;
                //    cmd.Parameters.AddWithValue("@Id", id);
                //    con.Open();
                //    count = cmd.ExecuteNonQuery();

                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return count;
        }
    }
}
