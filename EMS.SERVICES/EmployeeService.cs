using EMS.DAL;
using EMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.SERVICES
{
    public class EmployeeService
    {
        private EMSContext db = new EMSContext();
        public IEnumerable<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }
        public Employee GetEmployeeDetails(int? id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return null;
            }
            return employee;
        }
        public bool AddEmployee(Employee employee)
        {
            try
            {

                db.Employees.Add(employee);
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                //logger
                return false;
            }
        }
        public bool UpdateEmployee(Employee employee)
        {
            try
            {

                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                //logger
                return false;
            }
        }
        public List<Employee> GetEmployeesByOccassion(Occasions occasion)
        {
            if (occasion == Occasions.Birthday)
            {
                var employee = db.Employees.Where(x=>x.DateOfBirth.Day==DateTime.Today.Day && x.DateOfBirth.Month == DateTime.Today.Month).ToList();
                return employee;
            }
            else
            {
                var employee = db.Employees.Where(x => x.DateOfJoining.Day == DateTime.Today.Day && x.DateOfJoining.Month == DateTime.Today.Month).ToList();
                return employee;
            }
        }
        public List<string> GetAllEmployeesAlias()
        {
            var alias = db.Employees.Select(x => x.Alias).ToList();
            return alias;
        }
    }
}
