using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


//This class manages the movement and physics of the player and the death of the player.
public class PlayerController : MonoBehaviour
{
    public float jumpHeight = 15;
    private Rigidbody2D rb;
    private bool isGround = false;
    public GameObject levelCompleteUI;
    public AudioSource geoplexSolarRain;
    private bool audioFadeHit = false;
    public Text groundedDebugText;
    public Text velocityDebugText;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	void Update ()
    {
        if (isGround)
        {
            Jump();
        }

        //Debug Information
        groundedDebugText.text = "isGround = " + isGround.ToString();
        velocityDebugText.text = "Velocity = " + rb.velocity.ToString();

        if (audioFadeHit)
        {
            for (int i = 0; i < 50; i++)
            {
                geoplexSolarRain.volume -= 0.00001f;
            }
        }
    }

    //Two OnCollision functions test whether the player is touching the ground, if not, it sets isGround to false
    //The OnCollisionEnter2D function also tests for a player hitting a spike and hitting the finish line.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGround = true;
            rb.velocity = new Vector2(0, 0);
        }
        if (col.gameObject.tag == "Spike")
        {
            Death("Player hit Spike!");
        }
        if (col.gameObject.name == "FinalCube")
        {
            Debug.Log("LEVEL COMPLETE!");

            Time.timeScale = 0;
            geoplexSolarRain.volume = 0f;

            levelCompleteUI.SetActive(true);
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    void Death(string deathCondition)
    {
        //Anything that should happen upon player death should be put here
        GameManager.attempts += 1;
        Debug.Log(deathCondition);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //rb.AddForce(new Vector2(0, jumpForce));
            rb.velocity = rb.velocity + new Vector2(0, jumpHeight);
            Debug.Log("Space");
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //rb.AddForce(new Vector2(0, jumpForce));
            rb.velocity = rb.velocity + new Vector2(0, jumpHeight);
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "MainCamera")
        {
            Death("Player hit camera boundries!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (col.gameObject.tag == "AudioFade")
        {
            audioFadeHit = true;
        }
    }

    
}
