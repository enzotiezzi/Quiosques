using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;

namespace Tell_you_story.Models
{
    public class Cidade
    {
        public int ID { get; set; }

        public int IDLirotal { get; set; }

        public String Nome { get; set; }

        public String Estado { get; set; }

        public String ImageURL { get; set; }

        public int Qtd { get; set; }

    }
}
