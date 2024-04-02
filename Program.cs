using System;

namespace TicTacToe
{
    class Program
    {
        static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static bool playerTurn = true; // true for player X, false for player O

        static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain)
            {
                while (!IsGameOver())
                {
                    DrawBoard();
                    if (playerTurn)
                    {
                        PlayerMove();
                    }
                    else
                    {
                        ComputerMove();
                    }
                    playerTurn = !playerTurn;
                }

                DrawBoard();
                if (IsDraw())
                {
                    Console.WriteLine("It's a draw!");
                }
                else
                {
                    char winner = GetWinner();
                    if (winner == 'X')
                    {
                        Console.WriteLine("Congratulations! You win!");
                        playerTurn = false; // Game Master starts first in the next round
                    }
                    else
                    {
                        Console.WriteLine("Game Master wins! Here's a tip: Starting from the center position gives you more control over the game.");
                        playerTurn = true; // Player starts first in the next round
                    }
                }

                Console.WriteLine("Do you want to play again? Press 1 for Yes, any other key to exit.");
                char playAgainInput = Console.ReadKey().KeyChar;
                if (playAgainInput != '1')
                {
                    playAgain = false;
                }
                else
                {
                    ResetBoard();
                }
            }
        }

        static void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("   |   |   ");
            Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
            Console.WriteLine("___|___|___");
            Console.WriteLine("   |   |   ");
            Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
            Console.WriteLine("___|___|___");
            Console.WriteLine("   |   |   ");
            Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
            Console.WriteLine("   |   |   ");
        }

        static void PlayerMove()
        {
            Console.WriteLine("Enter your move (1-9): ");
            int move;
            while (!int.TryParse(Console.ReadLine(), out move) || move < 1 || move > 9 || board[move - 1] == 'X' || board[move - 1] == 'O')
            {
                Console.WriteLine("Invalid move! Try again.");
                Console.WriteLine("Enter your move (1-9): ");
            }

            board[move - 1] = 'X';
        }

        static void ComputerMove()
        {
            Random rand = new Random();
            int move;
            do
            {
                move = rand.Next(0, 9);
            } while (board[move] == 'X' || board[move] == 'O');

            board[move] = 'O';
            Console.WriteLine($"Computer selects square {move + 1}");
            Console.ReadKey(true);
        }

        static bool IsGameOver()
        {
            return IsDraw() || GetWinner() != ' ';
        }

        static bool IsDraw()
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i] != 'X' && board[i] != 'O')
                {
                    return false;
                }
            }
            return true;
        }

        static char GetWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                // Check rows
                if (board[i * 3] == board[i * 3 + 1] && board[i * 3 + 1] == board[i * 3 + 2])
                {
                    return board[i * 3];
                }
                // Check columns
                if (board[i] == board[i + 3] && board[i + 3] == board[i + 6])
                {
                    return board[i];
                }
            }
            // Check diagonals
            if ((board[0] == board[4] && board[4] == board[8]) || (board[2] == board[4] && board[4] == board[6]))
            {
                return board[4];
            }
            return ' ';
        }

        static void ResetBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                board[i] = (i + 1).ToString()[0];
            }
        }
    }
}
