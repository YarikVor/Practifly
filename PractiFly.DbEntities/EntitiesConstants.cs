namespace PractiFly.DbEntities
{
    public static class EntitiesConstantLengths
    {
        public const int LanguageCode = 2;
        public const int Code = 16;
        public const int Udc = 16;
        public const int Name = 128;
        public const int Note = 256;
        public const int Description = 65536;
        public const int Url = 2048;
        public const int Text = 2048;
    }

    public static class EntitiesConstants
    {
        public const string SubHeadingPattern = @"^(?:\d{2}(?:\.\d{2}){0,2})?$";
        public const string HeadingRegex = @"^\d{2}(?:\.\d{2}){0,3}$";
    }
}
