using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lottoGUI
{
    class Lotto
    {
        public string lottoszamokSor { get; set; }
        public int Sor { get; set; }
        public int[,] lottoszamok { get; set; }
        public Lotto(string lottoszamokSor, int sor, int[,] lottoszamok)
        {
            this.lottoszamokSor = lottoszamokSor;
            this.Sor = sor;
            this.lottoszamok = lottoszamok;
        }
    }
}
