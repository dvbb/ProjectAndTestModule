namespace GameManagement.HttpApi.Redis
{
    public interface IRedisTool
    {
        bool SetLongValue(string key, string value);
        bool SetValue(string key, string value, int outSecond);
        bool SetValue(string key, string value);
        bool Exists(string key);
        bool UpdateValue(string key, string value);
        string? GetValue(string key);
        T? GetValue<T>(string key);
        T? GetEntity<T>(string key);
        List<T>? GetLike<T>(string key);
        void DeleteKey(string key);
        void DeleteLike(string key);
    }
}
