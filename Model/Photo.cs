using System;
using System.Collections.Generic;
using System.Text;

namespace RoPE.Model
{
    public class Camera
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rover_id { get; set; }
        public string Full_name { get; set; }
    }

    public class Rover
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Landing_date { get; set; }
        public string Launch_date { get; set; }
        public string Status { get; set; }
    }

    public class Photo
    {
        public int Id { get; set; }
        public int Sol { get; set; }
        public Camera Camera { get; set; }
        public string Img_src { get; set; }
        public string Earth_date { get; set; }
        public Rover Rover { get; set; }
    }

    public class PhotoResponse
    {
        public IList<Photo> Photos { get; set; }
    }
}
