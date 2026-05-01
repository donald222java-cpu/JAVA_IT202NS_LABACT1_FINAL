namespace SmartParkingSystem
{
    public class ParkingManager
    {
        private static readonly string[] Rows = { "A", "B", "C", "D", "E", "F", "G" };
        private const int Cols = 5;

        public List<ParkingSlot> Slots { get; private set; } = new();

        public ParkingManager()
        {
            foreach (var row in Rows)
                for (int c = 1; c <= Cols; c++)
                    Slots.Add(new ParkingSlot { SlotId = $"{row}{c}" });
        }

        public ParkingSlot? GetSlot(string id) =>
            Slots.FirstOrDefault(s => s.SlotId.Equals(id, StringComparison.OrdinalIgnoreCase));

        public ParkingSlot? FindAvailableSlot() =>
            Slots.FirstOrDefault(s => !s.IsOccupied);

        public bool RegisterVehicle(string plate, string vehicleType, string slotId)
        {
            var slot = GetSlot(slotId);
            if (slot == null || slot.IsOccupied) return false;
            slot.IsOccupied = true;
            slot.PlateNumber = plate;
            slot.VehicleType = vehicleType;
            slot.TimeIn = DateTime.Now;
            return true;
        }

        public void ClearSlot(string slotId)
        {
            var slot = GetSlot(slotId);
            if (slot == null) return;
            slot.IsOccupied = false;
            slot.PlateNumber = "";
            slot.VehicleType = "";
            slot.TimeIn = null;
        }

        public static VehicleRecord CalculateFees(string plate, string vehicleType, int hours, string slotId, string discountType)
        {
            decimal baseRate = vehicleType switch
            {
                "Motorcycle" => 25m,
                "Van / Truck" => 60m,
                _ => 40m  // Car
            };

            decimal standardFee = baseRate * hours;
            decimal serviceCharge = 20m;
            decimal overtimeFee = hours > 8 ? (hours - 8) * 10m : 0m;
            decimal subtotal = standardFee + serviceCharge + overtimeFee;

            decimal discountPct = discountType switch
            {
                "Senior / PWD" => 0.20m,
                "Employee" => 0.10m,
                "Monthly" => 0.15m,
                _ => 0m
            };

            decimal discount = Math.Round(subtotal * discountPct, 2);
            decimal total = subtotal - discount;

            return new VehicleRecord
            {
                PlateNumber = plate,
                VehicleType = vehicleType,
                SlotId = slotId,
                HoursParked = hours,
                StandardFee = standardFee,
                ServiceCharge = serviceCharge,
                OvertimeFee = overtimeFee,
                Total = total,
                DiscountType = discountType,
                Discount = discount,
                PayAmount = 0,
                Change = 0
            };
        }
    }
}
