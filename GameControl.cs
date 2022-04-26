using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋
{
    class GameControl
    {
        //生成物件Board模组物件board
        private Board board = new Board();
        //当前棋子颜色
        private PieceType currentType = PieceType.Black;
        //通过getGameResultType取得棋子颜色
        PieceType getGameResultType = PieceType.None;
        public PieceType gameRestult { get { return getGameResultType; } }
        //Board的placeAPiece 传回1个piece
        public Piece placeAPiece(int x, int y)
        {
            Piece piece = board.PlaceAPiece(x, y, currentType);//currentType Board.PlaceAPiece需要一个type

            if (piece != null)
            {
                CheckResult();

                if (currentType == PieceType.Black)
                    currentType = PieceType.White;
                else if (currentType == PieceType.White)
                    currentType = PieceType.Black;

                return piece;
            }
            return null;
        }
        //Board的是否可以放置 传回值(提供给form1传回值切换显示鼠标)
        public bool isCanPlace(int x, int y)
        {
            return Board.isCanPlace(x, y);
        }
        //检查条件
        private void CheckResult()
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    //指派的值不影响显示的结果？？？
                    int count = 0;
                    int count2 = 0;

                    while (count < 5)
                    {
                        int targetX = board.lastPiece.X + x * count;
                        int targetY = board.lastPiece.Y + y * count;

                        //边界9怎么处理board.noderange
                        if (targetX < 0 || targetX >= 9 ||
                            targetY < 0 || targetY >= 9 ||
                            board.getPieceType(targetX, targetY) != currentType)
                            break;
                        //break所以会再执行一次回圈外？
                        count++;
                    }
                    if (count == 5)
                        getGameResultType = currentType;
                    //因为2个棋子所以-1？
                    while (count + count2 -1 < 5)
                    {
                        int targetX = board.lastPiece.X - x * count2;
                        int targetY = board.lastPiece.Y - y * count2;

                        if (targetX < 0 || targetX >= 9 ||
                            targetY < 0 || targetY >= 9 ||
                            board.getPieceType(targetX, targetY) != currentType)
                            break;

                        count2++;
                    }
                    if (count + count2 - 1 == 5)
                        getGameResultType = currentType;
                }
            }
        }
    }
}
