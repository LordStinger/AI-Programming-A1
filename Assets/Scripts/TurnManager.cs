using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject shield; // Stops the player from placing shapes.
    private MinimaxAI minimaxAI;
    private PlaceShapeScript placeShapeScript;
    public bool playerPlaysFirst = true;

    private void Start()
    {
        playerPlaysFirst = PlayerSettings.Instance.MoveFirst;

        minimaxAI = GetComponent<MinimaxAI>();
        placeShapeScript = GetComponent<PlaceShapeScript>();
        
        if (!playerPlaysFirst)
        {
            StartCoroutine(StartAITurn());
        }
        else
        {
            StartCoroutine(StartPlayersTurn());
        }
    }

    public IEnumerator StartPlayersTurn()
    {
        if (!placeShapeScript.AIOverride)
        {
            yield return new WaitForSeconds(0.5f);
            shield.SetActive(false);
        }
        else
        {
            Debug.Log("AIOverride is true, AI is now in control.");
            minimaxAI.makeMoveAsPlayer = true;
            yield return new WaitForSeconds(1f);
            StartCoroutine(StartAITurn());
        }
    }

    public IEnumerator StartAITurn()
    {
        shield.SetActive(true);
        Debug.Log("AI is thinking...");
        yield return new WaitForSeconds(1f);
        minimaxAI.MakeMove();
    }
}
