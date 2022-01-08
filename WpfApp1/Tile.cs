using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    enum Animation
    {
        up = 0,
        down = 1,
        left = 2,
        right = 3,
        spawn = 4,
        combine = 5,
        none = 6,
    }

    internal class Tile
    {
        public static int _lastValue = 0;
        public Animation animate { get; set; }  

        public int Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }


        public Tile(int row, int column,int value)
        {
            Value = value;
            Row = row;
            Column = column;

        }


        public Tile(int row, int column) 
        {
            Row = row;
            Column = column;
            Value = randomTileValue();
            animate = Animation.spawn;
        }


        protected int randomTileValue()
        {
            Random random = new Random();

            double rnd = random.NextDouble();

            int num = 0;

            if (_lastValue == 4)
            {
               num = 2;
               
            } 
            else if(rnd > 0.70)
            {
                num = 4;
            } 
            else
            {
                num = 2;
            }

            _lastValue = num;
            return num;

        }
    }
}
