using LoginFlow.Models;
using LoginFlow.Models.Request;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace LoginFlow.Views;

public partial class LoginPage : ContentPage{

    HttpClient client;
    JsonSerializerOptions _jsonSerializerOptions;
    public static Usuario usuarioGlobal;
    public LoginPage()
    {
        usuarioGlobal = new Usuario();
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
        IsCredentialCorrect(Email.Text, Password.Text);
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
            String urlUsuario = "https://localhost:44353/api/user?email="+username+"&password="+password;
            ReqObtenerUsuario reqUser = new ReqObtenerUsuario();
            ResObtenerUsuario resUser = new ResObtenerUsuario();
            reqUser.session = "12345";
            reqUser.email = username;
            reqUser.password = encrypt(password);
            var responseApiUser = await client.GetStringAsync(urlUsuario);
            resUser = JsonConvert.DeserializeObject<ResObtenerUsuario>(responseApiUser);

            usuarioGlobal.id = resUser.usuario.id;
            usuarioGlobal.username = resUser.usuario.username;
            usuarioGlobal.status = resUser.usuario.status;
            usuarioGlobal.isAdmin = resUser.usuario.isAdmin;
            usuarioGlobal.email = username;

            await SecureStorage.SetAsync("hasAuth", "true");
            await Shell.Current.GoToAsync("///home");
        }
        else
        {
            await DisplayAlert("Login failed", "Uusername or password if invalid", "Try again");
        }

        
    }
    private string encrypt(string password)
    {
        if (password == null)
        {
            return null;
        }
        byte[] encryted = System.Text.Encoding.Unicode.GetBytes(password);
        return Convert.ToBase64String(encryted);
    }
}