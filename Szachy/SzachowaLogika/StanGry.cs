using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachowaLogika
{
    public class StanGry
    {
        public Plansza Plansza { get; }
        public Player CurrentPlayer { get; private set; }

        public StanGry(Player player, Plansza plansza)
        {
            CurrentPlayer = player;
            Plansza = plansza; 
        }
    }
}
