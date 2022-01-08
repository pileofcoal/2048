using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board board = new Board(4);

        public MainWindow()
        {
            InitializeComponent();
            UpdateBoard();
            MainBorder.Background = new SolidColorBrush(Color.FromArgb(255, 205, 193, 180)); 
        }

        private void MainGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }


        Timer t1 = new Timer();

        private  void UpdateBoard()
        {
            MainGrid.Children.Clear();

            byte[,] colors = new byte[,] { { 255, 238, 228, 218 }, {255, 238,225,201}, {255,242,181,126 },{255,246,151,101},{255,247,128,96}, {255,247,100,64},{ 255, 237, 208, 115 }, {255,238,201,80 } };
            byte[,] txtcolors = new byte[,] { { 255, 119, 110, 101 }, { 255, 255, 246, 242 } };
            int colorType = 0;
            int txtcolorType = 0;

            foreach (Tile tile in board.board)
            {
                if(tile.Value == 2)
                {
                    colorType = 0;
                    txtcolorType = 0;

                } 
                else if (tile.Value == 4) 
                {
                    colorType = 1;
                    txtcolorType = 0;
                }
                else if (tile.Value == 8)
                {
                    colorType = 2;
                    txtcolorType = 1;
                }
                else if (tile.Value == 16)
                {
                    colorType = 3;
                    txtcolorType = 1;
                }
                else if (tile.Value == 32)
                {
                    colorType = 4;
                    txtcolorType = 1;
                }
                else if (tile.Value == 64)
                {
                    colorType = 5;
                    txtcolorType = 1;
                }
                else if (tile.Value == 128)
                {
                    colorType = 6;
                    txtcolorType = 1;
                }
                else if (tile.Value == 256)
                {
                    colorType = 6;
                    txtcolorType = 1;
                }
                else if (tile.Value == 512)
                {
                    colorType = 7;
                    txtcolorType = 1;
                }
                else if (tile.Value == 1024)
                {
                    colorType = 7;
                    txtcolorType = 1;
                }
                else if (tile.Value == 2048)
                {
                    colorType = 7;
                    txtcolorType = 1;
                } else
                {
                    colorType = 7;
                    txtcolorType = 1;
                }


                if (tile.Value > 0)
                {
                    Button button = new Button();

                    button.Background = new SolidColorBrush(Color.FromArgb(colors[colorType, 0], colors[colorType, 1], colors[colorType, 2], colors[colorType, 3]));
                    WrapPanel pnl = new WrapPanel();

                    Style style = new Style(typeof(Border));
                    
                    style.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(7)));

                    button.BorderThickness = new Thickness(0);

                    button.Resources.Add(typeof(Border), style);
        
                    button.Margin = new Thickness(4);

                    TextBlock txt = new TextBlock();
                    txt.Text = tile.Value.ToString();
                    txt.Foreground = new SolidColorBrush(Color.FromArgb(txtcolors[txtcolorType, 0], txtcolors[txtcolorType, 1], txtcolors[txtcolorType, 2], txtcolors[txtcolorType, 3]));
                    txt.FontSize = 32;
                    txt.FontWeight = FontWeights.Bold;  
                    pnl.Children.Add(txt);

                    button.Content = pnl;



                    Grid.SetRow(button, tile.Row);
                    Grid.SetColumn(button, tile.Column);
                    
                    if(tile.animate == Animation.spawn)
                    {
                        DoubleAnimation animation = new DoubleAnimation();
                        animation.To = 1;
                        animation.From = 0;
                        animation.Duration = TimeSpan.FromMilliseconds(500);
                        animation.EasingFunction = new QuadraticEase();

                        Storyboard sb = new Storyboard();
                        sb.Children.Add(animation);

                        button.Opacity = 1;
                        button.Visibility = Visibility.Visible;

                        Storyboard.SetTarget(sb, button);
                        Storyboard.SetTargetProperty(sb, new PropertyPath(Control.OpacityProperty));

                        sb.Begin();
                        tile.animate = Animation.left;

                    }


                    if (tile.animate == Animation.combine)
                    {
                        var animation = new ThicknessAnimation()
                        {
                            From = new Thickness(7, 7, 7, 7),
                            To = new Thickness(0, 0, 0, 0),
                            FillBehavior = FillBehavior.Stop,
                            AutoReverse = true,
                            Duration = TimeSpan.FromSeconds(0.15),
                        };





                        Storyboard sb = new Storyboard();
                        sb.Children.Add(animation);
                        button.Visibility = Visibility.Visible;
                       

                        Storyboard.SetTarget(sb, button);
                        Storyboard.SetTargetProperty(sb, new PropertyPath(Control.MarginProperty));

                        sb.Begin();
                        
                        tile.animate = Animation.none;
                    }

                        MainGrid.Children.Add(button);



                }
            }
        }


        private void MainGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.A)
            {
                board.Move(Direction.left);

            }
            if (e.Key == Key.D)
            {
                board.Move(Direction.right);
            }

            if (e.Key == Key.W)
            {
                board.Move(Direction.up);
            }
            if (e.Key == Key.S)
            {
                board.Move(Direction.down);
            }

            if (e.Key == Key.Escape)
            {
                board = new Board(4);
                UpdateBoard();
            }

            if (board.YouWin)
            {
                MessageBox.Show("You did it!!!");
                
                board.YouWin = false;
                board.AddRandomTile();
                UpdateBoard();
            } 
            else if (e.Key != Key.Escape)
            {
                board.isFirstHit = true;
                board.AddRandomTile();
                UpdateBoard();
            }

        }
    }
}
