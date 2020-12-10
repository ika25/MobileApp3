using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioIcon : MonoBehaviour
{
    public Sprite onSprite, offSprite;

    private Image image;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }

    // Function Switching between sprites
    public void SpriteSwitch()
    {
        if (image.sprite == onSprite)
        {
            image.sprite = offSprite;
        }
        else
        {
            image.sprite = onSprite;
        }
    }
}
