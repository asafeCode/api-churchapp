using System.Diagnostics.CodeAnalysis;

namespace Tesouraria.Domain.Extensions;

public static  class StringExtension
{
    public static bool IsNotEmpty([NotNullWhen(true)] this string? value) => 
        string.IsNullOrWhiteSpace(value).IsFalse();
    
    public static bool IsEmpty([NotNullWhen(false)] this string? value) => 
        string.IsNullOrWhiteSpace(value);
}