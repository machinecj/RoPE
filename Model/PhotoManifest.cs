using System;
using System.Collections.Generic;
using System.Text;

namespace RoPE.Model
{
    public class PhotoStats
    {
        public int Sol { get; set; }
        public string Earth_date { get; set; }
        public int Total_photos { get; set; }
        public IList<string> Cameras { get; set; }
    }

    public class PhotoManifest
    {
        public string Name { get; set; }
        public string Landing_date { get; set; }
        public string Launch_date { get; set; }
        public string Status { get; set; }
        public int Max_sol { get; set; }
        public string Max_date { get; set; }
        public int Total_photos { get; set; }
        public IList<PhotoStats> PhotoStats { get; set; }
    }
}
