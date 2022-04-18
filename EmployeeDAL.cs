using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace MVC_Demo2.Models
{
    public class EmployeesDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public EmployeesDAL()
        {
            con = new SqlConnection(Startup.ConnectionString);

        }

        public List<Employees> GetAllEmployees()
        {
            cmd = new SqlCommand("Select * from Employees", con);
            con.Open();
            dr = cmd.ExecuteReader();
            List<Employee> employeesList = new List<Employee>();
            employeesList = ArrangeList(dr);
            con.Close();
            return employeesList;
        }
        public List<Employee> ArrangeList(SqlDataReader dr)
        {
            List<Employee> employeesList = new List<Employee>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Employee employees = new Employee();

                    employees.Id = Convert.ToInt32(dr["EmpId"]);
                    employees.Name = (dr["EmpName"]).ToString();
                    employees.Salary = Convert.ToDecimal(dr["EmpSalary"]);
                    employeesList.Add(employees);
                }
                return employeesList;
            }
            else
            {
                return null;
            }
        }
        public int Save(Employees employees)
        {
            string qry = "Insert into Employees values (@EmpName, @EmpSalary)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@EmpName", employees.EmpName);
            cmd.Parameters.AddWithValue("@EmpSalary", employees.EmpSalary);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public Employees GetEmployeeById(int id)
        {
            cmd = new SqlCommand("select * from Employees where EmpId = @EmpId", con);
            cmd.Parameters.AddWithValue("@EmpId", id);
            con.Open();
            Employee employees = new Employee();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    employees.Id = Convert.ToInt32(dr["EmpId"]);
                    employees.Name = dr["EmpName"].ToString();
                    employees.Salary = Convert.ToDecimal(dr["EmpSalary"]);
                }
            }
            con.Close();
            return employees;
        }

        public int Update(Employees employees)
        {
            cmd = new SqlCommand("update Employees set EmpName=@name,EmpSalary=@Salary where EmpId=@id", con);
            cmd.Parameters.AddWithValue("@name", employees.EmpName);
            cmd.Parameters.AddWithValue("@Salary", employees.EmpSalary);
            cmd.Parameters.AddWithValue("@id", employees.EmpId);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int Delete(int id)
        {

            cmd = new SqlCommand("delete from Employees where EmpId=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}

