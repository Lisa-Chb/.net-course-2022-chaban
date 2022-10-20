using Models;
using Newtonsoft.Json;

namespace Services
{
    public class CurrencyService
    {
        public async Task<decimal> CurrencyConversationAsync(Account account, string currencyTo, string apiKey)
        {
                 
            var uriString = $"https://www.amdoren.com/api/currency.php?api_key={apiKey}&from={account.Currency.Name}&to={currencyTo}&amount={account.Amount}";
            var endpoit = new Uri(uriString);

            using var client = new HttpClient();
            var amountResponse = JsonConvert.DeserializeObject<AmountResponce>(await client.GetAsync(endpoit).Result.Content.ReadAsStringAsync());

            return amountResponse.amount;
        }
    }
}
