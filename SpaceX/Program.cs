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
            // Utilizando os Métodos Assíncronos de Forma Síncrona
                DateTime begin = DateTime.Now; 
                ISpaceRepository spaceRepository = new SpaceRepository();

                var nextLaunch = spaceRepository.GetNextLaunch().Result;
                var lastLaunch = spaceRepository.GetLastLaunch().Result;
                var pastLaunches = spaceRepository.GetPastLaunches().Result;
                var upcomingLaunches = spaceRepository.GetUpcomingLaunches().Result;

                Console.WriteLine("Utilizando os Métodos Assíncronos de Forma Síncrona:   Tempo Gasto = " + Convert.ToString(DateTime.Now - begin));

            // Utilizando os Métodos Assíncronos de Forma Paralela
                DateTime beginAsync = DateTime.Now;

                Task<IEnumerable<Launch>>[] tasks = new Task<IEnumerable<Launch>>[4];
                    tasks[0] = spaceRepository.GetNextLaunch();
                    tasks[1] = spaceRepository.GetLastLaunch();
                    tasks[2] = spaceRepository.GetPastLaunches();
                    tasks[3] = spaceRepository.GetUpcomingLaunches();
                Task.WaitAll(tasks);

                Launch nextLaunchAsync = tasks[0].Result.FirstOrDefault();
                Launch lastLaunchAsync = tasks[1].Result.FirstOrDefault();
                IEnumerable<Launch> pastLaunchesAsync = tasks[2].Result;
                IEnumerable<Launch> upcomingLaunchesAsync = tasks[3].Result;

                Console.WriteLine("Utilizando os Métodos Assíncronos de Forma Paralela:   Tempo Gasto = " + Convert.ToString(DateTime.Now - beginAsync));
                Console.WriteLine("");

            // Informações dos Lançamentos
                Console.WriteLine("PRÓXIMO LANÇAMENTO : Número do Vôo = " + nextLaunchAsync.FlightNumber + "    Missão = " + nextLaunchAsync.MissionName
                                                     + "    Ano de Lançamento = " + nextLaunchAsync.LaunchYear.ToString() + "    Data de Lançamento = " + nextLaunchAsync.LaunchDate);
                Console.WriteLine("");
                Console.WriteLine("ÚLTIMO LANÇAMENTO : Número do Vôo = " + lastLaunchAsync.FlightNumber + "    Missão = " + lastLaunchAsync.MissionName
                                         + "    Ano de Lançamento = " + lastLaunchAsync.LaunchYear.ToString() + "    Data de Lançamento = " + lastLaunchAsync.LaunchDate);

                Console.WriteLine("");
                Console.WriteLine("LANÇAMENTOS PASSADOS: ");
                foreach (var launch in pastLaunchesAsync)
                {
                    Console.WriteLine("     Número do Vôo = " + launch.FlightNumber + "    Missão = " + launch.MissionName
                                     + "    Ano de Lançamento = " + launch.LaunchYear.ToString() + "    Data de Lançamento = " + launch.LaunchDate);
                }

                Console.WriteLine("");
                Console.WriteLine("LANÇAMENTOS FUTUROS: ");
                foreach (var launch in upcomingLaunchesAsync)
                {
                    Console.WriteLine("     Número do Vôo = " + launch.FlightNumber + "    Missão = " + launch.MissionName
                                     + "    Ano de Lançamento = " + launch.LaunchYear.ToString() + "    Data de Lançamento = " + launch.LaunchDate);
                }

        }
    }
}
