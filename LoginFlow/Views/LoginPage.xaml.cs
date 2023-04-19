using LoginFlow.Models;
using LoginFlow.Models.Request;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace LoginFlow.Views;

public partial class LoginPage : ContentPage{

    HttpClient client;
    JsonSerializerOptions _jsonSerializerOptions;
    public LoginPage()
    {
        InitializeComponent();
        client = new HttpClient();
        _jsonSerializerOptions = new JsonSerializerOptions();
    }

    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        IsCredentialCorrect(Username.Text, Password.Text);
    }

    async void IsCredentialCorrect(string username, string password)
    {
        String url = "https://localhost:44353/api/login";
        ReqLoginUser req = new ReqLoginUser();
        req.user = new Usuario();

        req.user.email = username;
        req.user.password = password;
        req.session = "1234";

        String json = JsonConvert.SerializeObject(req);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        Console.WriteLine(content);
        ResLoginUser res = new ResLoginUser();

        var responseApi = await client.PostAsync(url, content);
        var respuestaNormal = await responseApi.Content.ReadAsStringAsync();
        res = System.Text.Json.JsonSerializer.Deserialize<ResLoginUser>(respuestaNormal);

        if (res.result)
        {
            await SecureStorage.SetAsync("hasAuth", "true");
            await Shell.Current.GoToAsync("///home");
        }
        else
        {
            await DisplayAlert("Login failed", "Uusername or password if invalid", "Try again");
        }
    }
}