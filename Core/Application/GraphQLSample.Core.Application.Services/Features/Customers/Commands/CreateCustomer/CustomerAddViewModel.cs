﻿using System.ComponentModel.DataAnnotations;
using GraphQLSample.Core.Infrastructure.Domain.Enums;

namespace GraphQLSample.Core.Application.Services.Features.Customers.Commands.CreateCustomer
{
    public class CustomerAddViewModel
    {
        /// <summary>
        /// Get or set the customer email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Code { get; set; }

        /// <summary>
        /// Get or set the current Status of the Customer
        /// </summary>
        [Required]
        public Status Status { get; set; }

        public bool IsBlocked { get; set; }
    }
}
