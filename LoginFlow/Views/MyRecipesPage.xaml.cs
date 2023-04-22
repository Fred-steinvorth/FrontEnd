using System;
using System.Collections.Generic;
using LoginFlow.Models;
using Microsoft.Maui.Controls;

namespace LoginFlow.Views
{
    public partial class MyRecipesPage : ContentPage
    {
        public List<AdminRecipes> MyRecipes { get; set; }

        public MyRecipesPage()
        {
            InitializeComponent();

            MyRecipes = new List<AdminRecipes>();

            var recipe1 = new AdminRecipes
            {
                Name = "Ensalada de tomate",
                Ingredients = "Tomates, cebolla, aceite de oliva, vinagre balsámico, sal, pimienta",
                Steps = "1. Cortar los tomates y la cebolla en rodajas\n2. Mezclar el aceite de oliva, el vinagre balsámico, la sal y la pimienta en un bol\n3. Colocar las rodajas de tomate y cebolla en un plato y rociar la mezcla de aceite y vinagre por encima"
            };

            MyRecipes.Add(recipe1);

            var recipe2 = new AdminRecipes
            {
                Name = "Arroz con pollo",
                Ingredients = "Arroz, pollo, cebolla, ajo, pimiento, guisantes, zanahoria, aceite, sal, pimienta, caldo de pollo",
                Steps = "1. Dorar el pollo en una olla con aceite\n2. Agregar la cebolla, ajo, pimiento, guisantes y zanahoria picados y saltear\n3. Agregar el arroz y saltear\n4. Agregar el caldo de pollo y dejar cocinar hasta que el arroz esté listo"
            };

            MyRecipes.Add(recipe2);

            ListView.ItemsSource = MyRecipes;

        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var selectedRecipe = (sender as Button).BindingContext as AdminRecipes;

            if (MyRecipes.Contains(selectedRecipe))
            {
                bool answer = await DisplayAlert("Confirm", "Are you sure you want to delete this user?", "Yes", "No");

                if (answer)
                {
                    MyRecipes.Remove(selectedRecipe);
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

            // Muestra cuadros de diálogo para editar los campos de la receta
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

            // Si el usuario no ingresó ningún valor en los cuadros de diálogo, salir del método
            if (updatedRecipe.Name == selectedRecipe.Name &&
                updatedRecipe.Ingredients == selectedRecipe.Ingredients &&
                updatedRecipe.Steps == selectedRecipe.Steps)
            {
                return;
            }

            // Busca el índice de la receta seleccionada en la lista
            var index = MyRecipes.IndexOf(selectedRecipe);

            if (index >= 0)
            {
                // Reemplaza la receta anterior con la actualizada
                MyRecipes[index] = updatedRecipe;

                // Actualiza la vista de la lista de recetas
                ListView.ItemsSource = null;
                ListView.ItemsSource = MyRecipes;
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
    }
}
