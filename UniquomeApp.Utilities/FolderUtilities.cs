

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace UniquomeApp.Utilities;

public static class FolderUtilities
{
    public static string CreateYearMonthFolderStructure(string rootFolder, int year, int month)
    {
        var sMonth = $"{month}";
        if (month < 10)
            sMonth = "0" + month;

        var finalDirectory = $"{rootFolder}/{year}/{sMonth}";
        if (Directory.Exists(finalDirectory)) return finalDirectory;

        //Create Root Folder Structure
        var subfolders = rootFolder.Split('/');
        var previousSubFolder = "";
        foreach (var subfolder in subfolders)
        {
            if (string.IsNullOrEmpty(subfolder)) continue;
            if (!string.IsNullOrEmpty(previousSubFolder))
            {
                if (!Directory.Exists($"{previousSubFolder}/{subfolder}"))
                    Directory.CreateDirectory($"{previousSubFolder}/{subfolder}");
            }
            else
            {
                if (!Directory.Exists($"{subfolder}"))
                    Directory.CreateDirectory($"{subfolder}");
            }
            previousSubFolder += $"{subfolder}/";
        }

        if (!Directory.Exists($"{rootFolder}/{year}"))
            Directory.CreateDirectory($"{rootFolder}/{year}");
        if (!Directory.Exists($"{rootFolder}/{year}/{sMonth}"))
            Directory.CreateDirectory($"{rootFolder}/{year}/{sMonth}");
        return finalDirectory;
    }

    public static string CreateFullDateFolderStructure(string rootFolder, DateTime date)
    {                        
        var year = date.Year;
        var month = $"{date.Month}";
        if (date.Month < 10)
            month = "0" + date.Month;
        var day = $"{date.Day}";
        if (date.Day < 10)
            day = "0" + date.Day;
        var finalDirectory = $"{rootFolder}/{year}/{month}/{day}";
        if (Directory.Exists(finalDirectory)) return finalDirectory;

        //Create Root Folder Structure
        var subfolders = rootFolder.Split('/');
        var previousSubFolder = "";
        foreach (var subfolder in subfolders)
        {
            if (string.IsNullOrEmpty(subfolder)) continue;
            if (!string.IsNullOrEmpty(previousSubFolder))
            {
                if (!Directory.Exists($"{previousSubFolder}/{subfolder}"))
                    Directory.CreateDirectory($"{previousSubFolder}/{subfolder}");
            }
            else
            {
                if (!Directory.Exists($"{subfolder}"))
                    Directory.CreateDirectory($"{subfolder}");
            }
            previousSubFolder += $"{subfolder}/";
        }
            
        if (!Directory.Exists($"{rootFolder}/{year}"))
            Directory.CreateDirectory($"{rootFolder}/{year}");
        if (!Directory.Exists($"{rootFolder}/{year}/{month}"))
            Directory.CreateDirectory($"{rootFolder}/{year}/{month}");
        if (!Directory.Exists($"{rootFolder}/{year}/{month}/{day}"))
            Directory.CreateDirectory($"{rootFolder}/{year}/{month}/{day}");
        return finalDirectory;
    }

    public static string CreateFullFolderStructure(string rootFolder)
    {            
        var finalDirectory = $"{rootFolder}";
        if (Directory.Exists(finalDirectory)) return finalDirectory;

        //Create Root Folder Structure
        var subfolders = rootFolder.Split('/');
        var previousSubFolder = "";
        foreach (var subfolder in subfolders)
        {
            if (!string.IsNullOrEmpty(previousSubFolder))
            {
                if (!Directory.Exists($"{previousSubFolder}/{subfolder}"))
                    Directory.CreateDirectory($"{previousSubFolder}/{subfolder}");
            }
            else
            {
                if (!Directory.Exists($"{subfolder}") && !string.IsNullOrEmpty($"{subfolder}"))
                    Directory.CreateDirectory($"{subfolder}");
            }
            previousSubFolder += $"{subfolder}/";
        }            
        return finalDirectory;
    }

    public static IList<string> GetRecursiveDirectoryFolderStructure(string rootDirectory)
    {
        return Directory.GetDirectories(rootDirectory, "*.*", SearchOption.AllDirectories);
    }

    public static IList<string> GetRecursiveDirectoryContents(string rootDirectory)
    {
        return Directory.GetFiles(rootDirectory, "*.*", SearchOption.AllDirectories);
    }
}