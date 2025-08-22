//Para importar o namespace que contém o modelo 'Produto'
using MauiAppComprasMensais.Models;

//Definir o namespace onde está a classe tela de novo produto
namespace MauiAppComprasMensais.Views;

//declara a classe 'NovoProduto', que representa uma página da interface
public partial class NovoProduto : ContentPage
{
	//Construtor da classe, acionado quando a página é criada
	public NovoProduto()
	{

		//inicializa componentes visuais definidos no XAML
		InitializeComponent();
	}

	//Método assincrono executado quando o toolbar for clicado
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			//cria novo objeto do tipo produto que serão preenchidos com dados nos campos de texto
			Produto p = new Produto
			{
				Descricao = txt_descricao.Text, //pega o texto da descrição
				Quantidade = Convert.ToDouble(txt_quantidade.Text), //converte texto para número
				Preco = Convert.ToDouble(txt_preco.Text), //converte texto preço para número
			};

			//Insere novo produto no banco de dados de forma asíncrona
			await App.Db.Insert(p);
			await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

		} catch (Exception ex) //caso ocorra algum erro
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}