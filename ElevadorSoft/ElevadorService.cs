using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaAdmissionalCSharpApisul
{
    public class ElevadorService : IElevadorService
    {

        private List<ElevadorInfo> BuscarInformacoesElevador()
        {
            using (StreamReader r = new StreamReader("../../../input.json"))
            {
                string json = r.ReadToEnd();
                List<ElevadorInfo> elevadorInfos = JsonConvert.DeserializeObject<List<ElevadorInfo>>(json);

                return elevadorInfos;
            }
        }
        private Dictionary<char, int> BuscarDicionarioQuantidadeElevadores()
        {
            List<ElevadorInfo> elevadorInfos = BuscarInformacoesElevador();
            List<char> elevadores = elevadorInfos.Select(x => x.elevador).ToList();

            Dictionary<char, int> result = elevadores.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            return result;
        }
        private float CalcularPercentualUsoElevador(char elevador)
        {
            List<ElevadorInfo> elevadorInfos = BuscarInformacoesElevador();
            List<char> elevadores = elevadorInfos.Select(x => x.elevador).ToList();

            float percentualDeUsoElevadorA = ((elevadores.Count(x => x == elevador) * 100) / elevadores.Count());

            return percentualDeUsoElevadorA;
        }

        public List<int> andarMenosUtilizado()
        {
            List<ElevadorInfo> elevadorInfos = BuscarInformacoesElevador();
            List<int> andares = elevadorInfos.Select(x => x.andar).ToList();

            Dictionary<int, int> result = andares.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            List<int> andaresMenosUtilizados = result.Where(x => x.Value == result.Values.Min()).Select(x => x.Key).ToList();

            return andaresMenosUtilizados;
        }

        public List<char> elevadorMaisFrequentado()
        {
            Dictionary<char, int> result = BuscarDicionarioQuantidadeElevadores();
            List<char> elevadoresMaisFrequentados = result.Where(x => x.Value == result.Values.Max()).Select(x => x.Key).ToList();

            return elevadoresMaisFrequentados;
        }

        public List<char> elevadorMenosFrequentado()
        {
            Dictionary<char, int> result = BuscarDicionarioQuantidadeElevadores();
            List<char> elevadoresMenosFrequentados = result.Where(x => x.Value == result.Values.Min()).Select(x => x.Key).ToList();

            return elevadoresMenosFrequentados;
        }

        public float percentualDeUsoElevadorA()
        {
            return CalcularPercentualUsoElevador('A');
        }

        public float percentualDeUsoElevadorB()
        {
            return CalcularPercentualUsoElevador('B');
        }

        public float percentualDeUsoElevadorC()
        {
            return CalcularPercentualUsoElevador('C');
        }

        public float percentualDeUsoElevadorD()
        {
            return CalcularPercentualUsoElevador('D');
        }

        public float percentualDeUsoElevadorE()
        {
            return CalcularPercentualUsoElevador('E');
        }

        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            List<ElevadorInfo> elevadorInfos = BuscarInformacoesElevador();
            List<char> elevadoresMaisFrequentados = elevadorMaisFrequentado();

            List<char> turnosElevador = elevadorInfos.Where(x => elevadoresMaisFrequentados.Contains(x.elevador)).Select(x => x.turno).ToList();
            Dictionary<char, int> result = turnosElevador.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            return result.Where(x => x.Value == result.Values.Max()).Select(x => x.Key).ToList();
        }

        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            List<ElevadorInfo> elevadorInfos = BuscarInformacoesElevador();
            List<char> turnos = elevadorInfos.Select(x => x.turno).ToList();

            Dictionary<char, int> result = turnos.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            return result.Where(x => x.Value == result.Values.Max()).Select(x => x.Key).ToList();
        }

        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            List<ElevadorInfo> elevadorInfos = BuscarInformacoesElevador();
            List<char> elevadoresMenosFrequentados = elevadorMenosFrequentado();

            List<char> turnosElevador = elevadorInfos.Where(x => elevadoresMenosFrequentados.Contains(x.elevador)).Select(x => x.turno).ToList();
            Dictionary<char, int> result = turnosElevador.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            return result.Where(x => x.Value == result.Values.Min()).Select(x => x.Key).ToList();
        }
    }
}
