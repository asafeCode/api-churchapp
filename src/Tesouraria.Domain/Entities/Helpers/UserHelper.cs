using System.Globalization;
using System.Text;
using Tesouraria.Domain.Extensions;

namespace Tesouraria.Domain.Entities.Helpers;

public static class UserHelper
{
    public static string NormalizeUsername(this string userName)
    {
        if (userName.IsEmpty()) return string.Empty;

        userName = new string(
            userName.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray()
        ).Normalize(NormalizationForm.FormC);

        userName = userName.ToLowerInvariant();
        
        return userName;
    }
    
    public static int CalculateAge(DateOnly birthDate, DateOnly referenceDate)
    {
        var age = referenceDate.Year - birthDate.Year;
        if (referenceDate < birthDate.AddYears(age))
            age--;
        return age;
    }
}
