using System;
using System.Collections.Generic;
using System.Text;

namespace RoPE.Model.Manifest
{
    public class Photo
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
        public IList<Photo> Photos { get; set; }
    }

    public class PhotoManifestResponse
    {
        public PhotoManifest photo_manifest { get; set; }
    }
}
