using Models;
using Services;
using Xunit;

namespace ServiceTests
{
    public class CurrencyServiceTests
    {
        [Fact]
        public async Task RequestTest()
        {
            //Arrange
            var service = new CurrencyService();
            var account = new Account() {AccountId = Guid.NewGuid(), Amount = 500, Currency = new Currency() { Name = "USD"} };
            var currencyToConvert = "RUB";
            var apiKey = "QfGyJd6V7rbnaLkjgV7VY9rx5TsPUr";

            //Act
            var newAmount = await service.CurrencyConversationAsync(account, currencyToConvert, apiKey);

            //Assert
            Assert.NotEqual(0, newAmount);
         
        }
    }
}
