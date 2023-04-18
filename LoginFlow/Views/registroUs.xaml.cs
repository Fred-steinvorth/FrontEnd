using CommunityToolkit.Maui.Views;
namespace LoginFlow.Views;

public partial class registroUs : Popup
{
	public registroUs()
	{
		InitializeComponent();


	}

    private void btnRegister_Clicked(System.Object sender, System.EventArgs e)
    {
        this.Close();
    }


    private void btnCancel_Clicked(System.Object sender, System.EventArgs e)
    {
        this.Close();
    }




    

}
