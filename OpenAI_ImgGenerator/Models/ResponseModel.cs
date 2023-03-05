namespace DALLE_webapp.Models
{
        public class Input
        {
            public string? prompt { get; set; }
            public short? n { get; set; }
            public string? size { get; set; }
        }

        public class Link
        {
            public string? url { get; set; }
        }

        public class ResponseModel
        {
            public long created { get; set; }
            public List<Link>? data { get; set; }
        }

    public class VariationInput
    {
        public string? image { get; set; }
        public short? n { get; set; }
        public string? size { get; set; }
    }
}
