using ProjBiblioteca;

var acervo = new Livros();
int opcao;

do
{
    ExibirMenu();
    opcao = LerInt("Opção: ");
    Console.WriteLine();

    switch (opcao)
    {
        case 0: Console.WriteLine("Fechando..."); break;
        case 1: AdicionarLivro(); break;
        case 2: PesquisarLivro(analitico: false); break;
        case 3: PesquisarLivro(analitico: true); break;
        case 4: AdicionarExemplar(); break;
        case 5: RegistrarEmprestimo(); break;
        case 6: RegistrarDevolucao(); break;
        default: Console.WriteLine("Opção invalida."); break;
    }

    if (opcao != 0)
    {
        Console.WriteLine("\nPressione 'enter' para continuar...");
        Console.ReadLine();
    }
} while (opcao != 0);

void ExibirMenu()
{
    Console.Clear();
    Console.WriteLine("=== BIBLIOTECA ===");
    Console.WriteLine("0. Sair");
    Console.WriteLine("1. Adicionar livro");
    Console.WriteLine("2. Pesquisar livro (sintético)");
    Console.WriteLine("3. Pesquisar livro (analítico)");
    Console.WriteLine("4. Adicionar exemplar");
    Console.WriteLine("5. Registrar empréstimo");
    Console.WriteLine("6. Registrar devolução");
    Console.WriteLine();
}

void AdicionarLivro()
{
    int isbn = LerInt("ISBN: ");

    if (acervo.Pesquisar(new Livro(isbn)) != null)
    {
        Console.WriteLine("Já existe um livro cadastrado com esse ISBN.");
        return;
    }

    string titulo = LerTexto("Título: ");
    string autor = LerTexto("Autor: ");
    string editora = LerTexto("Editora: ");

    acervo.Adicionar(new Livro(isbn, titulo, autor, editora));
    Console.WriteLine("Livro adicionado.");
}

void PesquisarLivro(bool analitico)
{
    Livro? livro = BuscarLivro();
    if (livro == null) return;

    ExibirSintetico(livro);
    if (analitico)
        ExibirAnalitico(livro);
}

void AdicionarExemplar()
{
    Livro? livro = BuscarLivro();
    if (livro == null) return;

    int tombo = LerInt("Tombo do exemplar: ");

    if (acervo.PesquisarExemplar(tombo) != null)
    {
        Console.WriteLine("Já existe um exemplar com esse tombo.");
        return;
    }

    livro.AdicionarExemplar(new Exemplar(tombo));
    Console.WriteLine("Exemplar adicionado.");
}

void RegistrarEmprestimo()
{
    Exemplar? exemplar = BuscarExemplarDoLivro();
    if (exemplar == null) return;

    if (exemplar.Emprestar())
        Console.WriteLine("Empréstimo registrado.");
    else
        Console.WriteLine("Exemplar indisponível (já está emprestado).");
}

void RegistrarDevolucao()
{
    Exemplar? exemplar = BuscarExemplarDoLivro();
    if (exemplar == null) return;

    if (exemplar.Devolver())
        Console.WriteLine("Devolução registrada com sucesso.");
    else
        Console.WriteLine("Não há empréstimo em aberto para este exemplar.");
}

void ExibirSintetico(Livro livro)
{
    Console.WriteLine($"ISBN:                 {livro.Isbn}");
    Console.WriteLine($"Título:               {livro.Titulo}");
    Console.WriteLine($"Autor:                {livro.Autor}");
    Console.WriteLine($"Editora:              {livro.Editora}");
    Console.WriteLine($"Total de exemplares:  {livro.QtdeExemplares()}");
    Console.WriteLine($"Disponíveis:          {livro.QtdeDisponiveis()}");
    Console.WriteLine($"Total de empréstimos: {livro.QtdeEmprestimos()}");
    Console.WriteLine($"% de disponibilidade: {livro.PercDisponibilidade():F1}%");
}

void ExibirAnalitico(Livro livro)
{
    Console.WriteLine("\n Exemplares ");

    if (livro.QtdeExemplares() == 0)
    {
        Console.WriteLine("(nenhum exemplar cadastrado)");
        return;
    }

    foreach (Exemplar e in livro.Exemplares)
    {
        string situacao = e.Disponivel() ? "Disponivel" : "Emprestado";
        Console.WriteLine($"\nTombo {e.Tombo} - {situacao} - {e.QtdeEmprestimos()} empréstimo(s)");

        foreach (Emprestimo emp in e.Emprestimos)
        {
            string devolucao = emp.DtDevolucao.HasValue
                ? emp.DtDevolucao.Value.ToString("dd/MM/yyyy HH:mm")
                : "não devolvido";

            Console.WriteLine($"Empréstimo: {emp.DtEmprestimo:dd/MM/yyyy HH:mm}  /  Devolução: {devolucao}");
        }
    }
}

Livro? BuscarLivro()
{
    int isbn = LerInt("ISBN do livro: ");
    Livro? livro = acervo.Pesquisar(new Livro(isbn));

    if (livro == null)
        Console.WriteLine("Livro não encontrado.");

    return livro;
}

Exemplar? BuscarExemplarDoLivro()
{
    Livro? livro = BuscarLivro();
    if (livro == null) return null;

    int tombo = LerInt("Tombo do exemplar: ");
    Exemplar? exemplar = livro.Exemplares.FirstOrDefault(e => e.Tombo == tombo);

    if (exemplar == null)
        Console.WriteLine("Exemplar não encontrado para este livro.");

    return exemplar;
}

int LerInt(string mensagem)
{
    while (true)
    {
        Console.Write(mensagem);
        if (int.TryParse(Console.ReadLine(), out int valor))
            return valor;

        Console.WriteLine("Valor invalido. Digite um número inteiro.");
    }
}

string LerTexto(string mensagem)
{
    while (true)
    {
        Console.Write(mensagem);
        string? texto = Console.ReadLine();
        if (!string.IsNullOrEmpty(texto))
            return texto.Trim();

        Console.WriteLine("Campo obrigatório.");
    }
}