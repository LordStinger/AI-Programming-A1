using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerWinCanvas;
    [SerializeField] private GameObject AIWinCanvas;
    [SerializeField] private GameObject DrawCanvas;

    public void TriggerPlayerWin()
    {
        PlayerWinCanvas.SetActive(true);
    }

    public void TriggerAIWin()
    {
        AIWinCanvas.SetActive(true);
    }

    public void TriggerDraw()
    {
        DrawCanvas.SetActive(true);
    }
}
