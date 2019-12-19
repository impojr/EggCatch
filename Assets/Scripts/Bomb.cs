using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bomb", menuName = "Bomb")]
public class Bomb : Item
{
    public float damage;

    public override void Interact(GameObject obj)
    {
        PlayerLogic playerLogic = obj.GetComponent<PlayerLogic>();
        if (playerLogic == null)
            throw new MissingComponentException("Missing PlayerLogic component on " + obj.name);

        playerLogic.TakeLife();
    }
}
