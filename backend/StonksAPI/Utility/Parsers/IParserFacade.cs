namespace StonksAPI.Utility.Parsers
{
    // Declares a contract of a Facade for all Parsers
    public interface IParserFacade
    {
        public IDeserializable ParseJsonResponse<T>(string data) where T : IDeserializable;
    }
}