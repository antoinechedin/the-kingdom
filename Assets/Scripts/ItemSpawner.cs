using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float spawnDuration;
    
    private float timer;
    private GameObject item;

    private void Awake()
    {
        timer = 0;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        if(timer > spawnDuration)
        {
            GameObject item = Instantiate(itemPrefab, transform.position, transform.rotation);
            this.item = item;
            timer = 0;
        }

        if (item == null)
        {
            timer += Time.deltaTime;
        }
    }
}
