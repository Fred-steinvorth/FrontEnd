using System;
using System.Collections.Generic;
using Microsoft.Maui.Graphics;
using LoginFlow.Models;
using Microsoft.Maui.Controls.Compatibility;

namespace LoginFlow.Views
{
    public partial class MaintenancePage : ContentPage
    {
        public List<User> Users { get; set; }

        public MaintenancePage()
        {
            InitializeComponent();

            // Creamos la lista de usuarios
            Users = new List<User>();

            // Creamos un nuevo usuario y le asignamos valores a sus propiedades
            var user1 = new User
            {
                Name = "Kimberley Rojas Alfaro",
                Email = "c",
                Password = "12345"
            };

            // Agregamos el usuario a la lista
            Users.Add(user1);

            var user2 = new User
            {
                Name = "Fred Steinvorth",
                Email = "b@A.COM",
                Password = "12345"
            };

            // Agregamos el usuario a la lista
            Users.Add(user2);

            // Asignamos la lista de usuarios a la ListView mediante el atributo ItemsSource en XAML
            ListView.ItemsSource = Users;
        }
        private void ViewButton_Clicked(object sender, EventArgs e)
        {
            // Obtiene el usuario seleccionado de la lista
            var selectedUser = (sender as Button).BindingContext as User;

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
                    new Label { Text = selectedUser.Name },
                    new Label { Text = "Email:", FontAttributes = FontAttributes.Bold },
                    new Label { Text = selectedUser.Email },
                    new Label { Text = "Password:", FontAttributes = FontAttributes.Bold },
                    new Label { Text = selectedUser.Password},
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
            var selectedItem = (sender as Button).BindingContext as User;

            // Muestra un cuadro de diálogo para editar el usuario
            var name = await DisplayPromptAsync("Editar usuario", "Nombre", placeholder: selectedItem.Name);
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var email = await DisplayPromptAsync("Editar usuario", "Email", placeholder: selectedItem.Email);
            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }

            var password = await DisplayPromptAsync("Editar usuario", "Contraseña", placeholder: selectedItem.Password);
            if (string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            // Actualiza la información del usuario seleccionado
            selectedItem.Name = name;
            selectedItem.Email = email;
            selectedItem.Password = password;

            // Actualiza la vista si se realizaron cambios
            ListView.ItemsSource = null;
            ListView.ItemsSource = Users;
        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var selectedItem = (sender as Button).BindingContext as User;

            if (Users.Contains(selectedItem))
            {
                bool answer = await DisplayAlert("Confirm", "Are you sure you want to delete this user?", "Yes", "No");

                if (answer)
                {
                    Users.Remove(selectedItem);
                }
            }
        }
    }
}
