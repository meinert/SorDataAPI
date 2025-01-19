using CsvHelper.Configuration;
using SorDataAPI.Models;
using System.Text.RegularExpressions;

namespace SorDataAPI.Mappings
{
    /// <summary>
    /// ClassMap for mapping CSV fields to Organization object with transformations.
    /// </summary>
    public class OrganizationMap : ClassMap<Organization>
    {
        /// <summary>
        /// The "enhedstype" is casted to a ENUM. If no matches TypeEnum.UKENDT is returned
        /// </summary>
        public OrganizationMap()
        {
            // Map each CSV column to the corresponding property in the Organization object
            Map(m => m.Name).Name("Enhedsnavn");
            Map(m => m.Type).Name("Enhedstype").Convert(args => mapToEnhedsType(args));
            Map(m => m.Region).Name("P_Region"); // TODO: Consider mapping this and others to an enum
            Map(m => m.Specialty).Name("Hovedspeciale");
            Map(m => m.SorCode).Name("SOR-kode");
            Map(m => m.ParentSorCode).Name("Parent-SOR-kode");
            Map(m => m.Cvr).Name("CVR");
        }

        /// <summary>
        /// Method to map the "enhedstype" to the TypeEnum. If no match is found, TypeEnum.UKENDT is returned
        /// </summary>
        public TypeEnum mapToEnhedsType(CsvHelper.ConvertFromStringArgs args)
        {
            try
            {
                // Ensure the Value and Row exist before accessing
                if (args.Row == null || args.Row[1] == null)
                {
                    Console.WriteLine("Type field is missing or empty.");
                    return TypeEnum.UKENDT;
                }

                // Getting the column with index 1 from the data
                string rawValue = args.Row[1]!;
                return castToEnum<TypeEnum>(rawValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing Enhedstype: {ex.Message}"); // TODO: Consider how to handle "Enhedstyper" that are not in the TypeEnum
                return TypeEnum.UKENDT;
            }
        }

        /// <summary>
        /// Method to cast to an enum of generic type T. The string value is converted to upper cases and sanitized for chars that cannot be used in Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rawValue"></param>
        /// <returns></returns>
        public T castToEnum<T>(string rawValue)
        {
            if (string.IsNullOrWhiteSpace(rawValue))
                throw new ArgumentException("Input cannot be null or empty.", nameof(rawValue));

            // Replace invalid characters with underscores
            var sanitized = Regex.Replace(rawValue, @"[^a-zA-Z0-9æøåÆØÅ_]", "_");

            // Remove consecutive underscores
            sanitized = Regex.Replace(sanitized, @"_+", "_");

            // Trim string for '_'
            sanitized = sanitized.Trim('_');

            // Ensure the name starts with a letter or underscore
            if (!char.IsLetter(sanitized[0]) && sanitized[0] != '_')
            {
                sanitized = "_" + sanitized;
            }
            return (T)Enum.Parse(typeof(T), sanitized.ToUpperInvariant());
        }
    }
}
