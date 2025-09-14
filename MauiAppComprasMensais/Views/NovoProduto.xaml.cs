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
			if (string.IsNullOrWhiteSpace(txt_descricao.Text))
				throw new Exception("A descri��o n�o pode estar vazia");

			if (!double.TryParse(txt_quantidade.Text, out double quantidade))
                throw new Exception("Quantidade inv�lida");

			if (!double.TryParse(txt_preco.Text, out double preco))
                throw new Exception("Pre�o inv�lido");

            if (datePickerCompra.Date < new DateTime(2020, 1, 1))
                throw new Exception("Data de compra n�o pode ser anterior a 2021.");

            if (datePickerCompra.Date > DateTime.Today)
                throw new Exception("Data de compra n�o pode ser maior que data atual.");

			//cria novo objeto do tipo produto que ser�o preenchidos com dados nos campos de texto
            Produto p = new Produto
			{
				Descricao = txt_descricao.Text, //pega o texto da descri��o
				Quantidade = quantidade,
				Preco = preco,
                DataCadastro = datePickerCompra.Date // adiciona a data de compra
            };

			//Insere novo produto no banco de dados de forma as�ncrona
			await App.Db.Insert(p);
			await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
			await Navigation.PopAsync();

		} catch (Exception ex) //caso ocorra algum erro
		{
            System.Diagnostics.Debug.WriteLine($"Erro ao salvar: {ex}");
            await DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}