using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Store.Domain.Entities;

namespace Store.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, Order order);
    }
}
