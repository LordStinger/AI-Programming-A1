using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public char[,] board = new char[3, 3];
    [SerializeField] private GameObject[] slotObjects = new GameObject[9]; // Assign the slots in the inspector - I know, ew, hard coding.
    public GameObject[,] slots = new GameObject[3, 3];

    // Other script references
    private PlaceShapeScript placeShapeScript;
    private CanvasManager canvasManager;

    // Ints
    public bool onGoingGame = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        placeShapeScript = GetComponent<PlaceShapeScript>();
        canvasManager = GetComponent<CanvasManager>();

        ConvertTo2D();
        InitializeBoard();
        UpdateBoard();
        UpdateSkeletonBoard();
    }

    private void Update() // TEMP REMOVE LATER
    {

    }

    private void ConvertTo2D()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                slots[2 - i, j] = slotObjects[i * 3 + j]; // Flip board so that the skeleton matches the 3D board.
            }
        }
    }

    private void InitializeBoard() // May not be needed at the end, remove before submitting.
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = 'E';
            }
        }
    }

    public void UpdateBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                SlotManager slotManager = slots[i, j].GetComponent<SlotManager>();
                board[i, j] = slotManager.slotValue;
            }
        }

        UpdateSkeletonBoard();
    }

    private void UpdateSkeletonBoard() // Personal use only. Not needed for the program to function.
    {
        string boardString = 
            $"{board[0, 0]} | {board[0, 1]} | {board[0, 2]}\n" +
            "--------\n" +
            $"{board[1, 0]} | {board[1, 1]} | {board[1, 2]}\n" +
            "--------\n" +
            $"{board[2, 0]} | {board[2, 1]} | {board[2, 2]}";

        Debug.Log($"Updated Board:\n{boardString}");
    }

    public void CheckForWinner()
    {
        char winner = 'E'; // Default to empty

        // Check rows & columns
        for (int i = 0; i < 3; i++)
        {
            if (CheckLine(board[i, 0], board[i, 1], board[i, 2])) // Row win
                winner = board[i, 0];

            if (CheckLine(board[0, i], board[1, i], board[2, i])) // Column win
                winner = board[0, i];
        }

        // Check diagonals
        if (CheckLine(board[0, 0], board[1, 1], board[2, 2])) // Main diagonal
            winner = board[0, 0];

        if (CheckLine(board[0, 2], board[1, 1], board[2, 0])) // Anti-diagonal
            winner = board[0, 2];

        // Announce winner or check for draw
        if (winner == 'X')
        {
            onGoingGame = false;
            Debug.Log("Player wins!");
            canvasManager.TriggerPlayerWin();
        }
        else if (winner == 'O')
        {
            onGoingGame = false;
            Debug.Log("AI wins!");
            canvasManager.TriggerAIWin();
        }
        else
        {
            CheckForDraw();
        }
    }

    public char CheckWinnerDirect(char[,] board) // AI Use Only - I could make this and the normal CheckForWinner() one function, but I'm lazy and don't want to refactor right now.
    {
        // Check rows & columns
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != 'E' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) return board[i, 0];
            if (board[0, i] != 'E' && board[0, i] == board[1, i] && board[1, i] == board[2, i]) return board[0, i];
        }

        // Check diagonals
        if (board[0, 0] != 'E' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) return board[0, 0];
        if (board[0, 2] != 'E' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) return board[0, 2];

        return 'E'; // No winner
    }

    private bool CheckLine(char a, char b, char c)
    {
        return a != 'E' && a == b && b == c;
    }

    private void CheckForDraw()
    {
        int counter = 0; // Tracks occupied slots

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] != 'E') // Count occupied slots
                {
                    counter++;
                }
            }
        }

        if (counter == 9) // If all slots are occupied, it's a draw
        {
            onGoingGame = false;
            Debug.Log("Draw!");
            canvasManager.TriggerDraw();
            return; 
        }
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene("Main");
    }
}
