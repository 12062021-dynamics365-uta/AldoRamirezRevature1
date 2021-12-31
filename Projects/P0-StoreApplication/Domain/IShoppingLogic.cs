using Model;
using System.Collections.Generic;

namespace Domain
{
    public interface IShoppingLogic
    {
        Customer CurrentCustomer { get; set; }
        Store CurrentStore { get; set; }

        bool AddProductToCart(Product product);
        void CancelOrder();
        bool Checkout();
        int ConvertInputToInt(string userInput);
        void Exit();
        List<Order> GetListOfOrders();
        void InitializeCurrentStoreProducts();
        void InitializePreviousStoreOrders();
        void InitializeStores();
        bool Login(string userName, string password);
        IEnumerable<KeyValuePair<(string Name, decimal Price), int>> ConvertCartToIEnum();
        IEnumerable<KeyValuePair<(string Name, decimal Price), int>> ConvertOrdersToIEnum(int i);
        void Register(string firstName, string lastName, string userName, string password);
        int ValidateMainMenuChoice(string userInput);
        int ValidateShoppingMenuChoice(string userInput, int numOfChoices);
        int ValidateStoreListMenuChoice(string userInput);
        int ValidateStoreMenuChoice(string userInput);
    }
}