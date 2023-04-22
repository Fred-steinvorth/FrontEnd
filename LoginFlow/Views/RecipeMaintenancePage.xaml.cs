using System;
using System.Collections.Generic;
using LoginFlow.Models;
using Microsoft.Maui.Controls;

namespace LoginFlow.Views
{
    public partial class RecipeMaintenancePage : ContentPage
    {
        public List<AdminRecipes> Recipes { get; set; }

        public RecipeMaintenancePage()
        {
            InitializeComponent();

            // Creamos la lista de recetas
            Recipes = new List<AdminRecipes>();

            // Creamos una nueva receta y le asignamos valores a sus propiedades
            var recipe1 = new AdminRecipes
            {
                Name = "Ensalada de Palmito",
                Ingredients = "Palmito, cebolla, aceite de oliva, vinagre bals�mico, sal, pimienta",
                Steps = "1. Cortar los Palmitos y la cebolla en rodajas\n2. Mezclar el aceite de oliva, el vinagre bals�mico, la sal y la pimienta en un bol\n3. Colocar las rodajas de tomate y cebolla en un plato y rociar la mezcla de aceite y vinagre por encima"
            };

            // Agregamos la receta a la lista
            Recipes.Add(recipe1);

            // Agregamos m�s recetas de la misma forma
            var recipe2 = new AdminRecipes
            {
                Name = "Arroz con Calamares",
                Ingredients = "Arroz, Calamares, cebolla, ajo, pimiento, guisantes, zanahoria, aceite, sal, pimienta, caldo de pollo",
                Steps = "1. Dorar el Calamares en una olla con aceite\n2. Agregar la cebolla, ajo, pimiento, guisantes y zanahoria picados y saltear\n3. Agregar el arroz y saltear\n4. Agregar el caldo de pollo y dejar cocinar hasta que el arroz est� listo"
            };

            Recipes.Add(recipe2);

            // Asignamos la lista de recetas a la ListView mediante el atributo ItemsSource en XAML
            ListView.ItemsSource = Recipes;

        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var selectedRecipe = (sender as Button).BindingContext as AdminRecipes;

            if (Recipes.Contains(selectedRecipe))
            {
                bool answer = await DisplayAlert("Confirm", "Are you sure you want to delete this user?", "Yes", "No");

                if (answer)
                {
                    Recipes.Remove(selectedRecipe);
                }
            }
        }
        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            // Obtiene la receta seleccionada de la lista
            var selectedRecipe = (sender as Button).BindingContext as AdminRecipes;

            // Crea un objeto AdminRecipes para almacenar la receta actualizada
            var updatedRecipe = new AdminRecipes
            {
                Name = selectedRecipe.Name,
                Ingredients = selectedRecipe.Ingredients,
                Steps = selectedRecipe.Steps
            };

            // Muestra cuadros de di�logo para editar los campos de la receta
            var name = await DisplayPromptAsync("Editar receta", "Nombre", placeholder: selectedRecipe.Name);
            if (!string.IsNullOrWhiteSpace(name))
            {
                updatedRecipe.Name = name;
            }

            var ingredients = await DisplayPromptAsync("Editar receta", "Ingredientes", placeholder: selectedRecipe.Ingredients);
            if (!string.IsNullOrWhiteSpace(ingredients))
            {
                updatedRecipe.Ingredients = ingredients;
            }

            var steps = await DisplayPromptAsync("Editar receta", "Pasos", placeholder: selectedRecipe.Steps);
            if (!string.IsNullOrWhiteSpace(steps))
            {
                updatedRecipe.Steps = steps;
            }

            // Si el usuario no ingres� ning�n valor en los cuadros de di�logo, salir del m�todo
            if (updatedRecipe.Name == selectedRecipe.Name &&
                updatedRecipe.Ingredients == selectedRecipe.Ingredients &&
                updatedRecipe.Steps == selectedRecipe.Steps)
            {
                return;
            }

            // Busca el �ndice de la receta seleccionada en la lista
            var index = Recipes.IndexOf(selectedRecipe);

            if (index >= 0)
            {
                // Reemplaza la receta anterior con la actualizada
                Recipes[index] = updatedRecipe;

                // Actualiza la vista de la lista de recetas
                ListView.ItemsSource = null;
                ListView.ItemsSource = Recipes;
            }
        }
        private void SeeButton_Clicked(object sender, EventArgs e)
        {
            // Obtenemos el objeto de la receta seleccionada
            var selectedRecipe = (sender as Button).BindingContext as AdminRecipes;

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
                    new Label { Text = selectedRecipe.Name },
                    new Label { Text = "Email:", FontAttributes = FontAttributes.Bold },
                    new Label { Text = selectedRecipe.Ingredients },
                    new Label { Text = "Password:", FontAttributes = FontAttributes.Bold },
                    new Label { Text = selectedRecipe.Steps},
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
                    Text = "Back to recipe Maintenance",
                    Command = new Command(() =>
                    {
                        Navigation.PopModalAsync();
                    })
                }
            }
            };

            // Creamos el StackLayout principal que contendr� ambos StackLayouts
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

            // Mostramos el StackLayout principal como una p�gina modal
            Navigation.PushModalAsync(new ContentPage
            {
                BackgroundImageSource = "seebutton.jpg",
                Content = mainStackLayout
            });
        }
    }
}

