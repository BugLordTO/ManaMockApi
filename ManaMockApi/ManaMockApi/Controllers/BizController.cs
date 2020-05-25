﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManaMockApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManaMockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BizController : ControllerBase
    {
        [HttpGet]
        public OpeningTime GetOpeningTime(string bizId, string serviceId)
        {
            return new OpeningTime
            {
                Sunday = new List<Period> { new Period { OrderFromTime = 0000, OrderThruTime = 0000 }, },
                Monday = new List<Period> { new Period { OrderFromTime = 0000, OrderThruTime = 0000 }, },
                Tuesday = new List<Period> { new Period { OrderFromTime = 0000, OrderThruTime = 0000 }, },
                Wednesday = new List<Period> { new Period { OrderFromTime = 0000, OrderThruTime = 0000 }, },
                Thursday = new List<Period> { new Period { OrderFromTime = 0000, OrderThruTime = 0000 }, },
                Friday = new List<Period> { new Period { OrderFromTime = 0000, OrderThruTime = 0000 }, },
                Saturday = new List<Period> { new Period { OrderFromTime = 0000, OrderThruTime = 0000 }, },
                TemporaryCloseThruTime = null
            };
        }

        [HttpPost("{bizId}/{serviceId}")]
        public void UpdateOpeningTime(string bizId, string serviceId, [FromBody] OpeningTime bizTime)
        {

        }

        [HttpPost("{bizId}/{serviceId}")]
        public void SetTemporaryClose(string bizId, string serviceId, DateTime TemporaryCloseThruTime)
        {

        }
    }

}