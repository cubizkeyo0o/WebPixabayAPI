namespace WebPixabayAPI.Models
{
    public class GetApiModel
    {
        public int total {  get; set; }
        public int totalHits { get; set; }
        List<ImageModel>? hits { get; set; }
    }
}
