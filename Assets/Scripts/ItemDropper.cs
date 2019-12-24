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

    private int itemsDropped = 0;
    private float currentDropInterval;

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

            GameObject itemToDrop = GetItemToDrop();
            int positionToDropIndex = Random.Range(0, droppableObjectPositions.Length);
            GameObject item = Instantiate(itemToDrop, droppableObjectPositions[positionToDropIndex].position, Quaternion.identity, droppableObjectPositions[positionToDropIndex]);
            itemsDropped++;

            UpdateDropInterval();
        }
    }

    private GameObject GetItemToDrop()
    {
        if (itemsDropped < 3)
        {
            return egg;
        }
        else if (itemsDropped < 15)
        {
            int randomNumber = Random.Range(0, 10);
            if (randomNumber < 8)
            {
                return egg;
            }
            else
            {
                return bomb;
            }
        }
        else
        {
            int randomNumber = Random.Range(0, 10);
            if (randomNumber < 6)
            {
                return egg;
            }
            else if (randomNumber < 9)
            {
                return bomb;
            }
            else
            {
                return goldenEgg;
            }
        }
    }

    private void UpdateDropInterval()
    {
        if (currentDropInterval > minDropInterval)
        {
            if (itemsDropped % itemsBetweenSpeedChange == 0)
            {
                currentDropInterval -= dropSpeedInterval;
                currentDropInterval = Mathf.Round(currentDropInterval * 100f) / 100f;
            }

            if (currentDropInterval < minDropInterval)
                currentDropInterval = minDropInterval;
        }
    }
}
