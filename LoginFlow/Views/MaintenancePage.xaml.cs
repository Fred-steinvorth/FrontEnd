using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using LoginFlow.Models;
using LoginFlow.Models.Request;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace LoginFlow.Views
{
    public partial class MaintenancePage : ContentPage
    {

        HttpClient client;
       
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
        }
    }
}

