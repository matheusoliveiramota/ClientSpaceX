using SpaceX.Business;
using SpaceX.Business.Entities;
using SpaceX.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceX
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ISpaceRepository spaceRepository = new SpaceRepository();
                DateTime beginAsync = DateTime.Now;

                Task<IEnumerable<Launch>>[] tasks = new Task<IEnumerable<Launch>>[4];
                tasks[0] = spaceRepository.GetNextLaunch();
                tasks[1] = spaceRepository.GetLastLaunch();
                tasks[2] = spaceRepository.GetPastLaunches();
                tasks[3] = spaceRepository.GetUpcomingLaunches();
                Task.WaitAll(tasks);

                Launch nextLaunch = tasks[0].Result.FirstOrDefault();
                Launch lastLaunch = tasks[1].Result.FirstOrDefault();
                IEnumerable<Launch> pastLaunches = tasks[2].Result;
                IEnumerable<Launch> upcomingLaunches = tasks[3].Result;

                Console.WriteLine("Tempo gasto para realizar as requisições= " + Convert.ToString(DateTime.Now - beginAsync));
                Console.WriteLine("");

                // Informações dos Lançamentos
                Console.WriteLine("PRÓXIMO LANÇAMENTO : Número do Vôo = " + nextLaunch.FlightNumber + "    Missão = " + nextLaunch.MissionName
                                                     + "    Ano de Lançamento = " + nextLaunch.LaunchYear.ToString() + "    Data de Lançamento = " + nextLaunch.LaunchDate);
                Console.WriteLine("");
                Console.WriteLine("ÚLTIMO LANÇAMENTO : Número do Vôo = " + lastLaunch.FlightNumber + "    Missão = " + lastLaunch.MissionName
                                         + "    Ano de Lançamento = " + lastLaunch.LaunchYear.ToString() + "    Data de Lançamento = " + lastLaunch.LaunchDate);

                Console.WriteLine("");
                Console.WriteLine("LANÇAMENTOS PASSADOS: ");
                foreach (var launch in pastLaunches)
                {
                    Console.WriteLine("     Número do Vôo = " + launch.FlightNumber + "    Missão = " + launch.MissionName
                                     + "    Ano de Lançamento = " + launch.LaunchYear.ToString() + "    Data de Lançamento = " + launch.LaunchDate);
                }

                Console.WriteLine("");
                Console.WriteLine("LANÇAMENTOS FUTUROS: ");
                foreach (var launch in upcomingLaunches)
                {
                    Console.WriteLine("     Número do Vôo = " + launch.FlightNumber + "    Missão = " + launch.MissionName
                                     + "    Ano de Lançamento = " + launch.LaunchYear.ToString() + "    Data de Lançamento = " + launch.LaunchDate);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Erro ao recuperar informações dos vôos !");
                Console.WriteLine("MENSAGEM DE ERRO: " + e.Message);
            }

            Console.Read();
        }
    }
}
