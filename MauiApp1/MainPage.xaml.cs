namespace MauiApp1;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

    private void SwipeItem_Invoked(object sender, EventArgs e)
    {

    }

    private void OnCounterClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new ContentPageDemo());

	}
}

