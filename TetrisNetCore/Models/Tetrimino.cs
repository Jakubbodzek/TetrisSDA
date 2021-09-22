using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TetrisNetCore.Utilities;

namespace TetrisNetCore.Models
{
    public class Tetrimino
    {
        #region Właściwości

        /// <summary>
        /// Rodzaj figury
        /// </summary>
        public TetriminoKind Kind { get; set; }

        public Color Color => GetKindBlockColor(Kind);

        public Position Position { get; set; }

        public Direction Direction { get; set; }

        public List<Block> Blocks { get; set; }

        #endregion

        #region Konstruktor

        public Tetrimino(TetriminoKind kind)
        {
            Kind = kind;
            Position = InitPosition(kind);
            Blocks = CreateBlocks(Position);
        }



        #endregion

        #region Metody

        private Position InitPosition(TetriminoKind kind)
        {
            int length = 0;
            switch (kind)
            {
                case TetriminoKind.I:
                    length = 4;
                    break;
                case TetriminoKind.O:
                    length = 2;
                    break;
                case TetriminoKind.S:
                case TetriminoKind.Z:
                case TetriminoKind.J:
                case TetriminoKind.L:
                case TetriminoKind.T:
                    length = 3;
                    break;
                default:
                    throw new InvalidOperationException("Unknown Tetrimino");
            }

            int row = -length;
            int column = (Field.ColumnCount - length) / 2;
            return new Position(row, column);
        }

        internal static Tetrimino Create(TetriminoKind? kind)
        {
            kind = kind ?? RandomKind();
            return new Tetrimino(kind.Value);
        }

        public static TetriminoKind RandomKind()
        {
            return (TetriminoKind)RandomProvider.ThreadRandom.Next(6);
        }

        private List<Block> CreateBlocks(Position position, Direction direction = Direction.Up)
        {
            int[,] pattern = null;
            switch (Kind)
            {
                #region I
                case TetriminoKind.I:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new int[,]
                            {
                                { 0, 1, 0, 0 },
                                { 0, 1, 0, 0 },
                                { 0, 1, 0, 0 },
                                { 0, 1, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            pattern = new int[,]
                            {
                                { 0, 0, 0, 0 },
                                { 1, 1, 1, 1 },
                                { 0, 0, 0, 0 },
                                { 0, 0, 0, 0 },
                            };
                            break;

                        case Direction.Down:
                            pattern = new int[,]
                            {
                                { 0, 0, 1, 0 },
                                { 0, 0, 1, 0 },
                                { 0, 0, 1, 0 },
                                { 0, 0, 1, 0 },
                            };
                            break;

                        case Direction.Left:
                            pattern = new int[,]
                            {
                                { 0, 0, 0, 0 },
                                { 0, 0, 0, 0 },
                                { 1, 1, 1, 1 },
                                { 0, 0, 0, 0 },
                            };
                            break;
                    }
                    break;
                #endregion

                #region O
                case TetriminoKind.O:
                    pattern = new int[,]
                    {
                        { 1, 1 },
                        { 1, 1 },
                    };
                    break;
                #endregion

                #region S
                case TetriminoKind.S:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new int[,]
                            {
                                { 0, 1, 1 },
                                { 1, 1, 0 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 0, 1, 1 },
                                { 0, 0, 1 },
                            };
                            break;

                        case Direction.Down:
                            pattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 0, 1, 1 },
                                { 1, 1, 0 },
                            };
                            break;

                        case Direction.Left:
                            pattern = new int[,]
                            {
                                { 1, 0, 0 },
                                { 1, 1, 0 },
                                { 0, 1, 0 },
                            };
                            break;
                    }
                    break;
                #endregion

                #region Z
                case TetriminoKind.Z:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new int[,]
                            {
                                { 1, 1, 0 },
                                { 0, 1, 1 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            pattern = new int[,]
                            {
                                { 0, 0, 1 },
                                { 0, 1, 1 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Down:
                            pattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 1, 1, 0 },
                                { 0, 1, 1 },
                            };
                            break;

                        case Direction.Left:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 1, 1, 0 },
                                { 1, 0, 0 },
                            };
                            break;
                    }
                    break;
                #endregion

                #region J
                case TetriminoKind.J:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new int[,]
                            {
                                { 1, 0, 0 },
                                { 1, 1, 1 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            pattern = new int[,]
                            {
                                { 0, 1, 1 },
                                { 0, 1, 0 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Down:
                            pattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 1, 1, 1 },
                                { 0, 0, 1 },
                            };
                            break;

                        case Direction.Left:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 0, 1, 0 },
                                { 1, 1, 0 },
                            };
                            break;
                    }
                    break;
                #endregion

                #region L
                case TetriminoKind.L:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new int[,]
                            {
                                { 0, 0, 1 },
                                { 1, 1, 1 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 0, 1, 0 },
                                { 0, 1, 1 },
                            };
                            break;

                        case Direction.Down:
                            pattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 1, 1, 1 },
                                { 1, 0, 0 },
                            };
                            break;

                        case Direction.Left:
                            pattern = new int[,]
                            {
                                { 1, 1, 0 },
                                { 0, 1, 0 },
                                { 0, 1, 0 },
                            };
                            break;
                    }
                    break;
                #endregion

                #region T
                case TetriminoKind.T:
                    switch (direction)
                    {
                        case Direction.Up:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 1, 1, 1 },
                                { 0, 0, 0 },
                            };
                            break;

                        case Direction.Right:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 0, 1, 1 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Down:
                            pattern = new int[,]
                            {
                                { 0, 0, 0 },
                                { 1, 1, 1 },
                                { 0, 1, 0 },
                            };
                            break;

                        case Direction.Left:
                            pattern = new int[,]
                            {
                                { 0, 1, 0 },
                                { 1, 1, 0 },
                                { 0, 1, 0 },
                            };
                            break;
                    }
                    break;
                    #endregion
            }

            List<Block> result = Enumerable.Range(0, pattern.GetLength(0))
                                 .SelectMany(r => Enumerable.Range(0, pattern.GetLength(1)).Select(c => new Position(r, c)))
                                 .Where(x => pattern[x.Row, x.Column] == 1)
                                 .Select(x => new Position(x.Row + position.Row, x.Column + position.Column))
                                 .Select(x => new Block(Color, x))
                                 .ToList();


            return result;
        }

        public bool Move(MoveDirection direction, Func<Block, bool> checkCollision)
        {
            Position position = this.Position;
            if (direction == MoveDirection.Down)
            {
                position = new Position(position.Row + 1, position.Column);
            }
            else
            {
                int delta = direction == MoveDirection.Right ? 1 : -1;
                position = new Position(position.Row, position.Column + delta);
            }

            List<Block> blocks = CreateBlocks(position);

            if (blocks.Any(checkCollision))
            {
                return false;
            }

            this.Position = position;
            this.Blocks = blocks;
            return true;
        }

        public Color GetKindBlockColor(TetriminoKind kind)
        {
            switch (kind)
            {
                case TetriminoKind.I: return Colors.LightBlue;
                case TetriminoKind.O: return Colors.Yellow;
                case TetriminoKind.S: return Colors.YellowGreen;
                case TetriminoKind.Z: return Colors.Red;
                case TetriminoKind.J: return Colors.Blue;
                case TetriminoKind.L: return Colors.Orange;
                case TetriminoKind.T: return Colors.Purple;
            }
            throw new InvalidOperationException("Unknown Tetrimino");
        }

        public bool Rotation(RotationDirection rotationDirection, Func<Block, bool> checkCollision)
        {
            int count = Enum.GetValues(typeof(Direction)).Length;
            int delta = (rotationDirection == RotationDirection.Right) ? 1 : -1;
            int direction = (int)this.Direction + delta;
            if (direction > 0) direction += count;
            if (direction >= count) direction %= count;

            int[] adjustPattern = Kind == TetriminoKind.I
                ? new[] { 0, 1, -1, 2, -2 }
                : new[] { 0, 1, -1 };

            foreach (int adjust in adjustPattern)
            {
                Position position = new Position(Position.Row, Position.Column + adjust);
                var blocks = CreateBlocks(position, (Direction)direction);

                if (!blocks.Any(checkCollision))
                {
                    Direction = (Direction)direction;
                    Position = position;
                    Blocks = blocks;
                    return true;
                }
            }
            return false;
        }


        #endregion
    }
}
