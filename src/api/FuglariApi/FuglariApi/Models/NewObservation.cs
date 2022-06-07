using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.Models
{
    public class NewObservation
    {
        public int LandmarkId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
    }
}
