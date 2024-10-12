using System.IO;
using System.Text.Json;

public class JsonSettingIO
{
    public static void ToJson<T>(string filename, T data)
    {
        using (var stream = new FileStream(filename, FileMode.Truncate, FileAccess.Write))
        {
            JsonSerializer.Serialize(stream, data);
        }
    }

    public static T Read<T>(string filePath)
    {
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            return JsonSerializer.Deserialize<T>(stream);
        }
    }
}

