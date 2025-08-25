using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByIdAsync(int id);
        Task<IEnumerable<Payment>> GetByOrderIdAsync(int orderId);
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
    }
}
