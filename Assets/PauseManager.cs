using StarterAssets;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject player;
    public bool isPaused = false;
    private ThirdPersonController ThirdPersonController;

    private void Start()
    {
        ThirdPersonController = player.GetComponent<ThirdPersonController>();
        pausePanel.SetActive(false); // Hide pause panel or UI

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // Freezes the scene
        Cursor.visible = true; // Displays cursor
        pausePanel.SetActive(true); // Display pause panel or UI
        ThirdPersonController.enabled = false;
    }

    void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resumes normal time flow
        Cursor.visible = false; // Hides cursor
        pausePanel.SetActive(false); // Hide pause panel or UI
        ThirdPersonController.enabled = true;
    }
}
