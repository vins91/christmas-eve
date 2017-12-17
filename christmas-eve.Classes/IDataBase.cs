using System;
using System.Collections.Generic;
using System.Text;

namespace christmas_eve.Classes
{
    interface IDataBase
    {
        Users GetUser(Users user);

        IEnumerable<Toys> GetAllToys();
        Toys GetToy(string id);

        IEnumerable<Orders> GetAllOrder();

        Orders GetOrder(string id);

        bool UpdateStatus(Orders order);

        bool UpdateAmountToy(Toys toy);

        bool RemoveToy(string id);

        List<Decimal> SumRequest(string name);

    }
}
