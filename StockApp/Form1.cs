using CefSharp;
using CefSharp.WinForms;
using StockApp.Helpers;
using StockApp.DataAccess;
using System.Configuration;

namespace StockApp;


public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        IsMdiContainer = true; // Définit ce formulaire comme un conteneur MDI
    }
    private void InitializeComponent()
    {
        this.Text = Var.AppName; // Definir le titre du formulaire parent
        this.WindowState = FormWindowState.Maximized; // Maximiser la fenetre MDI
        this.Icon = new Icon(Var.favicon); // Changer l'icone de l'application
        string htmlPath = Var.index;
        string fileUri = new Uri(htmlPath).AbsoluteUri; // Convertit le chemin en URI "file://"

        var browser = new ChromiumWebBrowser(fileUri);
        this.Controls.Add(browser);

        browser.JavascriptObjectRepository.NameConverter = new CefSharp.JavascriptBinding.CamelCaseJavascriptNameConverter();
        browser.JavascriptObjectRepository.Register("ProductHelper", new ProductHelper(), options: BindingOptions.DefaultBinder);
        browser.JavascriptObjectRepository.Register("UserHelper", new UserHelper(), options: BindingOptions.DefaultBinder);
        browser.JavascriptObjectRepository.Register("CategoryHelper", new CategoryHelper(), options: BindingOptions.DefaultBinder); 
        browser.JavascriptObjectRepository.Register("RoleHelper", new RoleHelper(), options: BindingOptions.DefaultBinder); 
        browser.JavascriptObjectRepository.Register("OperationHelper", new OperationHelper(), options: BindingOptions.DefaultBinder);

        //DBConnection.OpenConnection(); // Ouvrir la connexion à la base de données
    }
}
