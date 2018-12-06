using EMS.Models;
using EMS.SERVICES;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace EMS.BirthDayWisher
{
    public class EmailSender
    {
        List<string> _alias = new List<string>();
        private string SendGridApiKey = ConfigurationSettings.AppSettings["SendGridApiKey"];
        public async void SendEmail(List<Employee> employees, Occasions occassion)
        {
            try
            {
                if (employees.Count != 0)
                {
                    foreach (Employee e in employees)
                    {
                        // var email = CreateEmail(e, occassion);
                        // await SendEmail(email);
                        SendEmail(e, occassion);
                    }
                }
            }
            
            catch(Exception ex)
            {
                // logger
            }
}
private async void SendEmail(Employee e, Occasions occassion)
{

    try
    {
        MailMessage mailMsg = new MailMessage();
        // To
        mailMsg.To.Add(new MailAddress($"{e.Alias}@microsoft.com"));

        // From
        mailMsg.From = new MailAddress(ConfigurationSettings.AppSettings["FromAddress"], "EAS Fun Team");
        mailMsg.CC.Add(new MailAddress(ConfigurationSettings.AppSettings["CCAddress"]));

        string HTMLcontent;
        if (occassion == Occasions.Birthday)
        {
            string image = BirthdayImages();
            HTMLcontent = "<h3>Hi " + e.Name.Split(' ')[0] + ", <h3><h3> Wish you a very Happy Birthday!! May this day bring special moments to your life and make it much more wonderful.</h3><img src='" + image + "'";
            mailMsg.Subject = "Happy Birthday " + e.Name + " !!";
            mailMsg.IsBodyHtml = true;
            mailMsg.Body = HTMLcontent;
        }
        if (occassion == Occasions.Anniversary)
        {
            string year;
            string image = AnniImages();
            var PresentYear = DateTime.Now.Year;
            var AnniYear = e.DateOfJoining.Year;
            var TotalYears = PresentYear - AnniYear;
            if (TotalYears == 1)
            {
                year = "year";
            }
            else
            {
                year = "years";
            }
            HTMLcontent = "<h4> Hi " + e.Name.Split(' ')[0] + ", </h4><h4>Happy Work Anniversary!!! Congratulations on completing " + TotalYears + " " + year + " in Microsoft !!! Wishing you many more successful and happy years ahead. </h4> <img src='" + image + "'";
            mailMsg.Subject = "Congratulations " + e.Name + " on completing " + TotalYears + " " + year + " in Microsoft";
            mailMsg.IsBodyHtml = true;
            mailMsg.Body = HTMLcontent;
        }

        // Init SmtpClient and send
        SmtpClient smtpClient = new SmtpClient(Convert.ToString(ConfigurationSettings.AppSettings["SendgridHostName"]), Convert.ToInt32(587));
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(Convert.ToString(ConfigurationSettings.AppSettings["SendgridUserName"]), Convert.ToString(ConfigurationSettings.AppSettings["SendgridPassWord"]));
        smtpClient.Credentials = credentials;

        smtpClient.Send(mailMsg);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }


}


public EmailSender()
{
    EmployeeService employeeService = new EmployeeService();
    this._alias = employeeService.GetAllEmployeesAlias();
}

private SendGridMessage CreateEmail(Employee e, Occasions occassion)
{

    var msg = new SendGridMessage();
    msg.SetFrom(new EmailAddress(ConfigurationSettings.AppSettings["FromAddress"], "EAS Fun Team"));

    msg.AddTo($"{e.Alias}@microsoft.com");
    //List<string> EmailsForBCC = new List<string>(_alias);
    //EmailsForBCC.Remove(e.Alias);

    //List<EmailAddress> EmailAddressForBCC = new List<EmailAddress>();
    //foreach (string email in EmailsForBCC)
    //{
    //    EmailAddress emailAddress = new EmailAddress(email);
    //    EmailAddressForBCC.Add(emailAddress);
    //}
    msg.AddCc(ConfigurationSettings.AppSettings["CCAddress"]);
    //if (EmailAddressForBCC.Count != 0)
    //{
    //    msg.AddBccs(EmailAddressForBCC);
    //}

    string HTMLcontent;
    if (occassion == Occasions.Birthday)
    {
        string image = BirthdayImages();
        HTMLcontent = "<h3>Hi " + e.Name.Split(' ')[0] + ", <h3><h3> Wish you a very Happy Birthday!! May this day bring special moments to your life and make it much more wonderful.</h3><img src='" + image + "'";
        msg.SetSubject("Happy Birthday " + e.Name + " !!");
        msg.AddContent(MimeType.Html, HTMLcontent);
    }
    if (occassion == Occasions.Anniversary)
    {
        string year;
        string image = AnniImages();
        var PresentYear = DateTime.Now.Year;
        var AnniYear = e.DateOfJoining.Year;
        var TotalYears = PresentYear - AnniYear;
        if (TotalYears == 1)
        {
            year = "year";
        }
        else
        {
            year = "years";
        }
        HTMLcontent = "<h4> Hi " + e.Name.Split(' ')[0] + ", </h4><h4>Happy Work Anniversary!!! Congratulations on completing " + TotalYears + " " + year + " in Microsoft !!! Wishing you many more successful and happy years ahead. </h4> <img src='" + image + "'";
        msg.SetSubject("Congratulations " + e.Name + " on completing " + TotalYears + " " + year + " in Microsoft");
        msg.AddContent(MimeType.Html, HTMLcontent);
    }

    return msg;
}

private string BirthdayImages()
{
    List<string> Birthdayimages = new List<string>();
    Birthdayimages.Add("https://s-media-cache-ak0.pinimg.com/736x/ac/cb/94/accb941ce3fbf5575431b4e91da16f2b--greeting-cards-birthday-card-birthday.jpg");
    Birthdayimages.Add("http://photo.elsoar.com/wp-content/images/Happy-birthday-design-with-monkey-754x780.jpg");
    Birthdayimages.Add("http://www.chhotaghalib.com/wp-content/uploads/Happy-Birthday-wish.jpg");

    Random random = new Random();
    var number = random.Next(4);
    string image = Birthdayimages[number];
    return image;
}

private string AnniImages()
{
    List<string> Anniimages = new List<string>();
    Anniimages.Add("http://www.freelarge-images.com/wp-content/uploads/2015/01/Happy_Work_Anniversary_02.jpg");
    Anniimages.Add("https://pbs.twimg.com/media/CsKT3p7WIAA3PPY.jpg");
    Anniimages.Add("http://prodimages.hrdirect.com/G0771-Golden-Banner-Anniversary-Business-Anniversary-Card_xl.jpg");
    Anniimages.Add("https://s-media-cache-ak0.pinimg.com/600x315/e9/30/8e/e9308e1ba3d5085a887d99722d93c42a.jpg");

    Random random = new Random();
    var number = random.Next(4);
    string image = Anniimages[number];
    return image;
}

private async Task SendEmail(SendGridMessage message)
{
    try
    {
        var client = new SendGridClient(SendGridApiKey);
        await client.SendEmailAsync(message);
    }
    catch (Exception e)
    {

    }
}

    }

}
