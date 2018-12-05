using EMS.Models;
using EMS.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.BirthDayWisher
{
    public class ExecuteDaily
    {
        public static void SendEmails(Occasions occasion)
        {
            EmployeeService employeeService = new EmployeeService();
            List<Employee> employees = employeeService.GetEmployeesByOccassion(occasion);
            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail(employees, occasion);
        }
    }
}
