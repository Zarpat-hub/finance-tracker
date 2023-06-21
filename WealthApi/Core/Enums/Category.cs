using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WealthApi.Core.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
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
