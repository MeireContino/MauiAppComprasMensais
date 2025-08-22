//Para importar o namespace que cont�m o modelo 'Produto'
using MauiAppComprasMensais.Models;

//Definir o namespace onde est� a classe tela de novo produto
namespace MauiAppComprasMensais.Views;

//declara a classe 'NovoProduto', que representa uma p�gina da interface
public partial class NovoProduto : ContentPage
{
	//Construtor da classe, acionado quando a p�gina � criada
	public NovoProduto()
	{

		//inicializa componentes visuais definidos no XAML
		InitializeComponent();
	}

	//M�todo assincrono executado quando o toolbar for clicado
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			//cria novo objeto do tipo produto que ser�o preenchidos com dados nos campos de texto
			Produto p = new Produto
			{
				Descricao = txt_descricao.Text, //pega o texto da descri��o
				Quantidade = Convert.ToDouble(txt_quantidade.Text), //converte texto para n�mero
				Preco = Convert.ToDouble(txt_preco.Text), //converte texto pre�o para n�mero
			};

			//Insere novo produto no banco de dados de forma as�ncrona
			await App.Db.Insert(p);
			await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

		} catch (Exception ex) //caso ocorra algum erro
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}