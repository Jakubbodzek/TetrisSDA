using Reactive.Bindings;
using System.Windows.Media;

namespace TetrisNetCore.ViewModels
{
    public class CellViewModel
    {
        public ReactiveProperty<Color> Color { get; set; } = new ReactiveProperty<Color>();
    }
}
