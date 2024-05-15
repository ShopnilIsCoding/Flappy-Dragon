using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseConfirmationPanel;

    void Start()
    {
        // Hide the pause confirmation panel at the start
        pauseConfirmationPanel.SetActive(false);
        
    }

    void Update()
    {
        //if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            pauseConfirmationPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            AudioListener.pause = true;
           
        }
    }
    public void Continue()
    {
        pauseConfirmationPanel.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause= false;
    }
   

}
