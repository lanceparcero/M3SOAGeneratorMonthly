//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SOAEmailSenderMonthly
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_SOAMonthlyHeaders
    {
        public int merchant_id { get; set; }
        public short syear { get; set; }
        public byte smonth { get; set; }
        public string registration_name { get; set; }
        public string tax_id { get; set; }
        public string biz_address { get; set; }
        public string biz_city { get; set; }
        public decimal BroughtForward { get; set; }
        public decimal TotalTransactions { get; set; }
        public Nullable<decimal> TotalCBRF { get; set; }
        public decimal TotalTransactionAdjustment { get; set; }
        public decimal Others { get; set; }
        public decimal LessPaid { get; set; }
        public decimal CarriedForward { get; set; }
        public decimal total_fraud_netamount { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string description { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string taxid { get; set; }
        public string businessregistrationid { get; set; }
        public string emailaddress { get; set; }
        public string contactnumber { get; set; }
        public Nullable<int> DocID { get; set; }
        public string CompanyCode { get; set; }
        public string SettlementCurrency { get; set; }
    }
}