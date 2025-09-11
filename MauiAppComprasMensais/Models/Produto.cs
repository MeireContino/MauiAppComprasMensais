 using SQLite;

namespace MauiAppComprasMensais.Models
{
    public class Produto
    {
        string _descricao;
        double _quantidade;
        double _preco;
        DateTime _dataCadastro = DateTime.Now;


        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Descricao 
        { 
            get => _descricao;
            set 
            {
                if (value == null) 
                {
                    throw new Exception("Por favor, preencha a descrição");
                }

                _descricao = value;
            }
        }
        public double Quantidade 
        {
            get => _quantidade; 
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Por favor, preencha a quantidade maior que zero");
                }

                _quantidade = value;
            }
        }
        public double Preco 
        {
            get => _preco; 
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Por favor, preencha um preço válido");
                }

                _preco = value;
            }
        }
        public double Total => Quantidade * Preco;

        public DateTime DataCadastro
        {
            get => _dataCadastro;
            set
            {
                if (value > DateTime.Now)
                {
                    throw new Exception("Data de compra não pode ser posterior a data atual");
                }

                _dataCadastro = value;
            }
        }  
    }
}
