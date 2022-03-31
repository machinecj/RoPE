using System;
using System.Collections.Generic;
using System.Text;

namespace RoPE.Model
{
    class Manifest
    {
        public PhotoManifest photo_manifest { get; set; }
    }

    public class PhotoStats
    {
        public int sol { get; set; }
        public string earth_date { get; set; }
        public int total_photos { get; set; }
        public IList<string> cameras { get; set; }
    }

    public class PhotoManifest
    {
        public string name { get; set; }
        public string landing_date { get; set; }
        public string launch_date { get; set; }
        public string status { get; set; }
        public int max_sol { get; set; }
        public string max_date { get; set; }
        public int total_photos { get; set; }
        public IList<PhotoStats> photoStats { get; set; }
    }
}
