using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.Models
{
    public class NewDataset
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
    }
}
