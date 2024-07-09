using UnityEngine;
using UnityEngine.Events;

public class ItemCollector : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnCoinCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            IPickable pickedItem = collision.gameObject.GetComponent<IPickable>();

            pickedItem.TakeIt();

            OnCoinCollected.Invoke();
        }
    }
}
