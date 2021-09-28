using System.Linq;
using AutoMapper;
using GraphQLSample.Core.Application.Services.BaseServices;
using GraphQLSample.Core.Application.Services.Features.Customers.Commands.CreateCustomer;
using GraphQLSample.Core.Application.Services.Features.Customers.Commands.UpdateCustomer;
using GraphQLSample.Core.Application.Services.Features.Customers.Queries.GetCustomerById;
using GraphQLSample.Core.Application.Services.Services.Contracts;
using GraphQLSample.Core.Infrastructure.DataLayer.DAL;
using GraphQLSample.Core.Infrastructure.DataLayer.Repositories;
using GraphQLSample.Core.Infrastructure.Domain.Entities;

namespace GraphQLSample.Core.Application.Services.Services
{
    public class CustomerService : ServiceBase<long, Customer, CustomerViewModel, CustomerAddViewModel, CustomerEditViewModel>, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork, 
            IGenericRepository<long, Customer> repository, IMapper mapper) : base(unitOfWork, repository, mapper)
        {
        }

        public override IQueryable<Customer> FilterByTerm(IQueryable<Customer> query, string title)
        {
            return query.Where(x => x.Name.Contains(title));
        }
    }
}
