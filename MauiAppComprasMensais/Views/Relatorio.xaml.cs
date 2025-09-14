using MauiAppComprasMensais.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MauiAppComprasMensais.Views;

public partial class Relatorio : ContentPage
{
    ObservableCollection<Produto> resultados = new ObservableCollection<Produto>();
    public Relatorio()
    {
        InitializeComponent();
        dateInicio.Date = DateTime.Now.AddDays(-60); // últimos 60 dias
        dateFinal.Date = DateTime.Now; // data de hoje

        //Define a fonte de dados da CollectionView
        lst_relatorio.ItemsSource = resultados;
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        DateTime inicio = dateInicio.Date;
        DateTime final = dateFinal.Date;

        //Valida intervalo
        if (inicio > final)
        {
            await DisplayAlert("Ops", "Data de Início deve ser menor que a Data Final!", "OK");
            return;
        }

        if ((final - inicio).TotalDays > 365)
        {
            await DisplayAlert("Ops", "O intervalo deve ser até um ano. Tente novamente.", "Ok");
            return;
        }

        try
        {
            //Busca de todos produtos comprados 
            var todosProdutos = await App.Db.GetAll();
            //Fitro do período de compras
            var filtrados = todosProdutos
                .Where(p => p.DataCadastro != default &&
                    p.DataCadastro.Date >= inicio &&
                    p.DataCadastro.Date <= final)
                .ToList();

            resultados.Clear();
            filtrados.ForEach(p => resultados.Add(p));



            if (!filtrados.Any())
            {
                await DisplayAlert("Ops!", "Produtos não encontrados nesse período!", "OK!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro no relatório: {ex}");
            await DisplayAlert("Ops!", ex.Message, "OK");
        }
    }
}