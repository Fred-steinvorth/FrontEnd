using LoginFlow.Views;
namespace LoginFlow;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        //Register all routes
        Routing.RegisterRoute("login", typeof(LoginPage));
        Routing.RegisterRoute("main", typeof(MainPage));
        Routing.RegisterRoute("home", typeof(HomePage));
        Routing.RegisterRoute("LogOut", typeof(LogOutPage));
        Routing.RegisterRoute("UserMaintenance", typeof(MaintenancePage));
        Routing.RegisterRoute("RecipeMaintenance", typeof(RecipeMaintenancePage));
        Routing.RegisterRoute("MyRecipes", typeof(MyRecipesPage));
    }
}
