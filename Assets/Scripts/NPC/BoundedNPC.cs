using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedNPC : Interactable
{
    public float moveSpeed;
    private Rigidbody2D myRigidbody;
    private Animator anim;

    public Collider2D walkZone;
    public bool isWalking;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    private int WalkDirection;

    private bool hasWalkZone;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();

        if(walkZone != null)
        {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerInRange)
        {
            Move();
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
            UpdateAnimation();
        }

    }


    private void Move()
    {
        if (isWalking)
        {
            walkCounter -= Time.deltaTime;
            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
            switch (WalkDirection)
            {
                case 0:
                    myRigidbody.velocity = new Vector2(0, moveSpeed);
                    if (hasWalkZone && transform.position.y > maxWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 1:
                    myRigidbody.velocity = new Vector2(moveSpeed, 0);
                    if (hasWalkZone && transform.position.x > maxWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 2:
                    myRigidbody.velocity = new Vector2(0, -moveSpeed);
                    if (hasWalkZone && transform.position.y < minWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 3:
                    myRigidbody.velocity = new Vector2(-moveSpeed, 0);
                    if (hasWalkZone && transform.position.x < minWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    break;
                default:
                    break;
            }
            UpdateAnimation();
        }
        else
        {
            waitCounter -= Time.deltaTime;

            myRigidbody.velocity = Vector2.zero;
            UpdateAnimation();
            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }

    void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
        
    }

    void UpdateAnimation()
    {
        anim.SetFloat("MoveX", myRigidbody.velocity.x);
        anim.SetFloat("MoveY", myRigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("kolizja");
        Vector2 temp = myRigidbody.velocity;
        ChooseDirection();
        int loops = 0;
        while(temp == myRigidbody.velocity && loops < 100)
        {
            loops++;
            ChooseDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Debug.Log("gracz");
            context.Raise();
            playerInRange = true;
        }
    }
}
