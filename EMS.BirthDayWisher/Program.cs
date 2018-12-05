using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Models;
using Microsoft.Azure.WebJobs;

namespace EMS.BirthDayWisher
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {

            ExecuteDaily.SendEmails(Occasions.Birthday);
            ExecuteDaily.SendEmails(Occasions.Anniversary);
        }
    }
}
