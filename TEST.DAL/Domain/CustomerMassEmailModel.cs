using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST.DAL
{
   public class CustomerMassEmailModel
    {
        public int CustomerMassEmailID { get; set; }
        public Nullable<int> EmailTypeID { get; set; }
        public Nullable<System.DateTime> ScheduledDate { get; set; }
        public string StatusIND { get; set; }
        public Nullable<System.DateTime> RunDate { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
