using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.RequestModels
{
    public class CreateDatasetRequest
    {
        public string Email { get; set; } // user that is requesting x ... 
        public string Title { get; set; }
        public int ProjectId { get; set; }
    }
}
