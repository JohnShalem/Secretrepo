using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using WhatsAppAPI.Data;
using WhatsAppAPI.IServices;
using WhatsAppAPI.Models.Registration;

namespace WhatsAppAPI.Services
{
    public class WhatsAppIntegrator : IWhatsAppIntegrator
    {
        private readonly RegistrationDbContext _context;
        public WhatsAppIntegrator(RegistrationDbContext registrationDbContext)
        {
            _context = registrationDbContext;
        }
        public void SaveCustomerData(string fieldName, string value,int? customerId)
        {

            try
            {
                var propertyPath = fieldName.Split('.', StringSplitOptions.RemoveEmptyEntries);

                Type propertyType = null;
                Type classType = Type.GetType("WhatsAppAPI.Models.Registration." + propertyPath[0]);

                PropertyInfo propertyInfo = classType.GetProperty(propertyPath[1], BindingFlags.Public | BindingFlags.Instance);
                var convertedvalue = Convert.ChangeType(value, propertyInfo.PropertyType);

                Customer? customerObject = null;
                CustomerContact? customerContactObject = null;

                if (propertyPath[0] == "Customer")
                {
                    customerObject = _context.Customer.Where(x => x.CustomerId == customerId).FirstOrDefault();
                    propertyInfo.SetValue(customerObject, convertedvalue);
                    _context.Update(customerObject);
                    _context.SaveChanges();
                }
                else if (propertyPath[0] == "CustomerContact")
                {
                    customerContactObject = _context.Customer.Where(x => x.CustomerId == customerId).FirstOrDefault().CustomerContact;
                    propertyInfo.SetValue(customerContactObject, convertedvalue);
                    _context.Update(customerContactObject);
                    _context.SaveChanges();
                }               
            }
            catch (Exception ex)
            {

                throw ex;
            }

           
        }
    }
}


