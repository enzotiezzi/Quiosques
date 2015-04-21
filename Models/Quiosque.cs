using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tell_you_story.Models
{
    public class Quiosque
    {
        public int ID { get; set; }

        public int IDPraia { get; set; }

        public String Nome { get; set; }

        public int NumeroQuiosque { get; set; }

        public String Rua { get; set; }

        public String Bairro { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public String ImageURL { get; set; }

        public int Qtd { get; set; }
    }
}
