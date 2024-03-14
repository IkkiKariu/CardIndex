using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CardIndex.Core.Entities;
using Npgsql;


namespace CardIndex.DataAccess.Postgres.Repositories
{
    public class EmployeeRepository
    {
        private static NpgsqlConnection InitConnection(string host, string username, string password, string database)
        {
            return new NpgsqlConnection($"Host={host};Username={username};Password={password};Database={database}");
        }

        public IEnumerable<Employee> GetAll()
        {
            return null;
        }

        public void SaveEmployee(Employee employee)
        {

        }

        private void InsertEmployee(Employee employee)
        {
            using(var conn = InitConnection("localhost", "postgres", "0000", "card_index"))
            {
                conn.Open();

                using(var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO employee (first_name, middle_name, last_name, birth_date, employment_date, position, department) " +
                                      "VALUES (@first_name, @middle_name, @last_name, @birth_date, @employment_date, @position, @department)";
                    cmd.Parameters.AddWithValue("first_name", employee.FirstName);
                    cmd.Parameters.AddWithValue("middle_name", employee.MiddleName);
                    cmd.Parameters.AddWithValue("last_name", employee.LastName);
                    cmd.Parameters.AddWithValue("birth_date", employee.BirthDate);
                    cmd.Parameters.AddWithValue("employment_date", employee.EmploymentDate);
                    cmd.Parameters.AddWithValue("position", employee.Position);
                    cmd.Parameters.AddWithValue("department", employee.Department);
                }
            }
        }

        private void UpdateEmployee(Employee employee)
        {

        }

        public void DeleteEmployee(int employeeId)
        {

        }

        public static void CreateEmployeeTable()
        {
            using (var conn = InitConnection("localhost", "postgres", "0000", "card_index"))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS employee(" +
                                      "id SERIAL PRIMARY KEY," +
                                      "first_name VARCHAR(255)," +
                                      "middle_name VARCHAR(255)," +
                                      "last_name VARCHAR(255)," +
                                      "birth_date TIMESTAMP," +
                                      "employment_date TIMESTAMP," +
                                      "position VARCHAR(255)," +
                                      "department VARCHAR(255)" +
                                      ")";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
