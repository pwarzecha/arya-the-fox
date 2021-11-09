using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    
    public GameObject actionBox;
    public Text actionText;
    public string actionDialog;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    
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
           
            if (xPressed == false )
            {
                actionBox.SetActive(true);
                actionText.text = actionDialog;
            }
            if (Input.GetButtonDown("action"))// && playerInRange)
            {

                if (dialogBox.activeInHierarchy)
                {
                    dialogBox.SetActive(false);
                    actionBox.SetActive(true);
                    actionText.text = actionDialog;
                    xPressed = false;
                }
                else
                {
                    dialogBox.SetActive(true);
                    dialogText.text = dialog;
                    actionBox.SetActive(false);
                    xPressed = true;
                }
            }
            


        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
            actionBox.SetActive(false);
        }
    }

}

