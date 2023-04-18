using System;
using System.Collections.Generic;
using LoginFlow.Models;
using Microsoft.Maui.Controls;

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
                    Email = "A@A.COM",
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
        }
    }

