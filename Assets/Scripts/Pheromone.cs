using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pheromone : MonoBehaviour
{
    [SerializeField] float pheromoneDecayRate = 1f;
    [SerializeField] float pheromoneDecayAmount = 0.1f;

    public float strength;
    private GameObject pheromone;
    public int type {get; set;} //0 == home; 1 == food
    private Vector2 position;
    
    private void Awake() {
    InvokeRepeating("Decay", pheromoneDecayRate, pheromoneDecayRate);
    }

    public void InitializePheromone(float _x, float _y, GameObject _pheromone, int _type)
    {
        this.position.x = _x;
        this.position.y = _y;
        this.type = _type;
        this.pheromone = _pheromone;
        this.pheromone.GetComponent<Renderer>().material.color = _type==0 ? Color.blue : Color.red;
        this.strength = 1;
    }

    private void Decay()
    {
        strength-=pheromoneDecayAmount;
        
        if(strength>0)
        {
            SpriteRenderer renderer = pheromone.GetComponent<SpriteRenderer>();
            Color color = renderer.material.color;
            color.a=strength;
            renderer.material.color = color;
        }

        else Destroy(gameObject);

    }
}
