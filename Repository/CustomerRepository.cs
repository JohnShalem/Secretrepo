using Microsoft.Identity.Client;
using System.Security.Cryptography;
using WhatsAppAPI.Data;
using WhatsAppAPI.GenericRepository;
using WhatsAppAPI.Models.Registration;

namespace WhatsAppAPI.Repository
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        private readonly RegistrationDbContext _context;
        public CustomerRepository(RegistrationDbContext registrationDbContext) : base(registrationDbContext)
        {
            _context = registrationDbContext;
        }

        public int? CreateCustomer(Customer customer)
        {
            Customer newCustomer = Add(customer);
            SaveChanges();
            return newCustomer.CustomerId;
            
        }

        public int? GetCustomerIdByPhoneNumber(string phoneNumber)
        {
            var customerId = (from CustomerPhone in _context.CustomerContact
                              join Customer in _context.Customer
                              on CustomerPhone.CustomerId equals Customer.CustomerId
                              where CustomerPhone.PrimaryPhone == phoneNumber
                              select Customer.CustomerId).FirstOrDefault();
            return customerId;
        }

        public int GetCurrentStatusByCustomerId(int CustomerId)
        {
            var customer = (from CustomerStatus in _context.CustomerStatus
                            join Customer in _context.Customer
                            on CustomerStatus.CustomerId equals Customer.CustomerId
                            where Customer.CustomerId == CustomerId
                            select CustomerStatus.Status).FirstOrDefault();
            return (int)customer;

        }

        public void SaveCustomerContact(CustomerContact customerContact)
        {
            _context.CustomerContact.Add(customerContact);
            _context.SaveChanges();
        }


        
        public int? GetLastUserPromptIdByCustId(int? CustomerId)
        {
            var promptId = (from customer in _context.Customer
                            join commmunication in _context.Communication
                            on customer.CustomerId equals commmunication.CustomerId
                            where customer.CustomerId == CustomerId && commmunication.IsRePrompt == false && commmunication.UserPromptsId!=null
                            orderby commmunication.CommunicationId
                            select commmunication.UserPromptsId).LastOrDefault();
            return promptId;

        }

        public int? GetLastCommunicationIdByCustomerId(int? customerId)
        {
            int? CommunicationId = (from communication in _context.Communication
                                    join customer in _context.Customer
                                    on communication.CustomerId equals customer.CustomerId
                                    where communication.CustomerId == customerId && communication.IsRePrompt == false && communication.UserPromptsId!=null
                                    orderby communication.CommunicationId
                                    select communication.CommunicationId).LastOrDefault();
            if (CommunicationId == 0)
            {
                CommunicationId = null;
            }
            return CommunicationId;

        }



        public int? GetNextPromptIdByCurrentPromptIdInFlow(List<FlowDetails> flowDetails, int? lastPromptId)
        {
            var NextPromptId = flowDetails.SkipWhile(s => s.UserPromptsId != lastPromptId).Skip(1).FirstOrDefault()?.UserPromptsId;
            return NextPromptId;
        }

        public int? GetFlowId(int? customerId)
        {
            return _context.Customer.Where(x => x.CustomerId == customerId).Select(x => x.FlowId).First();
        }

        public int? GetUserPromptIdByCommunicationId(int? Id)
        {
            return _context.Communication.Where(x => x.CommunicationId == Id).FirstOrDefault().UserPromptsId;
        }

        public string? GetLanguageCodeById(int? customerId)
        {
            return _context.Customer.Where(x => x.CustomerId == customerId).Select(x => x.LanguageCode).FirstOrDefault();
        }

    }
}
