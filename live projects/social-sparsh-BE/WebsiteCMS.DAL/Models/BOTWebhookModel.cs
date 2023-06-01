using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteCMS.DAL.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Button
    {
        [JsonProperty("payload")]
        public string Payload { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Change
    {
        [JsonProperty("value")]
        public Value Value { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }
    }

    public class Entry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("changes")]
        public List<Change> Changes { get; set; }
    }

    public class Message
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public Text? Text { get; set; }

        [JsonProperty("button")]
        public Button? Button { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("display_phone_number")]
        public string DisplayPhoneNumber { get; set; }

        [JsonProperty("phone_number_id")]
        public string PhoneNumberId { get; set; }
    }

    public class BOTWebhookModel
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("entry")]
        public List<Entry> Entry { get; set; }
    }

    public class Text
    {
        [JsonProperty("body")]
        public string Body { get; set; }
    }

    public class Value
    {
        [JsonProperty("whatsapp_business_api_data")]
        public WhatsappBusinessApiData? WhatsappBusinessApiData { get; set; }

        [JsonProperty("metadata")]
        public Metadata? Metadata { get; set; }

        [JsonProperty("messages")]
        public List<Message>? Messages { get; set; }
    }

    public class WhatsappBusinessApiData
    {
        [JsonProperty("display_phone_number")]
        public string DisplayPhoneNumber { get; set; }

        [JsonProperty("phone_number_id")]
        public string PhoneNumberId { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
    }


}
