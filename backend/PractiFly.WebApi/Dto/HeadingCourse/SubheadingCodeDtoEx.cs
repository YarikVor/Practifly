namespace PractiFly.WebApi.Dto.HeadingCourse;

public static class SubheadingCodeDtoEx
{
    public static string GetCodeLike(this string dto)
    {
        return string.IsNullOrEmpty(dto)
            ? "__"
            : $"{dto}.__";
    }
}