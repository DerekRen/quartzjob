//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuartzJob.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class jobconfiguration
    {
        public long Id { get; set; }
        public string TriggerName { get; set; }
        public string TriggerUrl { get; set; }
        public string Explain { get; set; }
        public string CronExpr { get; set; }
        public int Classify { get; set; }
        public int IsValid { get; set; }
        public int RunStatus { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
