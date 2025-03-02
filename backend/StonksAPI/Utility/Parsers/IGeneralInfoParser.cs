namespace StonksAPI.Utility.Parsers
{
    public interface IGeneralInfoParser
    {
        public GeneralAssetInformation ParseJsonResponse(string jsonString);
    }
}
