using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    int index;

    public GameObject[] pages;

    public GameObject nextButton;
    public GameObject prevButton;

    public void NextPage()
    {
        pages[index].SetActive(false);
        index++;
        pages[index].SetActive(true);
        
        CheckButtons();
    }

    public void PrevPage()
    {
        pages[index].SetActive(false);
        index--;
        pages[index].SetActive(true);

        CheckButtons();
    }
    
    void CheckButtons()
    {
        print(index);
        print(pages.Length);

        if(index == 0)
        {
            prevButton.SetActive(false);
        }
        else if(index == 1)        
        {
            prevButton.SetActive(true);
        }
        if(index == pages.Length - 1)
        {
            nextButton.SetActive(false);
        }
        else if (index == pages.Length - 2)
        {
            nextButton.SetActive(true);
        }
    }

}
