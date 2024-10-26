using System;

class TicTacToe
{
    static char[] board = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
    static char player = 'X';
    static char bot = 'O';

    static void Main()
    {
        while (true)
        {
            PrintBoard();
            PlayerMove();
            if (IsWinner(player))
            {
                PrintBoard();
                Console.WriteLine("Ты победил!");
                break;
            }
            if (IsDraw())
            {
                PrintBoard();
                Console.WriteLine("Ничья!");
                break;
            }

            BotMove();
            if (IsWinner(bot))
            {
                PrintBoard();
                Console.WriteLine("Бот победил!");
                break;
            }
            if (IsDraw())
            {
                PrintBoard();
                Console.WriteLine("Ничья!");
                break;
            }
        }
    }

    static void PrintBoard()
    {
        Console.Clear();
        Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
    }

    static void PlayerMove()
    {
        int move;
        do
        {
            Console.WriteLine("Выбери позицию (1-9): ");
        } while (!int.TryParse(Console.ReadLine(), out move) || move < 1 || move > 9 || board[move - 1] != ' ');

        board[move - 1] = player;
    }

    static void BotMove()
    {
        int bestScore = int.MinValue;
        int move = -1;

        for (int i = 0; i < 9; i++)
        {
            if (board[i] == ' ')
            {
                board[i] = bot;
                int score = Minimax(false, int.MinValue, int.MaxValue);
                board[i] = ' ';
                if (score > bestScore)
                {
                    bestScore = score;
                    move = i;
                }
            }
        }
        board[move] = bot;
    }

    static int Minimax(bool isMaximizing, int alpha, int beta)
    {
        if (IsWinner(bot)) return 1;
        if (IsWinner(player)) return -1;
        if (IsDraw()) return 0;

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == ' ')
                {
                    board[i] = bot;
                    int score = Minimax(false, alpha, beta);
                    board[i] = ' ';
                    bestScore = Math.Max(score, bestScore);
                    alpha = Math.Max(alpha, bestScore);
                    if (beta <= alpha) break;
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == ' ')
                {
                    board[i] = player;
                    int score = Minimax(true, alpha, beta);
                    board[i] = ' ';
                    bestScore = Math.Min(score, bestScore);
                    beta = Math.Min(beta, bestScore);
                    if (beta <= alpha) break;
                }
            }
            return bestScore;
        }
    }

    static bool IsWinner(char p)
    {
        int[,] winCombinations = {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
            {0, 4, 8}, {2, 4, 6}
        };

        for (int i = 0; i < winCombinations.GetLength(0); i++)
        {
            if (board[winCombinations[i, 0]] == p &&
                board[winCombinations[i, 1]] == p &&
                board[winCombinations[i, 2]] == p)
                return true;
        }
        return false;
    }

    static bool IsDraw()
    {
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == ' ') return false;
        }
        return true;
    }
}
