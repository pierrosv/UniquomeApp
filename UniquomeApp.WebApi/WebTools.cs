namespace UniquomeApp.WebApi;

public class WebTools
{
    public static string GetNewDownloadFileName(string pathRoot)
    {
        var filename = $"{Guid.NewGuid()}.csv";
        var path = $"{pathRoot}/{filename}";
        return path;
    }

    public static string GetContentType(string path)
    {
        var types = WebTools.GetMimeTypes();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }

    public static Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
        {
            {".txt", "text/plain"},
            {".bed", "text/plain"},
            {".pdf", "application/pdf"},
            {".doc", "application/vnd.ms-word"},
            {".docx", "application/vnd.ms-word"},
            {".xls", "application/vnd.ms-excel"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif", "image/gif"},
            {".csv", "text/csv"},
            {".json", "text/json"}
        };
    }
}