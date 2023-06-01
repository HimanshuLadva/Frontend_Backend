using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebsiteCMS.DAL.Models
{

    public class Buttons
    {
        public string type { get; set; }
        public string text { get; set; }
        public string? url { get; set; }

    }

    public class Component
    {
        public string type { get; set; }
        public string? format { get; set; }
        public string? text { get; set; }
        public Example? example { get; set; }
        public List<Buttons>? buttons { get; set; }
    }

    public class Example
    {
        public List<string> header_handle { get; set; }
    }

    public class BOTWhatsappMessageTemplateModel
    {
        public string Name { get; set; }
        public string Language { get; set; } = "en";
        public string Category { get; set; } = "Marketing";
        public List<Component> Components { get; set; }
        public long ChatBotId { get; set; }
        public long QuestionId { get; set; }
    }

    //public class BOTWhatsAppTemplateComponentsModel
    //{
    //    public string type { get; set; }

    //    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    //    public string? format { get; set; }

    //    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    //    public string? text { get; set; } = null;

    //    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    //    public ICollection<BOTWhatsAppButtonModel>? Buttons { get; set; } = null;

    //    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    //    public object? example { get; set; } = null;

    //    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    //    public string? Url { get; set; } = null;

    //    //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    //    //[JsonPropertyName("example")]
    //    //public ICollection<string>? urls { get; set; } = null;

    //}
    //public class BOTWhatsAppButtonModel
    //{
    //    public string type { get; set; }
    //    public string text { get; set; }
    //    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    //    public string? Phone_number { get; set; } = null;
    //}

    //public class BOTWhatsappComponentExampleModel
    //{
    //    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    //    public string? header_handle { get; set; } = null;
    //    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    //    public string? body_text { get; set; } = null;
    //    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    //    public string? header_text { get; set; } = null;
    //}
}
