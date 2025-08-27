//importa o namespace onde está a classe que ajuda a lidar com o banco de dados SQLite
using MauiAppComprasMensais.Helpers;

namespace MauiAppComprasMensais

{   //classe principal do aplicativo, que herda de Application
    public partial class App : Application
    {
        //Declara uma variável estática para o acesso ao banco de dados campo '_db'
        static SQLiteDatabaseHelper _db;

        //propriedade pública que retorna a instância do banco de dados propriedade 'Db'
        public static SQLiteDatabaseHelper Db
        {
            //método get - por ser uma propriedade somente de leitura
            get 
            {
                //Define o caminho do arquivo do banco de dados dentro da pasta local do aplicativo
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3");

                    //   cria a instância do helper do banco com o caminho definido            
                    _db = new SQLiteDatabaseHelper(path);
                }
                //Se _db == for nulo, cria e retorna a instância 
                return _db;
            }
        }
        public App()
        {
            
            InitializeComponent();

            //MainPage = new AppShell(); Pode usar o AppShell
            //define a página inicial como NavigationPage carregando a tela de lista de produtos
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
