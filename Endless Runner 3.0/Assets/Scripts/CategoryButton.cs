using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    public Sprite onSprite, offSprite;
    public GameObject achievementMenu;//referance to the menu that will be activated
    private Image image;

    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Switch()
    {
        if (image.sprite == onSprite)
        {
            image.sprite = offSprite;
            achievementMenu.SetActive(false);//deactivaet window
        }
        else
        {
            image.sprite = onSprite;
            achievementMenu.SetActive(true);// activated
        }
    }
}
