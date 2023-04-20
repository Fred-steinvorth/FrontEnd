namespace LoginFlow.Views;

public partial class LogOutPage : ContentPage
{
	public LogOutPage()
	{
        InitializeComponent();
        String username = LoginPage.usuarioGlobal.username;
        TextDear.Text = "Dear " + username;
    }

	private async void LogoutButton_Clicked(object sender, EventArgs e)
	{
        
        
        if (await DisplayAlert("Are you sure?", "You will be logged out.", "Yes", "No"))
		{
			SecureStorage.RemoveAll();
			await Shell.Current.GoToAsync("///login");
		}
	}
}