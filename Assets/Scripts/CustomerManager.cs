using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject Customer;
    [SerializeField] private float spawnDistance, delayDecreaseAmount;
    [SerializeField] private Vector2 spawnDelayMinMax;

    float spawnDelay;

    private void Start ()
    {
        StartCoroutine (SpawnLoop ());
    }

    private void SpawnCustomer ()
    {
        float angle = Random.Range (-Mathf.PI, Mathf.PI);
        Vector3 dir = new Vector3 (Mathf.Cos (angle), 1, Mathf.Sin (angle));
        Debug.Log (Mathf.Cos (angle));
        Debug.Log (Mathf.Sin (angle));
        Vector3 position = dir * spawnDistance;
        position.y = 1;
        Instantiate (Customer, position, Quaternion.identity);
    }

    IEnumerator SpawnLoop ()
    {
        spawnDelay = spawnDelayMinMax.y;

        while (true)
        {
            SpawnCustomer ();
            yield return new WaitForSeconds (spawnDelay);
            spawnDelay = Mathf.Max (spawnDelay - delayDecreaseAmount, spawnDelayMinMax.x);
        }
    }
}
