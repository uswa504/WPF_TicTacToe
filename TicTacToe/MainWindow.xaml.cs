using System;
using System.Collections.Generic;
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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
    #region Private Members
        private Mark_Type[] results;
        private bool player_turn;
        private bool game_end; 
    #endregion
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }
    #endregion
        private void NewGame()
        {
            results = new Mark_Type[9];
            for (var i = 0; i < results.Length; i++)
                results[i] = Mark_Type.Free;
            player_turn = true;
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
            });
            game_end = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (game_end)
            {
                NewGame();
                return;
            }
            var btn = (Button) sender;
            var cols = Grid.GetColumn(btn);
            var rows = Grid.GetRow(btn);
            var index = cols + (rows * 3);
            if (results[index] != Mark_Type.Free)
                return;
            btn.Foreground = Brushes.SkyBlue;
            results[index] = player_turn ? Mark_Type.Cross : Mark_Type.Noughts;
            btn.Content = player_turn ? "X" : "O";

            if (!player_turn)
            {
                btn.Foreground = Brushes.Red;
            }
            player_turn ^= true;
            Check_Winner();
        }
        public void Check_Winner(){
            #region Horizontal Wins
            if (results[0] != Mark_Type.Free && (results[0] & results[1] & results[2]) == results[0])
            {
                game_end = true;
                btn_0.Background = btn_1.Background = btn_2.Background = Brushes.Black;
            }
            if (results[3] != Mark_Type.Free && (results[3] & results[4] & results[5]) == results[3])
            {
                game_end = true;
                btn_3.Background = btn_4.Background = btn_5.Background = Brushes.Black;
            }
            if (results[6] != Mark_Type.Free && (results[6] & results[7] & results[8]) == results[6])
            {
                game_end = true;
                btn_6.Background = btn_7.Background = btn_8.Background = Brushes.Black;
            }

            #endregion

            #region Vertical Wins
            if (results[0] != Mark_Type.Free && (results[0] & results[3] & results[6]) == results[0])
            {
                game_end = true;
                btn_0.Background = btn_3.Background = btn_6.Background = Brushes.Black;
            }
            if (results[1] != Mark_Type.Free && (results[1] & results[4] & results[7]) == results[1])
            {
                game_end = true;
                btn_1.Background = btn_4.Background = btn_7.Background = Brushes.Black;
            }
            if (results[2] != Mark_Type.Free && (results[2] & results[5] & results[8]) == results[2])
            {
                game_end = true;
                btn_2.Background = btn_5.Background = btn_8.Background = Brushes.Black;
            }
            #endregion
            #region Diagonal Wins
            if (results[0] != Mark_Type.Free && (results[0] & results[4] & results[8]) == results[0])
            {
                game_end = true;
                btn_0.Background = btn_4.Background = btn_8.Background = Brushes.Black;
            }
            if (results[2] != Mark_Type.Free && (results[2] & results[4] & results[6]) == results[2])
            {
                game_end = true;
                btn_6.Background = btn_4.Background = btn_2.Background = Brushes.Black;
            }
            #endregion
            #region No Winners
            if (!results.Any(f => f == Mark_Type.Free)){
                game_end = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion
        }

    }
}
