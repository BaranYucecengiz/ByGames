using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rgb;
    public Animator animator;
    public GameObject player;
    
    //Movement
    Vector3 velocity;
    Vector3 start_position = new Vector3(-5.91f, -3.56f, 0f);
    float speed = 4f;
    float jumpAmount = 6f;
    bool jump = false;
    float maxYVel;

    
    //HealthBar
    public Image[] hearts;
    private int maxHealth = 5;
    private int currenthealth = 5;
    //PlayerScore
    public TextMeshProUGUI playerScoreText;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        animator = GetComponent<Animator>();
        rgb = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
        //Move
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
        transform.position += velocity * speed * Time.deltaTime;
        animator.SetFloat("Speed",Mathf.Abs(Input.GetAxis("Horizontal")));
        
        if(Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.rotation = Quaternion.Euler(0f,0f,0f);
        }
        if (Input.GetKeyDown(KeyCode.W) && !animator.GetBool("IsJump"))
        {
            
                rgb.AddForce(Vector3.up * jumpAmount, ForceMode2D.Impulse);
            maxYVel = 0;
            

        }
        //Score
        playerScoreText.text = score.ToString();

        //Damage
        if (currenthealth > 0)
        {
            if (rgb.velocity.y == 0 && !jump)
            {
                if (maxYVel <= -10)
                {
                    if (currenthealth > maxHealth)
                    {
                        currenthealth = maxHealth;
                    }
                    else
                    {
                        Damage(1);

                    }
                    jump = true; ;

                }

            }
            else if (jump)
            {
                if (rgb.velocity.y < maxYVel)
                {
                    maxYVel = rgb.velocity.y;
                }
            }
            
        }
        else
        {
            currenthealth = maxHealth;
            transform.position = start_position;
            Regen(maxHealth -(maxHealth - currenthealth));
            Debug.Log(maxHealth - (maxHealth - currenthealth));
        }
        
        }
    
    
    //Damage - Regen
    void Damage(int amount)
    {

        if (currenthealth > 0)
        {
            hearts[currenthealth - 1].enabled = false;
            currenthealth -= amount;
        }
        Debug.Log("Damage");

    }
    void Regen(int amount)
    {
        currenthealth += amount;

        for (int i = 0; i < currenthealth; i++)
        {

            hearts[i].enabled = true;
        }

    }
    //

    private void OnCollisionEnter2D(Collision2D collision) // Baþka bir colliderla etkileþimden sonra çalýþan fonk
    {
        if (collision.gameObject.tag == "Ground" )
        {
           
            animator.SetBool("IsJump", false);
            jump = false;
        }
        if (collision.gameObject.tag == "See")
        {
            transform.position = start_position;
            Damage(5);
        }
        if (collision.gameObject.tag == "Blocks")
        {

            animator.SetBool("IsJump", false);
            jump = false;
        }
        if (collision.gameObject.tag == "Platform")
        {

            animator.SetBool("IsJump", false);
            jump = false;
            player.transform.parent = collision.gameObject.transform; // Cismimizin platformla birlikte hareket etmesini saðlýyor
        }                                                           //parent -> öncesinde gelen deðerin elemanlarý , ->transform.parent burada parent x y z yi temsil ediyor. 
    }
    private void OnCollisionExit2D(Collision2D collision) // Baþka bir colliderla etkileþimi kestikten sonra çalýþan fonk
    {
        if(collision.gameObject.tag == "Ground" )
        {
            
            animator.SetBool("IsJump", true);
            jump = true;
        }
        
        if (collision.gameObject.tag == "Platform")
        {

            animator.SetBool("IsJump",true);
            jump = true;
            player.transform.parent = null;
        }
    }
    
}

