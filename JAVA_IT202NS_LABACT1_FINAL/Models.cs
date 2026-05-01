namespace SmartParkingSystem
{
    public class ParkingSlot
    {
        public string SlotId { get; set; } = "";
        public bool IsOccupied { get; set; } = false;
        public string PlateNumber { get; set; } = "";
        public string VehicleType { get; set; } = "";
        public DateTime? TimeIn { get; set; }
    }

    public class VehicleRecord
    {
        public string PlateNumber { get; set; } = "";
        public string VehicleType { get; set; } = "";
        public string SlotId { get; set; } = "";
        public int HoursParked { get; set; }
        public decimal StandardFee { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal OvertimeFee { get; set; }
        public decimal Total { get; set; }
        public string DiscountType { get; set; } = "None";
        public decimal Discount { get; set; }
        public decimal PayAmount { get; set; }
        public decimal Change { get; set; }
    }
}
