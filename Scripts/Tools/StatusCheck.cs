
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusCheck : MonoBehaviour
{
    [SerializeField] private Panel panel;
    public static StatusCheck Instance;
    public int tokenCount=0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ResetGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    private void OpenPanel() => panel.gameObject.SetActive(true);
    
    public void UpdateScore()
    {
        if (tokenCount != 3) return;
        OpenPanel();
    }
}
