
using UnityEngine;

public class ScrapItem : MonoBehaviour
{
    [Header("Scrap Settings")]
    [SerializeField] private int scrapValue = 10;
    [SerializeField] private float rotationSpeed = 60f;

    private void Update()
    {
        // Make the scrap rotate so it is easy to spot.
        transform.Rotate(
            Vector3.up * rotationSpeed * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        ScrapCollector collector =
            other.GetComponentInParent<ScrapCollector>();

        if (collector != null)
        {
            collector.CollectScrap(scrapValue);

            // Remove the scrap after collection.
            Destroy(gameObject);
        }
    }
}
