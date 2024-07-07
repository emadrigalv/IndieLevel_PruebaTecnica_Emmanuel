using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            IPickable pickedItem = collision.gameObject.GetComponent<IPickable>();

            pickedItem.TakeIt();
        }
    }
}
