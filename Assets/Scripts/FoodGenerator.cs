using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    [SerializeField] GameObject foodGameObject;
    [Header("Food generation")]
    [Tooltip("Food per second")]
    [SerializeField] float generationRate = 3f;
    

    void Awake()
    {
        InvokeRepeating("ProduceFood", 1f, generationRate);
    }

    private void ProduceFood()
    {
        Instantiate(foodGameObject, (Vector2)transform.position + (Random.insideUnitCircle * 2), Quaternion.identity);
    }
    
}
