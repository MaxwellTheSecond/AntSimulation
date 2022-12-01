using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pheromone : MonoBehaviour
{
    [SerializeField] float pheromoneDecayRate = 1f;
    [SerializeField] float pheromoneDecayAmount = 0.1f;

    public float strength {get; private set;}
    public int type {get; private set;} //0 == home; 1 == food
    public Vector2 position {get; private set;}
    
    private void Awake() {
    InvokeRepeating("Decay", pheromoneDecayRate, pheromoneDecayRate);
    }

    public void InitializePheromone(float _x, float _y, int _type)
    {
        position = new Vector2(_x, _y);
        this.type = _type;
        this.GetComponent<Renderer>().material.color = _type==0 ? Color.blue : Color.red;
        this.strength = 1;
        PheromoneMap.instance.AddPheromone(this);
    }

    private void Decay()
    {
        strength-=pheromoneDecayAmount;
        
        if(strength>0)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Color color = renderer.material.color;
            color.a=strength;
            renderer.material.color = color;
        }
        else
        {   
            PheromoneMap.instance.RemovePheromone(this);
            Destroy(gameObject);
        }

    }
}
