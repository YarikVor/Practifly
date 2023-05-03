using System.ComponentModel;
using System.Globalization;

namespace PractiFly.DateJsonConverter;

public abstract class StringTypeConverterBase<T> : TypeConverter
{
    protected abstract T Parse(string s, IFormatProvider? provider);

    protected abstract string ToIsoString(T source, IFormatProvider? provider);

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) 
               || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        return value is string str 
            ? Parse(str, GetFormat(culture)) 
            : base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string) 
               || base.CanConvertTo(context, destinationType);
    }
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        return destinationType == typeof(string) && value is T typedValue
            ? ToIsoString(typedValue, GetFormat(culture))
            : base.ConvertTo(context, culture, value, destinationType);
    }

    private static IFormatProvider? GetFormat(CultureInfo? culture)
    {
        return (IFormatProvider?)culture.GetFormat(typeof(DateTimeFormatInfo)) ?? culture;
    }
}