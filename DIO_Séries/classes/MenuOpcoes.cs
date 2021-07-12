using System;
using System.Collections.Generic;
using System.Text;

namespace DIO_Séries
{
    class MenuOpcoes
    {
        static SerieRepositorio repositorio = new SerieRepositorio();

        static internal void DisplayMenu()
        {
            String opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Digite a opção válida correspondente.");
                        break;
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }

        static internal void ExcluirSerie()
        {
            // Não executa se o repositório de séries estiver vazio.
            if (repositorio.Lista().Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            // Tenta obter o ID da série até o usuário indicar um ID dentro dos valores válidos.
            while (1 == 1)
            {
                try
                {
                    Console.Write("Digite o ID da série (0 - {0}): ", repositorio.ProximoId() - 1);
                    int indiceSerie = int.Parse(Console.ReadLine());
                    Serie escolhida = repositorio.Lista()[indiceSerie];

                    // Tenta obter confirmação do usuário até que seja inserida uma opção válida.
                    while (1 == 1)
                    {
                        try
                        {
                            Console.WriteLine("Tem certeza que deseja excluir essa série do catálogo? (S/N)");
                            String confirma = Console.ReadLine().ToUpper();

                            if (confirma == "S")
                            {
                                repositorio.Exclui(indiceSerie);
                                Console.WriteLine("\nA série {0} foi excluída.", escolhida.RetornaTitulo());
                                return;
                            }
                            else if (confirma == "N")
                                return;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Valor inválido. A entrada deve ser uma das opções indicadas.\n");
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Valor inválido. A entrada deve ser um número dentro dos valores indicados.\n");
                }
            }
        }
            

        static internal void VisualizarSerie()
        {
            int indiceSerie;

            // Tenta obter o ID da série até o usuário indicar um ID dentro dos valores válidos.
            while (1 == 1)
            {
                try
                {
                    Console.Write("Digite o ID da série (0 - {0}): ", repositorio.ProximoId() - 1);
                    indiceSerie = int.Parse(Console.ReadLine());
                    Serie escolhida = repositorio.Lista()[indiceSerie];
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Valor inválido. A entrada deve ser um número dentro dos valores indicados.\n");
                }
            }

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
        }

        static internal void AtualizarSerie()
        {
            int indiceSerie;
            int genreQty = 0;
            int entradaAno;
            Genero genEscolhido;

            // Tenta obter o ID da série até o usuário indicar um ID dentro dos valores válidos.
            while (1 == 1)
            {
                try
                {
                    Console.Write("Digite o ID da série (0 - {0}): ", repositorio.ProximoId() - 1);
                    indiceSerie = int.Parse(Console.ReadLine());
                    Serie escolhida = repositorio.Lista()[indiceSerie];
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Valor inválido. A entrada deve ser um número dentro dos valores indicados.\n");
                }
            }

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
                genreQty++;
            }

            Console.WriteLine();

            // Tenta obter um gênero até que o usuário informe um valor válido.
            while (1 == 1)
            {
                try
                {
                    Console.Write("Digite o gênero entre as opções acima (1 - {0}): ", genreQty);
                    int entradaGenero = int.Parse(Console.ReadLine());

                    // Verifica se o número informado está dentro das opções disponíveis do Enum Genero.
                    if (entradaGenero < 0 || entradaGenero > genreQty)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        genEscolhido = (Genero)entradaGenero;
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Valor inválido. A entrada deve ser um número dentro dos valores indicados.\n");
                }
            }

            Console.Write("Digite o título da série: ");
            String entradaTitulo = Console.ReadLine();

            // Tentar obter um número até que o usuário informa uma entrada válida.
            while (1 == 1)
            {
                try
                {
                    Console.Write("Digite o ano de início da série: ");
                    entradaAno = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Valor inválido. A entrada deve ser um número.\n");
                }
            }

            Console.Write("Digite a descrição da série: ");
            String entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: indiceSerie,
                                        genero: genEscolhido,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Atualiza(indiceSerie, atualizaSerie);
        }

        static internal void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.RetornaExcluido();
                Console.WriteLine("#ID {0}: - {1}  {2}", serie.RetornaId(), serie.RetornaTitulo(), (excluido ? "*Excluído*" : ""));
            }
        }

        static internal void InserirSerie()
        {
            Genero genEscolhido;
            int entradaAno;
            int genreQty = 0;

            Console.WriteLine("Inserir nova série\n");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
                genreQty++;
            }

            Console.WriteLine();

            // Tenta obter um gênero até que o usuário informe um valor válido.
            while (1 == 1)
            {
                try
                {
                    Console.Write("Digite o gênero entre as opções acima (1 - {0}): ", genreQty);
                    int entradaGenero = int.Parse(Console.ReadLine());

                    // Verifica se o número informado está dentro das opções disponíveis do Enum Genero.
                    if (entradaGenero < 0 || entradaGenero > genreQty)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        genEscolhido = (Genero)entradaGenero;
                        break;
                    }                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Valor inválido. A entrada deve ser um número dentro dos valores indicados.\n");
                }
            }            

            Console.Write("Digite o título da série: ");
            String entradaTitulo = Console.ReadLine();

            // Tentar obter um número até que o usuário informa uma entrada válida.
            while (1 == 1)
            {
                try
                {
                    Console.Write("Digite o ano de início da série: ");
                    entradaAno = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Valor inválido. A entrada deve ser um número.\n");
                }
            }            

            Console.Write("Digite a descrição da série: ");
            String entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: genEscolhido,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
        }

        static internal string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Séries a seu dispor!");
            Console.WriteLine("Informe a opção desejada.");

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar séries");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("C- Limpar tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            String opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();

            return opcaoUsuario;

        }
    }
}
