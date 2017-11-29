namespace Neco.Client
{
    public interface IDataStore
    {
        void SetString(object context, string key, string value);
        string GetString(object context, string key);
    }
}
