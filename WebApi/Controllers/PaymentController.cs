using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentRepository _repository;

        public PaymentsController(IPaymentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrder(int orderId)
        {
            var payments = await _repository.GetByOrderIdAsync(orderId);
            return Ok(payments);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Payment payment)
        {
            payment.Status = PaymentStatus.Pending;
            await _repository.AddAsync(payment);
            return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> MarkCompleted(int id, [FromBody] string transactionId)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null) return NotFound();

            payment.Status = PaymentStatus.Completed;
            payment.TransactionId = transactionId;

            // ✅ Update order status
            payment.Order.Status = OrderStatus.Paid;
            await _repository.UpdateAsync(payment);

            return Ok(payment);
        }

        [HttpPut("{id}/fail")]
        public async Task<IActionResult> MarkFailed(int id)
        {
            var payment = await _repository.GetByIdAsync(id);
            if (payment == null) return NotFound();

            payment.Status = PaymentStatus.Failed;
            await _repository.UpdateAsync(payment);

            return Ok(payment);
        }
    }
}
