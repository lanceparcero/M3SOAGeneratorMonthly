using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOAEmailSenderMonthly
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                args = new string[] { DateTime.Now.ToShortDateString() };

            DateTime date;
            if (DateTime.TryParse(args[0], out date))
            {
                int[] mid;
                int month = date.Month;
                int year = date.Year;
                Console.WriteLine("Fetching Merchant Settlement Cutoff");
                using (M3Entities entity = new M3Entities())
                {
                    mid = (from sb in entity.bil_MonthlySettlementCutOff
                           where sb.smonth == month && sb.syear == year && sb.Total_SOASendEmail > 0
                           select sb.merchant_id).ToArray();
                }

                Fetch get = new Fetch();
                string path = string.Empty;

                foreach (int m in mid)
                {
                    Console.WriteLine("Creating PDF...");
                    path = get.MonthlySOA(date.ToShortDateString(), m.ToString());
                    if (!string.IsNullOrEmpty(path))
                    {
                        try
                        {
                            using (M3Entities entity = new M3Entities())
                            {
                                Console.WriteLine("Fetching merchant contact");
                                var mercontact = (from mer in entity.m3_merchant
                                                  join contact in entity.vw_MerchantFirstContact on mer.merchant_id equals contact.merchant_id into mercon
                                                  from mc in mercon.DefaultIfEmpty()
                                                  where mer.merchant_id == m
                                                  select new
                                                  {
                                                      email = mc.email,
                                                      mer.CorporateID
                                                  }).FirstOrDefault();

                                m3_EmailSender emailsender = new m3_EmailSender();
                                emailsender.Body = Properties.Settings.Default.Body;
                                emailsender.Subject = Properties.Settings.Default.Subject;
                                emailsender.RetryCount = 0;
                                emailsender.Status = 1;
                                emailsender.CreatedOn = DateTime.Now;
                                emailsender.CorporateID = mercontact.CorporateID;
                                emailsender.merchant_id = m;
                                entity.m3_EmailSender.Add(emailsender);
                                entity.SaveChanges();

                                Console.WriteLine("Checking email sender");
                                if (emailsender.EmailSenderID > 0)
                                {
                                    m3_EmailRecipient emailreceipient = new m3_EmailRecipient();
                                    emailreceipient.EmailSenderID = emailsender.EmailSenderID;
                                    emailreceipient.Email = mercontact.email ?? string.Empty;
                                    emailreceipient.RecipientType = 1;
                                    entity.m3_EmailRecipient.Add(emailreceipient);

                                    m3_EmailAttachment emailattachment = new m3_EmailAttachment();
                                    emailattachment.EmailSenderID = emailsender.EmailSenderID;
                                    emailattachment.FilePath = path;
                                    emailattachment.FilePathType = 1;
                                    entity.m3_EmailAttachment.Add(emailattachment);

                                    entity.SaveChanges();
                                }

                            }
                            Console.WriteLine(path + " Success");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(path + " Failed");
                            Console.WriteLine(ex.Message);
                        }

                    }
                }
            }

            Console.WriteLine("Finish");
            Thread.Sleep(5000);

        }


    }
}
