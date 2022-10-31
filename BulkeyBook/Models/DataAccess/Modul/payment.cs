namespace BulkeyBook.Models.DataAccess.Modul
{
    public class Payment
    {
        public double amount { get; set; }

        public string purpose { get; set; }

        public bool allow_repeated_payments { get; set; } = false;
        public bool send_email { get; set; } = false;
        public bool send_sms { get; set; } = false;
    }
}
