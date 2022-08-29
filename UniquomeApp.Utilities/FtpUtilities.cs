using System.Net;
using System.Text;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace UniquomeApp.Utilities;

public static class FtpUtilities
{
    public static string UploadFileToFtp(string ftpLocation, string ftpUsername, string ftpPassword, string filename)
    {
        try
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpLocation + "/" + Path.GetFileName(filename));
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            // Get the object used to communicate with the server.
            //var request = (FtpWebRequest)WebRequest.Create(ftpLocation);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

            // Copy the contents of the file to the request stream.
            var sourceStream = new StreamReader(filename);
            var fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;

            var requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            var response = (FtpWebResponse)request.GetResponse();

            var result = $"Upload File Complete, status {response.StatusDescription}";

            response.Close();
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not upload file : \r\n" + ex.Message);
        }
    }

    public static string DeleteFromFtp(string ftpLocation, string ftpUsername, string ftpPassword, string filename)
    {
        try
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpLocation + "/" + Path.GetFileName(filename));
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            // Get the object used to communicate with the server.
            //var request = (FtpWebRequest)WebRequest.Create(ftpLocation);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            var response = (FtpWebResponse)request.GetResponse();
            var result = $" File Deleted, status {response.StatusDescription}";

            response.Close();
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not upload file : \r\n" + ex.Message);
        }
    }


    public static void DownloadFileFromFtp(string ftpLocation, string ftpUsername, string ftpPassword, string fileToDownload, string saveAsFilename)
    {
        try
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpLocation + "/" + fileToDownload);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            var response = (FtpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream);

            using (var writer = new FileStream(saveAsFilename, FileMode.Create))
            {
                var length = response.ContentLength;
                var bufferSize = 2048;
                var buffer = new byte[2048];
                var readCount = responseStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    writer.Write(buffer, 0, readCount);
                    readCount = responseStream.Read(buffer, 0, bufferSize);
                }
            }

            reader.Close();
            response.Close();
        }
        catch (Exception ex)
        {
            throw new Exception("Could not upload file : \r\n" + ex.Message);
        }
    }

    public static long GetFileSizeFromFtp(string ftpLocation, string ftpUsername, string ftpPassword, string filename)
    {
        try
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpLocation + "/" + filename);
            //Get the file size first (for progress bar)
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = true; //don't close the connection
            var dataLength = (int)request.GetResponse().ContentLength;
            return dataLength;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not upload file : \r\n" + ex.Message);
        }
    }

    public static IList<string> GetFileListFromFtp(string ftpLocation, string ftpUsername, string ftpPassword)
    {
        try
        {
            var ftpRequest = (FtpWebRequest)WebRequest.Create(ftpLocation);
            ftpRequest.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            var response = (FtpWebResponse)ftpRequest.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException());
            var directories = new List<string>();
            var line = streamReader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                directories.Add(line);
                line = streamReader.ReadLine();
            }
            streamReader.Close();
            return directories;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not upload file : \r\n" + ex.Message);
        }
    }
}