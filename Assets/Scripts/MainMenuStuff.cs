using UnityEngine;
using UnityEngine.UI;

public class MainMenuStuff : MonoBehaviour
{
    private PlayerSettings playerSettings;

    void Start()
    {
        playerSettings = PlayerSettings.Instance;
        playerSettings.ResetSettings();
        Debug.Log("Player settings reset");
    }

    public void ToggleMoveFirst()
    {
        playerSettings.ToggleMoveFirst();
    }

    public void ToggleAIvsAI()
    {
        playerSettings.ToggleAIvsAI();
    }
}
