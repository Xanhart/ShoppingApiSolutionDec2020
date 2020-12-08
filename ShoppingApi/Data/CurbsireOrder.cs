using System;

namespace ShoppingApi.Data
{
    public enum CurbSideOrderStatus { Processing, Approved }

    public class CurbsireOrder
    {
        public int  Id { get; set; }
        public string WhoTheOrderIsFor { get; set; }
        public string Items { get; set; }
        public DateTime? PickupDate { get; set; }
        public CurbSideOrderStatus Status { get; set; }

    }
}
