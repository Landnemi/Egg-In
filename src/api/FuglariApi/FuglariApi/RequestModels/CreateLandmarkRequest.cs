using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.RequestModels
{
    public class CreateLandmarkRequest
    {
        public string Email { get; set; }
        public string Title { get; set; }
        public int DatasetId { get; set; }
        public string Species { get; set; }
        public float Latitude { get;  set; }
        public int Status { get;  set; }
        public int Progress { get;  set; }
        public float Longitude { get;  set; }
    }
}
