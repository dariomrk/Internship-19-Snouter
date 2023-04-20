namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static string Sanitize(this string value)
        {
            return value.Trim().ToLower().Normalize();
        }
    }
}
