namespace aspnet_api.Models
{
    public class Suburb
    {
        public int Id { get; set; }
        public required string SuburbName { get; set; }
        public int latitude { get; set; }
        public int longitude { get; set; }
    }
}
