using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.Models
{
    public class NewProject
    {
        public int UserId { get; set; }
        public string ProjectTitle { get; set; }
        public NewProject(int userId, string projectTitle)
        {
            UserId = userId;
            ProjectTitle = projectTitle;
        }
    }
}
