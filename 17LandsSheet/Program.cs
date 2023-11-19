using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace _17LandsSheet {
    class Program {

        struct Card : IComparable<Card> {
            public string name;
            public string color;
            public string rarity;

            public int seen;
            public float alsa;

            public int taken;
            public float ata;

            public int gp;
            public float pr;
            public float gpwr;

            public int oh;
            public float ohwr;

            public int gd;
            public float gdwr;

            public int gih;
            public float gihwr;

            public int gnd;
            public float gndwr;

            public float iwd;

            public int CompareTo(Card other) {
                if (gihwr > other.gihwr) return -1;
                if (gihwr == other.gihwr) return 0;
                return 1;
            }

            public override string ToString() {
                return $"{name} {color} {rarity} {seen} {alsa} {taken} {ata} {gp} {gpwr}";
            }

            
        }

        static void Main(string[] args) {


            string name = args.Length > 0 ? args[0] : "lci.txt";

            List<Card> cards = new List<Card>();
            string[] lines = File.ReadAllLines(name);
            for (int i = 1; i < lines.Length; i++) {

                Card c = new Card();
                string[] words = lines[i].Split(new char[]{'\t'}, StringSplitOptions.None);
                c.name = words[0];
                c.color = words[1];
                c.rarity = words[2];

                c.seen = ParseInt(words[3]);
                c.alsa = ParseFloat(words[4]);

                c.taken = ParseInt(words[5]);
                c.ata = ParseFloat(words[6]);

                c.gp = ParseInt(words[7]);
                c.pr = ParsePercent(words[8]);
                c.gpwr = ParsePercent(words[9]);

                c.oh = ParseInt(words[10]);
                c.ohwr = ParsePercent(words[11]);

                c.gd = ParseInt(words[12]);
                c.gdwr = ParsePercent(words[13]);

                c.gih = ParseInt(words[14]);
                c.gihwr = ParsePercent(words[15]);

                c.gnd = ParseInt(words[16]);
                c.gndwr = ParsePercent(words[17]);

                c.iwd = ParsePercent(words[18], 'p');

                cards.Add(c);
            }

            //foreach (Card card in cards) Console.WriteLine($"{card.name}|{card.ata}|{card.gihwr}|{card.iwd}");
            CreateSheet(cards);
        }

        static void CreateSheet(List<Card> cards) {
            List<Card> w = new List<Card>();
            List<Card> u = new List<Card>();
            List<Card> b = new List<Card>();
            List<Card> r = new List<Card>();
            List<Card> g = new List<Card>();
            List<Card> m = new List<Card>();

            cards.Sort();

            foreach (Card card in cards) {
                if (card.rarity != "C" && card.rarity != "U") continue;
                switch(card.color) {
                    case "W": w.Add(card); break;
                    case "U": u.Add(card); break;
                    case "B": b.Add(card); break;
                    case "R": r.Add(card); break;
                    case "G": g.Add(card); break;
                    default: m.Add(card); break;
                }
            }

            Console.WriteLine("WHITE|ATA|WR|IWD||BLUE|ATA|WR|IWD||BLACK|ATA|WR|IWD||RED|ATA|WR|IWD||GREEN|ATA|WR|IWD|||MULTI|ATA|WR|IWD|||W|J|B|R|G|M");

            int line = 0;
            bool keepDoingLines = true;
            while(keepDoingLines) {
                keepDoingLines = false;
                if (line < w.Count) { keepDoingLines = true; WriteCard(w[line]); } else Console.Write("|||||");
                if (line < u.Count) { keepDoingLines = true; WriteCard(u[line]); } else Console.Write("|||||");
                if (line < b.Count) { keepDoingLines = true; WriteCard(b[line]); } else Console.Write("|||||");
                if (line < r.Count) { keepDoingLines = true; WriteCard(r[line]); } else Console.Write("|||||");
                if (line < g.Count) { keepDoingLines = true; WriteCard(g[line]); } else Console.Write("|||||");
                if (line < m.Count) { keepDoingLines = true; Console.Write(m[line].color + "|"); WriteCard(m[line]); } else Console.Write("||||||");
                //Console.Write("|");
                if (line < w.Count) Console.Write(w[line].rarity + "|"); else Console.Write("|");
                if (line < u.Count) Console.Write(u[line].rarity + "|"); else Console.Write("|");
                if (line < b.Count) Console.Write(b[line].rarity + "|"); else Console.Write("|");
                if (line < r.Count) Console.Write(r[line].rarity + "|"); else Console.Write("|");
                if (line < g.Count) Console.Write(g[line].rarity + "|"); else Console.Write("|");
                if (line < m.Count) Console.Write(m[line].rarity + "|"); else Console.Write("|");
                line++;
                Console.WriteLine();
            }
        }

        static void WriteCard(Card card) {
            Console.Write($"{card.name}|{card.ata}|{card.gihwr}|{card.iwd}||");
        }

        static int ParseInt(string str) {
            if (str == "") return 0;
            return int.Parse(str);
        }

        static float ParseFloat(string str) {
            if (str == "") return 0;
            return float.Parse(str);
        }


        static float ParsePercent(string str, char trail = '%') {
            if (str == "") return 0;
            return float.Parse(str.TrimEnd(trail)) / 100;
        }
    }


}
