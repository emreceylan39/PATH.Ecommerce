﻿using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.IntegrationEvents
{
    public class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }

        public OrderStartedIntegrationEvent(string userId)
        {
            UserId = userId;
        }
    }
}
