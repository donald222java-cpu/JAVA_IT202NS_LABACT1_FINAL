using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartParkingSystem
{
    public partial class MainForm : Form
    {
        private readonly ParkingManager _manager = new();
        private readonly Dictionary<string, Button> _slotButtons = new();
        private VehicleRecord? _currentRecord;

        public MainForm()
        {
            InitializeComponent();
            BuildSlotGrid();
            WireEvents();
        }

        // ── Build the 7×5 slot grid ──────────────────────────────────────────
        private void BuildSlotGrid()
        {
            string[] rows = { "A", "B", "C", "D", "E", "F", "G" };
            int cols = 5;
            int btnW = 58, btnH = 35, gapX = 10, gapY = 8, startX = 8, startY = 8;

            for (int r = 0; r < rows.Length; r++)
            {
                for (int c = 1; c <= cols; c++)
                {
                    string id = $"{rows[r]}{c}";
                    var btn = new Button
                    {
                        Text = id,
                        Size = new Size(btnW, btnH),
                        Location = new Point(startX + (c - 1) * (btnW + gapX), startY + r * (btnH + gapY)),
                        BackColor = Color.LimeGreen,
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                        Tag = id
                    };
                    btn.FlatAppearance.BorderSize = 1;
                    btn.Click += SlotButton_Click;
                    _slotButtons[id] = btn;
                    pnlSlots.Controls.Add(btn);
                }
            }
        }

        // ── Wire up events ───────────────────────────────────────────────────
        private void WireEvents()
        {
            btnRegister.Click += BtnRegister_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnProcess.Click += BtnProcess_Click;
            btnReceipt.Click += BtnReceipt_Click;
            btnClear.Click += BtnClear_Click;
            txtPayAmount.TextChanged += TxtPayAmount_TextChanged;
            cmbDiscount.SelectedIndexChanged += (s, e) => RefreshFees();
        }

        // ── Slot button click → fill registration panel ──────────────────────
        private void SlotButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            string id = (string)btn.Tag!;
            txtSlot.Text = id;

            var slot = _manager.GetSlot(id);
            if (slot != null && slot.IsOccupied)
            {
                txtPlate.Text = slot.PlateNumber;
                int idx = cmbVehicleType.Items.IndexOf(slot.VehicleType);
                if (idx >= 0) cmbVehicleType.SelectedIndex = idx;
                if (slot.TimeIn.HasValue)
                    numHours.Value = Math.Min((decimal)(DateTime.Now - slot.TimeIn.Value).TotalHours + 1, 72);
                RefreshFees();
            }
        }

        // ── Register Vehicle ─────────────────────────────────────────────────
        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            string plate = txtPlate.Text.Trim();
            if (string.IsNullOrEmpty(plate))
            {
                MessageBox.Show("Please enter a plate number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string slotId = txtSlot.Text.Trim();
            if (string.IsNullOrEmpty(slotId))
            {
                // Auto-assign
                var available = _manager.FindAvailableSlot();
                if (available == null)
                {
                    MessageBox.Show("No available slots.", "Full", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                slotId = available.SlotId;
                txtSlot.Text = slotId;
            }

            var existingSlot = _manager.GetSlot(slotId);
            if (existingSlot != null && existingSlot.IsOccupied)
            {
                MessageBox.Show($"Slot {slotId} is already occupied.", "Occupied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ok = _manager.RegisterVehicle(plate, cmbVehicleType.Text, slotId);
            if (ok)
            {
                UpdateSlotButton(slotId, true, plate);
                RefreshFees();
                MessageBox.Show($"Vehicle {plate} registered in slot {slotId}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ── Update Status (recalculate fees) ─────────────────────────────────
        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            RefreshFees();
        }

        // ── Process Payment ──────────────────────────────────────────────────
        private void BtnProcess_Click(object? sender, EventArgs e)
        {
            if (_currentRecord == null)
            {
                MessageBox.Show("Please register/select a vehicle first.", "No Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPayAmount.Text, out decimal paid))
            {
                MessageBox.Show("Enter a valid pay amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (paid < _currentRecord.Total)
            {
                MessageBox.Show($"Insufficient payment. Total is P{_currentRecord.Total:F2}.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _currentRecord.PayAmount = paid;
            _currentRecord.Change = paid - _currentRecord.Total;
            txtChange.Text = _currentRecord.Change.ToString("F2");

            // Free the slot
            string slot = _currentRecord.SlotId;
            _manager.ClearSlot(slot);
            UpdateSlotButton(slot, false, "");

            MessageBox.Show($"Payment processed! Change: P{_currentRecord.Change:F2}", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ── Generate Receipt ─────────────────────────────────────────────────
        private void BtnReceipt_Click(object? sender, EventArgs e)
        {
            if (_currentRecord == null)
            {
                MessageBox.Show("No transaction to print.", "Receipt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string receipt =
                $"==============================\r\n" +
                $"   SMART PARKING RECEIPT\r\n" +
                $"==============================\r\n" +
                $"Date  : {DateTime.Now:yyyy-MM-dd HH:mm}\r\n" +
                $"Plate : {_currentRecord.PlateNumber}\r\n" +
                $"Type  : {_currentRecord.VehicleType}\r\n" +
                $"Slot  : {_currentRecord.SlotId}\r\n" +
                $"Hours : {_currentRecord.HoursParked}\r\n" +
                $"------------------------------\r\n" +
                $"Standard Fee  : P{_currentRecord.StandardFee:F2}\r\n" +
                $"Service Charge: P{_currentRecord.ServiceCharge:F2}\r\n" +
                $"Overtime Fee  : P{_currentRecord.OvertimeFee:F2}\r\n" +
                $"Discount ({_currentRecord.DiscountType}): -P{_currentRecord.Discount:F2}\r\n" +
                $"------------------------------\r\n" +
                $"TOTAL         : P{_currentRecord.Total:F2}\r\n" +
                $"Paid          : P{_currentRecord.PayAmount:F2}\r\n" +
                $"CHANGE        : P{_currentRecord.Change:F2}\r\n" +
                $"==============================\r\n" +
                $"   Thank you!\r\n";

            txtReceipt.Text = receipt;
        }

        // ── Clear Form ───────────────────────────────────────────────────────
        private void BtnClear_Click(object? sender, EventArgs e)
        {
            txtPlate.Clear();
            txtSlot.Clear();
            txtPayAmount.Clear();
            txtChange.Clear();
            txtReceipt.Clear();
            numHours.Value = 1;
            cmbVehicleType.SelectedIndex = 0;
            cmbDiscount.SelectedIndex = 0;
            _currentRecord = null;
            ClearTransactionDisplay();
            ClearFeeDisplay();
        }

        // ── Refresh fee calculation and transaction panel ─────────────────────
        private void RefreshFees()
        {
            string plate = txtPlate.Text.Trim();
            string slotId = txtSlot.Text.Trim();
            int hours = (int)numHours.Value;

            if (string.IsNullOrEmpty(plate) || string.IsNullOrEmpty(slotId)) return;

            _currentRecord = ParkingManager.CalculateFees(
                plate, cmbVehicleType.Text, hours, slotId, cmbDiscount.Text);

            // Transaction panel
            lblTxPlate.Text = plate;
            lblTxInfo.Text = cmbVehicleType.Text;
            lblTxDur.Text = $"{hours} hrs";
            lblTxSlot.Text = slotId;
            lblTxOT.Text = _currentRecord.OvertimeFee > 0 ? $"P{_currentRecord.OvertimeFee:F0}" : "—";

            // Fee panel
            lblFeeStd.Text = $"P{_currentRecord.StandardFee:F0}";
            lblFeeSvc.Text = $"P{_currentRecord.ServiceCharge:F0}";
            lblFeeTotal.Text = $"P{_currentRecord.Total:F2}";

            // Update change if pay amount is set
            if (decimal.TryParse(txtPayAmount.Text, out decimal paid))
                txtChange.Text = Math.Max(0, paid - _currentRecord.Total).ToString("F2");
        }

        private void TxtPayAmount_TextChanged(object? sender, EventArgs e)
        {
            if (_currentRecord == null) return;
            if (decimal.TryParse(txtPayAmount.Text, out decimal paid))
                txtChange.Text = Math.Max(0, paid - _currentRecord.Total).ToString("F2");
        }

        private void UpdateSlotButton(string id, bool occupied, string plate)
        {
            if (!_slotButtons.TryGetValue(id, out var btn)) return;
            btn.BackColor = occupied ? Color.Red : Color.LimeGreen;
            btn.Text = occupied ? plate.Length > 5 ? plate[..5] : plate : id;
            btn.ForeColor = Color.White;
        }

        private void ClearTransactionDisplay()
        {
            lblTxPlate.Text = lblTxInfo.Text = lblTxDur.Text = lblTxSlot.Text = lblTxOT.Text = "—";
        }

        private void ClearFeeDisplay()
        {
            lblFeeStd.Text = lblFeeSvc.Text = lblFeeTotal.Text = "—";
        }
    }
}
