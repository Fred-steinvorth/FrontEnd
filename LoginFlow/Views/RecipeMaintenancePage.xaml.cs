using System;
using System.Collections.Generic;
using LoginFlow.Models;
using Microsoft.Maui.Controls;

namespace LoginFlow.Views
{
    public partial class RecipeMaintenancePage : ContentPage
    {
        public List<Recipe> Recipes { get; set; }

        public RecipeMaintenancePage()
        {
            InitializeComponent();

            // Creamos la lista de recetas
            Recipes = new List<Recipe>();

            // Creamos una nueva receta y le asignamos valores a sus propiedades
            var recipe1 = new Recipe
            {
                Name = "Ensalada de tomate",
                Ingredients = "Tomates, cebolla, aceite de oliva, vinagre balsámico, sal, pimienta",
                Steps = "1. Cortar los tomates y la cebolla en rodajas\n2. Mezclar el aceite de oliva, el vinagre balsámico, la sal y la pimienta en un bol\n3. Colocar las rodajas de tomate y cebolla en un plato y rociar la mezcla de aceite y vinagre por encima"
            };

            // Agregamos la receta a la lista
            Recipes.Add(recipe1);

            // Agregamos más recetas de la misma forma
            var recipe2 = new Recipe
            {
                Name = "Arroz con pollo",
                Ingredients = "Arroz, pollo, cebolla, ajo, pimiento, guisantes, zanahoria, aceite, sal, pimienta, caldo de pollo",
                Steps = "1. Dorar el pollo en una olla con aceite\n2. Agregar la cebolla, ajo, pimiento, guisantes y zanahoria picados y saltear\n3. Agregar el arroz y saltear\n4. Agregar el caldo de pollo y dejar cocinar hasta que el arroz esté listo"
            };

            Recipes.Add(recipe2);

            // Asignamos la lista de recetas a la ListView mediante el atributo ItemsSource en XAML
            ListView.ItemsSource = Recipes;

        }
    }
}
