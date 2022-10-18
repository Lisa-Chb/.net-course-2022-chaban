using CsvHelper;
using CsvHelper.Configuration;
using Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace ExportTool
{
    public class ExportService
    {
        public Task WriteSerializePersonToCsv<T>(T persons, string pathToDirectory, string csvFileName)
        {
            var dirInfo = new DirectoryInfo(pathToDirectory);
            if (!dirInfo.Exists)
                dirInfo.Create();

            var fullPath = Path.Combine(pathToDirectory, csvFileName);

            string jsonPerson = JsonConvert.SerializeObject(persons);
            File.WriteAllText(fullPath, jsonPerson);
            return Task.CompletedTask;
        }

        public Task<T> ReadSerializePersonFromCsv<T>(string pathToDirectory, string csvFileName)
        {
            var fullPath = Path.Combine(pathToDirectory, csvFileName);

            string jsonPerson = File.ReadAllText(fullPath);
            var persons = JsonConvert.DeserializeObject<T>(jsonPerson);
            return Task.FromResult(persons);
        }
        public async Task WriteClientToCsv(List<Client> clients, string pathToDirectory, string csvFileName)
        {
            var dirInfo = new DirectoryInfo(pathToDirectory);
            if (!dirInfo.Exists)
                dirInfo.Create();

            var fullPath = Path.Combine(pathToDirectory, csvFileName);

            await using var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
            await using var streamWriter = new StreamWriter(fileStream, Encoding.UTF8);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };

            await using var writer = new CsvWriter(streamWriter, config);

            await writer.WriteRecordsAsync(clients);
            await writer.FlushAsync();
        }

        public async Task<List<Client>> ReadClientFromCsv(string pathToDirectory, string csvFileName)
        {
            var clientList = new List<Client>();

            string fullPath = Path.Combine(pathToDirectory, csvFileName);

            await using var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);

            using var streamReader = new StreamReader(fileStream, Encoding.UTF8);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            { Delimiter = ";" };

            using var reader = new CsvReader(streamReader, config);

            var clientsAsync = reader.EnumerateRecordsAsync(new Client());

            await foreach (var c in clientsAsync)
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
            return clientList;
        }
    }
}