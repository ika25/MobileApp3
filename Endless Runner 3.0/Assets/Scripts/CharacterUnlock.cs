using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUnlock : MonoBehaviour
{
    public Sprite unlocked, active;

    private Image image;
    // Use this for initialization
    void Awake()
    {
        image = GetComponent<Image>();// get image
    }
    public void Unlock()
    {
        image.sprite = unlocked;//unlocked
    }
    public void Activate()
    {
        image.sprite = active;//activated
    }

}
