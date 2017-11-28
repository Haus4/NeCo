namespace Neco.Client
{
    public interface IAuthStore
    {
        string GetKey(object context);
    }
}
