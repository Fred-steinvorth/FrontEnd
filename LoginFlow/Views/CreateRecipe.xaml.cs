namespace LoginFlow.Views;

public partial class CreateRecipe : ContentPage
{
    public CreateRecipe()
    {
        InitializeComponent();
    }

    private async void OnChooseImageClicked(object sender, EventArgs e)
    {
        // Implementa la lógica para seleccionar una imagen
        // y mostrarla en la vista previa de imagen
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Implementa la lógica para registrar al usuario
        // utilizando los valores de los campos de entrada
        // y la imagen seleccionada (si corresponde)
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}