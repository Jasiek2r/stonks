namespace StonksAPI.Utility.Parsers
{
    public interface IParserFacade
    {
        public IParsingResult ParseJsonResponse<T>(string data) where T : IParsingResult;
    }
}