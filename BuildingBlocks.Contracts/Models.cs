using System;

namespace BuildingBlocks.Contracts
{
    // User-related contracts
    public class CreateUserRequest
    {
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
    }

    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
    }

    // Product-related contracts
    public class CreateProductRequest
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }

    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }

    // Order-related contracts
    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = default!;
    }

    // Payment-related contracts
    public class PaymentRequest
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }

    public class PaymentResponse
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = default!;
    }

    // Notification-related contracts
    public class NotificationMessage
    {
        public Guid UserId { get; set; }
        public string Channel { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}