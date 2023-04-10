namespace PractiFly.WebApi.Extentions;

public static class DateEx
{
    public static bool TryToConvertToDateOnly(int year, int month, int day, out DateOnly date)
    {
        var isCorrect = year is >= 1 and <= 9999
                        && month is >= 1 and <= 12
                        && day >= 1 && day <= DateTime.DaysInMonth(year, month);

        date = isCorrect ? new DateOnly(year, month, day) : default;

        return isCorrect;
    }
}