using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


//This class manages the movement and physics of the player and the death of the player.
public class PlayerController : MonoBehaviour
{
    //Floats
    public float jumpHeight = 15;
    float groundRadius = 0.2f;

    //Bools
    private bool isGround = false;
    private bool audioFadeHit = false;

    //Objects
    private Rigidbody2D rb;
    public GameObject levelCompleteUI;
    public AudioSource geoplexSolarRain;
    public Transform groundCheck;
    public LayerMask Ground;
    //public Text groundedDebugText;
    //public Text velocityDebugText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, Ground); //Checks to see if the player is touching the ground via the 'groundCheck' object.
    }
	
    void Update()
    {
        if (isGround)
        {
            Jump();
        }

        //Debug Information rendered as UI.
        //groundedDebugText.text = "isGround = " + isGround.ToString();
        //velocityDebugText.text = "Velocity = " + rb.velocity.ToString();

        //Fades the audio nearing the end of a level.
        if (audioFadeHit)
        {
            for (int i = 0; i < 100; i++)
            {
                GameManager.volume = GameManager.volume - 0.00001f;
            }
        }
    }

    //Called in Update(): when jump key is pressed, increase the Y velocity of this object by the value of jumpHeight.
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.velocity = rb.velocity + new Vector2(0, jumpHeight);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isGround)
        {
            rb.velocity = rb.velocity + new Vector2(0, jumpHeight);
        }
    }

    //The OnCollisionEnter2D function tests for a player hitting a spike and hitting the finish line.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Spike")
        {
            Death();
        }

        if (col.gameObject.name == "FinalCube")
        {
            Debug.Log("LEVEL COMPLETE!");

            Time.timeScale = 0;
            geoplexSolarRain.volume = 0f;

            levelCompleteUI.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "MainCamera")
        {
            Death();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (col.gameObject.name == "AudioFade")
        {
            audioFadeHit = true;
        }
    }

    //Increases attempts counter and resets the level on death.
    void Death()
    {
        GameManager.attempts += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
