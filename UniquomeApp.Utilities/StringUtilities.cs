using System.IO.Compression;
using System.Text;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace UniquomeApp.Utilities;

public static class StringUtilities
{
    public static string ConvertListOfStringsToSingle(IList<string> listOfStrings)
    {
        if (listOfStrings == null || listOfStrings.Count == 0) return "";
        return listOfStrings.Aggregate("", (current, listOfString) => current + (listOfString + Environment.NewLine));
    }

    public static string ClearString(string text)
    {
        return !string.IsNullOrEmpty(text) ? text.Trim() : text;
    }

    public static string ClearStringFromChars(string text, IList<char> unwantedChars, bool trimText = false)
    {
        if (string.IsNullOrEmpty(text)) return text;
        if (trimText)
            text = text.Trim();
        return text.Where(ch => !unwantedChars.Contains(ch)).Aggregate("", (current, ch) => current + ch);
    }

    public static string CreateAutoNumbering(string prefix, string suffix, int number, int totalDigits)
    {
        var middle = number.ToString();
        var numLen = totalDigits - middle.Length;
        if (middle.Length < totalDigits) for (var i = 0; i < numLen - 1; i++) middle = "0" + middle;
        var sPrefix = "";
        if (prefix != null) sPrefix = prefix;
        var sSuffix = "";
        if (suffix != null) sSuffix = suffix;
        return $"{sPrefix}{middle}{sSuffix}".Trim();
    }

    public static string GetFileContents(string filename)
    {
        var lines = CsvUtilities.GetLinesOfCsv(filename);
        return ConvertListOfStringsToSingle(lines);
    }

    public static string CompressText(string textToCompress)
    {
        var text = Encoding.ASCII.GetBytes(textToCompress);
        // Use compress method.
        var compress = Compress(text);
        // Write compressed data.
        //File.WriteAllBytes("compress.gz", compress);
        return compress.ToString();
    }

    public static byte[] Compress(byte[] raw)
    {
        using var memory = new MemoryStream();
        using (var gzip = new GZipStream(memory, CompressionMode.Compress, true))
            gzip.Write(raw, 0, raw.Length);
        return memory.ToArray();
    }

    public static string[] TokenizeString(string text, char delimiter, bool trim)
    {
        var tokens = text.Split(delimiter);
        for (var i = 0; i < tokens.Length; i++)
            tokens[i] = tokens[i].Trim();
        return tokens;
    }

    //TODO: This was taken from Cleartrade's sample. It was used as a string Extension.Maybe I could use it in the same way.
    public static string Truncate(string text, int characters)
    {
        return text.Length <= characters ? text : text.Substring(0, characters);
    }

    public static string GetPaddedInteger(int number, int totalNumberOfCharacters, char paddedCharacter = '0')
    {
        var s = number.ToString();
        for (var i = s.Length; i < totalNumberOfCharacters; i++)
            s = paddedCharacter + s;
        return s;
    }
}