using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public GameObject[] droppableObjects;
    public Transform[] droppableObjectPositions;

    // Start is called before the first frame update
    void Start()
    {
        if (droppableObjects.Length == 0)
            throw new UnassignedReferenceException("Please add droppable objects into array");

        if (droppableObjectPositions.Length == 0)
            throw new UnassignedReferenceException("Please add droppable locations into array");

        StartCoroutine(DropItems());
    }

    IEnumerator DropItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            int objectToDropIndex = Random.Range(0, droppableObjects.Length);
            int positionToDropIndex = Random.Range(0, droppableObjectPositions.Length);

            GameObject item = Instantiate(droppableObjects[objectToDropIndex], droppableObjectPositions[positionToDropIndex].position, Quaternion.identity, droppableObjectPositions[positionToDropIndex]);
        }
    }
}
