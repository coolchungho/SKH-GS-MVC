using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKH_GS_MVC.Models
{
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(TeachMetaData))]
    public partial class teach
    {
    public class TeachMetaData
    {
            public long id { get; set; }
            [DisplayName("日期")]
             [DataType(DataType.Date)]
            public Nullable<System.DateTime> teach_date { get; set; }
            [DisplayName("星期")]
            public string weekDay { get; set; }
            [DisplayName("時間")]         
            public string teach_time { get; set; }
            [DisplayName("教學活動")]
            public string activity_name { get; set; }
            [DisplayName("報告者")]
            public string learner { get; set; }
            [DisplayName("指導者")]
            public string supervisor { get; set; }
            [DisplayName("地點")]
            public string location { get; set; }


        }

    }

}