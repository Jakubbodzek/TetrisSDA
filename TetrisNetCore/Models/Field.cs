using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TetrisNetCore.Extensions;

namespace TetrisNetCore.Models
{
    public class Field
    {
        public const int RowCount  = 24;
        public const int ColumnCount = 10;


        #region Właściwości

        public ReactiveProperty<List<Block>> PlacedBlocks { get; set; }

        public ReactiveProperty<Tetrimino> Tetrimino { get; set; } = new ReactiveProperty<Tetrimino>();

        public ReactiveProperty<bool> IsActivated { get; set; } 
            = new ReactiveProperty<bool>(mode: ReactivePropertyMode.DistinctUntilChanged);

        public ReactiveProperty<bool> IsUpperLimitOvered { get; set; } 
            = new ReactiveProperty<bool>(mode: ReactivePropertyMode.DistinctUntilChanged);

        public ReactiveProperty<int> LastRemovedRowCount { get; set; } = new ReactiveProperty<int>();

        private Timer Timer { get; set; } = new Timer();

        #endregion

        #region Konstruktor

        public Field()
        {
            PlacedBlocks = new ReactiveProperty<List<Block>>(new List<Block>(), ReactivePropertyMode.RaiseLatestValueOnSubscribe);

            Timer.Elapsed += Timer_Elapsed;
        }

        #endregion


        #region Metody

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            MoveTetrimino(MoveDirection.Down);
        }

        public void Activate(TetriminoKind kind)
        {
            this.IsActivated.Value = true;
            this.IsUpperLimitOvered.Value = false;
            this.Tetrimino.Value = Models.Tetrimino.Create(kind);
            this.PlacedBlocks.Value = new List<Block>();
            Timer.Interval = 1000; // 1s
            Timer.Start();
        }

        public void RotationTetrimino(RotationDirection direction)
        {
        }

        public void MoveTetrimino(MoveDirection direction)
        {
            if (IsActivated.Value == false)
                return;

            if (direction == MoveDirection.Down)
            {
                Timer.Stop();
                if (Tetrimino.Value.Move(direction, CheckCollision))
                    Tetrimino.ForceNotify();
                else
                    FixTetrimino();

                Timer.Start();
            }
            else
            {
                if (Tetrimino.Value.Move(direction, CheckCollision))
                    Tetrimino.ForceNotify();
            }
        }

        private void FixTetrimino()
        {
            Tuple<int, Block[]> result = RemoveAndFixBlock();

            int removedRowCount = result.Item1;
            if (removedRowCount > 0)
                this.LastRemovedRowCount.Value = removedRowCount;

            if (result.Item2.Any(x => x.Position.Row < 0))
            {
                IsActivated.Value = false;
                IsUpperLimitOvered.Value = true;
                return;
            }

            this.Tetrimino.Value = null;
            PlacedBlocks.Value = result.Item2.ToList();

        }

        private Tuple<int, Block[]> RemoveAndFixBlock()
        {
            var rows = PlacedBlocks.Value
                .Concat(Tetrimino.Value.Blocks)
                .GroupBy(x => x.Position.Row)
                .Select(x => new 
                            {
                                Row = x.Key,
                                IsFilled = ColumnCount <= x.Count(),
                                Blocks =x
                            }
                ).ToArray();

            Block[] blocks = rows
                .OrderByDescending(x => x.Row)
                .WithIndex(x => x.IsFilled)
                .Where(x => !x.Element.IsFilled)
                .SelectMany(x =>
                {
                    if (x.Index == 0)
                        return x.Element.Blocks;

                    return x.Element.Blocks.Select(y =>
                    {
                        Position position = new Position(y.Position.Row + x.Index, y.Position.Column);
                        return new Block(y.Color, position);
                    });
                }).ToArray();

            int removedRowCount = rows.Count(x => x.IsFilled);
            return Tuple.Create(removedRowCount, blocks);
        }

        private bool CheckCollision(Block block)
        {
            if (block == null)
                throw new ArgumentNullException(nameof(block));

            if (block.Position.Column < 0)
                return true;

            if (block.Position.Column >= ColumnCount)
                return true;

            if (block.Position.Row >= RowCount)
                return true;

            //TODO: nadpisać operator ==
            if (PlacedBlocks.Value.Any(x => x.Position.Row == block.Position.Row && x.Position.Column == block.Position.Column))
                return true;

            return false;
        }

        #endregion
    }
}
