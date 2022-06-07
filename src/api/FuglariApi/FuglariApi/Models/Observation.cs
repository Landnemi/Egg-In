using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.Models
{
    public class Observation
    {
        public int Id { get; set; }
        public int LandmarkId { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
