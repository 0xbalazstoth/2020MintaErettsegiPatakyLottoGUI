using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lottoGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<int> PrimSzamok = new List<int>();
        static List<int> Szamok = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
            Beolvas();
        }

        private void F10()
        {
            SortedDictionary<int, int> rendezett = new SortedDictionary<int, int>();

            for (int sor = 0; sor < lotto.lottoszamok.GetLength(0); sor++)
            {
                for (int oszlop = 0; oszlop < lotto.lottoszamok.GetLength(1); oszlop++)
                {
                    if (lotto.lottoszamok[sor, oszlop] != 0)
                    {
                        if (rendezett.ContainsKey(lotto.lottoszamok[sor, oszlop]))
                            rendezett[lotto.lottoszamok[sor, oszlop]]++;
                        else
                            rendezett[lotto.lottoszamok[sor, oszlop]] = 1;
                    }
                }
            }
            foreach (var szamok in rendezett)
            {
                listBoxSzamok.Items.Add($"{szamok.Key}: {szamok.Value}");
            }
        }

        static Lotto lotto;
        static List<Lotto> Adat = new List<Lotto>();
        public void Beolvas()
        {
            int sor = 0;
            int[,] matrix = new int[53, 5];
            using (StreamReader olvas = new StreamReader(@"lotto52.txt"))
            {
                while (!olvas.EndOfStream)
                {
                    sor++;
                    string lottoszamokSor = olvas.ReadLine();
                    string[] split = lottoszamokSor.Split(' ');
                    for (int i = 0; i < 5; i++)
                    {
                        matrix[sor, i] = Convert.ToInt32(split[i]);
                    }
                    lotto = new Lotto(lottoszamokSor, sor, matrix);
                    Adat.Add(lotto);
                }
            }
        }

        private void btnTiz_Click(object sender, RoutedEventArgs e)
        {
            F10();
        }

        private void btnTizene_Click(object sender, RoutedEventArgs e)
        {
            F11();
        }

        private void F11()
        {
            for (int x = 2; x < 91; x++)
            {
                int isPrim = 0;
                for (int y = 1; y < x; y++)
                {
                    if (x % y == 0)
                        isPrim++;
                    if (isPrim == 2)
                        break;
                }
                if (isPrim != 2)
                    PrimSzamok.Add(x);
                isPrim = 0;
            }
            for (int sor = 0; sor < lotto.lottoszamok.GetLength(0); sor++)
            {
                for (int oszlop = 0; oszlop < lotto.lottoszamok.GetLength(1); oszlop++)
                {
                    if(lotto.lottoszamok[sor, oszlop] != 0)
                        Szamok.Add(lotto.lottoszamok[sor, oszlop]);
                }
            }
            var rendezettSzamok = Szamok.OrderByDescending(x => x).Distinct().ToList();
            var hasonlo = rendezettSzamok.Intersect(PrimSzamok.Where(x => x != 0)).ToList();
            var kul = PrimSzamok.Except(hasonlo).ToList();
            for (int i = 0; i < kul.Count; i++)
            {
                listBoxNPrim.Items.Add(kul[i]);
            }
        }
    }
}
