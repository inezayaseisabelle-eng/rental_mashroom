namespace RentalAndMushroomApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // The login form is the main form: closing it exits the app.
            Application.Run(new LoginForm());
        }
    }
}
