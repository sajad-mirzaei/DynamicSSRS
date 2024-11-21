using System;
using System.IO;

namespace DynamicSSRS
{
    public class FileHelper
    {
        public string[] GetRdlFiles(string folderPath, bool AllNestedDirectories = false)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    throw new DirectoryNotFoundException("The specified folder '{folderPath}' does not exist.");
                }

                //Get RDLs
                if (AllNestedDirectories)
                    return Directory.GetFiles(folderPath, "*.rdl", SearchOption.AllDirectories);
                else
                    return Directory.GetFiles(folderPath, "*.rdl", SearchOption.TopDirectoryOnly);
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("Access denied to the specified folder.");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: {ex.Message}");
            }
        }
    }

}