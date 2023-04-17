using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace MauiApp1;

public partial class ContentPageDemo : TabbedPage
{
	public ContentPageDemo()
	{
		InitializeComponent();
	}

 

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        DisplayAlert("Boton presionado", "Hola mundo!!", "Aceptar");
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        DisplayAlert("Boton presionado", $"Changed: {e.Value}", "Aceptar");
    }

    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        DisplayAlert("Buscando...", $"Buscando: {searchControl.Text}", "Aceptar");
    }

    private void SwipeItem_Invoked(object sender, EventArgs e)
    {
        DisplayAlert("SwipeView", $"Elemento seleccionado", "Aceptar");
    }

    private void slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        lblSlider.Text = slider.Value.ToString();
    }

    private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (stepper != null) {
            lblstepper.Text = stepper.Value.ToString();
        }
    }

    private void txtName_TextChanged(object sender, TextChangedEventArgs e)
    {
        Debug.WriteLine(txtName.Text);
    }

    private void txtName_Completed(object sender, EventArgs e)
    {
        DisplayAlert("Nombre completado", $"Su nombre: {txtName.Text}", "Aceptar");
    }
}