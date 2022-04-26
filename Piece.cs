using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace 五子棋
{
    public abstract class Piece : PictureBox
    {
        int image_widthh = 50;
        
        public Piece(int x, int y)
        {
            this.BackColor = Color.Transparent;
            this.Location = new Point(x - image_widthh / 2, y - image_widthh / 2);
            this.Size = new Size(image_widthh, image_widthh);
        }

        public abstract PieceType GetPieceType();
    }
}
