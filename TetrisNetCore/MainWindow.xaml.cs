using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using TetrisNetCore.Extensions;
using TetrisNetCore.ViewModels;

namespace TetrisNetCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml - Logika interakcji
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Właściwości

        public GameViewModel Game
        {
            get { return this.DataContext as GameViewModel; }
            set { this.DataContext = value; }
        }

        #endregion

        #region Konstruktor

        /// <summary>
        /// Utwórz instancję.
        /// </summary>
        public MainWindow()
        {
            this.Game = new GameViewModel();
            this.InitializeComponent();

            SetupField(this.field, this.Game.Field.Cells, 30);
            SetupField(this.nextField, this.Game.NextField.Cells, 18);
            AttachEvents();
            Game.Play();
        }

        #endregion

        #region Inicjalizacja

        private void SetupField(Grid field, CellViewModel[,] cells, byte blockSize)
        {
            for (int r = 0; r < cells.GetLength(0); r++)
            {
                field.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(blockSize, GridUnitType.Pixel)
                });
            }

            for (int c = 0; c < cells.GetLength(1); c++)
            {
                field.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(blockSize, GridUnitType.Pixel)
                });
            }

            foreach (IndexedItem2<CellViewModel> item in cells.WithIndex())
            {
                SolidColorBrush brush = new SolidColorBrush();
                TextBlock control = new TextBlock
                {
                    DataContext = item.Element,
                    Background = brush,
                    Margin = new Thickness(1)
                };

                BindingOperations.SetBinding(brush, SolidColorBrush.ColorProperty, new Binding("Color.Value"));

                Grid.SetRow(control, item.X);
                Grid.SetColumn(control, item.Y);
                field.Children.Add(control);
            }
        }

        private void AttachEvents()
        {
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: this.Game.Field.RotationTetrimino(Models.RotationDirection.Right); break;
                case Key.Z: this.Game.Field.RotationTetrimino(Models.RotationDirection.Left); break;
                case Key.X: this.Game.Field.RotationTetrimino(Models.RotationDirection.Right); break;
                
                case Key.Right: this.Game.Field.MoveTetrimino(Models.MoveDirection.Right); break;
                case Key.Left: this.Game.Field.MoveTetrimino(Models.MoveDirection.Left); break;
                case Key.Down: this.Game.Field.MoveTetrimino(Models.MoveDirection.Down); break;
                
                case Key.Escape: this.Game.Play(); break;
            }
        }

        #endregion
    }
}
