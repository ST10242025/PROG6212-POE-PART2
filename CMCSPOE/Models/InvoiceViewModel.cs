namespace CMCSPOE.Models
{
    public class InvoiceViewModel
    {
        public string? LecturerName { get; set; }
        public decimal? HoursWorked { get; set; }
        public decimal? HourlyRate { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime ClaimDate { get; set; }
        public string? ReportGeneratedBy { get; set; } // Optional metadata
        public DateTime ReportDate { get; set; }
        public decimal GrandTotal { get; set; } // Sum of all approved claims
    }

}
