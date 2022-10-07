using CsvHelper;
using CsvHelper.Configuration;
using Models;
using System.Globalization;
using System.Text;

namespace ExportTool
{
    public class ExportService
    {
   
        public void WriteClientToCsv(List<Client> clients, string pathToDirectory, string csvFileName)
        {
            var dirInfo = new DirectoryInfo(pathToDirectory);
            if (!dirInfo.Exists)
                dirInfo.Create();

            var fullPath = Path.Combine(pathToDirectory, csvFileName);

            using (var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {

                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";"
                    };

                    using (var writer = new CsvWriter(streamWriter, config))
                    {
                        writer.WriteRecords(clients);
                        writer.Flush();
                    }
                }
            }
        }

        public List<Client> ReadClientFromCsv(string pathToDirectory, string csvFileName)
        {
            var clientList = new List<Client>();

            string fullPath = Path.Combine(pathToDirectory, csvFileName);

            using (var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {

                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {

                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    { Delimiter = ";" };

                    using (var reader = new CsvReader(streamReader, config))
                    {

                        var clients = reader.EnumerateRecords(new Client());

                        foreach (var c in clients)
                        {
                            clientList.Add(new Client
                            {
                                ClientId = c.ClientId,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                NumberOfPassport = c.NumberOfPassport,
                                SeriesOfPassport = c.SeriesOfPassport,
                                Phone = c.Phone,
                                DateOfBirth = c.DateOfBirth.ToUniversalTime(),
                                BonusDiscount = c.BonusDiscount,
                            });
                        }
                    }
                }
            }

            return clientList;
        }
    }
}