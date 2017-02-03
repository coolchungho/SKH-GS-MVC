using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SKH_GS_MVC.Models
{
    public class DutyItemModel
    {
        [DataType(DataType.Date)]
        public DateTime dutyDate { get; set; }
        public string weekDay { get; set; }
        public string duty1Man { get; set; }
        public string duty2Man { get; set; }
        public string duty3Man { get; set; }
        public string duty4Man { get; set; }
        public string duty5Man { get; set; }

    }

    public class SingleDuty
    {
        [DataType(DataType.Date)]
        public DateTime dutyDate { get; set; }
        public string weekDay { get; set; }
        public string typeDay { get; set; }
        public string duty_type_id { get; set; }
        public string duty_man { get; set; }
    }
}