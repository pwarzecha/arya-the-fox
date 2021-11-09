using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    public Signal playerHit;

    public GameObject deathPanel;
    public string mainMenu;

    
    public Vector2 playerInput;

    // Start is called before the first frame update
    void Start() 
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        change.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack
            && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }

       
        //UpdateAnimationAndMove();

        /*
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (change != Vector3.zero)
        {
            MoveCharacter();
        }*/
    }

    void FixedUpdate()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        myRigidbody.velocity = playerInput * speed;
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        //yield return null;
        yield return new WaitForSeconds(0.1f); //null; //delay 1 frame
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.35f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            //transform.Translate(new Vector3(change.x, change.y)); //zastapiane funkcja
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);

        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
    
    void MoveCharacter()
    {
        //wersja 1, problem z poruszaniem
       //myRigidbody.MovePosition(
       //transform.position + change * speed  * Time.deltaTime );

        //change.Normalize(); //cos nie pyklo speedhack XD

        transform.Translate(new Vector3(change.x, change.y)); //dopracowac atak po skosie
        if (change.x < 0)
        { change.x = -1; }
        else if (change.x > 0)
        { change.x = 1; }
        else if (change.y < 0)
        { change.y = -1; }
        else if (change.y > 0)
        { change.y = 1; }
    }
    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {

            StartCoroutine(KnockCo(knockTime));
            //enemyhit
        } else
        {
            FindObjectOfType<AudioManager>().Stop("Theme");
            FindObjectOfType<AudioManager>().Play("PlayerDeath");

            this.gameObject.SetActive(false);

            deathPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        
    }

    private IEnumerator KnockCo(float knockTime)
    {
        FindObjectOfType<AudioManager>().Play("EnemyHit");
        playerHit.Raise();
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
