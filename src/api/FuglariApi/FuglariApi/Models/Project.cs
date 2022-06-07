using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OwnerId { get; set; }
    }
}
