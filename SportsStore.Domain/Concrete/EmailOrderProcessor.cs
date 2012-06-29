using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using System.Net.Mail;
using System.Net;

namespace Store.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        #region IOrderProcessor Members

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {

                smtpClient.EnableSsl = emailSettings.useSsl;
                smtpClient.Host = emailSettings.serverName;
                smtpClient.Port = emailSettings.serverPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.userName, emailSettings.password);

                if (emailSettings.writeAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.UnitPrice.Value * line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity,
                                      line.Product.ProductName,
                                      subtotal);
                }

                body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State ?? "")
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift wrap: {0}", shippingInfo.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.mailFromAddress,   // From
                                       emailSettings.mailToAddress,     // To
                                       "New order submitted!",          // Subject
                                       body.ToString());                // Body

                if (emailSettings.writeAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                else
                {
                    smtpClient.Send(mailMessage);
                }
            }
        }

        #endregion
    }
}
