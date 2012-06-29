using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Store.Domain.Abstract;
using Store.Domain.Entities;

using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Store.WebUI.Infrastructure;
using Ninject;
using Lucene.Net.Index;
using Lucene.Net.Analysis;
using System.Configuration;
using Lucene.Net.Store;
using System.ComponentModel;

namespace Store.Domain.Concrete
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreEntities context = new StoreEntities();
        public  Directory directory { get; set; }
        [Inject]
        public  Analyzer analyzer { get; set; }
        
        public EFStoreRepository()
        {
            directory = FSDirectory.Open(new System.IO.DirectoryInfo(ConfigurationManager.AppSettings["SearchDataPath"]));       
        }

        [Inject]
        public ILogger Logger { set; get; }
        
        #region Products

        public IQueryable<Product> Products
        {
            get
            {
                IQueryable<Product> products = context.Products.ToList().Trim<Product>().AsQueryable();
                return context.Products;
            }
        }

        public Product GetProductById(int productId)
        {
            return context.Products.SingleOrDefault(p => p.ProductID == productId);
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
                context.Products.Add(product);
            else
            {
                Product product_ = context.Products.Single(p => p.ProductID == product.ProductID);
                context.Entry(product_).CurrentValues.SetValues(product);
            }
            context.SaveChanges();
            MarkProductForSearcher(product);
            Logger.Info(string.Format("Product with ID = {0} and name {1} in database must be inserted or updated in database", product.ProductID, product.ProductName));
        }

        public void DeleteProduct(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            MarkProductForSearcher(product);
            Logger.Info(String.Format("Product with id = {0} and name {0} has been removed from database", product.ProductID, product.ProductName));
        }

        #endregion

        #region Categories

        IQueryable<Category> IStoreRepository.Categories
        {
            get 
            {
                IQueryable<Category> categories = (IQueryable<Category>)context.Categories.ToList().Trim<Category>().AsQueryable();
                return context.Categories; 
            }
        }

        public Category GetCategoryById(int id)
        {
            Category category = context.Categories.SingleOrDefault(c => c.CategoryID == id);
            return category;
        }

        public void SaveCategory(Category category)
        {
            if (category.CategoryID == 0)
                context.Categories.Add(category);
            else
            {
                Category cat = context.Categories.Single(c => c.CategoryID == category.CategoryID);
                context.Entry(cat).CurrentValues.SetValues(category);
            }
            context.SaveChanges(); 
            Logger.Info(String.Format("Category with id = {0} and name {0} is saved in database", category.CategoryID, category.CategoryName));
        }

        public void DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
            Logger.Info(String.Format("Category with id = {0} and name {0} must be removed from database", category.CategoryID, category.CategoryName));
        }

        #endregion
        
        #region Suppliers

        public IQueryable<Supplier> Suppliers
        {
            get
            {
                IQueryable<Supplier> suppliers = (IQueryable<Supplier>)context.Suppliers.ToList().Trim<Supplier>().AsQueryable();
                return suppliers;
            }
        }

        public Supplier GetSupplierById(int id)
        {
            Supplier supplier = context.Suppliers.SingleOrDefault(s => s.SupplierID == id);
            return supplier;
        }

        
        public void SaveSupplier(Supplier supplier)
        {
            if (supplier.SupplierID == 0)
                context.Suppliers.Add(supplier);
            else
            {
                Supplier supplier_ = context.Suppliers.Single(s => s.SupplierID == supplier.SupplierID);
                context.Entry(supplier_).CurrentValues.SetValues(supplier);
            }
            context.SaveChanges();
            Logger.Info(string.Format("Supplier with id {0} and CompanyName {1} saved in database", supplier.SupplierID, supplier.CompanyName));
        }

        public void DeleteSupplier(Supplier supplier)
        {
            context.Suppliers.Remove(supplier);
            context.SaveChanges();
            Logger.Info(string.Format("Supplier with id {0} and CompanyName {1} removed from database", supplier.SupplierID, supplier.CompanyName));
        }

        #endregion

        #region Shippers

        public IQueryable<Shipper> Shippers
        {
            get
            {
                IQueryable<Shipper> shippers = (IQueryable<Shipper>)context.Shippers.ToList().Trim<Shipper>().AsQueryable();
                return shippers;
            }
        }

        public Shipper GetShipperById(int id)
        {
            Shipper shipper = context.Shippers.SingleOrDefault(s => s.ShipperID == id);
            return shipper;
        }
    
        public void SaveShipper(Shipper shipper)
        {
            if (shipper.ShipperID == 0)
                context.Shippers.Add(shipper);
            else
            {
                Shipper shipper_ = context.Shippers.Single(s => s.ShipperID == shipper.ShipperID);
                context.Entry(shipper_).CurrentValues.SetValues(shipper);
            }
            context.SaveChanges();
            Logger.Info(string.Format("Suhipper with id {0} and CompanyName {1} saved in database", shipper.ShipperID, shipper.CompanyName));
        }

        public void DeleteShipper(Shipper shipper)
        {
            context.Shippers.Remove(shipper);
            context.SaveChanges();
            Logger.Info(string.Format("Shipper with id {0} and CompanyName {1} removed from database", shipper.ShipperID, shipper.CompanyName));
        }

        #endregion

        #region Orders

        public IQueryable<Order> Orders
        {
            get
            {
                IQueryable<Order> orders = (IQueryable<Order>)context.Orders.ToList().Trim<Order>().AsQueryable();
                return orders;
            }
        }

        public void SaveOrder(Order order)
        {
            if (order.OrderID == 0)
            {
                order.Customer = context.Customers.Single(c => c.CustomerID == order.CustomerID);
                context.Entry(order).State = EntityState.Added;
            }
            else
            {
                Order order_ = context.Orders.Single(o => o.OrderID == order.OrderID);
                context.Entry(order_).CurrentValues.SetValues(order);
            }
            context.SaveChanges();
            Logger.Info(string.Format("Order with id {0} saved in database", order.OrderID));
        }

        public void DeleteOrder(Order order)
        {
            context.Orders.Remove(order);
            context.SaveChanges();
            Logger.Info(string.Format("Order with id {0} removed from database", order.OrderID));
        }

        #endregion

        #region Administration

        public IQueryable<User> Users
        {
            get
            {
                IQueryable<User> users = (IQueryable<User>)context.Users.ToList().Trim<User>().AsQueryable();
                return users;
            }
        }

        public IQueryable<Role> Roles
        {
            get 
            {
                IQueryable<Role> roles = context.Roles;
                return roles;
            }
        }

        public IEnumerable<string> GetRolesForUserByName(string username)
        {
            if (username == null)
                return new string[] { };
            User user = context.Users.SingleOrDefault(u => u.Email == username);
            if (user == null)
                return new string[] { };
            IEnumerable<string> roles = user.Roles.Select(r => r.RoleName.TrimEnd());
            return roles;
        }

        #region IUserUnitOfWork Members

        #endregion

        public User GetUserByName(string userName)
        {
            User user = context.Users.SingleOrDefault(p => p.Email == userName);
            return user;
        }

        public void UpdateUser(User user)
        {
            user.Email = user.Email.TrimEnd();
            User user_ = context.Users.Single(u => u.UserID == user.UserID);
            context.Entry(user_).CurrentValues.SetValues(user);
            context.SaveChanges();
            Logger.Info(string.Format("User with Email {0} and password {1} was updated in database", user.Email, user.Password));
        }

        public void RegisterUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            Logger.Info(string.Format("User with Email {0} and password {1} was updated in database", user.Email, user.Password));
        }

        public void DeleteUser(User user)
        {
            User _user = context.Users.SingleOrDefault(u => u.UserID == user.UserID);
            context.Users.Remove(_user);
            context.SaveChanges();
            Logger.Info(string.Format("User with Email {0} and password {1} was deleted from database", user.Email, user.Password));
        }

        public User GetUserObjByUserName(string username, string sha1Pswd)
        {
            User user = context.Users.SingleOrDefault(u => u.Email == username);
            return user;
        }
        #endregion 

        #region CheckConstraint

        public bool CheckUserConstraint(User user)
        {
            if (context.Users.SingleOrDefault(m => m.Email == user.Email) != null)
                return false;
            else
                return true;
        }

        public bool CheckCustomerConstraint(Customer customer)
        {
            if (context.Customers.SingleOrDefault(c => c.CustomerID == customer.CustomerID) != null)
                return false;
            else
                return true;
        }

        #endregion

        #region Customers

        public IQueryable<Customer> Customers 
        {
            get
            {
                IQueryable<Customer> customers = (IQueryable<Customer>)context.Customers.ToList().Trim<Customer>().AsQueryable();
                return customers;
            }
        }

        public Customer GetCustomerByUserEmail(string email)
        {
            User user = context.Users.Single(u => u.Email.TrimEnd() == email);
            if (user != null)
                return user.Customer;
            return null;
        }

        #endregion

        private void MarkProductForSearcher(Product product)
        {
            if (IndexWriter.IsLocked(directory))
            {
                IndexWriter.Unlock(directory);
            }
            using (var writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED))
            {
                writer.AddDocument(product.GetDocument());
                writer.Optimize();
                writer.Close();
            }
        }

    }
}