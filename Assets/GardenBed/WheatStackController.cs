using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatStackController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
           if (collision.gameObject.GetComponent<Inventory>().Add()) Destroy(gameObject);
   }
}
