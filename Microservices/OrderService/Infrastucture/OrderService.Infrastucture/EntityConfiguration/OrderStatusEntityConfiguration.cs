using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Infrastucture.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastucture.EntityConfiguration
{
    public class OrderStatusEntityConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("orderStatus", OrderDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();

            builder.Property(o => o.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(o => o.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
