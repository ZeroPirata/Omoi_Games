namespace Omoi.Classes
{
    public class Menu
    {
        private static Menu _menu;
        static JogosRepositorio repositorio = new JogosRepositorio();
        private Menu() { }
        public static Menu ObterInstancia()
        {
            if (_menu == null)
                _menu = new Menu();

            return _menu;
        }



        public void MenuPrincipal()
        {
            while (true)
            {
                MostrarMenu();
                string opcao = ValidaOpcao();

                switch (opcao)
                {
                    case "1":
                        Console.Clear();
                        ListarJogos();
                        break;
                    case "2":
                        Console.Clear();
                        InserirJogos();
                        break;
                    case "3":
                        Console.Clear();
                        AtualizarJogos();
                        break;
                    case "4":
                        Console.Clear();
                        ExcluirJogos();
                        break;
                    case "5":
                        Console.Clear();
                        VisualizarJogos();
                        break;
                    case "C":
                        Console.Clear();
                        Console.Clear();
                        break;
                    case "X":
                        EncerrarAplicacao();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção não encontrada, tente novamente.");
                        break;
                }
            }
        }

        private void ListarJogos()
        {


            Console.WriteLine("Listar Jogos");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum Jogo cadastrado.");
                return;
            }

            foreach (var cadastro in lista)
            {
                var excluido = cadastro.retornaExcluido();

                Console.WriteLine("#ID {0}: - {1} {2}", cadastro.retornaId(), cadastro.retornaTitulo(), (excluido ? "*Excluído*" : ""));
            }
        }
        private void InserirJogos()
        {
            Console.WriteLine("Inserir nova Jogo");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            System.Console.WriteLine();
            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = ValidaNumero();

            Console.Write("Digite o nome do Jogo: ");
            string entradaTitulo = ValidaString();

            Console.Write("Digite o Ano de lançamento: ");
            int entradaAno = ValidaNumero();

            Console.Write("Digite a biogradia do Jogo: ");
            string entradaDescricao = ValidaString();

            Jogos novaJogos = new Jogos(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novaJogos);
        }
        private void AtualizarJogos()
        {
            Console.Write("Digite o id da Jogo: ");
            int indiceJogos = ValidaNumero();

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = ValidaNumero();

            Console.Write("Digite o nome do Jogo: ");
            string entradaTitulo = ValidaString();

            Console.Write("Digite o Ano de lançamento: ");
            int entradaAno = ValidaNumero();

            Console.Write("Digite a biogradia do Jogo: ");
            string entradaDescricao = ValidaString();

            Jogos atualizaJogos = new Jogos(id: indiceJogos,
                            genero: (Genero)entradaGenero,
                            titulo: entradaTitulo,
                            ano: entradaAno,
                            descricao: entradaDescricao);


            try
            {
                repositorio.Atualiza(indiceJogos, atualizaJogos);
                System.Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (System.Exception)
            {
                System.Console.WriteLine($"Problema encontrado ao achar id e/ou atualizar Tente novamente!");
            }
        }
        private void ExcluirJogos()
        {
            Console.Write("Digite o id da Jogo: ");
            int indiceJogos = ValidaNumero();


            try
            {
                repositorio.Exclui(indiceJogos);
                Console.WriteLine("Cadastro excluído com sucesso!");

            }
            catch (System.Exception)
            {
                Console.WriteLine($"Problema encontrado ao tentar excluir Tente novamente!");
            }

        }
        private void VisualizarJogos()
        {
            Console.Write("Digite o id do Jogo: ");
            int indiceJogos = ValidaNumero();


            try
            {
                System.Console.WriteLine();
                var jogo = repositorio.RetornaPorId(indiceJogos);

                Console.WriteLine(jogo);
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Cadastro não encontrado" + Environment.NewLine + "Tente novamente!");
            }

        }

        private int ValidaNumero()
        {
            bool tentarNovamente = true;
            int retorno = 0;

            while (tentarNovamente)
            {
                try
                {
                    retorno = Int32.Parse(s: Console.ReadLine());
                    tentarNovamente = false;
                }
                catch (System.FormatException)
                {
                    System.Console.WriteLine("Digite apenas números.");
                }
            }

            return retorno;
        }

        private string ValidaOpcao()
        {
            bool tentarNovamente = true;
            string retorno = "";

            while (tentarNovamente)
            {
                try
                {
                    retorno = Console.ReadLine().ToUpper();
                    Console.WriteLine();
                }
                catch
                {
                    Console.WriteLine("Escolha uma das opções.");
                }

                if (retorno.Length == 1)
                {
                    tentarNovamente = false;
                    continue;
                }
                Console.WriteLine("Digite apenas o índice da opção!");
            }
            return retorno;
        }

        private string ValidaString()
        {
            bool tentarNovamente = true;
            string retorno = "";

            while (tentarNovamente)
            {
                retorno = Console.ReadLine();

                if (retorno.Length > 1)
                    tentarNovamente = false;
                else Console.WriteLine("Entrada vazia ou curta demais.");
            }

            return retorno;
        }
        private void EncerrarAplicacao()
        {
            Console.WriteLine(Environment.NewLine + "Muito Obrigado."
                + Environment.NewLine + "Pressione qualquer tecla para sair!");
            Console.ReadKey();
            Environment.Exit(0);
        }
        private void MostrarMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Omoi jogos");
            Console.WriteLine("Seja bem vindo, aproveite o Demo");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine();

            Console.WriteLine("1- Listar Jogos");
            Console.WriteLine("2- Inserir novo Jogo");
            Console.WriteLine("3- Atualizar Jogo");
            Console.WriteLine("4- Excluir Jogo");
            Console.WriteLine("5- Visualizar Jogo");
            Console.WriteLine("C- Limpar tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();
        }
    }
}