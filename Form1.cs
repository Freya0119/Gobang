using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 五子棋
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        GameControl game = new GameControl();
        
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Piece piece = game.placeAPiece(e.X, e.Y);

            if (piece != null)
                this.Controls.Add(piece);

            if (game.gameRestult == PieceType.Black)
                MessageBox.Show("BlackWin");
            else if (game.gameRestult == PieceType.White)
                MessageBox.Show("WhiteWin");
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (game.isCanPlace(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
