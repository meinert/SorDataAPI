using CsvHelper;
using CsvHelper.Configuration;
using SorDataAPI.Mappings;
using SorDataAPI.Models;
using System.Globalization;

namespace SorDataAPI.Utils
{
    /// <summary>
    /// Utility class for parsing the SOR CSV file.
    /// </summary>
    public static class CsvParser
    {
        /// <summary>
        /// Parses the SOR CSV file into a list of Organization objects.
        /// </summary>
        /// <param name="filePath">The file path of the CSV file.</param>
        /// <returns>A list of Organization objects parsed from the CSV file.</returns>
        public static IEnumerable<Organization> ParseCsvFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }

            // To parse danish char-set iso-8859-1 encoding is used
            using StreamReader reader = new StreamReader(filePath, System.Text.Encoding.GetEncoding("iso-8859-1"));

            // The CsvReader is constructed. Delimiter and CsvMode.NoEscape is set to ensure correct parsing of the SOR data            
            using CsvReader csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HeaderValidated = null,
                MissingFieldFound = null,
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
                Mode = CsvMode.NoEscape,
                BadDataFound = context => {
                    Console.WriteLine($"Bad data found on row {context.Field}: {context.RawRecord}"); // Log bad data.
                }
            });

            // Register the class map to apply transformations
            csv.Context.RegisterClassMap<OrganizationMap>();

            var records = csv.GetRecords<Organization>().ToList();
            if (!records.Any())
            {
                throw new Exception("No valid rows found in the CSV file.");
            }

            return records;

            // Map the CSV fields to the Organization class
            // return csv.GetRecords<Organization>();
        }
    }
}
