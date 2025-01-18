using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SorDataAPI.Utils
{
    /// <summary>
    /// Utility class for downloading the sor.csv file if it does not already exist in the data folder.
    /// </summary>
    public static class CsvFileRetriever
    {
        private const string CsvFileName = "sor.csv";
        private const string CsvUrl = "https://sor-filer.sundhedsdata.dk/sor_produktion/data/sor/sorcsv/V_1_2_1_19/sor.csv";

        /// <summary>
        /// Ensures the sor.csv file is available in the data folder.
        /// If the file does not exist, it is downloaded from the specified URL.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task EnsureCsvFileAsync()
        {
            string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
            string filePath = Path.Combine(dataFolder, CsvFileName);

            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}. Downloading from {CsvUrl}...");
                await DownloadFileAsync(CsvUrl, filePath);
                Console.WriteLine($"File downloaded successfully to: {filePath}");
            }
            else
            {
                Console.WriteLine($"File already exists: {filePath}");
            }
        }

        /// <summary>
        /// Downloads a file from the given URL to the specified local path.
        /// </summary>
        /// <param name="url">The URL to download the file from.</param>
        /// <param name="filePath">The local file path where the file will be saved.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private static async Task DownloadFileAsync(string url, string filePath)
        {
            using HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                await using FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                await response.Content.CopyToAsync(fileStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading file: {ex.Message}");
                throw;
            }
        }
    }
}
