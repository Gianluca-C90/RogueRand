using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnableObjects;
    Vector3 offset = new Vector3(0, 3, 0);

    private void Awake()
    {
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
            spawnableObjects.Add(item.gameObject);
        }
    }

    public void DropRandomReward(Vector3 position)
    {
        GameObject pickedObject = RandomPicker();
        pickedObject.transform.localPosition = position + offset;
        pickedObject.SetActive(true);
    }

    GameObject RandomPicker()
    {
        int count = spawnableObjects.Count;
        int picked = UnityEngine.Random.Range(0, count);
        if (!spawnableObjects[picked].activeSelf)
            return spawnableObjects[picked];
        else
            RandomPicker();
        return spawnableObjects[picked];
    }
}
