using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InventoryManagement.Core.DataType
{
    public static class FileExtension
    {
        public static string[] ExcelFileType = { ".xlsx", ".xls", ".csv" };
        public static string[] ImageFileType = { ".png", ".bmp", ".jpg", ".jpeg" };
        public static string[] PdfType = { ".pdf" };
        public static long MaxFileSize = 5; // size in MB   
        public enum FileSizeUnit 
        {
            B,
            KB,
            MB,
            GB,
            TB,
            PB,
            EB,
            ZB,
            YB
        }

        public static string RootPath()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        public static (string fileName, string filePath) GenerateFilePath(IFormFile file, string filepath)
        {
            string file_name = string.Empty;
            string default_root = "~/Assets" + ((!string.IsNullOrEmpty(filepath)) ? "/" + filepath : "");

            var has_directory = Directory.Exists(Path.Combine(RootPath(), default_root));
            if (!has_directory)
                Directory.CreateDirectory(Path.Combine(RootPath(), default_root));

            file_name = Guid.NewGuid() + "_" + Path.GetFileNameWithoutExtension(file.FileName) + Path.GetExtension(file.FileName);
            default_root += "/" + file_name;

            (string fileName, string rootPath) tpl = (file_name, default_root);
            return tpl;
        }

        public static string CanonicalCombine(string basePath, string path)

        {

            if (String.IsNullOrEmpty(basePath) || string.IsNullOrEmpty(path))

                throw new ArgumentNullException();

            basePath = HttpUtility.UrlDecode(basePath);



            path = HttpUtility.UrlDecode(path);

            // Check for invalid characters

            if (path.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)

                throw new FileNotFoundException("FileName not valid");



            // Use Path.Combine

            string filePath = Path.Combine(basePath, path);



            // Check the composed path

            if (!filePath.StartsWith(basePath))

                throw new FileNotFoundException("Path not valid");

            return filePath;

        }

        public static String Combine(String basePath, String relativePath)
        {
            Uri baseUri = new Uri(ExpandUri(basePath), UriKind.Absolute);
            Uri relativeUri = new Uri(relativePath, UriKind.RelativeOrAbsolute);

            String result = baseUri.IsAbsoluteUri ? baseUri.GetLeftPart(UriPartial.Query) : baseUri.ToString();
            result += result.EndsWith("/") ? "" : "/";
            result += relativeUri.IsAbsoluteUri ? relativeUri.PathAndQuery : relativeUri.ToString();

            return ExpandUri(result);
        }

        public static String ExpandUri(String path)
        {
            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);

            if (uri.IsAbsoluteUri)
            {
                if (uri.IsFile) return uri.LocalPath;
                return uri.AbsoluteUri;
            }

            if (System.IO.File.Exists(path) || Directory.Exists(path)) return Path.GetFullPath(path);

            return path;
        }

        #region validation
        public static bool Valid(IFormFile file, string[] allowFileType, float maxFileSize)
        {
            if (file == null || file.Length <= 0 || string.IsNullOrEmpty(file.FileName))
                throw new ArgumentNullException("File not found.");

            float fileSize = ToMb(file.Length);
            string fileName = file.FileName;
            string fileType = Path.GetExtension(file.FileName);

            if (allowFileType != null && !allowFileType.Contains(fileType))
                throw new ArgumentException($"{fileName} is not a valid file. Allowed file types are [{string.Join(",", allowFileType.ToArray())}].");

            if (fileSize > maxFileSize)
                throw new ArgumentException($"{fileName} is not a valid file. Max file size is {maxFileSize}.");

            return true;
        }
        public static bool IsHttpFileExists(this string urlString)
        {
            var request = (HttpWebRequest)WebRequest.Create(urlString);
            request.Method = "HEAD";

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                response.Close();
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
        public static bool FileNotExists(this string filePath)
        {
            return !File.Exists(filePath);
        }

        public static bool IsValid(this IFormFile file, string[] allowFileType, long? maxFileSize)
        {
            if (file == null || file.Length <= 0 || string.IsNullOrEmpty(file.FileName))
                throw new ArgumentNullException("File not found.");

            maxFileSize = maxFileSize ?? MaxFileSize;
            if (file.Length > maxFileSize)
                throw new ArgumentException($"File size limit (Max file size: '{maxFileSize.Value.ToMb()} MB').");

            long fileSize = file.Length.ToMb();
            string filename = file.FileName;
            string fileType = Path.GetExtension(file.FileName);

            if (allowFileType != null && !allowFileType.Contains(fileType))
                throw new ArgumentException($"{filename} is not a valid file. Allowed file types are [{string.Join(",", allowFileType.ToArray())}].");

            if (fileSize > maxFileSize)
                throw new ArgumentException($"{filename} is not a valid file. Max file size is {maxFileSize}.");

            return true;
        }

        public static bool IsValidExtention(IFormFile file, List<string> allowedExtensions)
        {
            bool isValid = true;

            if (file != null)
            {
                var fileName = file.FileName;
                isValid = allowedExtensions.Any(y => fileName.EndsWith(y));
            }

            return isValid;
        }
        #endregion

        #region size
        public static int ToMb(this int? byteLength)
        {
            return Convert.ToInt32(byteLength).ToMb();
        }

        public static int ToMb(this int byteLength)
        {
            return byteLength / (1024 * 1024);

        }

        public static long ToMb(this long byteLength)
        {
            return byteLength / (1024 * 1024);

        }

        public static long K(this long byteLength)
        {
            return byteLength / 1024;
        }

        public static long M(this long byeLength)
        {
            return byeLength / (1024 * 1024);
        }

        //public static string FileSize(this long byteLength)
        //{
        //    string[] sizes = Enum<FileSizeUnit>.ToArray();
        //    //string[] sizes = { "B", "KB", "MB", "GB", "TB" };

        //    int order = 0;
        //    while (byteLength >= 1024 && order < sizes.Length - 1)
        //    {
        //        order++;
        //        byteLength = byteLength / 1024;
        //    }

        //    return string.Format("{0:0.##} {1}", byteLength, sizes[order]);
        //}

        public static string FileSizeString(this long byteLength)
        {
            FileSizeUnit unit = FileSizeUnit.B;

            while (byteLength >= 1024 && unit < FileSizeUnit.YB)
            {
                byteLength = byteLength / 1024;
                unit++;
            }

            return string.Format("{0:0.##} {1}", byteLength, unit);
        }

        //public static string FileSizeFormat(long fileSize, FileSizeUnit unit = FileSizeUnit.MB, string format = "0.##", bool useUnit = true)
        //{
        //    //FileSizeUnit unit = FileSizeUnit.B;
        //    while (fileSize >= 1024 && unit < FileSizeUnit.YB)
        //    {
        //        fileSize = fileSize / 1024;
        //        unit++;
        //    }
        //    return string.Format("{0:" + format + "} {1}", fileSize, useUnit ? unit.ToString() ? string.Empty);
        //}
        #endregion
    }
}
