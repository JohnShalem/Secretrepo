using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using WhatsAppAPI.Data;
using WhatsAppAPI.IServices;
using WhatsAppAPI.Models.Registration;
using WhatsAppAPI.Repository;

namespace WhatsAppAPI.Services
{
    public class WhatsAppIntegrator : IWhatsAppIntegrator
    {
        private readonly CustomerRepository _customerRepository;
        public WhatsAppIntegrator(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
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

                if (propertyPath[0] == "Customer")
                {
                    Customer? customer= _customerRepository.Find(s=>s.CustomerId== customerId).FirstOrDefault();

                    if (customer != null)
                    {
                        propertyInfo.SetValue(customer, convertedvalue);
                        _customerRepository.Update(customer);
                        _customerRepository.SaveChanges();
                    }

                }
                else if (propertyPath[0] == "CustomerContact")
                {
                    //CustomerContact? customerContactObject = _context.Customer.Where(x => x.CustomerId == customerId).FirstOrDefault().CustomerContact;
                    //propertyInfo.SetValue(customerContactObject, convertedvalue);
                    //_context.Update(customerContactObject);
                    //_context.SaveChanges();
                }               
            }
            catch (Exception ex)
            {

                throw ex;
            }

           
        }
    }
}


