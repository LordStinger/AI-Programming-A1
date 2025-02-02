using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerWinCanvas;
    [SerializeField] private GameObject AIWinCanvas;
    [SerializeField] private GameObject DrawCanvas;
    private PlaceShapeScript placeShapeScript;

    private void Start()
    {
        placeShapeScript = GetComponent<PlaceShapeScript>();
    }

    public void TriggerPlayerWin()
    {
        PlayerWinCanvas.SetActive(true);
        placeShapeScript.enabled = false;
    }

    public void TriggerAIWin()
    {
        AIWinCanvas.SetActive(true);
        placeShapeScript.enabled = false;
    }

    public void TriggerDraw()
    {
        DrawCanvas.SetActive(true);
        placeShapeScript.enabled = false;
    }
}
