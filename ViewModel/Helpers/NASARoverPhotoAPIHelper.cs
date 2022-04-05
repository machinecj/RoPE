using Newtonsoft.Json;
using RoPE.Model;
using RoPE.Model.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoPE.ViewModel.Helpers
{
    public class NASARoverPhotoAPIHelper
    {
        public const string BASE_URL = "https://api.nasa.gov/";
        public const string MANIFEST_ENDPOINT = "mars-photos/api/v1/manifests/{0}?api_key={1}"; // rover name and api key
        public const string PHOTOS_ENDPOINT = "mars-photos/api/v1/rovers/{0}/photos?api_key={1}&sol={2}"; // rover name, api key, and sol
        public const string API_KEY = "bfhQvZ4rTILlU6FdfFkak01RN3P93STycsF93Rx1";

        public static async Task<PhotoManifest> GetPhotoManifest(string roverName)
        {
            PhotoManifest photoManifest;
            string url = BASE_URL + string.Format(MANIFEST_ENDPOINT, roverName, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                var photoManifestResponse = (JsonConvert.DeserializeObject<PhotoManifestResponse>(json));
                photoManifest = photoManifestResponse.Photo_manifest;
            }

            return photoManifest;
        }

        public static async Task<List<Model.Photo>> GetPhotos(string roverName, int selectedSol)
        {
            List<Model.Photo> photos = new List<Model.Photo>();

            string url = BASE_URL + string.Format(PHOTOS_ENDPOINT, roverName, API_KEY, selectedSol);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                photos = JsonConvert.DeserializeObject<List<Model.Photo>>(json);
            }

            return photos;
        }
    }


}
