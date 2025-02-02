using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour
{
    GameManager gameManager;
    public char slotValue = 'E';

    private bool occupiedByX = false;
    public bool OccupiedByX
    {
        get { return occupiedByX; }
        set
        {
            occupiedByX = value;
            slotValue = value ? 'X' : 'E'; 
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
            slotValue = value ? 'O' : 'E';
            gameManager.UpdateBoard();
        }
    }

    private void Start()
    {
        gameManager = EventSystem.current.GetComponent<GameManager>();
    }
}   
