using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text lives;

    private int scoreValue = 0;

    public GameObject winTextobject;

    public GameObject loseTextobject; // makes object

    private int livesValue = 3; // life value

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    Animator anim;

    public Animator animator;
    private bool facingRight = true;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winTextobject.SetActive(false);
        lives.text = "Lives:"+ livesValue.ToString();
        loseTextobject.SetActive(false);
        musicSource.clip = musicClipOne;
        musicSource.Play();
        anim = GetComponent<Animator>();


    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        animator.SetFloat("HorizontalValue", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetFloat("VerticalValue", Input.GetAxis("Vertical"));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.D))

        {
          anim.SetInteger("State", 1); //walk
            anim.SetInteger("State",2);//Run

         }

         if (Input.GetKeyUp(KeyCode.D))

        {
          anim.SetInteger("State", 0); //stand

         }

        if (Input.GetKeyDown(KeyCode.A))

        {
          anim.SetInteger("State", 1); //walk

         }

        if (Input.GetKeyUp(KeyCode.A))

        {
          anim.SetInteger("State", 0); //stand

         }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State",3);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State",3);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score:"+ scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if(scoreValue == 4)
            {
                
                transform.position = new Vector3(71.0f,0.00f,0.00f); // new position
                livesValue = 3; 
                lives.text = "Lives:"+ livesValue.ToString();
                //reset lives
    
            }

         if( scoreValue >= 8)
            {
             winTextobject.SetActive(true); // win screen
             musicSource.clip = musicClipTwo;
            musicSource.Play();

            
            }
        }

     

        if(collision.collider.tag =="Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives:" + livesValue.ToString(); 
            Destroy(collision.collider.gameObject);

            if( livesValue == 0)
            {
                loseTextobject.SetActive(true); //Lose screen
                speed = 0;
                
            }
        }

    }

     void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
}