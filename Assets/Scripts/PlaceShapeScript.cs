using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlaceShapeScript : MonoBehaviour
{
    private bool isX = true;
    public bool AIOverride = false;

    [SerializeField] public GameObject xShapePrefab;
    [SerializeField] public GameObject oShapePrefab;

    private void Start()
    {
        AIOverride = PlayerSettings.Instance.AIvsAI;
    }

    // Update is called once per frame
    void Update()
    {   
        if (!AIOverride) 
        {
            // THIS IS TEMP - REMOVE LATER
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }

            if (Input.GetMouseButtonDown(0)) // Left click
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Slot")) // Make sure slots have a "Slot" tag
                    {
                        CheckIfPlaceable(hit.collider.gameObject);
                    }
                }
            } 
        }     
    }

    void CheckIfPlaceable(GameObject slot)
    {
        SlotManager slotManager = slot.GetComponent<SlotManager>();
        GameManager gameManager = EventSystem.current.GetComponent<GameManager>();
        TurnManager turnManager = EventSystem.current.GetComponent<TurnManager>();

        if (!slotManager.OccupiedByX && !slotManager.OccupiedByO)
        {
            if (isX == true)
            {
                turnManager.StartCoroutine(turnManager.StartAITurn());
                slotManager.OccupiedByX = true;
                Instantiate(xShapePrefab, slot.transform.position, slot.transform.rotation);

                Debug.Log("Placed shape on: " + slot.name);

                gameManager.CheckForWinner();
            }
            else
            {
                turnManager.StartCoroutine(turnManager.StartPlayersTurn());
                slotManager.OccupiedByO = true;
                Instantiate(oShapePrefab, slot.transform.position, Quaternion.identity);

                Debug.Log("Placed shape on: " + slot.name);

                gameManager.CheckForWinner();
            }
        }
        else
        {
            Debug.Log("Cannot place shape on: " + slot.name);
            return;
        }
    }
}

// Instantiate(isX ? xShapePrefab : oShapePrefab, slot.transform.position, Quaternion.identity);

