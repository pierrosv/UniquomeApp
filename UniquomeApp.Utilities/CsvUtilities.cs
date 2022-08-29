using System.Data;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace UniquomeApp.Utilities;

public static class CsvUtilities
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

    public static Tuple<string, IList<string>> GetLinesOfCsvWithHeader(string filename, bool firstRowContainsCaptions = true)
    {
        var lines = new List<string>();
        var header = "";
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
                        header = line;
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
        return new Tuple<string, IList<string>>(header, lines);
    }

    public static DataTable GetCsvFileAsDataset(string filename, bool firstRowContainsCaptions = true, char delimiter = ';')
    {
        var data = GetLinesOfCsvWithHeader(filename, firstRowContainsCaptions);
        var fieldNames = new List<string>();
        var fieldTypes = new List<Type>();
        if (!string.IsNullOrEmpty(data.Item1))
        {
            var tokens = data.Item1.Split(delimiter);
            foreach (var fName in tokens)
            {
                var serialNo = 0;
                var tempFilename = fName;
                while (fieldNames.Contains(tempFilename))
                {
                    serialNo++;
                    tempFilename = $"{fName}_{serialNo}";
                }
                fieldNames.Add(tempFilename);
            }
            //fieldNames.AddRange(tokens.Select(t => $"{t}"));
        }
        else
        {
            if (data.Item2.Count > 0)
            {
                var tokens = data.Item2[0].Split(delimiter);
                for (var i = 0; i < tokens.Length; i++)
                {                        
                    fieldNames.Add($"Field {i + 1}");
                }
            }
        }

        var separatedData = new List<string[]>();
        foreach (var line in data.Item2)
        {
            var dataLine = line.Split(delimiter);
            separatedData.Add(dataLine);
        }

        for (var field = 0; field < fieldNames.Count; field++)
        {
            fieldTypes.Add(typeof(string));
            Type fieldType = null;
            var sameFieldType = true;
            foreach (var line in separatedData)
            {
                if (string.IsNullOrEmpty(line[field])) continue;
                //TODO: Expand IsNumeric to Cover specific Numeric Types (Double/ Decimal / integer)
                if (NumericUtilities.IsNumeric(line[field]))
                {
                    if (fieldType == null)
                        fieldType = typeof(decimal);
                    else if (fieldType != typeof(decimal))
                        sameFieldType = false;
                }
                else if (DateUtilities.IsDateTime(line[field]))
                {
                    if (fieldType == null)
                        fieldType = typeof(DateTime);
                    else if (fieldType != typeof(DateTime))
                        sameFieldType = false;
                }
                else
                {
                    if (fieldType != null)
                        sameFieldType = false;
                }
            }

            if (sameFieldType && fieldType != null)
                fieldTypes[field] = fieldType;
            else
            {
                fieldTypes[field] = typeof(string);
            }
        }

        var dtToDisplay = new DataTable();
        //Build Columns                    
        for (var field = 0; field < fieldTypes.Count; field++)
        {
            var dc = new DataColumn(fieldNames[field], fieldTypes[field]);
            dtToDisplay.Columns.Add(dc);
        }

        var lineCount = 0;
        foreach (var line in separatedData)
        {
            lineCount++;
            try
            {
                var drNew = dtToDisplay.NewRow();
                var fieldCount = 0;
                for (var field = 0; field < fieldTypes.Count; field++)
                {
                    fieldCount++;
                    try
                    {
                        if (!string.IsNullOrEmpty(line[field]))
                        {
                            if (fieldTypes[field] == typeof(decimal))
                            {
                                drNew[fieldNames[field]] = NumericUtilities.GetDecimalOrZero(line[field]);
                            }
                            else if (fieldTypes[field] == typeof(DateTime))
                            {                                    
                                drNew[fieldNames[field]] = Convert.ToDateTime(line[field]);
                            }
                            //TOOD: Handle
                            // else if (fieldTypes[field] == typeof(Instant))
                            // {                                    
                            //     drNew[fieldNames[field]] = Convert.ToDateTime(line[field]);
                            // }
                            else
                            {
                                drNew[fieldNames[field]] = line[field];
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Unable to set field {fieldCount}:{e.Message}");
                    }
                }
                dtToDisplay.Rows.Add(drNew);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to set data in row {lineCount:n0}\r\n{ex.Message}");
            }
        }
        return dtToDisplay;
    }
}