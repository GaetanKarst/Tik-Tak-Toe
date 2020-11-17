using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public variables
        public enum MarkType { Free, Nought, Cross} //default value is Free as it's the 0 element

        //private variables
        private MarkType[] _results; // holds the current result in the cells

        private bool _player1Turn;
        private bool _gameEnded;


        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }
        #endregion

        //Starts a new game and resets all values to default
        private void NewGame()
        {
            //Creates a new blank array of free cells
            _results = new MarkType[9];

            for (int i = 0; i < _results.Length; i++)
            {
                _results[i] = MarkType.Free;
            }

            _player1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //Make sure the game hasn't ended
            _gameEnded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_gameEnded)
            {
                NewGame();
                return;
            }

            
            var button = (Button)sender;

            //Finds the button in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if (_results[index] != MarkType.Free)
            {
                return;
            }

            //Set the cell value based on which player turn it is
            _results[index] = _player1Turn ? MarkType.Cross : MarkType.Nought;
            button.Content = _player1Turn ? "X" : "O";

            if (!_player1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            _player1Turn ^= true;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            #region Lines & Columns variable
            var same = (_results[0] & _results[1] & _results[2]) == _results[0];
            var sameCol = (_results[0] & _results[3] & _results[6]) == _results[0];
            var sameDiag = (_results[0] &_results[4] & _results[8]) == _results[0];

            var same2 = (_results[3] & _results[4] & _results[5]) == _results[3];
            var same2Col = (_results[1] & _results[4] & _results[7]) == _results[1];
            var same2Diag = (_results[2] & _results[4] & _results[6]) == _results[2];

            var same3 = (_results[6] & _results[7] & _results[8]) == _results[6];
            var same3Col = (_results[2] & _results[5] & _results[8]) == _results[2];
            #endregion

            #region Horizontal win
            //Row 1
            if (same && _results[0] != MarkType.Free)
            {
                _gameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //Row 2
            if (same2 && _results[3] != MarkType.Free)
            {
                _gameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //Row 3
            if (same3 && _results[6] != MarkType.Free)
            {
                _gameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical win
            //Column1
            if (sameCol && _results[0] != MarkType.Free)
            {
                _gameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //Column2
            if (same2Col && _results[1] != MarkType.Free)
            {
                _gameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //Column3
            if (same3Col && _results[2] != MarkType.Free)
            {
                _gameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal win
            //Top Left Diagonal
            if (sameDiag && _results[0] != MarkType.Free)
            {
                _gameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            //Top Right Diagonal
            if (same2Diag && _results[2] != MarkType.Free)
            {
                _gameEnded = true;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion

            if (!_results.Any(result => result == MarkType.Free))
            {
                _gameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
        }
    }
}
