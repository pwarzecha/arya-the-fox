using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : Interactable
{

    public GameObject actionBox;
    public Text actionText;
    public string actionDialog;

    private bool xPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {

            if (xPressed == false)
            {
                actionBox.SetActive(true);
                actionText.text = actionDialog;
            }
            if (Input.GetButtonDown("action"))// && playerInRange)
            {
                FindObjectOfType<AudioManager>().Stop("Theme");
                FindObjectOfType<AudioManager>().Play("Radio");
            }



        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;
            FindObjectOfType<AudioManager>().Stop("Radio");
            FindObjectOfType<AudioManager>().Play("Theme");
            actionBox.SetActive(false);
        }
    }

}

