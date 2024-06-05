using System;

public interface Descontavel
{
    void aplicarDesconto(double porcentagem);
}

public class Produto : Descontavel
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }

    public Produto(string nome, decimal preco, int quantidade)
    {
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
    }

    public virtual decimal ValorEmEstoque()
    {
        return Preco * Quantidade;
    }

    public void aplicarDesconto(double porcentagem)
    {
        if (porcentagem < 0 || porcentagem > 100)
        {
            throw new ArgumentException("A porcentagem de desconto deve estar entre 0 e 100.");
        }
        Preco -= Preco * (decimal)(porcentagem / 100);
    }
}

public class ProdutoPerecivel : Produto
{
    public DateTime DataDeValidade { get; set; }

    public ProdutoPerecivel(string nome, decimal preco, int quantidade, DateTime dataDeValidade)
        : base(nome, preco, quantidade)
    {
        DataDeValidade = dataDeValidade;
    }

    public override decimal ValorEmEstoque()
    {
        if (DataDeValidade <= DateTime.Today)
        {
            return base.ValorEmEstoque() * 0.8m;
        }
        else
        {
            return base.ValorEmEstoque();
        }
    }

    public new void aplicarDesconto(double porcentagem)
    {
        if (DataDeValidade <= DateTime.Today)
        {
            base.aplicarDesconto(porcentagem + 20);
        }
        else
        {
            base.aplicarDesconto(porcentagem);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Produto produto = new Produto("Notebook", 3000m, 10);
        ProdutoPerecivel produtoPerecivel = new ProdutoPerecivel("Leite", 3.5m, 10, new DateTime(2024, 6, 1));

        Console.WriteLine($"Preço original do produto: {produto.Preco}");
        produto.aplicarDesconto(10);
        Console.WriteLine($"Preço do produto após 10% de desconto: {produto.Preco}");

        Console.WriteLine($"Preço original do produto perecível: {produtoPerecivel.Preco}");
        produtoPerecivel.aplicarDesconto(10);
        Console.WriteLine($"Preço do produto perecível após 10% de desconto: {produtoPerecivel.Preco}");
    }
}