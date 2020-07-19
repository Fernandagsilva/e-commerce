using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
            Items = new List<BasketItem>();
        }

        public CustomerBasket(string id)
        {
            Id = id;
            Items = new List<BasketItem>();
        }

        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
