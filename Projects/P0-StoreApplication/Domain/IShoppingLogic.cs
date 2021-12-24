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
        void Checkout();
        int ConvertInputToInt(string userInput);
        void Exit();
        List<Order> GetListOfOrders();
        void InitializePreviousStoreOrders();
        bool Login(string userName, string password);
        void Register(string firstName, string lastName, string userName, string password);
        int ValidateMainMenuChoice(string userInput);
        int ValidateShoppingMenuChoice(string userInput, int numOfChoices);
        int ValidateStoreListMenuChoice(string userInput);
        int ValidateStoreMenuChoice(string userInput);
    }
}