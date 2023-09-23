namespace CodeBase.Utility.Extension
{
    public static class StringExtension
    {
        public static bool HasContent(this string str) =>
            string.IsNullOrEmpty(str) == false;
    }
}
