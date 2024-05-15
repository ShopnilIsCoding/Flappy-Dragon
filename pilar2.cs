using UnityEngine;

public class Pillar2 : MonoBehaviour
{
    public float initialMoveSpeed = 5f; 
    public float maxMoveSpeed = 15f;
    public float accelerationRate = 0.3f; 
    public float destroyTime = 15f;

    private float currentMoveSpeed;
    private bool passedBird = false; 
    private GameManager gameManager; 
    void Start()
    {
        
        currentMoveSpeed = initialMoveSpeed;

       
        Destroy(gameObject, destroyTime);

        
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager script not found in the scene!");
        }
    }


    void Update()
    {
        
        transform.Translate(Vector3.left * currentMoveSpeed * Time.deltaTime);

       
        currentMoveSpeed = Mathf.Min(currentMoveSpeed + accelerationRate * Time.deltaTime, maxMoveSpeed);

        
        if (!passedBird && transform.position.x < gameManager.bird.position.x)
        {
            passedBird = true;
            gameManager.IncreaseScore();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Dragon"))
        {
            
            gameManager.EndGame();

        }
    }

    
}
