using System;
using Newtonsoft.Json;

namespace DarkestDungeonSaveManager.Serialization.Estate;

/// <summary>
/// Converts int value specified in save game file to resource type.
/// </summary>
public class StringToResourceTypeConverter : JsonConverter<ResourceType>
{
    /// <summary>
    /// Writes the JSON representation of the object.
    /// </summary>
    /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
    /// <param name="value">The value.</param>
    /// <param name="serializer">The calling serializer.</param>
    public override void WriteJson(JsonWriter writer, ResourceType value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Reads the JSON representation of the object.
    /// </summary>
    /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
    /// <param name="objectType">Type of the object.</param>
    /// <param name="existingValue">The existing value of object being read. If there is no existing value then <c>null</c> will be used.</param>
    /// <param name="hasExistingValue">The existing value has a value.</param>
    /// <param name="serializer">The calling serializer.</param>
    /// <returns>The object value.</returns>
    public override ResourceType ReadJson(JsonReader reader, Type objectType, ResourceType existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var value = (string?)reader.Value;

        if (!Enum.TryParse(typeof(ResourceType), value, true, out var type))
        {
            return ResourceType.Unrecognized;
        };

        return (ResourceType)type;
    }
}

