using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    [SerializeField] GameObject foodGameObject;
    [SerializeField] float generationRate = 3f;
    bool isProducing=false;
    // Start is called before the first frame update
    void Start()
    {
        isProducing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isProducing)
        StartCoroutine("ProduceFood");
    }

    IEnumerator ProduceFood()
    {
        isProducing=true;
        yield return new WaitForSeconds(generationRate);
        isProducing=false;
        Instantiate(foodGameObject, (Vector2)transform.position + (Random.insideUnitCircle * 2), Quaternion.identity);
    }
    
}
