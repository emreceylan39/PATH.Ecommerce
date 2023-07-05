using MediatR;
using OrderService.Domain.SeedWork;
using OrderService.Infrastucture.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastucture.Extensions
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEvents(this IMediator mediator, OrderDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker.Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList().ForEach(domainEvent => domainEvent.Entity.ClearDomainEvent());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
