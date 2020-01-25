using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public Sprite image;
    public AudioClip collectSound;
    public abstract void Interact(GameObject obj);
}