using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryScript : MonoBehaviour
{
    public Image activePages;

    public Sprite[] pages;

    public enum Pages
    {
        page1, page3, page5, page7, page9, max
    }

    public Pages openPage;

    private void Start()
    {
        openPage = Pages.page1;
        activePages.sprite = pages[0];
    }

    public void FlipPage(int direction)
    {
        openPage += direction;
        if (openPage < 0)
            openPage = 0;
        else if (openPage > Pages.max)
            openPage = (Pages)Pages.max - 1;

        switch (openPage)
        {
            case Pages.page1:
                activePages.sprite = pages[0];
                break;
            case Pages.page3:
                activePages.sprite = pages[1];
                break;
            case Pages.page5:
                activePages.sprite = pages[2];
                break;
            case Pages.page7:
                activePages.sprite = pages[3];
                break;
            case Pages.page9:
                activePages.sprite = pages[4];
                break;
        }
    }
}
