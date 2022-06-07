using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.RequestModels
{
    public class CreateProjectRequest
    {
        public string Email { get; set; }
        public string ProjectTitle { get; set; }

    }
}
