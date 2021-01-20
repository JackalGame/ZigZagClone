using UnityEngine;

public class CharController : MonoBehaviour
{
    private Rigidbody rb;
    private bool walkingRight = true;
    private Animator anim;
    private GameManager gameManager;
    private bool isFalling = false;

    [SerializeField] Transform rayStart;
    [SerializeField] GameObject crystalEffect;
    [SerializeField] float playerSpeed = 2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        if (!gameManager.gameStarted)
        {
            return;
        }
        else
        {
            anim.SetTrigger("gameStarted");
        }

        rb.transform.position += transform.forward * playerSpeed * Time.deltaTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFalling)
        {
            Switch();
        }

        RaycastHit hit;

        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity)) 
        {
            anim.SetBool("isFalling", true);
            isFalling = true;
        }
        else
        {
            anim.SetBool("isFalling", false);
            isFalling = false; 
        }

        if(transform.position.y < -2)
        {
            gameManager.EndGame();
        }

    }
    
    private void Switch()
    {
        if (!gameManager.gameStarted)
        {
            return;
        }
        
        walkingRight = !walkingRight;

        if (walkingRight)
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crystal")
        {
            gameManager.IncreaseScore(); 
            
            GameObject crystalFX = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(crystalFX, 2); 
            Destroy(other.gameObject);
        }
    }

}
