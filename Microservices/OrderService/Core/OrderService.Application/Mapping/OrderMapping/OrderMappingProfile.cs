﻿using AutoMapper;
using OrderService.Application.Features.Commands.CreateOrder;
using OrderService.Application.Features.Queries.ViewModels;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderService.Application.Features.Commands.CreateOrder.CreateOrderCommand;

namespace OrderService.Application.Mapping.OrderMapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, CreateOrderCommand>().ReverseMap();

            CreateMap<Domain.AggregateModels.OrderAggregate.OrderItem, OrderItemDTO>().ReverseMap();

            CreateMap<Order, OrderDetailViewModel>()
                .ForMember(x => x.City, y => y.MapFrom(z => z.Address.City))
                .ForMember(x => x.Country, y => y.MapFrom(z => z.Address.Country))
                .ForMember(x => x.Street, y => y.MapFrom(z => z.Address.Street))
                .ForMember(x => x.State, y => y.MapFrom(z => z.Address.State))
                .ForMember(x => x.Date, y => y.MapFrom(z => z.OrderDate))
                .ForMember(x => x.OrderNumber, y => y.MapFrom(z => z.Id.ToString()))
                .ForMember(x => x.Status, y => y.MapFrom(z => z.OrderStatus.Name))
                .ForMember(x => x.Total, y => y.MapFrom(z => z.OrderItems.Sum(i => i.Quantity * i.Price)));

            CreateMap<Domain.AggregateModels.OrderAggregate.OrderItem, Features.Queries.ViewModels.OrderItem>();

        }
    }
}
