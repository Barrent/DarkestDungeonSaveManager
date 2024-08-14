using System;
using System.Globalization;
using System.Windows.Data;
using DarkestDungeonSaveManager.Serialization.Estate;

namespace DarkestDungeonSaveManager.Converters;

/// <summary>
/// Converts <see cref="ResourceType"/> to icon <see cref="Uri"/>.
/// </summary>
public class ResourceToUriConverter : IValueConverter
{
    /// <summary>Converts a value. </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var type = value as ResourceType?;
        if (type == null)
        {
            return null;
        }
        var fileName = GetFileName(type.Value);
        if (string.IsNullOrEmpty(fileName))
        {
            return null;
        }
        
        return new Uri(@$"pack://application:,,,/DarkestDungeonSaveManager;component/Icons/{fileName}");
    }

    /// <summary>Converts a value. </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Maps resource type on icon file name.
    /// </summary>
    /// <param name="type"> Resource type. </param>
    /// <returns>Icon file name.</returns>
    private string? GetFileName(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Gold:
                return "Gold.png";
            case ResourceType.Bust:
                return "Bust.png";
            case ResourceType.Portrait:
                return "Portrait.png";
            case ResourceType.Deed:
                return "Deed.png";
            case ResourceType.Crest:
                return "Crest.png";
            case ResourceType.Shard:
                return "Shard.png";
            case ResourceType.Blueprint:
                return "Blueprint.png";
        }

        return null;
    }
}