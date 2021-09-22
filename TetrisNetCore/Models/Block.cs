using System.Windows.Media;

namespace TetrisNetCore.Models
{
    public class Block
    {
        public Color Color { get; set; }

        public Position Position { get; set; }

        public Block(Color color, Position position)
        {
            Color = color;
            Position = position;
        }
    }
}