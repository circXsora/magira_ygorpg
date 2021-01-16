using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject item_object;
    public InventroyObject inventroy_object;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item_object.item_image;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!inventroy_object.items.Contains(item_object))
            {
                inventroy_object.items.Add(item_object); item_object.item_count = 1;
            }
            else
            {
                item_object.item_count++;
            }
            Destroy(gameObject);
        }
    }
}
