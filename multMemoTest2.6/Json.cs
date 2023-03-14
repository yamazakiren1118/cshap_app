using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;


namespace multMemoTest2._6
{
    class Json
    {
        [JsonPropertyName("text")]
        public string text { get; set; }
    }
}
