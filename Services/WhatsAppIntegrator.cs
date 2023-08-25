using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using WhatsAppAPI.Data;
using WhatsAppAPI.IServices;

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
            var propertyPath = fieldName.Split('.', StringSplitOptions.RemoveEmptyEntries);

            Type propertyType = null;
            Type classType = Type.GetType("WhatsAppAPI.Models.Registration." + propertyPath[0]);

            PropertyInfo propertyInfo = classType.GetProperty(propertyPath[1], BindingFlags.Public | BindingFlags.Instance);            
            var convertedvalue = Convert.ChangeType(value, propertyInfo.PropertyType);
              
                       
            propertyInfo.SetValue(classType, convertedvalue);

            _context.Entry(classType).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}


