using System;

namespace LoginFlow.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.Quit();
            return true;
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (IsCredentialCorrect(Email.Text, Password.Text))
            {
                await SecureStorage.SetAsync("hasAuth", "true");

                if (Email.Text == "admin" && Password.Text == "1234")
                {
                    await Shell.Current.GoToAsync("//MaintenancePage");
                    await Shell.Current.GoToAsync("//RecipeMaintenancePage");
                }
                else
                {
                    await Shell.Current.GoToAsync("//home");
                }
            }
            else
            {
                await DisplayAlert("Login failed", "Username or password is invalid", "Try again");
            }
        }

            bool IsCredentialCorrect(string username, string password)
        {
            return username == "admin" && password == "1234";
        }
    }
}
