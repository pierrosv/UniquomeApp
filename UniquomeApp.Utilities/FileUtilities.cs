

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global
//TODO: Cleanup
//TODO: Make all functions throw Exceptions, or use some type of logger (And/Or Overrides with Safe suffixed)

using ICSharpCode.SharpZipLib.Zip;

namespace UniquomeApp.Utilities;

public static class FileUtilities
{
    public static IList<string> GetLinesOfCsv(string filename, bool firstRowContainsCaptions = true)
    {
        var lines = new List<string>();
        using (var textReader = new StreamReader(filename))
        {
            try
            {
                var lineCount = 0;
                var line = textReader.ReadLine();
                while (line != null)
                {
                    lineCount++;
                    if (lineCount == 1 && firstRowContainsCaptions)
                    {
                        line = textReader.ReadLine();
                        continue;
                    }
                    lines.Add(line);
                    line = textReader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("UNABLE TO PARSE CSV:\r\n" + ExceptionUtilities.GetExceptionMessage(ex));
            }
        }
        return lines;
    }

    public static bool WriteTextToFile(string filename, string text, bool singleLine = false)
    {
        try
        {
            using StreamWriter outputFile = new StreamWriter(filename);
            if (singleLine)
                outputFile.Write(text);
            else
                outputFile.WriteLine(text);
        }
        catch (Exception ex)
        {
            throw new Exception("UNABLE TO WRITE FILE:\r\n" + ExceptionUtilities.GetExceptionMessage(ex));
        }
        return true;
    }

    public static bool AppendTextToFile(string filename, string text, bool writeNewLine = true)
    {
        try
        {
            using StreamWriter outputFile = new StreamWriter(filename, true);
            if (writeNewLine)
                outputFile.WriteLine(text);
            else
                outputFile.Write(text);
            outputFile.Flush();
            outputFile.Close();
        }
        catch (Exception ex)
        {
            throw new Exception("UNABLE TO WRITE FILE:\r\n" + ExceptionUtilities.GetExceptionMessage(ex));
        }
        return true;
    }

    public static bool AppendTextToFile(string filename, IList<string> lines)
    {
        try
        {
            using (StreamWriter outputFile = new StreamWriter(filename, true))
            {
                foreach (var line in lines)
                    outputFile.WriteLine(line);
                outputFile.Flush();
                outputFile.Close();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("UNABLE TO WRITE FILE:\r\n" + ExceptionUtilities.GetExceptionMessage(ex));
        }
        return true;
    }

    public static string ReadTextFile(string filename)
    {
        using var textReader = new StreamReader(filename);
        try
        {
            return textReader.ReadToEnd();
        }
        catch (Exception ex)
        {
            throw new Exception("UNABLE TO PARSE CSV:\r\n" + ExceptionUtilities.GetExceptionMessage(ex));
        }
    }

    public static void MoveFile(string fromLocation, string toLocation)
    {
        File.Move(fromLocation, toLocation);
    }

    public static int CountLinesReader(string filename)
    {
        var lineCounter = 0;
        using var reader = new StreamReader(filename);
        while (reader.ReadLine() != null)
        {
            lineCounter++;
        }
        return lineCounter;
    }

    public static IList<string> GetLinesReader(string filename, int linesToRead, int linesToSkip = 0, bool hasHeader = true)
    {
        using var reader = new StreamReader(filename);
        var lineCounter = 0;
        var returnedLines = new List<string>();
        var line = reader.ReadLine();
        while (line != null)
        {
            lineCounter++;
            if (hasHeader && lineCounter == 1 || linesToSkip > 0 && lineCounter <= linesToSkip)
            {
                line = reader.ReadLine();
                continue;
            }
            if (returnedLines.Count == linesToRead) return returnedLines;
            returnedLines.Add(line);
            line = reader.ReadLine();
        }
        return returnedLines;
    }

    public static bool FileIsLocked(string filename, FileAccess forFileAccess)
    {
        // Try to open the file with the indicated access.
        try
        {
            var fs = new FileStream(filename, FileMode.Open, forFileAccess);
            fs.Close();
            return false;
        }
        catch (IOException)
        {
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool MoveAndDeleteFile(string file, string destination)
    {
        try
        {
            var countOfRetries = 0;
            while (FileIsLocked(file, FileAccess.ReadWrite))
            {
                Thread.Sleep(500);
                countOfRetries++;
                if (countOfRetries == 10)
                    throw  new Exception("Could not access the file. It's being used by another application");
            }
            File.Move(file, destination);
            File.Delete(file);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public static void ExtractZipFilesToFolder(string zippedFile, string extractionFolder, string newFilename = "")
    {
        using var zippedStream = new ZipInputStream(File.OpenRead(zippedFile));
        ZipEntry theEntry;
        while ((theEntry = zippedStream.GetNextEntry()) != null)
        {
            var filenameToUse = newFilename;
            if (string.IsNullOrEmpty(filenameToUse))
                filenameToUse = $"{extractionFolder}/{Path.GetFileName(theEntry.Name)}";
            if (string.IsNullOrEmpty(filenameToUse)) continue;
            using var streamWriter = File.Create(filenameToUse);
            var data = new byte[2048];
            while (true)
            {
                var size = zippedStream.Read(data, 0, data.Length);
                if (size > 0)
                    streamWriter.Write(data, 0, size);
                else
                    break;
            }
        }
    }
}