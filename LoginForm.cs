namespace RentalAndMushroomApp
{
    /// <summary>
    /// Login form. No backend: credentials are hard-coded.
    /// Username: admin   Password: 1234
    /// After a successful login the user picks which form to open.
    /// </summary>
    public class LoginForm : Form
    {
        private const string ValidUser = "admin";
        private const string ValidPassword = "1234";

        private readonly TextBox txtUsername = new TextBox();
        private readonly TextBox txtPassword = new TextBox();
        private readonly RadioButton rbBicycle = new RadioButton();
        private readonly RadioButton rbMushroom = new RadioButton();
        private readonly Button btnLogin = new Button();
        private readonly Button btnExit = new Button();

        public LoginForm()
        {
            Text = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            ClientSize = new Size(360, 300);
            Font = new Font("Segoe UI", 10F);

            var lblTitle = new Label
            {
                Text = "Welcome - Please log in",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(70, 15)
            };

            var lblUser = new Label { Text = "Username:", AutoSize = true, Location = new Point(30, 65) };
            txtUsername.Location = new Point(130, 62);
            txtUsername.Width = 190;

            var lblPass = new Label { Text = "Password:", AutoSize = true, Location = new Point(30, 105) };
            txtPassword.Location = new Point(130, 102);
            txtPassword.Width = 190;
            txtPassword.UseSystemPasswordChar = true;

            var grpChoice = new GroupBox
            {
                Text = "Go to",
                Location = new Point(30, 140),
                Size = new Size(290, 75)
            };
            rbBicycle.Text = "Bicycle Rental";
            rbBicycle.AutoSize = true;
            rbBicycle.Location = new Point(15, 30);
            rbBicycle.Checked = true;
            rbMushroom.Text = "Mushroom Finder";
            rbMushroom.AutoSize = true;
            rbMushroom.Location = new Point(145, 30);
            grpChoice.Controls.Add(rbBicycle);
            grpChoice.Controls.Add(rbMushroom);

            btnLogin.Text = "Login";
            btnLogin.Location = new Point(70, 235);
            btnLogin.Size = new Size(100, 35);
            btnLogin.Click += BtnLogin_Click;

            btnExit.Text = "Exit";
            btnExit.Location = new Point(190, 235);
            btnExit.Size = new Size(100, 35);
            btnExit.Click += (s, e) => Close();

            AcceptButton = btnLogin;

            Controls.AddRange(new Control[]
            {
                lblTitle, lblUser, txtUsername, lblPass, txtPassword, grpChoice, btnLogin, btnExit
            });
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() != ValidUser || txtPassword.Text != ValidPassword)
            {
                MessageBox.Show("Invalid username or password.", "Login failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            Form next = rbBicycle.Checked ? new BicycleForm() : new MushroomForm();

            // When the next form closes (Logout or the X button), come back to login.
            next.FormClosed += (s, args) =>
            {
                txtPassword.Clear();
                Show();
                txtUsername.Focus();
            };

            Hide();
            next.Show();
        }
    }
}
