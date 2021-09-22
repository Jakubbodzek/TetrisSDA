using Reactive.Bindings;
using TetrisNetCore.Models;

namespace TetrisNetCore.ViewModels
{
    public class GameResultViewModel
    {
        public GameResultViewModel(GameResult result)
        {
            Result = result;
        }

        public GameResult Result { get; }

        public IReactiveProperty<int> TotalRowCount => this.Result.TotalRowCount;
    }
}