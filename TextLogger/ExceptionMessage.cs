using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TEST.SyncService.Infrastructure
{
    public class ExceptionPayLoadMessage  
    {
        [DataMember]
        public string StatusCode
        { get; set; }

        [DataMember]
        public string StatusMessage
        { get; set; }

        [DataMember]
        public string ExceptionMessage { get; set; }

        [DataMember]
        public string InnerException { get; set; }

        [DataMember]
        public string StackTrace { get; set; }

    }
}
