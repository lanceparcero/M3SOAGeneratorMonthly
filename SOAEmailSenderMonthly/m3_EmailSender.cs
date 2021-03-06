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
    
    public partial class m3_EmailSender
    {
        public m3_EmailSender()
        {
            this.m3_EmailAttachment = new HashSet<m3_EmailAttachment>();
            this.m3_EmailRecipient = new HashSet<m3_EmailRecipient>();
        }
    
        public long EmailSenderID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int RetryCount { get; set; }
        public byte Status { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> CorporateID { get; set; }
        public Nullable<int> merchant_id { get; set; }
        public string LastRunError { get; set; }
        public Nullable<System.DateTime> LastRunDate { get; set; }
    
        public virtual ICollection<m3_EmailAttachment> m3_EmailAttachment { get; set; }
        public virtual ICollection<m3_EmailRecipient> m3_EmailRecipient { get; set; }
        public virtual m3_merchant m3_merchant { get; set; }
    }
}
