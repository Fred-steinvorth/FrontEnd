using Google.Apis.Admin.Directory.directory_v1.Data;
using LoginFlow.Models;
using LoginFlow.Models.Request;
using Newtonsoft.Json;

namespace LoginFlow.Views
{
    public partial class MaintenancePage : ContentPage
    {

        HttpClient client;
        List<Usuario> usuarios;
       
        public MaintenancePage()
        {
            InitializeComponent();
            client = new HttpClient();
            LoadUsersAsync();

        }

        public async void LoadUsersAsync()
        {
            String url = "https://localhost:44353/api/user";
            ReqObtenerTodosLosUsuarios req = new ReqObtenerTodosLosUsuarios();
            ResObtenerTodosLosUsuarios res = new ResObtenerTodosLosUsuarios();
            req.session = "12345";
            var responseApi = await client.GetStringAsync(url);
            res = JsonConvert.DeserializeObject<ResObtenerTodosLosUsuarios>(responseApi);
            ListView.ItemsSource = res.listaDeUsuarios;
            usuarios = res.listaDeUsuarios;
        }
        private void ViewButton_Clicked(object sender, EventArgs e)
        {
            // Obtiene el usuario seleccionado de la lista
            var selectedUser = (sender as Button).BindingContext as Usuario;

            // Creamos el frame que contendrá la información del usuario
            var frame = new Frame
            {
                BorderColor = Colors.Black,
                CornerRadius = 10,
                Padding = new Thickness(10),
                Margin = new Thickness(20, 40, 20, 0),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 300,
                HasShadow = true,
                Content = new Microsoft.Maui.Controls.StackLayout
                {
                    Spacing = 10,
                    Children =
                {
                    new Label { Text = "Name:", FontAttributes = FontAttributes.Bold },
                    new Label { Text = selectedUser.username },
                    new Label { Text = "Email:", FontAttributes = FontAttributes.Bold },
                    new Label { Text = selectedUser.email },
                    new Label { Text = "Password:", FontAttributes = FontAttributes.Bold },
                    new Label { Text = selectedUser.password},
                }
                }
            };

            var stackLayout2 = new Microsoft.Maui.Controls.StackLayout
            {
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center,
                Children =
            {
                new Button
                {
                    Text = "Back to user Maintenance",
                    Command = new Command(() =>
                    {
                        Navigation.PopModalAsync();
                    })
                }
            }
            };

            // Creamos el StackLayout principal que contendrá ambos StackLayouts
            var mainStackLayout = new Microsoft.Maui.Controls.StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Spacing = 10,
                Margin = new Thickness(0, 20, 0, 0),
                Children =
            {
                frame,
                stackLayout2
            }
            };

            // Mostramos el StackLayout principal como una página modal
            Navigation.PushModalAsync(new ContentPage
            {
                BackgroundImageSource = "seebutton.jpg",
                Content = mainStackLayout
            });
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            //Obtiene un elemento seleccionado de la lista
            var selectedItem = (sender as Button).BindingContext as Usuario;

            // Muestra un cuadro de diálogo para editar el usuario
            var name = await DisplayPromptAsync("Editar usuario", "Nombre", placeholder: selectedItem.username);
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var email = await DisplayPromptAsync("Editar usuario", "Email", placeholder: selectedItem.email);
            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }

            var password = await DisplayPromptAsync("Editar usuario", "Contraseña", placeholder: selectedItem.password);
            if (string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            // Actualiza la información del usuario seleccionado
            selectedItem.username = name;
            selectedItem.email = email;
            selectedItem.password = password;

            // Actualiza la vista si se realizaron cambios
            ListView.ItemsSource = null;
            ListView.ItemsSource = usuarios;
        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var selectedItem = (sender as Button).BindingContext as Usuario;

            if (usuarios.Contains(selectedItem))
            {
                bool answer = await DisplayAlert("Confirm", "Are you sure you want to delete this user?", "Yes", "No");

                if (answer)
                {
                    usuarios.Remove(selectedItem);
                }
            }
        }


    }
}


