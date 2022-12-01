using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneMap : MonoBehaviour
{
    public static PheromoneMap instance;

    Pheromone pheromone;
    int type;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    
    public PheromoneMap(Pheromone _pheromone, int _type)
    {
        this.pheromone = _pheromone;
        this.type = _type;
    }
}
