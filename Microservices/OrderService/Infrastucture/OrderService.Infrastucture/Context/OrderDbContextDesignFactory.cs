using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastucture.Context
{
    public class OrderDbContextDesignFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public OrderDbContextDesignFactory()
        {
            
        }
        public OrderDbContext CreateDbContext(string[] args)
        {
            var connection = "Data Source=localhost; Initial Catalog=OrderSevice; Persist Security=true, User ID=sa; Password=master.12";

            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>().UseSqlServer(connection);

            return new OrderDbContext(optionsBuilder.Options, new NonMediator());
        }

        class NonMediator : IMediator
        {
            public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult<TResponse>(default);
            }

            public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
            {
                return Task.FromResult<TRequest>(default);
            }

            public Task<object?> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult<object>(default);
            }
        }
    }
}
