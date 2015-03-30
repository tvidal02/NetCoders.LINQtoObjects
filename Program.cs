using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoders.LINQ.Aquecimento
{
    class Program
    {
        delegate int Multiplicar(int number);
        delegate double Subtrair(double n1, double n2);

        static void Main(string[] args)
        {
            //1ª Parte: Apresentação dos Conceitos Básicos sobre LINQ. 
            //Aquecimento: desenvolver algumas consultas em uma aplicação Console para se familiarizar com a sintaxe
            //LINQ to Objects

            //Criando a primeira query

            //obtendo o data source
            int[] arrNums = new int[] { 20, 38, 49, 52, 33, 01, 08, 19, 60 };

            //query para filtrar números pares
            var queryNumPares = from num in arrNums
                                where num % 2 == 0
                                select num;
            
            //executando a query
            Console.WriteLine("Filtrando números pares...");
            foreach (var numero in queryNumPares)
            {
                Console.WriteLine("Número: " + numero);
            }

            Console.WriteLine();

            //query para filtrar números maiores que 30
            //38, 49, 52, 33, 60
            var query = from num in arrNums
                        where num > 30
                        select num;

            Console.WriteLine("Filtrando números maiores que 30...");
            foreach (var numero in query)
            {
                Console.WriteLine("Número: " + numero);
            }

            Console.WriteLine();

            //object initializer
            //chama o construtor padrão da classe no momento da instanciação e seta suas propriedades
            //Exemplo
            //Pessoa p = new Pessoa() { Nome = "Thiago", Idade = 22, Sexo = 'M' };

            //Sem o initializer: mais código a ser escrito
            //Pessoa p = new Pessoa();
            //p.Nome = "Thiago";
            //p.Idade = 22;
            //p.Sexo = 'M';

            //IEnumerable e generics
            List<Pessoa> pessoas = new List<Pessoa>()
            {
                new Pessoa() { Nome = "João da Silva", Idade = 20, Sexo ='M' },
                new Pessoa() { Nome = "Érick Wendeu", Idade = 13, Sexo = 'M' },
                new Pessoa() { Nome = "Robson Cruzes", Idade = 76, Sexo = 'M' },
                new Pessoa() { Nome = "Mariana Teixeira", Idade = 29, Sexo = 'F' },
                new Pessoa() { Nome = "Miguel", Idade = 34, Sexo = 'M' },
                new Pessoa() { Nome = "Vitor", Idade = 110, Sexo = 'M' },
                new Pessoa() { Nome = "Renata", Idade = 38, Sexo = 'F' },
                new Pessoa() { Nome = "Paulo", Idade = 29, Sexo = 'M' },
                new Pessoa() { Nome = "Gabriela", Idade = 15, Sexo = 'F' },
            };

            //IEnumerable: para objetos
            var queryPessoa = from p in pessoas
                              where p.Sexo == 'F'
                              select p;
            
            Console.WriteLine("Filtrando pessoas do sexo feminino...");
            foreach (var pessoa in queryPessoa)
            {
                Console.WriteLine("{0}, {1} anos", pessoa.Nome, pessoa.Idade);
            }
            
            Console.WriteLine();

            /* Inferência de tipos
             * Regras de utilização
             * Não pode declarar uma variável var sem atribuir algum valor (o compilador não consegue inferir)
             * Não pode atribuir null a uma variável var
             * Não pode declarar atributo usando var (use-o somente como variável local)
             * Recomendação: utilize var para queries LINQ e para tipos por referência
             */
            
            var i = 10;
            var s = "teste";

            //com array: é necessário o operador new e []
            var array = new[] { 1, 2, 3 };

            //Dictionary: invoca o construtor default chama várias vezes o método Add
            Dictionary<int, Aluno> dicAlunos = new Dictionary<int, Aluno>() 
            {
                { 1, new Aluno() { Nome = "Josefá", Nota = 0 } },
                { 2, new Aluno() { Nome = "Pedro", Nota = 3 } },
                { 3, new Aluno() { Nome = "Rita", Nota = 9 } }
            };


            /* Tipos Anônimos
             * Recurso para encapsular propriedades a um objeto sem ter de criar uma nova classe
             * O nome do tipo é gerado pelo compilador e o tipo de suas propriedades é inferido por ele
             * O tipo não é acessível no código-fonte
             * As propriedades de um tipo anônimo são read-only
             * Não podem ser usados como atributos, métodos e eventos
             * Um tipo anônimo é definido sempre com a palavra-chave var
             */
            var obj = new { Preco = 34.90, Quantidade = 3 };

            var lstAlunos = new List<Aluno>() 
            {
                new Aluno() { AlunoId = 1, Nome = "Josefá", Nota = 0.0, DP = true, Disciplina_DP = "Algoritmos" },
                new Aluno() { AlunoId = 2, Nome = "Pedrita", Nota = 2.2, DP = true, Disciplina_DP = "Programação" },
                new Aluno() { AlunoId = 3, Nome = "Anderson", Nota = 8.9 },
                new Aluno() { AlunoId = 4, Nome = "Ronaldo", Nota = 6.5 },
                new Aluno() { AlunoId = 5, Nome = "Carlos", Nota = 5.1 },
                new Aluno() { AlunoId = 6, Nome = "Johnny", Nota = 2.4, DP = true, Disciplina_DP = "Cálculo 3" },
                new Aluno() { AlunoId = 7, Nome = "Kelly Key", Nota = 9.8 },
                new Aluno() { AlunoId = 8, Nome = "Mustafá", Nota = 1.0, DP = true, Disciplina_DP = "Álgebra Linear" },
                new Aluno() { AlunoId = 9, Nome = "Juanito", Nota = 3.2, DP = true, Disciplina_DP = "Compiladores" },
                new Aluno() { AlunoId = 10, Nome = "Pedro Bó", Nota = 10.0 },
            };

            //projeção: selecionar alunos com as maiores notas da turma
            //maiores notas: maior ou igual a 7
            var queryMelhoresAlunos = from a in lstAlunos
                                      where a.Nota >= 7
                                      select new { a.Nome, a.Nota };

            Console.WriteLine("Melhores alunos da turma...");
            foreach (var aluno in queryMelhoresAlunos)
            {
                Console.WriteLine("{0}: {1}", aluno.Nome, aluno.Nota);
            }

            Console.WriteLine();

            //outro exemplo: selecionar os alunos que possuem uma dependência em Álgebra Linear
            var queryDP = from a in lstAlunos
                          where a.DP == true && a.Disciplina_DP == "Álgebra Linear"
                          select new { a.Nome, a.Disciplina_DP };

            Console.WriteLine("Piores alunos da turma...");
            foreach (var aluno in queryDP)
            {
                Console.WriteLine("{0} pegou DP de {1}", aluno.Nome, aluno.Disciplina_DP);
            }

            Console.WriteLine();

            Multiplicar mD = new Multiplicar(multiply);
            int number = mD(10);

            Console.WriteLine("Multiplicação: " + number);

            Console.WriteLine();

            /* expressões lambda
             * Uma expressão lambda é uma função anônima que é possível criar delegates ou árvores de expressão
             * Você pode usar uma expressão lambda como uma função local e passar argumentos ou retornar valores de funções
             * Muito útil para escrever consultas LINQ
             */

            //Lambda com delegate
            Multiplicar multiplicarDelegate = x => x * x;
            int resultadoMultiplicacao = multiplicarDelegate(15);
            
            Console.WriteLine("Resultado da multiplicação: " + resultadoMultiplicacao);

            Console.WriteLine();

            Subtrair subtrairDelegate = (x,y) => x - y;
            double resultadoSubtracao = subtrairDelegate(10, 5);
            
            Console.WriteLine("Resultado da subtração: " + resultadoSubtracao);

            Console.WriteLine();
            
            //Lambda em consultas LINQ

            //array: 20, 38, 49, 52, 33, 01, 08, 19, 60
            //números ímpares: 49, 33, 01, 19
            var numerosImpares = arrNums.Where(x => x % 2 == 1);

            Console.WriteLine("Filtrando números ímpares...");
            foreach (var numero in numerosImpares)
            {
                Console.WriteLine(numero);
            }

            Console.WriteLine();

            /* Métodos de Extensão
             * Recurso do .Net que permite adicionar funcionalidades a classes existentes sem precisar recorrer à herança
             * Os métodos de extensão são definidos como estáticos (em classes também estáticas),
             * mas são chamados como os métodos de instância. 
             * O que acontece é que estamos adicionando um método à classe String
             * Para isso funcionar, basta escrever ‘this’ no parâmetro antes do nome da classe
             * Os métodos do LINQ são todos de extensão
             */

            string st = "thiago";
            Console.WriteLine(MyExtensions.ToURL(st));

            Console.WriteLine();

            //vários exemplos de consulta: apresentar outros métodos de extensão

            //selecionar a média da turma
            var notaMedia = lstAlunos.Average(a => a.Nota);
            Console.WriteLine("Média da turma: " + notaMedia);

            Console.WriteLine();

            string[] arrStrings = new string[] { "Bla", "texto", "outro", "Wendel", "um" };
            var elemento = arrStrings.ElementAt(2);

            Console.WriteLine("Elemento na posição 2 do array de strings: " + elemento);

            var elem = arrStrings.ElementAtOrDefault(10);

            Console.WriteLine();

            Funcionario f1 = new Funcionario() { Nome = "Rogério", Salario = 17000, Departamento = "Financeiro" };
            Funcionario f2 = new Funcionario() { Nome = "Claudiney", Salario = 49200, Departamento = "Marketing" };
            Funcionario f3 = new Funcionario() { Nome = "Marilson", Salario = 5000, Departamento = "RH" };
            Funcionario f4 = new Funcionario() { Nome = "Gian", Salario = 10000, Departamento = "Financeiro" };
            Funcionario f5 = new Funcionario() { Nome = "Lolita", Salario = 7500, Departamento = "Comercial" };
            Funcionario f6 = new Funcionario() { Nome = "Carlota", Salario = 4800, Departamento = "RH" };
            Funcionario f7 = new Funcionario() { Nome = "Evanildo", Salario = 18550, Departamento = "Financeiro" };
            Funcionario f8 = new Funcionario() { Nome = "Edemerson", Salario = 9120, Departamento = "Comercial" };
            Funcionario f9 = new Funcionario() { Nome = "Astrogildo", Salario = 27700, Departamento = "Marketing" };
            Funcionario f10 = new Funcionario() { Nome = "Juanita", Salario = 4012, Departamento = "RH" };

            var lstFuncionarios = new List<Funcionario>() { f1, f2, f3, f4, f5, f6, f7, f8, f9, f10 };

            //organizar salários de funcionários em ordem crescente
            var querySalarioFunc = lstFuncionarios.OrderBy(func => func.Salario);
            foreach (var funcionario in querySalarioFunc)
            {
                Console.WriteLine("Nome: {0}, Salário: {1}", funcionario.Nome, funcionario.Salario);
            }

            //sem o método de extensão
            var querySalarioFunc2 = from f in lstFuncionarios
                                    orderby f.Salario
                                    select f;

            Console.WriteLine();

            //Aplicando o LIKE do SQL
            //selecionar funcionárias que contenham "ita" no nome
            var queryFunc = from f in lstFuncionarios
                            where f.Nome.Contains("ita")
                            select f;
            
            foreach (var funcionario in queryFunc)
            {
                Console.WriteLine("Nome: " + funcionario.Nome);
            }

            Console.WriteLine();

            //agrupar funcionários por departamento
            var queryFuncionariosPorDepto = from funcionario in lstFuncionarios
                                            group funcionario by funcionario.Departamento into funcDepto
                                            select new 
                                            {
                                                Departamento = funcDepto.Key,
                                                Total = funcDepto.Count()
                                            };

            foreach (var funcionario in queryFuncionariosPorDepto)
            {
                Console.WriteLine("Departamento: {0} \t Total: {1}" , funcionario.Departamento, funcionario.Total);
            }

            Console.WriteLine();

            //Process[] procs = Process.GetProcesses();
            //foreach (var process in procs)
            //{
            //    Console.WriteLine("Processo: " + process.ProcessName);
            //}

            //exemplo de consulta sem LINQ
            List<Process> lstProcessos = new List<Process>();
            foreach (var processo in Process.GetProcesses())
            {
                if (processo.ProcessName.StartsWith("w"))
                {	
                    lstProcessos.Add(processo);
                    Console.WriteLine("Processo: " + processo.ProcessName);
                }
            }

            Console.WriteLine();

            //exemplo de consulta com LINQ
            var procs = Process.GetProcesses();
            var queryProcessos = from proc in procs
                                 where proc.ProcessName.StartsWith("w")
                                 select proc;

            //mais enxuto, com métodos de extensão
            //var processos = lstProcessos.Where(p => p.ProcessName.StartsWith("w"));

            Console.WriteLine("Procesos que começam com w...");
            foreach (var processo in queryProcessos)
            {
                Console.WriteLine("Processo: " + processo.ProcessName);
            }

            Console.ReadLine();
        }

        private static int multiply(int number)
        {
            return number * number;
        }
    }
}
