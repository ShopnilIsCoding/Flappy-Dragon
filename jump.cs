using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float jumpForce = 10f;
    public Animator animator;
    private Rigidbody rb;
    public AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (animator == null)
        {
            Debug.LogError("Animator reference not set in BirdController!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
    }
    public void jump()
    {
        // Trigger jump animation
        animator.SetTrigger("jump");
        audioSource.Play();

        rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
