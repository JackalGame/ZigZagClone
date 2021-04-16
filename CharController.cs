using System;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private Rigidbody rb;
    private bool walkingRight = true;
    private Animator anim;
    private GameManager gameManager;
    private bool isFalling = false;

    [SerializeField] Transform rayStart;
    [SerializeField] GameObject colourCrystalEffect;
    [SerializeField] GameObject neutralCrystalEffect;
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float playerIncreasedSpeedAmount = 0.1f;
    [SerializeField] float playerDecreaseSpeedAmount = 1f;
    [SerializeField] float maxPlayerSpeed = 4.5f;

    private float startSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        startSpeed = playerSpeed;
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
        if(other.tag == "Colour Crystal")
        {
            gameManager.IncreaseScore(); 
            
            GameObject colCrystalFX = Instantiate(colourCrystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(colCrystalFX, 2); 
            Destroy(other.gameObject);
            if(playerSpeed <= 4)
            {
                IncreaseSpeed();
            }
        }
        else if(other.tag == "Glass Crystal")
        {
            gameManager.IncreaseScore();

            GameObject neutCrystalFX = Instantiate(neutralCrystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(neutCrystalFX, 2);
            Destroy(other.gameObject);
            if (playerSpeed > 2)
            {
                DecreaseSpeed();
            }
        }


    }

    private void IncreaseSpeed()
    {
        if (playerSpeed >= maxPlayerSpeed) { return; }
        
        playerSpeed += playerIncreasedSpeedAmount;
    }

    private void DecreaseSpeed()
    {
        if((playerSpeed - playerDecreaseSpeedAmount) < startSpeed) { return; } 
        
        playerSpeed -= playerDecreaseSpeedAmount;
    }
}
