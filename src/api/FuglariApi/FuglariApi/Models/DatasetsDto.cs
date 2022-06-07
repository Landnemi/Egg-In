using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.Models
{
    public class DatasetsDto
    {
        public IEnumerable<DatasetDto> Datasets;
    }
    public class DatasetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<LandmarkDto> Landmarks { get; set; }
    }
}
