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
    
    public partial class m3_EmailAttachment
    {
        public long EmailAttachmentID { get; set; }
        public long EmailSenderID { get; set; }
        public string FilePath { get; set; }
        public byte FilePathType { get; set; }
    
        public virtual m3_EmailSender m3_EmailSender { get; set; }
    }
}
