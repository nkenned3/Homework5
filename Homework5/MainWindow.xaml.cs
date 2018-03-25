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
using System.Collections;

namespace Homework5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public enum Type { X, O};
    public partial class MainWindow : Window
    {
        private int counter = 0;
        public Player PlayerX { get; set; }
        public Player PlayerO { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            RefreshGame();

        }
        private void RefreshGame()
        {
            PlayerX = new Player();
            PlayerO = new Player();
            PlayerX.Type = Type.X;
            PlayerO.Type = Type.O;
            counter = 0;
        }
        private void uxNewGame_Click(object sender, RoutedEventArgs e)
        {
            RefreshGame();
            var child = uxGrid.Children.OfType<Button>();
            List<Button> list = child.ToList<Button>();
            foreach (var item in list)
            {
                item.Content = "";
                item.IsEnabled = true;
            }
            uxGrid.IsEnabled = true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            var button = (Button)sender;
            button.IsEnabled = false;
            var tag = button.Tag.ToString();
            
            var tagArr = tag.Split(',');
            Player current = new Player();
            if (counter % 2 == 0)
            {
                button.Content = "O";
                PlayerO.Rows.Add(tagArr[0]);
                PlayerO.Columns.Add(tagArr[1]);
                PlayerO.TagString += tag;
                current = PlayerO;
            }
            else
            {
                button.Content = "X";
                PlayerX.Rows.Add(tagArr[0]);
                PlayerX.Columns.Add(tagArr[1]);
                PlayerX.TagString += tag;
                current = PlayerX;
            }
            string currentType = Enum.GetName(typeof(Type), current.Type);
            uxTurn.Text = currentType + "'s turn";
            if (IsWinner(current))
            {                
                MessageBox.Show(currentType + " wins!");
                uxGrid.IsEnabled = false;
            }
            if (counter == 9)
            {
                MessageBox.Show("It's a tie! Also known as cat :3\r\nLet's start a new game!");
                uxNewGame_Click(null, null);
            }
        }
        public bool IsWinner(Player play)
        {
            if (play.Rows.Count < 3)
            {
                return false;
            }
            var list = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                var rows = play.Rows.Where(r => r == i.ToString());
                list = rows.ToList();
                if (list.Count >= 3)
                {
                    return true;
                }
                var columns = play.Columns.Where(r => r == i.ToString());
                list = columns.ToList();
                if (list.Count >= 3)
                {
                    return true;
                }
            }
            var rowInts = play.Rows.Select(int.Parse).ToList();
            var colInts = play.Columns.Select(int.Parse).ToList();
            if (colInts.Sum() == 3 && rowInts.Sum() == 3)
            {
                return true;
            }
            return false;
        }
        public class Player
        {
            public List<string> Rows { get; set; }
            public List<string> Columns {get; set;}
            public Type Type { get; set; }
            public String TagString { get; set; }

            public Player()
            {
                Rows = new List<string>();
                Columns = new List<string>();
                TagString = "";
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
