namespace GetDataFromBank.Services
{

    public interface Parser<T>
    {
        T parseJSON(string json);
    }
}
