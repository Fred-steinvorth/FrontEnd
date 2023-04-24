using System;
using System.Collections.Generic;
using LoginFlow.Models;
using LoginFlow.Models.Request;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace LoginFlow.Views
{
    public partial class MyRecipesPage : ContentPage
    {
        public List<Receta> recetas { get; set; }
        HttpClient client;

        public MyRecipesPage()
        {
            client= new HttpClient();   
            InitializeComponent();
            LoadRecipeAsync();
        }

        public async void LoadRecipeAsync()
        {
            String url = "https://localhost:44353/api/receta/"+ LoginPage.usuarioGlobal.id;
            ReqObtenerTodasLasRecetas req = new ReqObtenerTodasLasRecetas();
            ResObtenerTodasLasRecetas res = new ResObtenerTodasLasRecetas();
            req.session = "12345";
            var responseApi = await client.GetStringAsync(url);
            res = JsonConvert.DeserializeObject<ResObtenerTodasLasRecetas>(responseApi);
            ListView.ItemsSource = res.listaDeRecetas;
            recetas = res.listaDeRecetas;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var selectedRecipe = (sender as Button).BindingContext as Receta;

            if (recetas.Contains(selectedRecipe))
            {
                bool answer = await DisplayAlert("Confirm", "Are you sure you want to delete this user?", "Yes", "No");

                if (answer)
                {
                    recetas.Remove(selectedRecipe);
                }
            }
        }
        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            // Obtiene la receta seleccionada de la lista
            var selectedRecipe = (sender as Button).BindingContext as Receta;

            // Crea un objeto AdminRecipes para almacenar la receta actualizada
            var updatedRecipe = new Receta
            {
                nombreReceta = selectedRecipe.nombreReceta,
                ingredientes = selectedRecipe.ingredientes,
                pasos = selectedRecipe.pasos
            };

            // Muestra cuadros de diálogo para editar los campos de la receta
            var name = await DisplayPromptAsync("Editar receta", "Nombre", placeholder: selectedRecipe.nombreReceta);
            if (!string.IsNullOrWhiteSpace(name))
            {
                updatedRecipe.nombreReceta = name;
            }

            List<Ingrediente> nuevaListaIngrediente = new List<Ingrediente>();
            foreach (Ingrediente ingrediente in selectedRecipe.ingredientes)
            {
                var ingredients = await DisplayPromptAsync("Editar receta", "Ingredientes", placeholder: ingrediente.ingrediente);
                if (!string.IsNullOrWhiteSpace(ingredients))
                {
                    ingrediente.ingrediente = ingredients;
                    nuevaListaIngrediente.Add(ingrediente);
                }
            }
            updatedRecipe.ingredientes = nuevaListaIngrediente;

            List<Paso> nuevaListaPaso =  new List<Paso>();  
            foreach (Paso paso in selectedRecipe.pasos)
            {
                var steps = await DisplayPromptAsync("Editar receta", "Pasos", placeholder: paso.paso);
                if (!string.IsNullOrWhiteSpace(steps))
                {
                    paso.paso = steps;
                    nuevaListaPaso.Add(paso);
                }
            }
            updatedRecipe.pasos = nuevaListaPaso;

            // Si el usuario no ingresó ningún valor en los cuadros de diálogo, salir del método
            if (updatedRecipe.nombreReceta == selectedRecipe.nombreReceta &&
                updatedRecipe.ingredientes == selectedRecipe.ingredientes &&
                updatedRecipe.pasos == selectedRecipe.pasos)
            {
                return;
            }

            // Busca el índice de la receta seleccionada en la lista
            var index = recetas.IndexOf(selectedRecipe);

            if (index >= 0)
            {
                // Reemplaza la receta anterior con la actualizada
                recetas[index] = updatedRecipe;

                // Actualiza la vista de la lista de recetas
                ListView.ItemsSource = null;
                ListView.ItemsSource = recetas;
            }
        }
        private void SeeButton_Clicked(object sender, EventArgs e)
        {
            // Obtenemos el objeto de la receta seleccionada
            var selectedRecipe = (sender as Button).BindingContext as Receta;

            String ingredientesConvatenados = null;
            foreach (Ingrediente ingrediente in selectedRecipe.ingredientes)
            {
                ingredientesConvatenados += " [" + ingrediente + "] ";
            }

            String pasoConcatenados = null;
            foreach (Paso paso in selectedRecipe.pasos)
            {
                pasoConcatenados += " [" + paso + "] ";
            }

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
                    new Label { Text = "Nombre Receta:", FontAttributes = FontAttributes.Bold },
                    new Label { Text = selectedRecipe.nombreReceta },
                    new Label { Text = "Ingredientes:", FontAttributes = FontAttributes.Bold },
                    new Label { Text = ingredientesConvatenados },
                    new Label { Text = "Pasos:", FontAttributes = FontAttributes.Bold },
                    new Label { Text = pasoConcatenados },
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
