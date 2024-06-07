using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject startPanel;

    private void Start()
    {
        Time.timeScale = 0;
        startPanel.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);
    }
}