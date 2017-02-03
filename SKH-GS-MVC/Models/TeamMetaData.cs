using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SKH_GS_MVC.Models
{
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(TeamMetaData))]
    public partial class team
    {
    public class TeamMetaData
    {
            public long id { get; set; }
            [DisplayName("日期")]
            [DataType(DataType.Date)]
            public Nullable<System.DateTime> team_date { get; set; }
            public Nullable<int> team_no { get; set; }
            [DisplayName("VS")]
            public string vs { get; set; }
            public string fellow { get; set; }
            public string cr { get; set; }
            public string r { get; set; }
            public string pgy { get; set; }
            public string intern { get; set; }
            public string clerk { get; set; }

        }

    }

}