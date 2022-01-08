using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    enum Direction {
        up =  0,
        down = 1,
        left = 2,
        right = 3,
    }

    internal class Board
    {
        public int Size 
        {
            get;
            set;
        }

        public bool isFirstHit { get; set; }
        public bool GameOver { get; private set; }
        public bool YouWin { get; set; }

        private int[,] direction;


        public Tile[,] board { get; }

        public Board(int size)
        {
            Size = size;
            board = new Tile[Size, Size];
            direction = new int[,] { { -1, 0, 0 }, { 1, 0, size -1 }, { 0, -1, 0 }, { 0, 1,  size -1 } };
            StartBoard();
        }


        public void Move(Direction dir)
        {
           List<Tile> Ti = new List<Tile>();

            for (int j = 0; j < Size; j++)
            {
                foreach (Tile t in board)
                {
                    int moveRow = direction[(int)dir, 0];
                    int moveColumn = direction[(int)dir, 1];
                    int startPoint = direction[(int)dir, 2];


                    if ((moveColumn == 0 ? t.Row : t.Column) != startPoint)
                    {
                           Tile nextTile = board[t.Row + moveRow, t.Column + moveColumn];

                            if (nextTile.Value == 0)
                            {
                                nextTile.Value = board[t.Row, t.Column].Value;
                                t.Value = 0;
                            } 
                            else if(nextTile.Value == t.Value && isFirstHit)
                            {
                                nextTile.Value += board[t.Row, t.Column].Value;
                                t.Value = 0;
                                nextTile.animate = Animation.combine;
                                if(nextTile.Value == 2048)
                                {
                                    YouWin = true;
                                }
                            }
                    }
                }
                isFirstHit = false;

            }
            
        }




        private void StartBoard()
         {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Tile t = new Tile(i, j, 0);

                    board[i, j] = t;
                  
                }
            }
   
            AddRandomTile();
            AddRandomTile();
        }

        public void AddRandomTile()
        {
            List<Tile> EmptyTiles = new List<Tile>();

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i,j].Value == 0)
                    {
                        Tile t = new Tile(i, j, 0);

                        EmptyTiles.Add(t);
                    }


                }
            }

            if (EmptyTiles.Count() != 0)
            {
                Random rnd = new Random();
                int select = rnd.Next(EmptyTiles.Count());

                Tile t = new Tile(EmptyTiles[select].Row, EmptyTiles[select].Column);

                EmptyTiles.RemoveAt(select);

                board[t.Row, t.Column] = t;
            } 


        }

    }
}
