using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    [SerializeField] [Range(10, 30)] int asteroidCount = 30;
    [SerializeField][Range(20f, 50f)] private float radius = 50f;
    [SerializeField][Range(1f, 5f)] private float _maxscale = 5f;
    [SerializeField]private List<GameObject> asteroidPrefabs;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
       for (int i = 0; i < asteroidCount; i++)
        {
            GameObject _asteroid = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Count)], _transform.position, Quaternion.identity);
           float scale = Random.Range(0.5f, _maxscale);

            _asteroid.transform.localScale = new Vector3(x:scale, y:scale, scale);

            _asteroid.transform.position += Random.insideUnitSphere * radius;

            _asteroid.GetComponent<Rigidbody>()?.AddTorque(Random.insideUnitCircle * Random.Range(0f, 50f));
        }
    }
}
