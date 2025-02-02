using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MinimaxAI : MonoBehaviour
{

    // This version of the Minimax algorithm is designed to be played as either 1 AI or 2 AI players.

    private GameManager gameManager;
    private PlaceShapeScript placeShapeScript;
    private TurnManager turnManager;
    public bool makeMoveAsPlayer = false;

    private void Start()
    {
        gameManager = EventSystem.current.GetComponent<GameManager>();
        placeShapeScript = EventSystem.current.GetComponent<PlaceShapeScript>();
        turnManager = EventSystem.current.GetComponent<TurnManager>();
    }

    public void MakeMove()
    {
        if (gameManager.onGoingGame)
        {
            char aiSymbol = makeMoveAsPlayer ? 'X' : 'O'; // AI plays X or O based on game mode
            Vector2Int bestMove = GetBestMove(aiSymbol);

            if (bestMove != new Vector2Int(-1, -1))
            {
                if (aiSymbol == 'O') // AI is O
                {
                    gameManager.board[bestMove.x, bestMove.y] = 'O';
                    gameManager.slots[bestMove.x, bestMove.y].GetComponent<SlotManager>().OccupiedByO = true;
                    Instantiate(placeShapeScript.oShapePrefab, gameManager.slots[bestMove.x, bestMove.y].transform.position, Quaternion.identity);
                    turnManager.StartCoroutine(turnManager.StartPlayersTurn());
                    gameManager.CheckForWinner();
                }
                else // AI is X
                {
                    gameManager.board[bestMove.x, bestMove.y] = 'X';
                    gameManager.slots[bestMove.x, bestMove.y].GetComponent<SlotManager>().OccupiedByX = true;
                    Instantiate(placeShapeScript.xShapePrefab, gameManager.slots[bestMove.x, bestMove.y].transform.position, gameManager.slots[bestMove.x, bestMove.y].transform.rotation);
                    makeMoveAsPlayer = false; // Reset for next turn
                    turnManager.StartCoroutine(turnManager.StartAITurn());
                    gameManager.CheckForWinner();
                }
            }
            else
            {
                Debug.LogWarning("No valid moves available for the AI.");
            }
        }
    }

    private Vector2Int GetBestMove(char aiSymbol)
    {
        int bestScore = int.MinValue;
        Vector2Int bestMove = new Vector2Int(-1, -1);

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (gameManager.board[row, col] == 'E') // Empty slot
                {
                    gameManager.board[row, col] = aiSymbol; // Simulate AI move
                    int score = Minimax(gameManager.board, false, aiSymbol); // Evaluate move
                    gameManager.board[row, col] = 'E'; // Undo move

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = new Vector2Int(row, col);
                    }
                }
            }
        }
        return bestMove;
    }

    private int Minimax(char[,] board, bool isMaximizing, char aiSymbol)
    {
        char playerSymbol = (aiSymbol == 'X') ? 'O' : 'X'; // Opponent's symbol

        char winner = gameManager.CheckWinnerDirect(board);
        if (winner == playerSymbol) return -10; // Opponent wins
        if (winner == aiSymbol) return 10;      // AI wins
        if (IsDraw(board)) return 0;            // Draw

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == 'E') // Empty spot
                    {
                        board[row, col] = aiSymbol; // AI makes a move
                        int score = Minimax(board, false, aiSymbol); // Opponent's turn
                        board[row, col] = 'E'; // Undo move
                        bestScore = Mathf.Max(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == 'E') // Empty spot
                    {
                        board[row, col] = playerSymbol; // Opponent makes a move
                        int score = Minimax(board, true, aiSymbol); // AI's turn
                        board[row, col] = 'E'; // Undo move
                        bestScore = Mathf.Min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
    }

    private bool IsDraw(char[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == 'E') return false;
            }
        }
        return true; // No empty spots, it's a draw
    }
}


