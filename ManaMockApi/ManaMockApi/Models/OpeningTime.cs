using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManaMockApi.Models
{
    public class OpeningTime
    {
        public IEnumerable<Period> Sunday { get; set; }
        public IEnumerable<Period> Monday { get; set; }
        public IEnumerable<Period> Tuesday { get; set; }
        public IEnumerable<Period> Wednesday { get; set; }
        public IEnumerable<Period> Thursday { get; set; }
        public IEnumerable<Period> Friday { get; set; }
        public IEnumerable<Period> Saturday { get; set; }

        //เวลาร้านชั่วคราวถึงเวลา
        public DateTime? TemporaryCloseThruTime { get; set; }
    }

    public class Period
    {
        // 24h formate  : hhmm => 0800 , 2200
        public int OrderFromTime { get; set; }
        public int OrderThruTime { get; set; }
    }
}
