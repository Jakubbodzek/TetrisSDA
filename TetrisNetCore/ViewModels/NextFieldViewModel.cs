using System;
using Reactive.Bindings;
using System.Reactive.Linq;
using System.Windows.Media;
using TetrisNetCore.Extensions;
using TetrisNetCore.Models;
using System.Collections.Generic;

namespace TetrisNetCore.ViewModels
{
    public class NextFieldViewModel
    {
        #region Stałe

        private const byte RowCount = 5;
        
        private const byte ColumnCount = 5;

        #endregion


        #region Właściwości

        public CellViewModel[,] Cells { get; set; }

        public Color BackgroundColor => Colors.WhiteSmoke;

        #endregion

        #region Konstruktor

        public NextFieldViewModel(IReactiveProperty<TetriminoKind> nextTetrimino)
        {
            this.Cells = new CellViewModel[RowCount, ColumnCount];

            foreach (var item in this.Cells.WithIndex())
            {
                this.Cells[item.X, item.Y] = new CellViewModel();
            }

            nextTetrimino
                .Select(x => Tetrimino.Create(x).Blocks.ToDictionary2(y => y.Position.Row, y => y.Position.Column))
                .Subscribe(x => {
                    Position offset = new Position((-6 - x.Count) / 2, 2);

                    foreach (var item in Cells.WithIndex())
                    {
                        Color color = x.GetValueOrDefault(item.X + offset.Row)?.GetValueOrDefault(item.Y + offset.Column)
                                      ?.Color ?? this.BackgroundColor;

                        item.Element.Color.Value = color;
                    }
                });
        }

        #endregion
    }
}