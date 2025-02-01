using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour
{
    GameManager gameManager;

    private bool isEmpty = true;
    public char slotValue = 'E';

    private bool occupiedByX = false;
    public bool OccupiedByX
    {
        get { return occupiedByX; }
        set
        {
            occupiedByX = value;
            slotValue = value ? 'X' : 'E'; // If occupied by X, set slotValue to 'X', otherwise reset to 'E'
            gameManager.UpdateBoard();
        }
    }


    private bool occupiedByO = false;
    public bool OccupiedByO
    {
        get { return occupiedByO; }
        set 
        { 
            occupiedByO = value; 
            slotValue = value ? 'O' : 'E'; // If occupied by O, set slotValue to 'O', otherwise reset to 'E'
            gameManager.UpdateBoard();
        }
    }

    private void Start()
    {
        gameManager = EventSystem.current.GetComponent<GameManager>();
    }
}   
