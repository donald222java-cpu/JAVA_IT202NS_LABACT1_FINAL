namespace SmartParkingSystem
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Form
            this.Text = "Smart Parking Management System";
            this.Size = new System.Drawing.Size(1000, 620);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Font = new System.Drawing.Font("Segoe UI", 9f);

            // ── LEFT PANEL: Vehicle Registration ──
            grpRegistration = new System.Windows.Forms.GroupBox
            {
                Text = "Vehicle Registration",
                Location = new System.Drawing.Point(12, 12),
                Size = new System.Drawing.Size(170, 200),
                Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold)
            };

            lblPlate = new System.Windows.Forms.Label { Text = "Plate Number", Location = new System.Drawing.Point(8, 22), AutoSize = true };
            txtPlate = new System.Windows.Forms.TextBox { Location = new System.Drawing.Point(8, 40), Width = 150, CharacterCasing = System.Windows.Forms.CharacterCasing.Upper };

            lblVehicleType = new System.Windows.Forms.Label { Text = "Vehicle Type", Location = new System.Drawing.Point(8, 65), AutoSize = true };
            cmbVehicleType = new System.Windows.Forms.ComboBox
            {
                Location = new System.Drawing.Point(8, 82),
                Width = 150,
                DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            };
            cmbVehicleType.Items.AddRange(new object[] { "Car", "Motorcycle", "Van / Truck" });
            cmbVehicleType.SelectedIndex = 0;

            lblHours = new System.Windows.Forms.Label { Text = "Hours Parked", Location = new System.Drawing.Point(8, 110), AutoSize = true };
            numHours = new System.Windows.Forms.NumericUpDown
            {
                Location = new System.Drawing.Point(8, 127),
                Width = 80,
                Minimum = 1,
                Maximum = 72,
                Value = 1
            };

            lblSlot = new System.Windows.Forms.Label { Text = "Assigned Slot", Location = new System.Drawing.Point(8, 155), AutoSize = true };
            txtSlot = new System.Windows.Forms.TextBox { Location = new System.Drawing.Point(8, 172), Width = 150, ReadOnly = true, BackColor = System.Drawing.Color.LightYellow };

            grpRegistration.Controls.AddRange(new System.Windows.Forms.Control[] { lblPlate, txtPlate, lblVehicleType, cmbVehicleType, lblHours, numHours, lblSlot, txtSlot });

            // Actions group
            grpActions = new System.Windows.Forms.GroupBox
            {
                Text = "Actions",
                Location = new System.Drawing.Point(12, 220),
                Size = new System.Drawing.Size(170, 100)
            };

            btnRegister = new System.Windows.Forms.Button
            {
                Text = "Register Vehicle",
                Location = new System.Drawing.Point(8, 22),
                Size = new System.Drawing.Size(150, 30),
                BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat
            };

            btnUpdate = new System.Windows.Forms.Button
            {
                Text = "Update Status",
                Location = new System.Drawing.Point(8, 60),
                Size = new System.Drawing.Size(150, 30),
                BackColor = System.Drawing.Color.FromArgb(60, 140, 60),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat
            };

            grpActions.Controls.AddRange(new System.Windows.Forms.Control[] { btnRegister, btnUpdate });

            // ── CENTER: Parking Status ──
            grpParking = new System.Windows.Forms.GroupBox
            {
                Text = "Parking Status",
                Location = new System.Drawing.Point(192, 12),
                Size = new System.Drawing.Size(370, 550),
                Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold)
            };

            pnlSlots = new System.Windows.Forms.Panel
            {
                Location = new System.Drawing.Point(8, 22),
                Size = new System.Drawing.Size(352, 520),
                AutoScroll = true
            };
            grpParking.Controls.Add(pnlSlots);

            // ── RIGHT-CENTER: Current Transaction ──
            grpTransaction = new System.Windows.Forms.GroupBox
            {
                Text = "Current Transaction",
                Location = new System.Drawing.Point(572, 12),
                Size = new System.Drawing.Size(200, 170),
                Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold)
            };

            int ty = 22;
            lblTxPlateLabel = MakeLabel("Plate Number", 8, ty); lblTxPlate = MakeLabel("—", 110, ty); ty += 20;
            lblTxInfoLabel = MakeLabel("Vehicle Info", 8, ty); lblTxInfo = MakeLabel("—", 110, ty); ty += 20;
            lblTxDurLabel = MakeLabel("Duration", 8, ty); lblTxDur = MakeLabel("—", 110, ty); ty += 20;
            lblTxSlotLabel = MakeLabel("Slot", 8, ty); lblTxSlot = MakeLabel("—", 110, ty); ty += 20;
            lblTxOTLabel = MakeLabel("Overtime Fee", 8, ty); lblTxOT = MakeLabel("—", 110, ty);

            grpTransaction.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTxPlateLabel, lblTxPlate, lblTxInfoLabel, lblTxInfo,
                lblTxDurLabel, lblTxDur, lblTxSlotLabel, lblTxSlot,
                lblTxOTLabel, lblTxOT });

            // Fee Calculation
            grpFee = new System.Windows.Forms.GroupBox
            {
                Text = "Fee Calculation",
                Location = new System.Drawing.Point(572, 190),
                Size = new System.Drawing.Size(200, 110),
                Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold)
            };

            int fy = 22;
            lblFeeStdLabel = MakeLabel("Standard Fee", 8, fy); lblFeeStd = MakeLabel("—", 110, fy); fy += 20;
            lblFeeSvcLabel = MakeLabel("Service Charge", 8, fy); lblFeeSvc = MakeLabel("—", 110, fy); fy += 20;
            lblFeeTotalLabel = MakeLabel("Total", 8, fy, bold: true); lblFeeTotal = MakeLabel("—", 110, fy, bold: true);

            grpFee.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblFeeStdLabel, lblFeeStd, lblFeeSvcLabel, lblFeeSvc, lblFeeTotalLabel, lblFeeTotal });

            // Payments and Receipts
            grpPayment = new System.Windows.Forms.GroupBox
            {
                Text = "Payments and Receipts",
                Location = new System.Drawing.Point(782, 12),
                Size = new System.Drawing.Size(195, 400),
                Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold)
            };

            int py = 22;
            lblDiscount = new System.Windows.Forms.Label { Text = "Discount", Location = new System.Drawing.Point(8, py), AutoSize = true }; py += 18;
            cmbDiscount = new System.Windows.Forms.ComboBox
            {
                Location = new System.Drawing.Point(8, py),
                Width = 175,
                DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            };
            cmbDiscount.Items.AddRange(new object[] { "None", "Senior / PWD", "Employee", "Monthly" });
            cmbDiscount.SelectedIndex = 0;
            py += 30;

            lblPayAmount = new System.Windows.Forms.Label { Text = "Pay Amount", Location = new System.Drawing.Point(8, py), AutoSize = true }; py += 18;
            txtPayAmount = new System.Windows.Forms.TextBox { Location = new System.Drawing.Point(8, py), Width = 175 }; py += 30;

            lblChange = new System.Windows.Forms.Label { Text = "Change", Location = new System.Drawing.Point(8, py), AutoSize = true }; py += 18;
            txtChange = new System.Windows.Forms.TextBox { Location = new System.Drawing.Point(8, py), Width = 175, ReadOnly = true, BackColor = System.Drawing.Color.LightYellow }; py += 30;

            txtReceipt = new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(8, py),
                Size = new System.Drawing.Size(175, 150),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = System.Windows.Forms.ScrollBars.Vertical,
                BackColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Courier New", 8f)
            };

            grpPayment.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblDiscount, cmbDiscount, lblPayAmount, txtPayAmount, lblChange, txtChange, txtReceipt });

            btnProcess = new System.Windows.Forms.Button
            {
                Text = "Process Payment",
                Location = new System.Drawing.Point(782, 420),
                Size = new System.Drawing.Size(195, 30),
                BackColor = System.Drawing.Color.FromArgb(0, 122, 204),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat
            };

            btnReceipt = new System.Windows.Forms.Button
            {
                Text = "Generate Receipt",
                Location = new System.Drawing.Point(782, 460),
                Size = new System.Drawing.Size(195, 30),
                BackColor = System.Drawing.Color.FromArgb(60, 140, 60),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat
            };

            btnClear = new System.Windows.Forms.Button
            {
                Text = "Clear Form",
                Location = new System.Drawing.Point(782, 500),
                Size = new System.Drawing.Size(195, 30),
                BackColor = System.Drawing.Color.FromArgb(180, 60, 60),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat
            };

            // Add all to form
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                grpRegistration, grpActions, grpParking,
                grpTransaction, grpFee, grpPayment,
                btnProcess, btnReceipt, btnClear
            });
        }

        private System.Windows.Forms.Label MakeLabel(string text, int x, int y, bool bold = false)
        {
            return new System.Windows.Forms.Label
            {
                Text = text,
                Location = new System.Drawing.Point(x, y),
                AutoSize = true,
                Font = bold ? new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold) : null
            };
        }

        // Controls
        private System.Windows.Forms.GroupBox grpRegistration = null!;
        private System.Windows.Forms.GroupBox grpActions = null!;
        private System.Windows.Forms.GroupBox grpParking = null!;
        private System.Windows.Forms.GroupBox grpTransaction = null!;
        private System.Windows.Forms.GroupBox grpFee = null!;
        private System.Windows.Forms.GroupBox grpPayment = null!;
        private System.Windows.Forms.Panel pnlSlots = null!;

        private System.Windows.Forms.Label lblPlate = null!, lblVehicleType = null!, lblHours = null!, lblSlot = null!;
        private System.Windows.Forms.TextBox txtPlate = null!, txtSlot = null!;
        private System.Windows.Forms.ComboBox cmbVehicleType = null!;
        private System.Windows.Forms.NumericUpDown numHours = null!;

        private System.Windows.Forms.Button btnRegister = null!, btnUpdate = null!;
        private System.Windows.Forms.Button btnProcess = null!, btnReceipt = null!, btnClear = null!;

        private System.Windows.Forms.Label lblTxPlateLabel = null!, lblTxPlate = null!;
        private System.Windows.Forms.Label lblTxInfoLabel = null!, lblTxInfo = null!;
        private System.Windows.Forms.Label lblTxDurLabel = null!, lblTxDur = null!;
        private System.Windows.Forms.Label lblTxSlotLabel = null!, lblTxSlot = null!;
        private System.Windows.Forms.Label lblTxOTLabel = null!, lblTxOT = null!;

        private System.Windows.Forms.Label lblFeeStdLabel = null!, lblFeeStd = null!;
        private System.Windows.Forms.Label lblFeeSvcLabel = null!, lblFeeSvc = null!;
        private System.Windows.Forms.Label lblFeeTotalLabel = null!, lblFeeTotal = null!;

        private System.Windows.Forms.Label lblDiscount = null!, lblPayAmount = null!, lblChange = null!;
        private System.Windows.Forms.ComboBox cmbDiscount = null!;
        private System.Windows.Forms.TextBox txtPayAmount = null!, txtChange = null!, txtReceipt = null!;
    }
}
