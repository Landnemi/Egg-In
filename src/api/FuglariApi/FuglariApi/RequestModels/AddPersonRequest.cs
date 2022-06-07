using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.RequestModels
{
    public class AddPersonRequest
    {
        public int projectId { get; set; }
        public string inviterEmail { get; set; }
        public string inviteeEmail { get; set; }
    }
}
