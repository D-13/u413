//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace U413.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Message
    {
        public long MessageID { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public bool MessageRead { get; set; }
        public System.DateTime SentDate { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
        public bool RecipientLocked { get; set; }
        public bool SenderLocked { get; set; }
    
        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set; }
    }
}