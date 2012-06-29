using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Store.Domain.Entities;

namespace Store.Domain.Abstract
{
    public interface IStoreRepository 
    {

        #region Administrations

        IQueryable<User> Users { get; }

        IQueryable<Role> Roles { get; }

        User GetUserByName(string userName);

        User GetUserObjByUserName(string username, string sha1Pswd);

        IEnumerable<string> GetRolesForUserByName(string username);
        
        void UpdateUser(User user);

        void RegisterUser(User user);

        void DeleteUser(User user);
        
        #endregion

        #region Products

        IQueryable<Product> Products { get; }
        
        Product GetProductById(int productId);
        
        void SaveProduct(Product product);

        void DeleteProduct(Product product);
        
        #endregion

        #region Categories
        
        IQueryable<Category> Categories { get; }

        Category GetCategoryById(int id);

        void SaveCategory(Category category);
        
        void DeleteCategory(Category category);
        
        #endregion

        #region Suppliers
        
        IQueryable<Supplier> Suppliers { get; }

        Supplier GetSupplierById(int id);

        void SaveSupplier(Supplier supplier);

        void DeleteSupplier(Supplier supplier);

        #endregion

        #region Shippers

        IQueryable<Shipper> Shippers { get; }

        Shipper GetShipperById(int id);

        void SaveShipper(Shipper shipper);

        void DeleteShipper(Shipper shipper);

        #endregion

        #region Orders

        IQueryable<Order> Orders { get; }

        void SaveOrder(Order order);

        void DeleteOrder(Order order);

        #endregion

        #region CheckConstraint

        bool CheckUserConstraint(User user);

        bool CheckCustomerConstraint(Customer customer);   

        #endregion

        #region Customers

        IQueryable<Customer> Customers { get; }

        Customer GetCustomerByUserEmail(string p);

        #endregion


    }
}
