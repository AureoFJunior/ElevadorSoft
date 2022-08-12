using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaAdmissionalCSharpApisul
{
    public class Program
    {
        static void Main(string[] args)
        {
            ElevadorService ElevadorService = new ElevadorService();

            var aux = ElevadorService.percentualDeUsoElevadorA();
        }
    }
}
