using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisNetCore.Models
{
    public class GameResult
    {
        public IReactiveProperty<int> TotalRowCount { get; set; }

        public GameResult()
        {
            TotalRowCount = new ReactiveProperty<int>(0);
        }

        public void AddRowCount(int count)
        {
            TotalRowCount.Value += count;
        }
    }
}
