using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneMap : MonoBehaviour
{
    public static PheromoneMap instance;

    public List<Pheromone> homePheromones {get; private set;}
    public List<Pheromone> foodPheromones {get; private set;}
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
        homePheromones = new List<Pheromone>();
        foodPheromones = new List<Pheromone>();
    }

    public void AddPheromone(Pheromone _pheromone)
    {
        if(_pheromone.type == 0)
            homePheromones.Add(_pheromone);
        else foodPheromones.Add(_pheromone);
    }
    
    public void RemovePheromone(Pheromone _pheromone)
    {
        if(_pheromone.type == 0)
            homePheromones.Remove(_pheromone);
        else foodPheromones.Remove(_pheromone);
    }

    public List<Pheromone> GetAllInCircle(Vector2 _position, float _r, int _type)
    {
        List<Pheromone> found = new List<Pheromone>();
        if(_type==0)
        {
            foreach(Pheromone pheromone in homePheromones)
            {
                if((_position.x-pheromone.position.x) * (_position.x-pheromone.position.x) + (_position.y-pheromone.position.y) * (_position.y-pheromone.position.y) - _r * _r <=0)
                found.Add(pheromone);
            }
            return found;
        }
        else
        {
            foreach(Pheromone pheromone in foodPheromones)
            {
                if((_position.x-pheromone.position.x) * (_position.x-pheromone.position.x) + (_position.y-pheromone.position.y) * (_position.y-pheromone.position.y) - _r * _r <=0)
                found.Add(pheromone);
            }
            return found;
        }
    }

}
