namespace CMCSPOE.Services
{
    using CMCSPOE.Data;
    using CMCSPOE.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class InvoiceService 
    {
        private readonly ApplicationDbContext _context;

        public InvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<InvoiceViewModel> GenerateInvoice()
        {
            var approvedClaims = _context.Claims
                .Where(c => c.Status == "Approved")
                .ToList();

            var invoiceList = approvedClaims.Select(c => new InvoiceViewModel
            {
                LecturerName = $"{c.Name} {c.Surname}",
                HoursWorked = c.HoursWorked ?? 0,
                HourlyRate = c.HourlyRate ?? 0,
                TotalAmount = c.Total,
                ClaimDate = c.ClaimDate,
                ReportGeneratedBy = "System", // Metadata
                ReportDate = DateTime.Now
            }).ToList();

            // Calculate the grand total
            if (invoiceList.Any())
            {
                invoiceList[0].GrandTotal = (decimal)invoiceList.Sum(i => i.TotalAmount);
            }

            return invoiceList;
        }
    }

}
