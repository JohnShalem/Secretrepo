using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using System;
using WhatsAppAPI.Data;
using WhatsAppAPI.GenericRepository;
using WhatsAppAPI.Models.Registration;

namespace WhatsAppAPI.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Customer> CustomerRepository { get; }

    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RegistrationDbContext _context;
        public UnitOfWork(RegistrationDbContext context)
        {
            _context = context;
        }

        private IGenericRepository<Customer> _customerRepository;
        public IGenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(_context);
                }

                return _customerRepository;
            }
        }

    }
}
