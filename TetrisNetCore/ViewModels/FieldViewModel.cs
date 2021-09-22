using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Media;
using TetrisNetCore.Extensions;
using TetrisNetCore.Models;

namespace TetrisNetCore.ViewModels
{
    public class FieldViewModel
    {
        #region Właściwości

        public CellViewModel[,] Cells { get; set; }

        private Color BackgroundColor => Colors.WhiteSmoke;

        public Field Field { get; }

        #endregion

        #region Konstruktor

        public FieldViewModel(Field field)
        {
            Field = field;

            this.Cells = new CellViewModel[Field.RowCount, Field.ColumnCount];

            foreach (var item in this.Cells.WithIndex())
            {
                this.Cells[item.X, item.Y] = new CellViewModel();
            }

            this.Field.Tetrimino.CombineLatest
                (
                this.Field.PlacedBlocks,
                (t, p) => (t == null ? p : p.Concat(t.Blocks))
                .ToDictionary2(x => x.Position.Row, x => x.Position.Column)
                )
                .Subscribe(x => {

                    foreach (var item in Cells.WithIndex())
                    {
                        Color color = x.GetValueOrDefault(item.X)?.GetValueOrDefault(item.Y)
                                      ?.Color ?? this.BackgroundColor;

                        item.Element.Color.Value = color;
                    }
                });
        }

        #endregion

        #region Metody

        public void MoveTetrimino(MoveDirection direction) => Field.MoveTetrimino(direction);

        public void RotationTetrimino(RotationDirection direction) => Field.RotationTetrimino(direction);

        #endregion

    }
}