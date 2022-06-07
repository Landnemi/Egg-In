using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.RequestModels
{
    public class PostObservationRequest
    {
        public string Email { get; set; }
        public int LandMarkId { get; set; }
        public string Comment { get; set; }

    }
}
