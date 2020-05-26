using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManaMockApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManaMockApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class BizController : ControllerBase
    {
        [HttpGet("{bizId}/service/{serviceId}/openingtime")]
        public OpeningTime GetOpeningTime(string bizId, string serviceId)
        {
            return new OpeningTime
            {
                Sunday = new List<Period>(),
                Monday = new List<Period>
                {
                    new Period { OrderFromTime = 0800, OrderThruTime = 2200 },
                },
                Tuesday = new List<Period>
                {
                    new Period { OrderFromTime = 0800, OrderThruTime = 1400 },
                    new Period { OrderFromTime = 1500, OrderThruTime = 2200 },
                },
                Wednesday = new List<Period>
                {
                    new Period { OrderFromTime = 0800, OrderThruTime = 2200 },
                },
                Thursday = new List<Period>
                {
                    new Period { OrderFromTime = 0800, OrderThruTime = 2200 },
                },
                //24hr
                Friday = new List<Period>
                {
                    new Period { OrderFromTime = 0000, OrderThruTime = 2400 },
                },
                Saturday = new List<Period>(),
                TemporaryCloseThruTime = null
            };
        }

        [HttpPost("{bizId}/service/{serviceId}/openingtime")]
        public void UpdateOpeningTime(string bizId, string serviceId, [FromBody] OpeningTime bizTime)
        {

        }

        [HttpPost("{bizId}/service/{serviceId}/temporarytime")]
        public void SetTemporaryClose(string bizId, string serviceId, DateTime TemporaryCloseThruTime)
        {

        }

        [HttpGet("service/{serviceId}/openingbiz")]
        public IEnumerable<BizAccount> GetOpeningBiz(string serviceId)
        {
            return new List<BizAccount>
            {
                new BizAccount { id ="01" , Name="บ้านมอ" },
                new BizAccount { id ="02" , Name="Red Door" },
                new BizAccount { id ="03" , Name="Black Door" },
                new BizAccount { id ="04" , Name="Dior" },
            };
        }

    }

}