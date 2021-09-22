using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisNetCore.Models;

namespace TetrisNetCore.ViewModels
{
    public class GameViewModel
    {
        #region Właściwości
        
        private Game Game { get; } = new Game();

        public GameResultViewModel Result { get; set; }

        public FieldViewModel Field { get; set; }

        public NextFieldViewModel NextField { get; set; }

        #endregion

        #region Konstruktor

        public GameViewModel()
        {
            this.Result = new GameResultViewModel(this.Game.Result);
            this.Field = new FieldViewModel(this.Game.Field);
            this.NextField = new NextFieldViewModel(this.Game.NextTetrimino);
        }

        #endregion

        #region Metody

        public void Play()
        {
            this.Game.Play();
        }

        #endregion
    }
}
