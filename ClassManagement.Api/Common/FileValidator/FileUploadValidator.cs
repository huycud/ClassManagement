using ClassManagement.Api.Common.Exceptions;
using Utilities.Enums;
using Utilities.Messages;
using static Utilities.Enums.EnumTypes;

namespace ClassManagement.Api.Common.FileValidator
{
    public static class FileUploadValidator
    {
        public static bool IsValidFile(byte[] fileByte, string fileContentType, FileType type)
        {
            if (string.IsNullOrEmpty(fileContentType)) throw new BadRequestException(ErrorMessages.UNKNOWN);

            bool isValid = false;

            var imgExtension = FileExtension.None;

            if (type == FileType.Image)
            {
                if (fileContentType.Contains("jpg") | fileContentType.Contains("jpeg")) imgExtension = FileExtension.Jpg;

                else if (fileContentType.Contains("png")) imgExtension = FileExtension.Png;

                isValid = IsValidCompressionFile(fileByte, imgExtension);
            }

            else if (type == FileType.Compression)
            {
                if (fileContentType.Contains("rar")) imgExtension = FileExtension.Rar;

                else if (fileContentType.Contains("zip")) imgExtension = FileExtension.Zip;

                isValid = IsValidCompressionFile(fileByte, imgExtension);
            }

            return isValid;
        }

        //public static bool IsValidImageFile(byte[] fileByte, string fileContentType)
        //{
        //    if (string.IsNullOrEmpty(fileContentType)) throw new BadRequestException(ErrorMessages.UNKNOWN);

        //    bool isValid = false;

        //    byte[] jpgByte = { 255, 216, 255, 224 };

        //    byte[] pngByte = { 137, 80, 78, 71 };

        //    byte[] rarByte = { 82, 97, 114, 33, 26, 7 };

        //    byte[] zipByte = { 80, 75, 3, 4 };

        //    var imgExtension = FileExtension.NONE;

        //    if (fileContentType.Contains("jpg") | fileContentType.Contains("jpeg")) imgExtension = FileExtension.JPG;

        //    else if (fileContentType.Contains("png")) imgExtension = FileExtension.PNG;

        //    if (imgExtension == FileExtension.JPG || imgExtension == FileExtension.JPEG)
        //    {
        //        if (fileByte.Length >= 4)
        //        {
        //            int j = 0;

        //            for (int i = 0; i <= 3; i++)
        //            {
        //                if (fileByte[i] == jpgByte[i])
        //                {
        //                    j++;

        //                    if (j == 3) isValid = true;
        //                }
        //            }
        //        }
        //    }

        //    if (imgExtension == FileExtension.PNG)
        //    {
        //        if (fileByte.Length >= 4)
        //        {
        //            int j = 0;

        //            for (int i = 0; i <= 3; i++)
        //            {
        //                if (fileByte[i] == pngByte[i])
        //                {
        //                    j++;

        //                    if (j == 3) isValid = true;
        //                }
        //            }
        //        }
        //    }

        //    //if (imgExtension == ImageFileExtension.BMP)
        //    //{
        //    //    if (fileByte.Length >= 4)
        //    //    {
        //    //        int j = 0;

        //    //        for (int i = 0; i <= 1; i++)
        //    //        {
        //    //            if (fileByte[i] == bmpByte[i])
        //    //            {
        //    //                j++;

        //    //                if (j == 2) isValid = true;
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //if (imgExtension == ImageFileExtension.GIF)
        //    //{
        //    //    if (fileByte.Length >= 4)
        //    //    {
        //    //        int j = 0;
        //    //        for (int i = 0; i <= 1; i++)
        //    //        {
        //    //            if (fileByte[i] == gifByte[i])
        //    //            {
        //    //                j++;

        //    //                if (j == 3) isValid = true;
        //    //            }
        //    //        }
        //    //    }
        //    //}

        //    return isValid;
        //}

        public static bool IsValidCompressionFile(byte[] fileByte, FileExtension extension)
        {
            Dictionary<FileExtension, byte[]> fileTypes = new()
            {
                { FileExtension.Jpg, new byte[] { 255, 216, 255, 224 } },

                { FileExtension.Png, new byte[] { 137, 80, 78, 71 } },

                { FileExtension.Rar, new byte[] { 82, 97, 114, 33, 26, 7 } },

                { FileExtension.Zip, new byte[] { 80, 75, 3, 4 }}
            };

            foreach (var keyValue in fileTypes)
            {
                if (extension == keyValue.Key && CheckFileType(fileByte, keyValue.Value)) return true;
            }

            return false;
        }

        private static bool CheckFileType(byte[] fileBytes, byte[] fileTypeBytes)
        {
            if (fileBytes.Length < fileTypeBytes.Length) return false;

            for (int i = 0; i < fileTypeBytes.Length; i++)
            {
                if (fileBytes[i] != fileTypeBytes[i]) return false;
            }

            return true;
        }
    }
}
