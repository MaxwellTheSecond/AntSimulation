using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Controller options")]
    [SerializeField] GameObject foodGameObject;
    [SerializeField] GameObject antGameObject;
    [SerializeField] int startingAnts = 10;

    private Vector3 clickPosition;

    void Awake() {
        for(int i = 0; i< startingAnts; i++)
        {
            Instantiate(antGameObject, new Vector3(0,0,0), new Quaternion(Random.value,Random.value,Random.value,0));
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0;
            Instantiate(foodGameObject, clickPosition, Quaternion.identity);
        }

        if(Input.GetMouseButtonDown(2))
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0;
            Instantiate(antGameObject, clickPosition, Quaternion.identity);
        }
    }
}
