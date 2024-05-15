using UnityEngine;

public class skyandGround : MonoBehaviour
{
    public GameObject end;
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Dragon"))
        {
            
            EndGame();
        }
    }

    public void EndGame()
    {
        Debug.Log("ended");



        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        end.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;

    }
}
