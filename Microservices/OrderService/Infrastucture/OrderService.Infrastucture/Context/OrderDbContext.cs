using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Domain.SeedWork;
using OrderService.Infrastucture.EntityConfiguration;
using OrderService.Infrastucture.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastucture.Context
{
    public class OrderDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "ordering";
        private readonly IMediator mediator;


        public OrderDbContext() : base()
        {

        }

        public OrderDbContext(DbContextOptions<OrderDbContext> options, IMediator mediator) : base(options)
        {
            this.mediator = mediator;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await mediator.DispatchDomainEvents(this);
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusEntityConfiguration());

            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus(id: 1, name: "Submitted"),
                new OrderStatus(id: 2, name: "AwaitingValidation"),
                new OrderStatus(id: 3, name: "StockConfirmed")
                );
        }
    }
}
