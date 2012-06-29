using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Store.Domain.Abstract;

namespace Store.Domain.Concrete
{
    public class SimpleOrderProcessor : IOrderProcessor
    {

        public IStoreRepository repository;

        public SimpleOrderProcessor(IStoreRepository repo)
        {
            repository = repo;
        }

        #region IOrderProcessor Members

        public void ProcessOrder(Entities.Cart cart, Entities.Order order)
        {
            foreach (var line in cart.Lines)
                order.Order_Details.Add(new Entities.Order_Detail { Product = line.Product, Quantity = line.Quantity });
            repository.SaveOrder(order);
        }

        #endregion
    }
}
