using System;
using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.Estate;

public class StringToResourceTypeConverter : JsonConverter<ResourceType>
{
    public override void WriteJson(JsonWriter writer, ResourceType value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override ResourceType ReadJson(JsonReader reader, Type objectType, ResourceType existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var value = (string)reader.Value;


        if (!Enum.TryParse(typeof(ResourceType), value, true, out var type))
        {
            return ResourceType.Unrecognized;
        };

        return (ResourceType)type;
    }
}

