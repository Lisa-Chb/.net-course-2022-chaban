using Models;
using Models.ModelsValidationExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClientService
    {
        private List<Client> _clients; 
        public void AddNewClient(List<Client> _clients, Client client)
        {
            try
            {
                _clients.Add(client);
            }
           catch (ClientAgeValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(ClientSeriesOfPassportValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(ClientNumberOfPassportValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
