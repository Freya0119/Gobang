using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace 五子棋
{
    public class Board
    {
        //格子长度 棋盘与边框距离 变手半径
        private static readonly int distance = 75;
        private static readonly int offset = 75;
        private static readonly int radius = 10;
        //无效的point 为了传回
        private static readonly Point noPoint = new Point(-1, -1);
        //棋盘阵列范围9
        private static readonly int nodeRange = 9;
        //宣告棋子点范围为[nodeRange = 9, nodeRange =9]
        private Piece[,] pieces = new Piece[nodeRange, nodeRange];
        //取得最后一个棋子的point[x, y]
        public Point getLastPiece = noPoint;
        public Point lastPiece { get { return getLastPiece; } }

        //棋子位置从像素转换为坐标[x, y]
        private static int findPointPosition(int position)
        {
            if (position < offset)
                return -1;

            position -= offset;

            int quotient = position / distance;
            int remainder = position % distance;

            if (remainder <= radius)
                return quotient;
            else if (remainder >= distance - radius)
                return quotient + 1;
            else
                return -1;
        }
        //取得除商findPointPosition()以后的point 可看作[x, y]
        private static Point findPoint(int x, int y)
        {
            int pointX = findPointPosition(x);
            int pointY = findPointPosition(y);

            if (pointX == -1 || pointX > 9)
                return noPoint;
            if (pointY == -1 || pointX > 9)
                return noPoint;

            return new Point(pointX, pointY);
        }
        //bool是否可以下子
        internal static bool isCanPlace(int x, int y)
        {
            Point nodePosition = findPoint(x, y);

            if (nodePosition == noPoint)
                return false;
            else
                return true;
        }
        //还原点实际的像素距离 从point到piece
        private Point ConvertPosition(Point nodePosition)
        {
            Point trans = new Point
            {
                X = nodePosition.X * distance + offset,
                Y = nodePosition.Y * distance + offset
            };

            return trans;
        }
        //放置棋子 对应的point[x, y]放置对应坐标的棋子
        public Piece PlaceAPiece(int x, int y, PieceType type)
        {
            Point nodePosition = findPoint(x, y);
            Point transPosition = ConvertPosition(nodePosition);


            if (nodePosition == noPoint)
                return null;

            if (pieces[nodePosition.X, nodePosition.Y] != null)
                return null;

            if (type == PieceType.Black)
                pieces[nodePosition.X, nodePosition.Y] = new BlackPiece(transPosition.X, transPosition.Y);
            else if (type == PieceType.White)
                pieces[nodePosition.X, nodePosition.Y] = new WhitePiece(transPosition.X, transPosition.Y);

            getLastPiece = nodePosition;

            return pieces[nodePosition.X, nodePosition.Y];
        }
        //取得棋子颜色 连结piece class里面的override
        public PieceType getPieceType(int x, int y)
        {
            if (pieces[x, y] == null)
                return PieceType.None;
            else
                return pieces[x, y].GetPieceType();
        }
    }
}
