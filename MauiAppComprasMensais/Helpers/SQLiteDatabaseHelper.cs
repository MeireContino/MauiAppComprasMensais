//importa o namespace onde está contida a classe Produto
using MauiAppComprasMensais.Models;
//importa a biblioteca SQLite para manipular o banco de dados
using SQLite;
//Define onde a classe está localizda 
namespace MauiAppComprasMensais.Helpers
{
    //Declara qual é a classe responsável que irá interagir com o banco d dados
    public class SQLiteDatabaseHelper
    {
        //Cria uma conexão assíncrona com o banco de dados que será usada para acessar e manipular os dados.
        readonly SQLiteAsyncConnection _conn;

        //cria uma classe que vai cuidar da comunicação entre o aplicativo e o banco de dados
        public SQLiteDatabaseHelper(string path)
        {
            //para abrir uma conexão com o banco usando o caminho que for informado
            _conn = new SQLiteAsyncConnection(path);
            //cria uma tabela Produto no banco de dados, caso ela ainda não exista
            Task.Run(async () => await _conn.CreateTableAsync<Produto>()).Wait();
        }

        //cria um método para inserir novo produto ao banco de dados
        public Task<int> Insert(Produto p)
        {
            //para adicionar um produto ao banco sem travar o aplicativo  
            return _conn.InsertAsync(p);
        }
        //cria o método de atualização para um produto já cadastrado
        public async Task<int> Update(Produto p)

        {
            //Define um comando que altera (atualiza) dados de um produto com base no id informado
            string sql = "UPDATE Produto SET Descricao = ?, Quantidade = ?, Preco = ? WHERE Id = ?";

            //executa a atualização usando os dados do produto que foi informado
            return await _conn.ExecuteAsync(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }
        //cria um metodo que remove um produto do banco usando um id p.Preco, p.Id);
        public Task<int> Delete(int id)
        {
            //usando a expressão lambda, exclui o produto que tiver o ID correspodente
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }
        //cria um método que busca todos os produtos cadastrado
        public async Task<List<Produto>> GetAll()
        {
            try
            {
                //Retorna os produtos do banco em forma de lista
                return await _conn.Table<Produto>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar produtos: {ex.Message}");
                return new List<Produto>();
            }
        }

        //método que procura produtos usando a palavra chave
        public Task<List<Produto>> Search(string q)
        {
            //Define o comando SQL para fazer a busca dos produto que tiverem a palavra informada
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE ?";

            //Faz a busca e retorna com os produtos encontrados
            return _conn.QueryAsync<Produto>(sql, $"%{q}%");
        }
    }
}
