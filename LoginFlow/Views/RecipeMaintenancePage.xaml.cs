using LoginFlow.Models.Request;
using Newtonsoft.Json;

namespace LoginFlow.Views
{
    public partial class RecipeMaintenancePage : ContentPage
    {
        HttpClient client;
        public RecipeMaintenancePage()
        {
            client = new HttpClient();
            InitializeComponent();
            LoadRecipeAsync();
        }

        public async void LoadRecipeAsync()
        {
            String url = "https://localhost:44353/api/receta";
            ReqObtenerTodasLasRecetas req = new ReqObtenerTodasLasRecetas();
            ResObtenerTodasLasRecetas res = new ResObtenerTodasLasRecetas();
            req.session = "12345";
            var responseApi = await client.GetStringAsync(url);
            res = JsonConvert.DeserializeObject<ResObtenerTodasLasRecetas>(responseApi);
            ListView.ItemsSource = res.listaDeRecetas;
        }
    }
}
