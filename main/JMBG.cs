using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveraJMBG.main
{
    internal class JMBG
    {
        public string[] republike = ["Stranci", "Bosna i Hercegovina", "Crna Gora", "Hrvatska", "Makedonija", "Slovenija", "Građani sa privremenim boravkom", "Centralna Srbija", "Vojvodina", "Kosovo i Metohija"];

        public string[,] regioni = {
        // 00 - 09: Stranci
        {"mešana upotreba", "stranci u Bosni i Hercegovini", "stranci u Crnoj Gori", "stranci u Hrvatskoj", "stranci u Makedoniji", "stranci u Sloveniji", "ne koristi se", "stranci u Centralnoj Srbiji", "stranci u Vojvodini", "stranci na Kosovu i Metohiji" },
        // 10 - 19: BiH
        {"Banja Luka", "Bihać", "Doboj", "Goražde", "Livno", "Mostar", "Prijedor", "Sarajevo", "Tuzla", "Zenica"},
        // 20 - 29: Crna Gora
        {"ne koristi se", "Podgorica", "Bar i Ulcinj", "Budva, Kotor, Tivat", "Herceg Novi", "Cetinje", "Nikšić", "Berane, Rožaje, Plav, Andrijevica", "Bijelo Polje, Mojkovac", "Pljevlja, Žabljak" },
        // 30 - 39: Hrvatska
        {"Osijek", "Bjelovar, Virovitica, Koprivnica, Pakrac", "Varaždin, Međimurje", "Zagreb","Karlovac", "Lika", "Istra", "Sisak, Banija", "Dalmacija", "mešana upotreba" },
        // 40 - 49: Makedonija
        {"ne koristi se", "Bitolj", "Kumanovo", "Ohrid", "Prilep", "Skopje", "Strumica", "Tetovo", "Veles", "Štip" },
        // 50 - 59: Slovenija
        {"cela teritorija Slovenije", "van upotrebe", "van upotrebe", "van upotrebe", "van upotrebe", "van upotrebe", "van upotrebe", "van upotrebe", "van upotrebe", "van upotrebe" },
        // 60 - 69: Privremeni boravak
        {"građani sa privremenim boravkom", "građani sa privremenim boravkom", "građani sa privremenim boravkom", "građani sa privremenim boravkom", "građani sa privremenim boravkom", "građani sa privremenim boravkom", "građani sa privremenim boravkom", "građani sa privremenim boravkom", "građani sa privremenim boravkom", "građani sa privremenim boravkom" },
        // 70 - 79: Centralna Srbija
        {"Građani upisani u matičnu knjigu rođenih DKP RS", "Beograd", "Kragujevac, Jagodina", "Niš, Pirot, Toplica", "Leskovac, Vranje", "Zaječar, Bor", "Smederevo, Požarevac", "Mačva, Kolubara", "Čačak, Kraljevo, Kruševac", "Užice" },
        // 80 - 89: Vojvodina
        {"Novi Sad", "Sombor", "Subotica", "Vrbas","Kikinda", "Zrenjanin", "Pančevo", "Vršac", "Ruma", "Sremska Mitrovica" },
        // 90 - 99: Kosovo i Metohija
        {"ne koristi se", "Priština","Kosovska Mitrovica","Peć","Đakovica","Prizren","Gnjilane, Kosovska Kamenica, Vitina, Novo Brdo", "van upotrebe", "van upotrebe","van upotrebe" }
        };

        public string uzmiJMBG()
        {
            Console.WriteLine("Unesite jedinstveni matični broj građana (JMBG):");
            string unetiJmbg = Console.ReadLine();
            return unetiJmbg;
        }

        public static bool kontrolaDuzine(string jmbg)
        {
            if (string.IsNullOrEmpty(jmbg))
                throw new Exception("Uneti JMBG je prazan.");

            if (jmbg.Length != 13 || !jmbg.All(char.IsDigit))
                throw new Exception("Uneseni JMBG nije u ispravnom formatu (mora imati tačno 13 cifara).");

            return true;
        }

        public static bool kontrolaRegije(string jmbg)
        {
            // ISPRAVLJENO: Regija počinje od 8. cifre (indeks 7 u C#-u)
            string regija = jmbg.Substring(7, 2);

            string[] nevazeci = ["06", "20", "40", "51", "52", "53", "54", "55", "56", "57", "58", "59", "90", "97", "98", "99"];

            for (int i = 0; i < nevazeci.Length; i++)
            {
                if (nevazeci[i] == regija)
                    throw new Exception("Uneti region (" + regija + ") se ne koristi ili je van upotrebe.");
            }
            return true;
        }

        public static void odrediRodjendan(string jmbg)
        {
            string rodjendan = jmbg.Substring(0, 7);
            string[] mesec = ["JAN", "FEB", "MAR", "APR", "MAJ", "JUN", "JUL", "AVG", "SEP", "OKT", "NOV", "DEC"];

            int dan1 = int.Parse(rodjendan.Substring(0, 1));
            int dan2 = int.Parse(rodjendan.Substring(1, 1));
            int mesec1 = int.Parse(rodjendan.Substring(2, 1));
            int mesec2 = int.Parse(rodjendan.Substring(3, 1));
            int godinaCifre = int.Parse(rodjendan.Substring(4, 3));
            int godina = (godinaCifre > 500) ? (1000 + godinaCifre) : (2000 + godinaCifre);

            int totalDan = dan1 * 10 + dan2;
            int totalMesec = mesec1 * 10 + mesec2;

            Console.WriteLine("Datum rođenja: " + totalDan + ". " + mesec[totalMesec - 1] + " " + godina + ".");
        }

        public static void odrediRepublikuRegiju(string jmbg, string[] republike, string[,] regije)
        {
            // ISPRAVLJENO: Indeks postavljen na 7
            string regija = jmbg.Substring(7, 2);
            int r1 = int.Parse(regija.Substring(0, 1)); // Prva cifra regiona (red)
            int r2 = int.Parse(regija.Substring(1, 1)); // Druga cifra regiona (kolona)

            Console.WriteLine("Republika: " + republike[r1]);
            Console.WriteLine("Mesto ili region rođenja: " + regije[r1, r2]);
        }

        public static void odrediPol(string jmbg)
        {
            int pol = int.Parse(jmbg.Substring(9, 3));
            if (pol >= 500)
                Console.WriteLine("Pol: Ženski");
            else
                Console.WriteLine("Pol: Muški");
        }

        public static bool kontrolaCifre(string jmbg)
        {
            int[] A = new int[13];
            for (int i = 0; i < 13; i++)
            {
                A[i] = jmbg[i] - '0';
            }

            int S = 7 * A[0] + 6 * A[1] + 5 * A[2] + 4 * A[3] + 3 * A[4] + 2 * A[5]
                  + 7 * A[6] + 6 * A[7] + 5 * A[8] + 4 * A[9] + 3 * A[10] + 2 * A[11];

            int m = S % 11;
            int K;

            if (m == 0)
            {
                K = 0;
            }
            else if (m == 1)
            {
                throw new Exception("Matični broj je pogrešan (m = 1). Potrebno je uvećati BBB za 1 i generisati ponovo.");
            }
            else
            {
                K = 11 - m;
            }

            if (K != A[12])
            {
                throw new Exception($"Kontrolna cifra nije ispravna! Očekivana: {K}, Pročitana iz JMBG-a: {A[12]}");
            }

            return true;
        }

        public static void Main(string[] args)
        {
            JMBG program = new JMBG();
            while (true)
            {
                try
                {
                    string jmbg = program.uzmiJMBG();

                    kontrolaDuzine(jmbg);
                    kontrolaRegije(jmbg);
                    kontrolaCifre(jmbg);

                    Console.WriteLine("\n--- PODACI IZ JMBG-a ---");
                    odrediRodjendan(jmbg);
                    odrediRepublikuRegiju(jmbg, program.republike, program.regioni);
                    odrediPol(jmbg);
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("JMBG je potpuno validan!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nGREŠKA: " + ex.Message);
                }

                Console.ReadLine();
            }
        }
    }
}