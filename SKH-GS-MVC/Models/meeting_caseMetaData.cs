using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKH_GS_MVC.Models
{
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(meeting_caseMetaData))]
    public partial class meeting_case
        {
         public class meeting_caseMetaData
         {
 
            public int id { get; set; }
            [DisplayName("姓名")]
            public string name { get; set; }
            [DisplayName("年齡")]
            public Nullable<int> age { get; set; }
            [DisplayName("性別")]
            public string sex { get; set; }
            [DisplayName("病歷號")]
            public string chartNo { get; set; }
            [DisplayName("病史")]
            public string history { get; set; }
            [DisplayName("住院醫師")]
            public string GSR { get; set; }
            [DisplayName("外科主治醫師")]
            public string GSVS { get; set; }
            [DisplayName("病理醫師")]
            public string PathoVS { get; set; }
            [DisplayName("討論日期")]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> meet_date { get; set; }
        }
    }
}