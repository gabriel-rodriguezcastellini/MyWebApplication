namespace MyWebApplication.Parsers
{
    public static class Parsers
    {
        public static ParseResult GuidParser(object? input)
        {
            bool success = Guid.TryParse(input?.ToString(), out Guid result);
            return new(success, result);
        }
    }
}
