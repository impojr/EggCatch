using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public GameObject egg;
    public GameObject bomb;
    public GameObject goldenEgg;
    [Tooltip("Positions where the items will drop from")]
    public Transform[] droppableObjectPositions;

    [Space]
    [Header("Drop Parameters")]
    public float startDropInterval = 1.5f;
    public float minDropInterval = 0.25f;
    public float dropSpeedInterval = 0.25f;
    public int itemsBetweenSpeedChange = 10;

    [Header("Drop Chance Parameters")]
    [Space]
    [Tooltip("Golden Egg will drop in one every X eggs")]
    public int goldenEggDropRate = 10;
    [Tooltip("A 1 in chanceOfBomb chance of dropping first bomb")]
    public int chanceOfFirstBomb = 3;
    [Tooltip("A 1 in chanceOfBomb chance of dropping second bomb")]
    public int chanceOfSecondBomb = 5;
    [Tooltip("The first bomb won't start the drop logic until this many lines of items have dropped")]
    public int linesOfItemsDroppedBeforeFirstBombDrop = 15;
    [Tooltip("The second bomb won't start the drop logic until this many lines of items have dropped")]
    public int linesOfItemsDroppedBeforeSecondBombDrop = 50;
    [Tooltip("Bomb drop chance will increase in one every x eggs")]
    public int bombDropIncreaseRate = 20;

    private int linesOfItemsDropped = 0;
    private float currentDropInterval;
    private List<Transform> availableDropPositions;

    // Start is called before the first frame update
    void Start()
    {
        if (egg == null || bomb == null || goldenEgg == null)
            throw new UnassignedReferenceException("Please add droppable objects into script");

        if (droppableObjectPositions.Length == 0)
            throw new UnassignedReferenceException("Please add droppable locations into script");

        currentDropInterval = startDropInterval;
        StartCoroutine(DropItems());
    }

    IEnumerator DropItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentDropInterval);

            availableDropPositions = CreateDropLocationList();

            DropEgg();
            RandomlyDropBombFromPercentage(linesOfItemsDroppedBeforeFirstBombDrop, true);
            RandomlyDropBombFromPercentage(linesOfItemsDroppedBeforeSecondBombDrop, false);

            linesOfItemsDropped++;
            UpdateDropInterval();
        }
    }

    private List<Transform> CreateDropLocationList()
    {
        List<Transform> dropPositions = new List<Transform>();

        foreach (Transform pos in droppableObjectPositions)
        {
            dropPositions.Add(pos);
        }

        return dropPositions;
    }

    private void DropEgg()
    {
        int positionToDropIndex = Random.Range(0, availableDropPositions.Count);
        Transform positionToDrop = availableDropPositions[positionToDropIndex];

        availableDropPositions.RemoveAt(positionToDropIndex);

        GameObject eggToDrop = GetEggTypeToDrop();
        GameObject eggItem = Instantiate(eggToDrop, positionToDrop.position, Quaternion.identity, positionToDrop);
    }

    private GameObject GetEggTypeToDrop()
    {
        if (linesOfItemsDropped == 0)
            return egg;

        return linesOfItemsDropped % goldenEggDropRate == 0 ? goldenEgg : egg;
    }

    /// <summary>
    /// Will determine whether or not to drop a bomb alongside the egg based on chance.
    /// Will increase bomb drop chance after certain amount of item drops
    /// </summary>
    /// <param name="lineItemsDropped">The lines of items already dropped</param>
    /// <param name="firstBomb">Is this the first bomb to drop alongside the egg?</param>
    private void RandomlyDropBombFromPercentage(int lineItemsDropped, bool firstBomb)
    {
        int chanceOfBomb;

        if (firstBomb)
        {
            chanceOfBomb = chanceOfFirstBomb;
        } else
        {
            chanceOfBomb = chanceOfSecondBomb;
        }

        if (linesOfItemsDropped >= lineItemsDropped)
        {
            if (Random.Range(0, chanceOfBomb) == 0)
            {
                int positionToDropIndex = Random.Range(0, availableDropPositions.Count);
                Transform positionToDrop = availableDropPositions[positionToDropIndex];
                availableDropPositions.RemoveAt(positionToDropIndex);

                GameObject bombItem = Instantiate(bomb, positionToDrop.position, Quaternion.identity, positionToDrop);
            }

            if (linesOfItemsDropped % bombDropIncreaseRate == 0)
            {
                if (firstBomb)
                {
                    chanceOfFirstBomb = chanceOfFirstBomb == 1 ? 1 : chanceOfFirstBomb - 1;
                } else
                {
                    chanceOfSecondBomb = chanceOfSecondBomb == 2 ? 2 : chanceOfSecondBomb - 1;
                }
            }
        }
    }

    /// <summary>
    /// Updates the drop interval of items by the specified drop speed chance, every time the specified amount of items has been dropped
    /// When speed is updated, the amount of items until the next speed increase is doubled
    /// If the drop interval is reduced to lower than the minimum, it will be made equal to the minimum
    /// </summary>
    private void UpdateDropInterval()
    {
        if (currentDropInterval > minDropInterval)
        {
            if (linesOfItemsDropped % itemsBetweenSpeedChange == 0)
            {
                currentDropInterval -= dropSpeedInterval;
                currentDropInterval = Mathf.Round(currentDropInterval * 100f) / 100f;

                itemsBetweenSpeedChange *= 2; //increase the amount of items between speed increase, this way the speed doesn't increase too fast each time
            }

            if (currentDropInterval < minDropInterval)
                currentDropInterval = minDropInterval;
        }
    }
}
