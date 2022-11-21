using System.Collections.Generic;
using System;

namespace InstamojoAPI
{
    //public class OrderInfo
    //{
    //    public object shipping_address { get; set; }
    //    public object shipping_city { get; set; }
    //    public object shipping_state { get; set; }
    //    public object shipping_zip { get; set; }
    //    public object shipping_country { get; set; }
    //    public int quantity { get; set; }
    //    public string unit_price { get; set; }
    //    public List<object> variants { get; set; }
    //}

    public class GetPaymentDetails
    {
        public string id { get; set; }
        public string title { get; set; }
        public string payment_type { get; set; }
        public string payment_request { get; set; }
        public bool status { get; set; }
        public object link { get; set; }
        public object product { get; set; }
        public string seller { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public object payout { get; set; }
        public object fees { get; set; }
        public object total_taxes { get; set; }
        public List<object> cases { get; set; }
        public object affiliate_id { get; set; }
        public object affiliate_commission { get; set; }
        public string instrument_type { get; set; }
        public string billing_instrument { get; set; }
        public Failure failure { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string tax_invoice_id { get; set; }
        public string resource_uri { get; set; }
    }


}
