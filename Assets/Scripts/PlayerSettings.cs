using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    private static PlayerSettings instance;
    public static PlayerSettings Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("PlayerSettings");
                instance = obj.AddComponent<PlayerSettings>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    private bool moveFirst = false;
    private bool aIvsAI = false;

    public bool MoveFirstReader;
    public bool AIvsAIReader;

    public void Update()
    {
        MoveFirstReader = MoveFirst;
        AIvsAIReader = AIvsAI;
    }

    public bool MoveFirst
    {
        get { return moveFirst; } 
        set { moveFirst = value; } 
    }

    public bool AIvsAI
    {
        get { return aIvsAI; }
        set { aIvsAI = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PlayerSettings created and persists.");
        }
        else
        {
            Debug.Log("Duplicate PlayerSettings destroyed.");
            Destroy(gameObject);
        }
    }

    public void ToggleMoveFirst()
    {
        MoveFirst = !MoveFirst; 
    }

    public void ToggleAIvsAI()
    {
        AIvsAI = !AIvsAI;
    }

    public void ResetSettings()
    {
        MoveFirst = false;
        AIvsAI = false;
    }
}
