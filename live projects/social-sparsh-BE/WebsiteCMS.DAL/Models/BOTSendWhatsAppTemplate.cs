using Newtonsoft.Json;

namespace WebsiteCMS.DAL.Models
{
    //public class Language
    //{
    //    [JsonProperty("code")]
    //    public string? Code { get; set; }
    //}

    //public class BOTSendWhatsAppTemplate
    //{
    //    [JsonProperty("messaging_product")]
    //    public string? MessagingProduct { get; } = "whatsapp";

    //    [JsonProperty("to")]
    //    public string? To { get; set; }

    //    [JsonProperty("type")]
    //    public string? Type { get; } = "template";

    //    [JsonProperty("template")]
    //    public Template? Template { get; set; }
    //}

    //public class Template
    //{
    //    [JsonProperty("name")]
    //    public string? Name { get; set; }

    //    [JsonProperty("language")]
    //    public Language? Language { get; set; }
    //}


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Components
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("parameters")]
        public List<Parameter>? Parameters { get; set; }
    }

    public class Images
    {
        [JsonProperty("link")]
        public string Link { get; set; }
    }
    
    public class Documents
    {
        [JsonProperty("link")]
        public string Link { get; set; }
    }

    public class Language
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class Parameter
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("image")]
        public Images? Image { get; set; }

        [JsonProperty("document")]
        public Documents? Document { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }
    }

    public class BOTSendWhatsAppTemplate
    {
        [JsonProperty("messaging_product")]
        public string? MessagingProduct { get; } = "whatsapp";

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("type")]
        public string? Type { get; } = "template";

        [JsonProperty("recipient_type")]
        public string? RecipientType { get; set; } = "individual";

        [JsonProperty("template")]
        public Template Template { get; set; }
    }

    public class Template
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }

        [JsonProperty("components")]
        public List<Components>? Components { get; set; }
    }
}
