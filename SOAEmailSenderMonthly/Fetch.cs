using GemBox.Spreadsheet;
using OfficeOpenXml;
using SOAEmailSenderMonthly.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SOAEmailSenderMonthly
{
    class Fetch
    {
        public string MonthlySOA(string SettlementDate = "", string MerchantID = "")
        {
            DateTime filterStartDate;
            bool isDate = DateTime.TryParse(SettlementDate, out filterStartDate);

            DateTime firstDayOfMonth = new DateTime(filterStartDate.Year, filterStartDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            Console.WriteLine("Initialize Excel Details");
            if (isDate)
            {
                DataAccess _excel = new DataAccess();
                MemoryStream memstr = new MemoryStream();
                var outputfilt = "";
                using (M3Entities mycontext = new M3Entities())
                {
                    string transactid = "";
                    int startrow = 21;
                    int startrowfooter = 0;
                    int no_ = 1;
                    DataTable settlementtransactions = _excel.getMonthlySoaHeaders(filterStartDate.Month, filterStartDate.Year, MerchantID);
                    DataTable transactions = _excel.getMonthlySoaTransactions(firstDayOfMonth, lastDayOfMonth, MerchantID);
                    //  DateTime settlementdate = DateTime.Parse(settlementtransactions.Rows[0]["settlement_date"].ToString());
                    FileInfo newFile = new FileInfo(Settings.Default.SourceFilePath.ToString() + "\\GHL Month Transaction Statement.xlsx");
                    ExcelPackage pck = new ExcelPackage(newFile);
                    var ws = pck.Workbook.Worksheets[1];
                    ws.View.ShowGridLines = false;

                    if (settlementtransactions.Rows.Count != 0)
                    {
                        outputfilt = Settings.Default.DestinationPath.ToString() + "\\SOAMonthly" + filterStartDate.ToString("yyyyMM") + "-" + settlementtransactions.Rows[0]["merchant_id"].ToString();
                       
                        //settlementdate = DateTime.Now;
                        ws.Cells["K4"].Value = firstDayOfMonth.ToString("MM/dd/yyyy") + "  -  " + lastDayOfMonth.ToString("MM/dd/yyyy");
                        ws.Cells["K5"].Value = settlementtransactions.Rows[0]["CompanyCode"].ToString() + "-" + filterStartDate.Year.ToString() + "-" + settlementtransactions.Rows[0]["DocID"].ToString().PadLeft(5, '0');
                        ws.Cells["K6"].Value = settlementtransactions.Rows[0]["merchant_id"].ToString();
                        ws.Cells["K9"].Value = decimal.Parse(settlementtransactions.Rows[0]["BalanceBroughtForward"].ToString());
                        ws.Cells["K10"].Value = decimal.Parse(settlementtransactions.Rows[0]["TotalTransaction"].ToString());
                        ws.Cells["K11"].Value = decimal.Parse(settlementtransactions.Rows[0]["TotalCBRF"].ToString());
                        ws.Cells["K12"].Value = decimal.Parse(settlementtransactions.Rows[0]["TotalTransactionAdjustment"].ToString());
                        ws.Cells["K13"].Value = decimal.Parse(settlementtransactions.Rows[0]["Others"].ToString());
                        ws.Cells["K14"].Value = decimal.Parse(settlementtransactions.Rows[0]["LessPaid"].ToString());
                        ws.Cells["K15"].Value = decimal.Parse(settlementtransactions.Rows[0]["BalanceCarryForward"].ToString());
                        //header indicator

                        ws.Cells["E19"].Value = "Txn Amount" + " (" + settlementtransactions.Rows[0]["SettlementCurrency"].ToString() + ")";
                        ws.Cells["H19"].Value = "MDR" + " (" + settlementtransactions.Rows[0]["SettlementCurrency"].ToString() + ")";
                        ws.Cells["I19"].Value = transactions.Rows[0]["COUNTRY"].ToString() == "PH" ? "WHT" : "GST" + " (" + settlementtransactions.Rows[0]["SettlementCurrency"].ToString() + ")";
                        ws.Cells["J19"].Value = "Amount - Section A" + " (" + settlementtransactions.Rows[0]["SettlementCurrency"].ToString() + ")";
                        ws.Cells["K19"].Value = "Amount - Section B" + " (" + settlementtransactions.Rows[0]["SettlementCurrency"].ToString() + ")";



                        ws.Cells["J26"].Value = ws.Cells["I19"].Value;

                        ws.Cells["B4"].Value = settlementtransactions.Rows[0]["InternalCompanyName"];
                        ws.Cells["B5"].Value = settlementtransactions.Rows[0]["Address1"];
                        ws.Cells["B6"].Value = settlementtransactions.Rows[0]["Address2"];
                        ws.Cells["B7"].Value = settlementtransactions.Rows[0]["Address3"];
                        ws.Cells["B8"].Value = "Company Tax Registration No: " + settlementtransactions.Rows[0]["taxid"];
                        ws.Cells["B9"].Value = "Company No: " + settlementtransactions.Rows[0]["businessregistionid"];
                        ws.Cells["B11"].Value = settlementtransactions.Rows[0]["registration_name"];
                        ws.Cells["B12"].Value = settlementtransactions.Rows[0]["ContactName"];
                        ws.Cells["B13"].Value = settlementtransactions.Rows[0]["TIN"];
                        ws.Cells["B14"].Value = settlementtransactions.Rows[0]["Address"];
                        ws.Cells["B15"].Value = settlementtransactions.Rows[0]["City"];
                        ws.Cells["B16"].Value = settlementtransactions.Rows[0]["ContactEmail"];


                        if (transactions.Rows.Count != 0)
                        {
                            ws.InsertRow(21, (transactions.Rows.Count), 21);

                            for (int i = 0; i < transactions.Rows.Count; i++)
                            {

                                ws.Cells[startrow, 1].Value = Convert.ToDateTime(transactions.Rows[i]["statementdate"]).ToString("dd/MM/yyy");
                                ws.Cells["A" + startrow + ":B" + startrow].Merge = true;
                                ws.Cells[startrow, 3].Value = Convert.ToDateTime(transactions.Rows[i].ItemArray[0]).ToString("yyyyMMdd") + "-" + settlementtransactions.Rows[0]["merchant_id"].ToString();
                                ws.Cells[startrow, 4].Value = transactions.Rows[i]["transactioncode"];
                                ws.Cells[startrow, 5].Value = transactions.Rows[i]["txnAmount"];
                                ws.Cells[startrow, 8].Value = transactions.Rows[i]["MDR"];
                                ws.Cells[startrow, 9].Value = transactions.Rows[i]["GST"];
                                ws.Cells[startrow, 10].Value = transactions.Rows[i]["AmountSectionA"];
                                ws.Cells[startrow, 11].Value = transactions.Rows[i]["AmountSectionB"];
                                startrow++;
                                no_++;
                            }
                            decimal
                                        transactionamountsum,
                                        mdramountsum,
                                        whtamountsum,
                                        netamountsum,
                                        amountpaidsum;

                            transactionamountsum = Convert.ToDecimal(transactions.Compute("Sum(txnAmount)", ""));
                            mdramountsum = Convert.ToDecimal(transactions.Compute("Sum(MDR)", ""));
                            whtamountsum = Convert.ToDecimal(transactions.Compute("Sum(GST)", ""));
                            netamountsum = Convert.ToDecimal(transactions.Compute("Sum(AmountSectionA)", ""));
                            amountpaidsum = Convert.ToDecimal(transactions.Compute("Sum(AmountSectionB)", ""));

                            ws.Cells[((startrow) + 1), 5].Value = transactionamountsum;
                            ws.Cells[((startrow) + 1), 8].Value = mdramountsum;
                            ws.Cells[((startrow) + 1), 9].Value = whtamountsum;
                            ws.Cells[((startrow) + 1), 10].Value = netamountsum;
                            ws.Cells[((startrow) + 1), 11].Value = amountpaidsum;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No transactions on Merchant #"+MerchantID);
                        return null;
                    }
                    
                    ws.Cells[((startrow) + 4), 2].Value = "For any inquiries related to your account, please email us at " + settlementtransactions.Rows[0]["InternalEmail"] + " or call us at " + settlementtransactions.Rows[0]["InternalContact"];
                    
                    Console.WriteLine("Saving as Excel");
                    pck.SaveAs(new FileInfo(outputfilt + ".xlsx"));
                    string BenboxKey = Settings.Default.SerialNum;
                    SpreadsheetInfo.SetLicense(BenboxKey);
                    Console.WriteLine("Converting from XLS to PDF");
                    ExcelFile.Load(outputfilt + ".xlsx").Save(outputfilt + ".pdf");
                }

                return outputfilt + ".pdf";
            }
            else
            {
                return null;
            }

        }
    }

    class DataAccess
    {
        public DataTable getMonthlySoaHeaders(int? month, int? year, string merchant = "")
        {
            DataTable dtTable = new DataTable();
            DataRow drRow;

            dtTable.Columns.Add("merchant_id", typeof(string));
            dtTable.Columns.Add("settlement_date", typeof(string));
            dtTable.Columns.Add("registration_name", typeof(string));
            dtTable.Columns.Add("TIN", typeof(string));
            dtTable.Columns.Add("Address", typeof(string));
            dtTable.Columns.Add("City", typeof(string));
            dtTable.Columns.Add("BalanceBroughtForward", typeof(decimal));
            dtTable.Columns.Add("TotalTransaction", typeof(decimal));
            dtTable.Columns.Add("TotalCBRF", typeof(decimal));
            dtTable.Columns.Add("TotalTransactionAdjustment", typeof(decimal));
            dtTable.Columns.Add("Others", typeof(decimal));
            dtTable.Columns.Add("LessPaid", typeof(decimal));
            dtTable.Columns.Add("BalanceCarryForward", typeof(decimal));
            dtTable.Columns.Add("ContactName", typeof(string));
            dtTable.Columns.Add("ContactEmail", typeof(string));
            dtTable.Columns.Add("InternalCompanyName", typeof(string));
            dtTable.Columns.Add("Address1", typeof(string));
            dtTable.Columns.Add("Address2", typeof(string));
            dtTable.Columns.Add("Address3", typeof(string));
            dtTable.Columns.Add("taxid", typeof(string));
            dtTable.Columns.Add("businessregistionid", typeof(string));
            dtTable.Columns.Add("InternalEmail", typeof(string));
            dtTable.Columns.Add("InternalContact", typeof(string));
            dtTable.Columns.Add("TotalWithdeld", typeof(decimal));
            dtTable.Columns.Add("DocID", typeof(decimal));
            dtTable.Columns.Add("CompanyCode", typeof(string));
            dtTable.Columns.Add("SettlementCurrency", typeof(string));



            using (M3Entities myContext = new M3Entities())
            {

                var myEntitySet = (from t in myContext.vw_SOAMonthlyHeaders
                                   where t.smonth == month && t.syear == year
                                   select new
                                   {
                                       t.smonth,
                                       t.syear,
                                       t.merchant_id,
                                       t.registration_name,
                                       t.tax_id,
                                       t.biz_address,
                                       t.biz_city,
                                       t.BroughtForward,
                                       t.TotalTransactions,
                                       t.TotalCBRF,
                                       t.TotalTransactionAdjustment,
                                       t.Others,
                                       t.LessPaid,
                                       t.CarriedForward,
                                       t.firstname,
                                       t.lastname,
                                       t.email,
                                       InternalCompany = t.description,
                                       t.AddressLine1,
                                       t.AddressLine2,
                                       t.AddressLine3,
                                       inctaxid = t.taxid,
                                       t.businessregistrationid,
                                       internalemail = t.emailaddress,
                                       internalcontact = t.contactnumber,
                                       t.total_fraud_netamount,
                                       t.DocID,
                                       t.SettlementCurrency,
                                       t.CompanyCode

                                   });




                if (!string.IsNullOrEmpty(merchant))
                {

                    int merchant1 = int.Parse(merchant);
                    //myEntitySet = myEntitySet.Where(t => t.merchant_id == merchant1 && t.smonth == month && t.syear == year);
                    myEntitySet = myEntitySet.Where(t => t.merchant_id == merchant1);// && t.smonth == month && t.syear == year);
                }

                foreach (var myEntityRow in myEntitySet)
                {
                    string ContactName = myEntityRow.firstname + " " + myEntityRow.lastname;

                    drRow = dtTable.NewRow();

                    drRow["merchant_id"] = myEntityRow.merchant_id;
                    drRow["settlement_date"] = "";
                    drRow["registration_name"] = myEntityRow.registration_name;
                    drRow["TIN"] = myEntityRow.tax_id;
                    drRow["Address"] = myEntityRow.biz_address;
                    drRow["City"] = myEntityRow.biz_city;
                    drRow["BalanceBroughtForward"] = myEntityRow.BroughtForward;
                    drRow["TotalTransaction"] = myEntityRow.TotalTransactions;
                    drRow["TotalCBRF"] = myEntityRow.TotalCBRF;
                    drRow["TotalTransactionAdjustment"] = myEntityRow.TotalTransactionAdjustment;
                    drRow["Others"] = myEntityRow.Others;
                    drRow["LessPaid"] = myEntityRow.LessPaid;
                    drRow["BalanceCarryForward"] = myEntityRow.CarriedForward;
                    drRow["ContactName"] = ContactName;
                    drRow["ContactEmail"] = myEntityRow.email;
                    drRow["InternalCompanyName"] = myEntityRow.InternalCompany;
                    drRow["Address1"] = myEntityRow.AddressLine1;
                    drRow["Address2"] = myEntityRow.AddressLine2;
                    drRow["Address3"] = myEntityRow.AddressLine3;
                    drRow["taxid"] = myEntityRow.inctaxid;
                    drRow["businessregistionid"] = myEntityRow.businessregistrationid;
                    drRow["InternalEmail"] = myEntityRow.internalemail;
                    drRow["InternalContact"] = myEntityRow.internalcontact;
                    drRow["TotalWithdeld"] = myEntityRow.total_fraud_netamount;
                    drRow["DocID"] = myEntityRow.DocID;
                    drRow["SettlementCurrency"] = myEntityRow.SettlementCurrency;
                    drRow["CompanyCode"] = myEntityRow.CompanyCode;

                    dtTable.Rows.Add(drRow);
                }
            }
            return dtTable;
        }
        public DataTable getMonthlySoaTransactions(DateTime dateFrom, DateTime dateTo, string merchant = "")
        {
            DataTable dtTable = new DataTable();
            DataRow drRow;

            dtTable.Columns.Add("statementdate", typeof(string));
            dtTable.Columns.Add("merchant_id", typeof(string));
            dtTable.Columns.Add("transactiontype", typeof(string));
            dtTable.Columns.Add("transactioncode", typeof(string));
            dtTable.Columns.Add("txnAmount", typeof(decimal));
            dtTable.Columns.Add("MDR", typeof(decimal));
            dtTable.Columns.Add("GST", typeof(decimal));
            dtTable.Columns.Add("AmountSectionA", typeof(decimal));
            dtTable.Columns.Add("AmountSectionB", typeof(decimal));
            dtTable.Columns.Add("COUNTRY", typeof(string));

            using (M3Entities myContext = new M3Entities())
            {
                //IQueryable<vw_SOADailyTransaction> RestOfMethod<vw_SOADailyTransaction>(IQueryable<vw_SOADailyTransaction> myEntitySet);           
                var myEntitySet = (from t in myContext.vw_SOAMonthlyTransaction
                                   where (t.settlement_date >= dateFrom && t.settlement_date <= dateTo)
                                   orderby t.settlement_date ascending
                                   select new
                                   {
                                       t.settlement_date,
                                       t.MERCHANT_ID,
                                       t.TRANSACTIONCODE,
                                       t.txnAmount,
                                       t.MDR,
                                       t.GST,
                                       t.AmountSectionA,
                                       t.AmountSectionB,
                                       t.COUNTRY
                                   });


                if (!string.IsNullOrEmpty(merchant))
                {
                    int merchant1 = int.Parse(merchant);
                    myEntitySet = myEntitySet.Where(t => t.MERCHANT_ID == merchant1); //(t.statementdate >= dateFrom && t.statementdate <= dateTo));
                }

                foreach (var myEntityRow in myEntitySet)
                {
                    drRow = dtTable.NewRow();

                    drRow["statementdate"] = myEntityRow.settlement_date;
                    drRow["merchant_id"] = myEntityRow.MERCHANT_ID;
                    drRow["transactiontype"] = "";//myEntityRow.TRANSACTIONTYPE;
                    drRow["transactioncode"] = myEntityRow.TRANSACTIONCODE;
                    drRow["txnAmount"] = myEntityRow.txnAmount;
                    drRow["MDR"] = myEntityRow.MDR;
                    drRow["GST"] = myEntityRow.GST;
                    drRow["AmountSectionA"] = myEntityRow.AmountSectionA;
                    drRow["AmountSectionB"] = myEntityRow.AmountSectionB;
                    drRow["COUNTRY"] = myEntityRow.COUNTRY;
                    dtTable.Rows.Add(drRow);
                }
            }
            return dtTable;
        }
    }
}
