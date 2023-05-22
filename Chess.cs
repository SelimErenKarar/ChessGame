using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace ProjectBasedLearning3
{
    class Program
    {
        static string filePath = "D:\\Chess.txt";
        static int[,] board = new int[8, 8];
        static int cursorX = 40, cursorY = 16; static int boardx = 7; static int boardy = 7;
        static int moveCounter = 0;
        static int blueLeftRook = 0; static int blueRightRook = 0; static int blueKing = 0;
        static int redLeftRook = 0; static int redRightRook = 0; static int redKing = 0;
        static int controlBlueLeftRook = 0; static int controlBlueRightRook = 0;
        static int controlRedLeftRook = 0; static int controlRedRightRook = 0;
        static bool enpassantleft = true; static bool enpassantright = true;

        static void PrintingFrame() // Printing chess board
        {
            int x, y;
            // + symbols and - signs
            y = 1;
            for (int i = 0; i < 2; i++)
            {
                x = 3;
                for (int j = 0; j < 41; j++)
                {
                    if (x == 3 || x == 43)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("+");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write("-");
                    }

                    x++;
                }
                //Thread.Sleep(30);
                y += 16;
            }
            // | symbols       
            x = 3;
            for (int i = 0; i < 2; i++)
            {
                y = 2;
                for (int j = 0; j < 15; j++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("|");
                    y++;
                }
                //Thread.Sleep(30);
                x += 40;
            }
            string alphabet = "abcdefgh";
            x = 5; y = 2;
            for (int i = 0; i < 8; i++) // numbers
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(1, y);
                Console.Write(8 - i);
                y += 2;
            }
            for (int i = 0; i < alphabet.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.SetCursorPosition(x, 18);
                Console.Write(alphabet[i]);
                x += 5;
            }
            Console.SetCursorPosition(10, 18);
            Console.ForegroundColor = ConsoleColor.Blue;

            string name = "DEUCENG  .CHESS.";
            x = 50; y = 2; int x1 = 48;
            for (int i = 0; i < name.Length; i++) //game name
            {
                Console.SetCursorPosition(x, y);
                if (i >= 8)
                {
                    Console.SetCursorPosition(x1, y + 1);
                    Console.ForegroundColor = ConsoleColor.White;
                    x1 += 2;
                }
                else if (i >= 3)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue; x += 2;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed; x += 2;
                }
                Console.Write(name[i]);
            }
        }   
        static void InitialValues() // Generating of stones
        {
            int red = 1;  // between 1-16 is equals to black pieces
            int blue = 116; // between 101-116 is equals to white pieces
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if ((i == 0) || (i == 1))
                    {
                        board[i, j] = red;
                        red++;
                    }
                    else if ((i == 6) || (i == 7))
                    {
                        board[i, j] = blue;
                        blue--;
                    }
                }
            }
        }
        static void PrintingExtra() // Printing additional improvements
        {
            Console.SetCursorPosition(10, 18);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string str = (@"
                                                                    _:_
                                                                   '-.-'
                                                       ()         __.'.__
                                                    .-:--:-.     |_______|
                                          ()         \____/       \=====/
                                          /\         {====}        )___(
                            (\=,         //\\         )__(        /_____\
      __       |'-'-'|     //  .\       (    )       /____\        |   |
     /  \      |_____|    (( \_  \       )__(         |  |         |   |
     \__/       |===|      ))  `\_)     /____\        |  |         |   |
    /____\      |   |     (/     \       |  |         |  |         |   |
     |  |       |   |      | _.-'|       |  |         |  |         |   |
     |__|       )___(       )___(       /____\       /____\       /_____\
    (====)     (=====)     (=====)     (======)     (======)     (=======)
    }===={     }====={     }====={     }======{     }======{     }======={
   (______)   (_______)   (_______)   (________)   (________)   (_________)
      P           R           N           B             Q            K
     Pawn        Rook       Knight      Bishop        Queen         King



");


            for (int i = 0; i < str.Length; i++) //taşları çizdirme
            {
                Console.Write(str[i]);
                Thread.Sleep(1);
            }
        }
        static void PrintingStones(int thisNotWriteX, int thisNotWriteY, int chosenStone) // Writing stones on the screen
        {
            int cursorx, cursory = 2;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                cursorx = 5;
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.SetCursorPosition(cursorx, cursory);
                    if ((cursorx == thisNotWriteX) && (cursory == thisNotWriteY))
                    {
                        cursorx += 5;
                        continue;
                    }
                    if (board[i, j] == chosenStone)
                    {
                        if (chosenStone > 100)
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                        else
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("*");
                        cursorx += 5;
                        continue;
                    }
                    if (board[i, j] > 0 && board[i, j] < 17)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (board[i, j] > 100 && board[i, j] < 117)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    if (board[i, j] == 0)
                    {
                        Console.Write(".");
                    }
                    else if (board[i, j] == 5 || board[i, j] == 104)
                    {
                        Console.Write("K");
                    }
                    else if (board[i, j] == 4 || board[i, j] == 105)
                    {
                        Console.Write('Q');
                    }
                    else if (board[i, j] == 3 || board[i, j] == 6 || board[i, j] == 103 || board[i, j] == 106)
                    {
                        Console.Write('B');
                    }
                    else if (board[i, j] == 2 || board[i, j] == 7 || board[i, j] == 102 || board[i, j] == 107)
                    {
                        Console.Write('N');
                    }
                    else if (board[i, j] == 1 || board[i, j] == 8 || board[i, j] == 101 || board[i, j] == 108)
                    {
                        Console.Write('R');
                    }
                    else if (((board[i, j] > 8) && (board[i, j] < 17)) || ((board[i, j] > 108) && (board[i, j] < 117)))
                    {
                        Console.Write('p');
                    }
                    cursorx += 5;

                }
                cursory += 2;
            }
            Console.SetCursorPosition(cursorX, cursorY);
        }
        static void DrawTheInitialVersionOfBoard() // Writing the initial version of the screen
        {
            PrintingFrame();
            PrintingExtra();
            InitialValues();
            PrintingStones(-1, -1, -1);
            Console.ResetColor();
            Message(7);
        }
        static string StringNotation(string Notation, int type) // Playing with moves input
        {
            while (true)
            {
                Message(1); // Turn
                string notation;
                if (type == 1) // String Mode
                {
                    Console.ResetColor();
                    Message(8);// Enter Your Move
                    Console.SetCursorPosition(70, 15);
                    notation = Console.ReadLine();
                }
                else // Demo Mode
                {
                    notation = Notation;
                }
                
                if (notation != null)
                {
                    if (notation.Length == 2)
                    {
                        boardx = (Int16)(Convert.ToChar(notation[0]) - 97);
                        boardy = (Int16)(Convert.ToChar(notation[1]) - 48);
                        RealBoardY();
                        if (moveCounter % 2 == 0)
                        {
                            int[] lastCoordinate = LastCoordinate(109, 116, 'p'); // Blue pawns moves
                            if (lastCoordinate != null)
                            {
                                Message(6);
                                if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                {
                                    continue;
                                }
                                Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                return notation;
                            }
                        }
                        else
                        {
                            int[] lastCoordinate = LastCoordinate(9, 16, 'p'); // Red pawns moves
                            if (lastCoordinate != null)
                            {
                                Message(6);
                                if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                {
                                    continue;
                                }
                                Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                return notation;
                            }
                        }
                    }
                    else if (notation.Length == 3 || (notation.Length == 4 && notation[1] == 'x')) 
                    {
                        if (notation.Length == 3)
                        {
                            boardx = (Int16)(Convert.ToChar(notation[1]) - 97);
                            boardy = (Int16)(Convert.ToChar(notation[2]) - 48);
                        }
                        else
                        {
                            boardx = (Int16)(Convert.ToChar(notation[2]) - 97);
                            boardy = (Int16)(Convert.ToChar(notation[3]) - 48);
                        }
                        RealBoardY();
                        if (moveCounter % 2 == 0)
                        {
                            if (notation[0].Equals('N')) // Blue knights moves
                            {
                                int[] lastCoordinate = LastCoordinate(102, 107, 'N'); 
                                if (lastCoordinate != null)
                                {
                                    Message(6);
                                    if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                    {
                                        continue;
                                    }
                                    Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                    return notation;
                                }
                            }
                            else if (notation[0].Equals('B')) // Blue bishops moves
                            {
                                int[] lastCoordinate = LastCoordinate(103, 106, 'B'); 
                                if (lastCoordinate != null)
                                {
                                    Message(6);
                                    if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                    {
                                        continue;
                                    }
                                    Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                    return notation;
                                }
                            }
                            else if (notation[0].Equals('R')) // Blue rook moves
                            {
                                int[] lastCoordinate = LastCoordinate(101, 108, 'R');
                                if (lastCoordinate != null)
                                {
                                    Message(6);
                                    if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                    {
                                        continue;
                                    }
                                    Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                    return notation;
                                }
                            }
                            else if (notation[0].Equals('Q')) // Blue queen moves
                            {
                                int[] lastCoordinate = LastCoordinate(105, -1, 'Q');
                                if (lastCoordinate != null)
                                {
                                    Message(6);
                                    if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                    {
                                        continue;
                                    }
                                    Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                    return notation;
                                }
                            }
                            else if (notation[0].Equals('K')) // Blue king moves
                            {
                                int[] lastCoordinate = LastCoordinate(104, -1, 'K');
                                if (lastCoordinate != null)
                                {
                                    Message(6);
                                    if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                    {
                                        continue;
                                    }
                                    Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                    return notation;
                                }
                            }
                            else if (notation[0].Equals('x')) // Taking stone
                            {
                                int[] lastCoordinate = LastCoordinate(109, 116, 'p');
                                if (lastCoordinate != null)
                                {
                                    Message(6);
                                    if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                    {
                                        continue;
                                    }
                                    Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                    return notation;
                                }
                            }
                            else if (notation.Equals("O-O")) // Short castling for blue
                            {
                                if ((blueRightRook == 0 && blueKing == 0))
                                {
                                    if (ShortCastlingControl(101))
                                    {
                                        ShortCastling(101);
                                        board[7, 7] = 0;
                                        moveCounter++;
                                        PrintingStones(-1, -1, -1);
                                        Message(6);
                                        return notation;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (notation[0].Equals('N')) // Red knigths moves
                            {
                                int[] lastCoordinate = LastCoordinate(2, 7, 'N');
                                if (lastCoordinate != null)
                                {
                                    Message(6);
                                    if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                    {
                                        continue;
                                    }
                                    Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                    return notation;
                                }
                            }
                            else if (notation[0].Equals('B')) // Red bishops moves
                            {
                                int[] lastCoordinate = LastCoordinate(3, 6, 'B');
                                if (lastCoordinate != null)
                                {
                                    Message(6);
                                    if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                    {
                                        continue;
                                    }
                                    Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                    return notation;
                                }
                            }
                            else if (notation[0].Equals('R')) // Red rooks moves
                            {
                                int[] lastCoordinate = LastCoordinate(1, 8, 'R');
                                if (lastCoordinate != null)
                                {
                                    Message(6);
                                    if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                    {
                                        continue;
                                    }
                                    Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                    return notation;
                                }
                            }
                            else if (notation[0].Equals('Q')) // Red queen moves
                            {
                                int[] lastCoordinate = LastCoordinate(4, -1, 'Q');
                                if (lastCoordinate != null)
                                {
                                    Message(6);
                                    if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                    {
                                        continue;
                                    }
                                    Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                    return notation;
                                }
                            }
                            else if (notation[0].Equals('K')) // Red king moves
                            {
                                int[] lastCoordinate = LastCoordinate(5, -1, 'K');
                                if (lastCoordinate != null)
                                {
                                    Message(6);
                                    if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                    {
                                        continue;
                                    }
                                    Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                    return notation;
                                }
                            }
                            else if (notation[0].Equals('x')) // Taking stone
                            {
                                int[] lastCoordinate = LastCoordinate(9, 16, 'p');
                                if (lastCoordinate != null)
                                {
                                    Message(6);
                                    if (!CanTheStoneSettle(lastCoordinate[0], lastCoordinate[1], boardx, boardy))
                                    {
                                        continue;
                                    }
                                    Location(lastCoordinate[0], lastCoordinate[1], lastCoordinate[2]);
                                    return notation;
                                }
                            }
                            else if (notation.Equals("O-O")) // Short castling for red 
                            {
                                if ((redRightRook == 0 && redKing == 0))
                                {
                                    if (ShortCastlingControl(8))
                                    {
                                        ShortCastling(8);
                                        board[0, 7] = 0;
                                        moveCounter++;
                                        PrintingStones(-1, -1, -1);
                                        Message(6);
                                        return notation;
                                    }
                                }
                            }
                        }
                    }
                    else if (notation.Length == 5)
                    {
                        if (moveCounter % 2 == 0)
                        {
                            if (notation.Equals("O-O-O")) // Long castling for blue
                            {
                                if ((blueLeftRook == 0 && blueKing == 0))
                                {
                                    if (LongCastlingControl(108))
                                    {
                                        LongCastling(108);
                                        board[7, 0] = 0;
                                        moveCounter++;
                                        PrintingStones(-1, -1, -1);
                                        Message(6);
                                        return notation;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (notation.Equals("O-O-O")) // Long castling for red
                            {
                                if ((redLeftRook == 0 && redKing == 0))
                                {
                                    if (LongCastlingControl(1))
                                    {
                                        LongCastling(1);
                                        board[0, 0] = 0;
                                        moveCounter++;
                                        PrintingStones(-1, -1, -1);
                                        Message(6);
                                        return notation;
                                    }
                                }
                            }
                        }
                    }
                }
                Message(6);
            }
        }
        static int[] LastCoordinate(int stone1, int stone2, char stoneName) // Finding the old position in string mode and deciding which stone to play
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (stoneName == 'N')
                    {
                        if (board[i, j] == stone1 || board[i, j] == stone2)
                        {
                            if (Knight(j, i, boardx, boardy))
                            {
                                int stoneNo = board[i, j];
                                int[] lastCoordinate = { j, i, stoneNo }; // Old location and selected stone for knigth
                                return lastCoordinate;
                            }
                        }
                    }
                    else if (stoneName == 'B')
                    {
                        if (board[i, j] == stone1 || board[i, j] == stone2)
                        {
                            if (Bishop(j, i, boardx, boardy))
                            {
                                int stoneNo = board[i, j];
                                int[] lastCoordinate = { j, i, stoneNo }; // Old location and selected stone for bishop
                                return lastCoordinate;
                            }
                        }
                    }
                    else if (stoneName == 'R')
                    {
                        if (board[i, j] == stone1 || board[i, j] == stone2)
                        {
                            if (Rook(j, i, boardx, boardy))
                            {
                                int stoneNo = board[i, j];
                                int[] lastCoordinate = { j, i, stoneNo }; // Old location and selected stone for rook
                                return lastCoordinate;
                            }
                        }
                    }
                    else if (stoneName == 'p')
                    {
                        if ((stone1 <= board[i, j]) && (board[i, j] <= stone2))
                        {
                            if (Pawn(j, i, boardx, boardy))
                            {
                                int stoneNo = board[i, j];
                                int[] lastCoordinate = { j, i, stoneNo }; // Old location and selected stone for pawn
                                return lastCoordinate;
                            }
                        }
                    }
                    else if (stoneName == 'Q')
                    {
                        if (stone1 == board[i, j])
                        {
                            if (Queen(j, i, boardx, boardy))
                            {
                                int stoneNo = board[i, j];
                                int[] lastCoordinate = { j, i, stoneNo }; // Old location and selected stone for queen
                                return lastCoordinate;
                            }
                        }
                    }
                    else if (stoneName == 'K')
                    {
                        if (stone1 == board[i, j])
                        {
                            if (King(j, i, boardx, boardy))
                            {
                                int stoneNo = board[i, j];
                                int[] lastCoordinate = { j, i, stoneNo }; // Old location and selected stone for king
                                return lastCoordinate;
                            }
                        }
                    }
                }
            }
            return null;
        }
        static void RealBoardY()
        {
            if (boardy == 8)
            {
                boardy = 0;
            }
            else if (boardy == 7)
            {
                boardy = 1;
            }
            else if (boardy == 6)
            {
                boardy = 2;
            }
            else if (boardy == 5)
            {
                boardy = 3;
            }
            else if (boardy == 4)
            {
                boardy = 4;
            }
            else if (boardy == 3)
            {
                boardy = 5;
            }
            else if (boardy == 2)
            {
                boardy = 6;
            }
            else if (boardy == 1)
            {
                boardy = 7;
            }
        }
        static void LongCastling(int stoneNo) // Making long castling
        {
            if (stoneNo == 1)
            {
                board[0, 2] = 5; 
                board[0, 3] = 1;
                board[0, 4] = 0;
            }
            else if (stoneNo == 108)
            {
                board[7, 2] = 104;
                board[7, 3] = 108;
                board[7, 4] = 0;
            }
        }
        static bool LongCastlingControl(int stoneNo) // Controls required to make long castling
        {
            if (stoneNo == 1)
            {
                if (board[0, 1] == 0 && board[0, 2] == 0 && board[0, 3] == 0) // Is it empty between rook and king
                {
                    controlRedLeftRook++;
                    return true;
                }
            }
            else if (stoneNo == 108)
            {
                if (board[7, 1] == 0 && board[7, 2] == 0 && board[7, 3] == 0) // Is it empty between rook and king
                {
                    controlBlueLeftRook++;
                    return true;
                }
            }
            return false;
        }
        static void ShortCastling(int stoneNo) // Making short castling
        {
            if (stoneNo == 8)
            {
                board[0, 6] = 5;
                board[0, 5] = 8;
                board[0, 4] = 0;
            }
            else if (stoneNo == 101)
            {
                board[7, 6] = 104;
                board[7, 5] = 101;
                board[7, 4] = 0;
            }
        }
        static bool ShortCastlingControl(int stoneNo) // Controls required to make short castling
        {
            if (stoneNo == 8)
            {
                if (board[0, 5] == 0 && board[0, 6] == 0) // Is it empty between rook and king
                {
                    controlRedRightRook++;
                    return true;
                }
            }
            else if (stoneNo == 101)
            {
                if (board[7, 5] == 0 && board[7, 6] == 0) // Is it empty between rook and king
                {
                    controlBlueRightRook++;
                    return true;
                }
            }
            return false;
        }
        static void ClearScreen() // Clearing input for pawn conversion
        {
            int coordinateX = 90; int coordinateY = 5;
            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(coordinateX, coordinateY + i);
                Console.Write("                            ");
            }
        }
        static void Message(int whichMessage) // To make the necessary information to the user
        {
            if (whichMessage == 1)
            {
                if (moveCounter % 2 == 0) // blue turn 
                {
                    Console.SetCursorPosition(50, 11);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Its blue's turn");
                }
                else // red turns
                {
                    Console.SetCursorPosition(50, 11);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Its red's turn ");
                }
                Console.ResetColor();
            }
            else if (whichMessage == 2)
            {
                Console.SetCursorPosition(50, 5);
                Console.WriteLine("TRY AGAİN !!!");
            }
            else if (whichMessage == 3)
            {
                Console.SetCursorPosition(50, 5);
                Console.WriteLine("              ");
            }
            else if (whichMessage == 6)
            {
                Console.SetCursorPosition(70, 15);
                Console.Write("     ");
            }
            else if (whichMessage == 7)
            {
                Console.SetCursorPosition(50, 13);
                Console.Write("Press 1 to play with cursor moves, Press 2 to play with string notation, Press 3 for Demo Mode -> ");
            }
            else if (whichMessage == 8)
            {
                Console.SetCursorPosition(50, 15);
                Console.Write("Enter Your Move -> ");
            }
            else if (whichMessage == 9)
            {
                Console.SetCursorPosition(50, 13);
                Console.Write("                                                                                                     ");
            }
        }
        static bool King(int x, int y, int x2, int y2) // King control
        {
            bool flag = false;
            if ((Math.Abs(x2 - x) == 1 && y2 == y) || (Math.Abs(y2 - y) == 1 && x == x2) || (Math.Abs(y2 - y) == 1 && Math.Abs(x2 - x) == 1)) //king can move any direction he wants but just 1 step 
            {
                flag = true;
            }
            return flag;
        }
        static bool Queen(int x, int y, int x2, int y2) // Queen control
        {
            bool flag = false;
            if (Rook(x, y, x2, y2) == true && Bishop(x, y, x2, y2) == false)
                flag = true;

            else if (Rook(x, y, x2, y2) == false && Bishop(x, y, x2, y2) == true)
                flag = true;
            return flag;
        }
        static bool Bishop(int x, int y, int x2, int y2) //Bishop control
        {
            bool flag = true;
            int xx, yy;
            int dist;
            xx = Math.Sign(x2 - x);
            yy = Math.Sign(y2 - y);
            if (Math.Abs(x2 - x) == Math.Abs(y2 - y))
            {
                dist = Math.Abs(x2 - x);
                for (int i = 1; i < dist; i++)
                {
                    if (board[y + i * yy, x + i * xx] != 0) //displacement x and y must be equal 
                    {
                        flag = false;
                        break;
                    }
                }
            }
            else
            {
                flag = false;
            }

            return flag;
        }
        static bool Rook(int x, int y, int x2, int y2) // Rook control
        {
            bool flag = false;
            int xx, yy;
            int dist;
            xx = Math.Sign(x2 - x);
            yy = Math.Sign(y2 - y);
            if (Math.Abs(x2 - x) == 0) //just y axis movement,x displacement=0
            {
                flag = true;
                dist = Math.Abs(y2 - y);
                for (int i = 1; i < dist; i++)
                {
                    if (board[y + i * yy, x] != 0) //displacement x and y must be equal 
                    {
                        flag = false;
                        break;
                    }
                }
            }
            else if (Math.Abs(y2 - y) == 0)//just x axis movement,y displacement=0
            {
                flag = true;
                dist = Math.Abs(x2 - x);
                for (int i = 1; i < dist; i++)
                {
                    if (board[y, x + i * xx] != 0) //displacement x and y must be equal 
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }
        static bool Knight(int x, int y, int x2, int y2) // Knight control
        {
            bool flag = false;
            if (Math.Abs(x2 - x) == 2 && Math.Abs(y2 - y) == 1) //..\n. horizontal L move (yatay)
                flag = true;

            else if (Math.Abs(x2 - x) == 1 && Math.Abs(y2 - y) == 2) //.\n.. vertical L move (dikey)
                flag = true;
            return flag;
        }
        static bool Pawn(int x, int y, int x2, int y2) // Pawn control
        {
            bool flag = false;
            if (enpassantleft == true)
            {
                if (board[y, x] > 9 && board[y, x] < 17 && y == 3) //for blue enpassant
                {
                    if (y2 - y == -1 && x2 - x == -1)
                        flag = true;
                }
            }
            if (board[y, x] > 108 && board[y, x] < 117) //for red pieces (just y++,x displacement=0)
            {
                if (y == 6 && (y2 - y == -2) && x2 == x) //first move
                {
                    if (board[y2, x2] == 0)
                        flag = true;
                }
                else if (y2 - y == -1 && x2 == x)
                {
                    if (board[y2, x2] == 0)
                        flag = true;
                }
                else if (y2 - y == -1 && Math.Abs(x2 - x) == 1 && board[y2, x2] > 0 && board[y2, x2] < 17) //cross eat
                {
                    flag = true;
                }
            }
            else if (board[y, x] < 17 && board[y, x] > 8) //for blue pieces (just y--,x displacement=0)
            {
                if (y == 1 && (y2 - y == 2) && x2 == x)//first move
                {
                    flag = true;
                }
                else if (y2 - y == 1 && x2 == x)
                {
                    flag = true;
                }
                else if (board[y2, x2] > 100 && board[y2, x2] < 117 && y2 - y == 1 && Math.Abs(x2 - x) == 1) //cross eat
                {
                    flag = true;
                }
            }
            return flag;
        }
        static void PawnTransformation() // Pawn promotion 
        {
            Console.ResetColor();
            Console.CursorVisible = true;
            Console.SetCursorPosition(90, 5);
            Console.WriteLine("Please choose stone -> ");
            Console.SetCursorPosition(90, 6);
            Console.WriteLine("1) N");
            Console.SetCursorPosition(90, 7);
            Console.WriteLine("2) B");
            Console.SetCursorPosition(90, 8);
            Console.WriteLine("3) R");
            Console.SetCursorPosition(90, 9);
            Console.WriteLine("4) Q");
            Console.SetCursorPosition(115, 5);
            int whichStone = Convert.ToInt32(Console.ReadLine());
            if (moveCounter % 2 == 0)
            {
                if (whichStone == 1) // Knight
                {
                    board[boardy, boardx] = 102;
                }
                else if (whichStone == 2) // Bishop
                {
                    board[boardy, boardx] = 103;
                }
                else if (whichStone == 3) // Rook
                {
                    board[boardy, boardx] = 101; 
                }
                else if (whichStone == 4) // Queen
                {
                    board[boardy, boardx] = 105; 
                }
            }
            else
            {
                if (whichStone == 1) // Knight
                {
                    board[boardy, boardx] = 2;
                }
                else if (whichStone == 2) // Bishop
                {
                    board[boardy, boardx] = 3;
                }
                else if (whichStone == 3) // Rook
                {
                    board[boardy, boardx] = 1;
                }
                else if (whichStone == 4) // Queen
                {
                    board[boardy, boardx] = 4;
                }
            }
        }
        static bool CanTheStoneSettle(int oldX, int oldY, int newX, int newY) // Checking the placement of the stone
        {
            bool flag = false;
            if (board[oldY, oldX] > 0 && board[oldY, oldX] < 17) // Red
            {
                if (board[newY, newX] > 100 && board[newY, newX] < 117)
                {
                    flag = true;
                }
                else if (board[newY, newX] == 0)
                {
                    flag = true;
                }
            }
            else if (board[oldY, oldX] > 100 && board[oldY, oldX] < 117) // Blue
            {
                if (board[newY, newX] > 0 && board[newY, newX] < 17)
                {
                    flag = true;
                }
                else if (board[newY, newX] == 0)
                {
                    flag = true;
                }
            }
            return flag;
        }
        static int chooseStone() // The section where the stone to play is selected
        {
            ConsoleKeyInfo button;
            while (true)
            {
                Console.CursorVisible = true;
                button = Console.ReadKey(true);
                int stoneNumber;

                //cursor movements
                if ((button.Key == ConsoleKey.RightArrow) && (cursorX < 40)) 
                {
                    cursorX += 5;
                    boardx++;
                }
                else if ((button.Key == ConsoleKey.LeftArrow) && (cursorX > 5))
                {
                    cursorX -= 5;
                    boardx--;
                }
                else if ((button.Key == ConsoleKey.DownArrow) && (cursorY < 16))
                {
                    cursorY += 2;
                    boardy++;
                }
                else if ((button.Key == ConsoleKey.UpArrow) && (cursorY > 2))
                {
                    cursorY -= 2;
                    boardy--;
                }
                else if (button.Key == ConsoleKey.Enter)
                {
                    if (board[boardy, boardx] != 0)
                    {
                        stoneNumber = board[boardy, boardx]; // New location
                    }
                    else
                    {
                        continue;
                    }
                    if (moveCounter % 2 == 0)
                    {
                        if (stoneNumber > 0 && stoneNumber < 17)
                            continue;
                    }
                    else 
                    {
                        if (stoneNumber > 100 && stoneNumber < 117)
                            continue;
                    }
                    return stoneNumber;
                }
                Console.SetCursorPosition(cursorX, cursorY);
            }
        }
        static int[] StoneMovement(int stoneNumber) // Function that moves the selected stone and returns the last coordinates in the stone placement 
        {
            int oldX = boardx; int oldY = boardy;
            ConsoleKeyInfo button;
            while (true)
            {
                Console.CursorVisible = false;
                button = Console.ReadKey(true);
                // Movement of the chosen stone
                if ((button.Key == ConsoleKey.RightArrow) && (cursorX < 40))
                {
                    cursorX += 5;
                    boardx++;
                }
                else if ((button.Key == ConsoleKey.LeftArrow) && (cursorX > 5))
                {
                    cursorX -= 5;
                    boardx--;
                }
                else if ((button.Key == ConsoleKey.DownArrow) && (cursorY < 16))
                {
                    cursorY += 2;
                    boardy++;
                }
                else if ((button.Key == ConsoleKey.UpArrow) && (cursorY > 2))
                {
                    cursorY -= 2;
                    boardy--;
                }
                else if (button.Key == ConsoleKey.Enter)
                {
                    if (!CanTheStoneSettle(oldX, oldY, boardx, boardy))
                    {
                        Message(2); // Try again
                        continue;
                    }
                    else
                    {
                        Message(3);
                    }
                    int[] location = new int[2];
                    location[0] = oldX;
                    location[1] = oldY;
                    return location;
                }
                else if (button.Key == ConsoleKey.L)
                {
                    if ((redLeftRook == 0 && redKing == 0) || (blueLeftRook == 0 && blueKing == 0))
                    {
                        if (LongCastlingControl(stoneNumber))
                        {
                            LongCastling(stoneNumber);
                        }
                    }
                    int[] location = new int[2];
                    location[0] = oldX;
                    location[1] = oldY;
                    return location;
                }
                else if (button.Key == ConsoleKey.S)
                {
                    if ((redRightRook == 0 && redKing == 0) || (blueRightRook == 0 && blueKing == 0))
                    {
                        if (ShortCastlingControl(stoneNumber))
                        {
                            ShortCastling(stoneNumber);
                        }
                    }
                    int[] location = new int[2];
                    location[0] = oldX;
                    location[1] = oldY;
                    return location;
                }
                // Coloring stones
                Console.SetCursorPosition(cursorX, cursorY);
                if (stoneNumber > 0 && stoneNumber < 17)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (stoneNumber > 100 && stoneNumber < 117)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.ResetColor();
                }
                // Printing stones
                if (stoneNumber == 5 || stoneNumber == 104)
                {
                    Console.Write('K');
                }
                else if (stoneNumber == 4 || stoneNumber == 105)
                {
                    Console.Write('Q');
                }
                else if (stoneNumber == 3 || stoneNumber == 6 || stoneNumber == 103 || stoneNumber == 106)
                {
                    Console.Write('B');
                }
                else if (stoneNumber == 2 || stoneNumber == 7 || stoneNumber == 102 || stoneNumber == 107)
                {
                    Console.Write('N');
                }
                else if (stoneNumber == 1 || stoneNumber == 8 || stoneNumber == 101 || stoneNumber == 108)
                {
                    Console.Write('R');
                }
                else if (((stoneNumber > 8) && (stoneNumber < 17)) || ((stoneNumber > 108) && (stoneNumber < 117)))
                {
                    Console.Write('p');
                }
                PrintingStones(cursorX, cursorY, stoneNumber);
            }
        }
        static void Location(int lastX, int lastY, int stoneNumber)// The selected stone is placed in new coordinates in this function.
        {
            // Placing stones
            if (stoneNumber == 1 || stoneNumber == 8 || stoneNumber == 101 || stoneNumber == 108) // Rook
            {
                if (Rook(lastX, lastY, boardx, boardy))
                {
                    if (board[boardy, boardx] != stoneNumber)
                    {
                        board[boardy, boardx] = stoneNumber;
                    }
                    board[lastY, lastX] = 0;
                    moveCounter++;
                }
            }
            else if (stoneNumber == 2 || stoneNumber == 7 || stoneNumber == 102 || stoneNumber == 107) // Knight
            {
                if (Knight(lastX, lastY, boardx, boardy))
                {
                    board[boardy, boardx] = stoneNumber;
                    board[lastY, lastX] = 0;
                    moveCounter++;
                }
            }
            else if (stoneNumber == 3 || stoneNumber == 6 || stoneNumber == 103 || stoneNumber == 106) // Bishop
            {
                if (Bishop(lastX, lastY, boardx, boardy))
                {
                    board[boardy, boardx] = stoneNumber;
                    board[lastY, lastX] = 0;
                    moveCounter++;
                }
            }
            else if (stoneNumber == 4 || stoneNumber == 105) // Queen
            {
                if (Queen(lastX, lastY, boardx, boardy))
                {
                    board[boardy, boardx] = stoneNumber;
                    board[lastY, lastX] = 0;
                    moveCounter++;
                }
            }
            else if (stoneNumber == 5 || stoneNumber == 104)
            {
                if (King(lastX, lastY, boardx, boardy)) // King
                {
                    board[boardy, boardx] = stoneNumber;
                    board[lastY, lastX] = 0;
                    blueKing++; redKing++;
                    moveCounter++;
                }
            }
            else if ((stoneNumber > 8 && stoneNumber < 17) || (stoneNumber > 108 && stoneNumber < 117)) // Pawn
            {
                enpassantleft = true; enpassantright = true;
                if (moveCounter % 3 == 0)
                {
                    enpassantleft = false;
                    enpassantright = false;
                }
                if (Pawn(lastX, lastY, boardx, boardy))
                {

                    if (lastY == 1 && boardy == 3) //for en passant
                    {
                        if (lastX < 7 && board[boardy, boardx + 1] > 8 && board[boardy, boardx + 1] < 17)
                            enpassantleft = true;
                        if (lastX > 0 && board[boardy, boardx - 1] > 8 && board[boardy, boardx - 1] < 17)
                            enpassantleft = true;
                    }
                    if (lastY == 6 && boardx == 4) //for en passant
                    {
                        if (lastX < 7 && board[boardy, boardx + 1] > 108 && board[boardy, boardx + 1] < 117)
                            enpassantleft = true;
                        if (lastX > 0 && board[boardy, boardx - 1] > 108 && board[boardy, boardx - 1] < 117)
                            enpassantright = true;
                    }
                    board[lastY, lastX] = 0;
                    if (boardy != 0 && boardy != 7)
                    {
                        board[boardy, boardx] = stoneNumber;
                        if ((enpassantleft == true || enpassantright == true) && stoneNumber > 109 && stoneNumber < 117) //en passant eating left red
                        {
                            board[boardy + 1, boardx] = 0;
                        }
                        if ((enpassantleft == true || enpassantright == true) && stoneNumber > 9 && stoneNumber < 17) //en passant eating left blue
                        {
                            board[boardy - 1, boardx] = 0;
                        }

                    }
                    else
                    {
                        PawnTransformation();
                        ClearScreen();
                    }
                    moveCounter++;
                }
            }
            PrintingStones(-1, -1, -1);
        }
        static string[] AllMoves() // A function for retrieving previously recorded game data from a file called Chess
        {
            if (!File.Exists(filePath)) 
            {
                File.Create(filePath); // Creating file
                return null;
            }
            StreamReader read = File.OpenText(filePath);
            int moveNumber = 0;
            do
            {
                read.ReadLine();
                moveNumber++;
            } while (!read.EndOfStream);
            read.Close();
            StreamReader reads = File.OpenText(filePath);
            string[] allMoves = new string[moveNumber];
            int move = 0;
            do
            {
                allMoves[move] = reads.ReadLine();
                move++;
            } while (!reads.EndOfStream);
            reads.Close();
            return allMoves;
        }
        static void GameMode(int mode) // The function in which the mode of the game is selected and played according to the mode to be played
        {
            Message(9);
            int[] movement = new int[2];
            string[] allMovesTake = AllMoves();
            StreamWriter f = File.CreateText(filePath);
            f.Close();
            ConsoleKeyInfo button;
            int i = 0;
            while (true)
            {
                Message(1);
                if (mode == 1) // Cursor mode
                {
                    Console.SetCursorPosition(cursorX, cursorY);
                    int stoneNo = chooseStone();
                    movement = StoneMovement(stoneNo);
                    Location(movement[0], movement[1], stoneNo);
                }
                else if (mode == 2) // String mode
                {
                    StreamWriter file = File.AppendText(filePath);
                    string move = StringNotation(null, 1);
                    file.WriteLine(move);
                    file.Close();
                }
                else if (mode == 3) // Demo mode
                {
                    Console.CursorVisible = false;
                    button = Console.ReadKey(true);
                    if (button.Key == ConsoleKey.Spacebar)
                    {
                        if (i < allMovesTake.Length)
                        {
                            string whichMove = allMovesTake[i];
                            StringNotation(whichMove, 2);
                            i++;
                        }
                    }
                    else if (button.Key == ConsoleKey.Escape)
                    {
                        Console.CursorVisible = true;
                        Console.SetCursorPosition(50, 15);
                        Console.Write("Which mode do you want to switch to cursor mode (1) string mode (2) -> ");
                        mode = Convert.ToInt16(Console.ReadLine());
                        Console.SetCursorPosition(50, 15);
                        Console.Write("                                                                         ");
                    }
                }
            }
        }
        static void GameInstructions() // General instructions about the game
        {
            Console.Title = "Chess Project";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine(@"   
    ____     ______   __  __            ______   ______   _   __   ______
   / __ \   / ____/  / / / /           / ____/  / ____/  / | / /  / ____/
  / / / /  / __/    / / / /  ______   / /      / __/    /  |/ /  / / __  
 / /_/ /  / /___   / /_/ /  /_____/  / /___   / /___   / /|  /  / /_/ /  
/_____/  /_____/   \____/            \____/  /_____/  /_/ |_/   \____/   
                                                                       
                                                                                              
                          
                             ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
                 ______   __  __   ______   _____    _____
                / ____/  / / / /  / ____/  / ___/   / ___/
               / /      / /_/ /  / __/     \__ \    \__ \ 
              / /___   / __  /  / /___    ___/ /   ___/ / 
              \____/  /_/ /_/  /_____/   /____/   /____/  
                                                          ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("HOW TO PLAY?\n");
            Console.WriteLine("1 -Select the respective rook to castling ");
            Console.WriteLine("and press 'L' for long castling 'S' for short castling.\n");
            Console.WriteLine("2 - In the cursor mode, select the stones with the enter button");
            Console.WriteLine("and drop them at the point you want with the enter button.\n");
            Console.WriteLine("3 - In the string mode, all stones except all pawns are written with\ntheir own symbols and the rotation to goafter must be written as 'letter number's.\n");
            Console.WriteLine("4-In Play Mode, go to the target with the arrow keys and select the move \nby pressing the 'Enter' key, press 'Enter' again to place the selected stone.\n");
            Console.WriteLine("5-In Demo Mode, press the 'Space' key to view moves,press the 'Esc' to exit Demo Mode.");
            Console.SetCursorPosition(80, 3);
            Console.Write("Press enter to continue...");


        }

        static void Main(string[] args)
        {
            GameInstructions();
            Console.ReadKey();
            Console.Clear();
            DrawTheInitialVersionOfBoard();
            int movementType = Convert.ToInt16(Console.ReadLine());
            GameMode(movementType);
        }
    }
}
