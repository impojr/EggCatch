using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [Tooltip("First Item should be bomb, Second Item should be egg, Third item should be golden egg")]
    public GameObject egg;
    public GameObject bomb;
    public GameObject goldenEgg;
    public Transform[] droppableObjectPositions;

    [Space]
    [Header("Drop Parameters")]
    public float startDropInterval = 1.5f;
    public float minDropInterval = 0.25f;
    public float dropSpeedInterval = 0.25f;
    public int itemsBetweenSpeedChange = 10;

    [Space]
    [Header("Drop Chance Parameters")]
    [Tooltip("A 1 in chanceOfBomb chance of dropping first bomb")]
    public int chanceOfFirstBomb = 3;
    [Tooltip("A 1 in chanceOfBomb chance of dropping second bomb")]
    public int chanceOfSecondBomb = 5;

    private int linesOfItemsDropped = 0;
    private float currentDropInterval;
    private List<Transform> dropPositions;

    // Start is called before the first frame update
    void Start()
    {
        if (egg == null || bomb == null || goldenEgg == null)
            throw new UnassignedReferenceException("Please add droppable objects into script");

        if (droppableObjectPositions.Length == 0)
            throw new UnassignedReferenceException("Please add droppable locations into array");

        currentDropInterval = startDropInterval;
        StartCoroutine(DropItems());
    }

    IEnumerator DropItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentDropInterval);

            dropPositions = CreateDropLocationList();

            DropEgg();
            DropBomb(15, true);
            DropBomb(50, false);

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
        int positionToDropIndex = Random.Range(0, dropPositions.Count);
        Transform positionToDrop = dropPositions[positionToDropIndex];

        dropPositions.RemoveAt(positionToDropIndex);

        GameObject eggToDrop = GetEggTypeToDrop();
        GameObject eggItem = Instantiate(eggToDrop, positionToDrop.position, Quaternion.identity, positionToDrop);
    }

    private GameObject GetEggTypeToDrop()
    {
        if (linesOfItemsDropped == 0)
            return egg;

        return linesOfItemsDropped % 10 == 0 ? goldenEgg : egg;
    }

    private void DropBomb(int lineItemsDropped, bool firstBomb)
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
                int positionToDropIndex = Random.Range(0, dropPositions.Count);
                Transform positionToDrop = dropPositions[positionToDropIndex];
                dropPositions.RemoveAt(positionToDropIndex);

                GameObject bombItem = Instantiate(bomb, positionToDrop.position, Quaternion.identity, positionToDrop);
            }

            if (linesOfItemsDropped % 20 == 0)
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

    private void UpdateDropInterval()
    {
        if (currentDropInterval > minDropInterval)
        {
            if (linesOfItemsDropped % itemsBetweenSpeedChange == 0)
            {
                currentDropInterval -= dropSpeedInterval;
                currentDropInterval = Mathf.Round(currentDropInterval * 100f) / 100f;

                itemsBetweenSpeedChange *= 2;
            }

            if (currentDropInterval < minDropInterval)
                currentDropInterval = minDropInterval;
        }
    }
}
