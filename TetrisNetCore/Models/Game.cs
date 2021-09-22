using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisNetCore.Models
{
    public class Game
    {
        public GameResult Result { get; internal set; } = new GameResult();
        public Field Field { get; internal set; } = new Field();
        public ReactiveProperty<TetriminoKind> NextTetrimino { get; set; } = new ReactiveProperty<TetriminoKind>();
        public ReactiveProperty<bool> IsPlaying => this.Field.IsActivated.ToReactiveProperty();

        public Game()
        {
            this.Field.PlacedBlocks.Subscribe(_ => {

                TetriminoKind kind = this.NextTetrimino.Value;
                NextTetrimino.Value = Tetrimino.RandomKind();
                Field.Tetrimino.Value = Tetrimino.Create(kind);
            });

            Field.LastRemovedRowCount.Subscribe(x => Result.TotalRowCount.Value += x);
        }

        internal void Play()
        {
            if (IsPlaying.Value)
                return;

            NextTetrimino.Value = Tetrimino.RandomKind();
            Field.Activate(Tetrimino.RandomKind());
            Result.TotalRowCount.Value = 0;
        }
    }
}
