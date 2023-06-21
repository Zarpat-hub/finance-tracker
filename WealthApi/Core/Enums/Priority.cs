using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WealthApi.Core.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Priority
    {
        LOW,
        MEDIUM,
        HIGH,
    }
}
