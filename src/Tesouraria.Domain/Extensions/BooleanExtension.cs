using System.Diagnostics.CodeAnalysis;

namespace Tesouraria.Domain.Extensions;

public static class BooleanExtension
{
    public static bool IsFalse(this bool value) => !value;
}