using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;

namespace WealthApi.Core.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    [DefaultValue(OTHER)]
    public enum Category
    {
        FOOD,
        HOME,
        TRANSPORT,
        HEALTHCARE,
        CLOTHES,
        HYGIENE,
        KIDS,
        ENTERTAINMNENT,
        DEBT,
        OTHER,
    }
}
