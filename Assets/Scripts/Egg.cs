using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Egg", menuName = "Egg")]
public class Egg : Item
{
    public int points;

    public override void Interact(GameObject obj)
    {
        PlayerLogic playerLogic = obj.GetComponent<PlayerLogic>();
        if (playerLogic == null)
            throw new MissingComponentException("Missing PlayerLogic component on " + obj.name);

        playerLogic.AddPoints(points);
    }
}
