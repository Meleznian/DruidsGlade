using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    int index;

    public GameObject[] pages;
    public GameObject[] backPages;

    public GameObject nextButton;
    public GameObject prevButton;
    public GameObject backNextButton;
    public GameObject backPrevButton;

    public void NextPage()
    {
        pages[index].SetActive(false);
        backPages[index].SetActive(false);

        index++;
        pages[index].SetActive(true);
        backPages[index].SetActive(true);


        CheckButtons();
    }

    public void PrevPage()
    {
        pages[index].SetActive(false);
        backPages[index].SetActive(false);
        index--;
        pages[index].SetActive(true);
        backPages[index].SetActive(true);

        CheckButtons();
    }
    
    void CheckButtons()
    {
        print(index);
        print(pages.Length);

        if(index == 0)
        {
            prevButton.SetActive(false);
            backPrevButton.SetActive(false);
        }
        else if(index == 1)        
        {
            prevButton.SetActive(true);
            backPrevButton.SetActive(true);
        }
        if(index == pages.Length - 1)
        {
            nextButton.SetActive(false);
            backNextButton.SetActive(false);

        }
        else if (index == pages.Length - 2)
        {
            nextButton.SetActive(true);
            backNextButton.SetActive(true);

        }
    }

}
