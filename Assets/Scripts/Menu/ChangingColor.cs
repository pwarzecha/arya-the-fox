using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingColor : MonoBehaviour
{
    public GameObject panel;
    public SpriteRenderer cape;
    public Image squareCapeDisplay;

    public Color pink;
    public Color blue;
    public Color green;
    public Color yellow;

    public int whatColor = 1;

    void Update()
    {
        squareCapeDisplay.color = cape.color;
        if (whatColor == 1)
        {
            cape.color = new Color(220, 65, 180, 1);//pozmieniac kolor
        } 
        else if (whatColor == 2) 
        {
            cape.color = new Color(220, 65, 180, 1);//pozmieniac kolor
        }
        else if (whatColor == 3) 
        {
            cape.color = new Color(220, 65, 180, 1); //pozmieniac kolor
        }
        else if (whatColor == 4) 
        { 
            cape.color = new Color(25, 255, 0, 1);
           
        }
    }
    public void OpenPanel()
    {
        panel.SetActive(true);
    }
    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void ChangeCapePink()
    {
        whatColor = 1;
    }

    public void ChangeCapeBlue()
    {
        whatColor = 2;
    }

    public void ChangeCapeGreen()
    {
        whatColor = 3;
    }

    public void ChangeCapeYellow()
    {
        whatColor = 4;
    }
}
