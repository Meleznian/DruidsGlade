using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RecipeBookUI : MonoBehaviour
{
    public List<Sprite> knownRecipes;         // Images of discovered recipes
    public List<Sprite> unknownPages;         // Placeholder pages for unknowns
    public Image leftPageImage;
    public Image rightPageImage;
    private int currentIndex = 0;

    void Start()
    {
        UpdatePages();
    }

    public void NextPage()
    {
        if (currentIndex < knownRecipes.Count - 2)
        {
            currentIndex += 2;
            UpdatePages();
        }
    }

    public void PreviousPage()
    {
        if (currentIndex >= 2)
        {
            currentIndex -= 2;
            UpdatePages();
        }
    }

    void UpdatePages()
    {
        leftPageImage.sprite = currentIndex < knownRecipes.Count ? knownRecipes[currentIndex] : unknownPages[0];
        rightPageImage.sprite = currentIndex + 1 < knownRecipes.Count ? knownRecipes[currentIndex + 1] : unknownPages[0];
    }
}
