namespace RentalAndMushroomApp
{
    /// <summary>
    /// Problem 1 - Bicycle rental.
    /// Rates per hour:
    ///   0-7  and 21-24 : 500 RWF
    ///   7-14 and 19-21 : 1000 RWF
    ///   14-19          : 1500 RWF
    /// Start is 0..23, end is 1..24, start &lt; end (no rental spans more than a day).
    /// </summary>
    public class BicycleForm : Form
    {
        private readonly NumericUpDown numStart = new NumericUpDown();
        private readonly NumericUpDown numEnd = new NumericUpDown();
        private readonly TextBox txtDetails = new TextBox();
        private readonly Label lblTotal = new Label();

        public BicycleForm()
        {
            Text = "Bicycle Rental";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            ClientSize = new Size(460, 470);
            Font = new Font("Segoe UI", 10F);

            var lblTitle = new Label
            {
                Text = "Bicycle Rental Calculator",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 15)
            };

            var lblRates = new Label
            {
                Text = "Rates:  0h-7h & 21h-24h = 500 RWF/h\n" +
                       "            7h-14h & 19h-21h = 1000 RWF/h\n" +
                       "            14h-19h = 1500 RWF/h",
                AutoSize = true,
                Location = new Point(20, 50)
            };

            var lblStart = new Label { Text = "Start hour (0-23):", AutoSize = true, Location = new Point(20, 125) };
            numStart.Location = new Point(170, 122);
            numStart.Width = 80;
            numStart.Minimum = 0;
            numStart.Maximum = 23;
            numStart.Value = 8;

            var lblEnd = new Label { Text = "End hour (1-24):", AutoSize = true, Location = new Point(20, 160) };
            numEnd.Location = new Point(170, 157);
            numEnd.Width = 80;
            numEnd.Minimum = 1;
            numEnd.Maximum = 24;
            numEnd.Value = 10;

            var btnCalc = new Button { Text = "Calculate", Location = new Point(280, 120), Size = new Size(150, 35) };
            btnCalc.Click += BtnCalc_Click;

            var btnClear = new Button { Text = "Clear", Location = new Point(280, 160), Size = new Size(150, 35) };
            btnClear.Click += (s, e) =>
            {
                numStart.Value = 0;
                numEnd.Value = 1;
                txtDetails.Clear();
                lblTotal.Text = "Total: -";
            };

            txtDetails.Location = new Point(20, 210);
            txtDetails.Size = new Size(410, 160);
            txtDetails.Multiline = true;
            txtDetails.ReadOnly = true;
            txtDetails.ScrollBars = ScrollBars.Vertical;
            txtDetails.Font = new Font("Consolas", 10F);

            lblTotal.Text = "Total: -";
            lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(20, 380);

            var btnLogout = new Button { Text = "Logout", Location = new Point(330, 420), Size = new Size(100, 35) };
            btnLogout.Click += (s, e) => Close(); // LoginForm re-appears on FormClosed

            AcceptButton = btnCalc;

            Controls.AddRange(new Control[]
            {
                lblTitle, lblRates, lblStart, numStart, lblEnd, numEnd,
                btnCalc, btnClear, txtDetails, lblTotal, btnLogout
            });
        }

        /// <summary>Rate in RWF for the one-hour slot that starts at the given hour.</summary>
        public static int RateForHour(int hour)
        {
            if (hour < 7 || hour >= 21) return 500;   // 0-7 and 21-24
            if (hour < 14 || hour >= 19) return 1000; // 7-14 and 19-21
            return 1500;                              // 14-19
        }

        /// <summary>Total cost for a rental from start to end (whole hours).</summary>
        public static int CalculateCost(int start, int end)
        {
            int total = 0;
            for (int h = start; h < end; h++)
                total += RateForHour(h);
            return total;
        }

        private void BtnCalc_Click(object sender, EventArgs e)
        {
            int start = (int)numStart.Value;
            int end = (int)numEnd.Value;

            if (start >= end)
            {
                MessageBox.Show("The start time must be less than the end time.",
                    "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var lines = new List<string>();
            int total = 0;
            for (int h = start; h < end; h++)
            {
                int rate = RateForHour(h);
                total += rate;
                lines.Add($"{h,2}h - {h + 1,2}h : {rate,5} RWF");
            }
            lines.Add(new string('-', 26));
            lines.Add($"Hours rented : {end - start}");

            txtDetails.Text = string.Join(Environment.NewLine, lines);
            lblTotal.Text = $"Total: {total:N0} RWF";
        }
    }
}
