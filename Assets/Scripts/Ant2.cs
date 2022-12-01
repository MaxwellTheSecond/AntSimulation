using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Ant2 : MonoBehaviour
{
    [Header("Ant Configuration")]
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float steerStrength = 1f;
    [SerializeField] float wanderStrength = 0.5f;
    [SerializeField] float viewRadius = 45f;
    [SerializeField] float viewAngle = 45f;

    [Header("Sensors")]
    [SerializeField] GameObject leftSensor;
    [SerializeField] GameObject centerSensor;
    [SerializeField] GameObject rightSensor;

    [Header("Pheromone")]
    [SerializeField] GameObject pheromoneModel;
    [SerializeField] float pheromoneCooldown;
    [SerializeField] LayerMask pheromoneMask;
    Vector2 position;
    Vector2 velocity;
    Vector2 desiredDirection;
    Food closestFood = null;
    bool carryingFood = false;
    Transform anthillTransform;
    public LayerMask foodLayer;
    List<Pheromone> map;

    void Awake()
    {
        anthillTransform = GameObject.FindGameObjectWithTag("Anthill").transform;
        position = transform.position;
        map = new List<Pheromone>();
        InvokeRepeating("LeavePheromone", 0f, pheromoneCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        if(closestFood == null) SeekFood();
        
        if(closestFood != null && !carryingFood) desiredDirection = (closestFood.transform.position - transform.position).normalized;

        //if(carryingFood) desiredDirection = (anthillTransform.position - transform.position).normalized;

        PheromoneSteering();
        Wander();
    }


    /*private void FindClosestFood()
	{
		float distanceToClosestFood = Mathf.Infinity;
		
		Food[] allFoods = GameObject.FindObjectsOfType<Food>();
        if(allFoods != null){
		foreach (Food currentFood in allFoods) {
			float distanceToFood = (currentFood.transform.position - this.transform.position).sqrMagnitude;
			if (distanceToFood < distanceToClosestFood && !currentFood.IsBeingCarried && !currentFood.IsWanted) {
				distanceToClosestFood = distanceToFood;
				closestFood = currentFood;
			}
		}
        if(closestFood != null ) closestFood.ToggleWanted();
        }
	}*/

    private void SeekFood()
    {
        if(closestFood == null)
        {
            Collider2D[] allFoods = Physics2D.OverlapCircleAll(position, viewRadius, foodLayer);
            if(allFoods.Length>0)
            {
                Transform food = allFoods[Random.Range(0,allFoods.Length)].transform;
                Food _thisFood  = food.GetComponent<Food>();
                Vector2 dirToFood = (food.transform.position - transform.position).normalized;
                if(Vector2.Angle(Vector3.forward, dirToFood) < viewAngle / 2)
                {
                    _thisFood.ToggleWanted();
                    closestFood = _thisFood;
                }
            }
        }
    }


    private void Wander()
    {
        desiredDirection = (desiredDirection + Random.insideUnitCircle * wanderStrength).normalized;

        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrength)/1;

        velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        position += velocity * Time.deltaTime;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90;
        transform.SetPositionAndRotation(position, Quaternion.Euler(0,0, angle));
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Food" && !carryingFood && other.gameObject == closestFood.gameObject)
        {
            other.transform.parent = transform.GetChild(0);
            carryingFood = true;
            closestFood.ToggleCarry();
            closestFood.GetComponent<Collider2D>().enabled = false;
            closestFood.gameObject.transform.position = transform.GetChild(0).position;
        }
        if(other.transform.tag == "Anthill" && carryingFood)
        {
            GameObject _thisFood = transform.GetChild(0).GetChild(0).gameObject;
            Destroy (_thisFood);
            carryingFood = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ant")
        {
        Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void LeavePheromone()
    {
        GameObject pheromone = Instantiate(pheromoneModel, new Vector3(position.x, position.y, 0), Quaternion.identity);
        Pheromone component = pheromone.GetComponent<Pheromone>();
        component.InitializePheromone(position.x, position.y, pheromone, carryingFood ? 1 : 0);
    }


    private void PheromoneSteering()
    {
        float leftSensorValue = GetSensorValue(leftSensor);
        float centerSensorValue = GetSensorValue(centerSensor);
        float rightSensorValue = GetSensorValue(rightSensor);

        if(centerSensorValue > Mathf.Max(leftSensorValue, rightSensorValue))
        desiredDirection = (desiredDirection + new Vector2(0,1)).normalized;
        else if(leftSensorValue > rightSensorValue)
        desiredDirection = (desiredDirection + new Vector2(-1,-1)).normalized;
        else if(rightSensorValue > leftSensorValue)
        desiredDirection =  (desiredDirection + new Vector2(1,1)).normalized;


    }
    
    private float GetSensorValue(GameObject _sensor)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_sensor.transform.position, 0.5f, pheromoneMask);
        float sensorValue = 0f;
        foreach (Collider2D collider in colliders)
        {
            Pheromone pheromone = collider.gameObject.GetComponent<Pheromone>();
            if(carryingFood && pheromone.type == 0)
            {
            sensorValue += pheromone.strength;
            Debug.Log("found blue bubble");
            }
            else if(!carryingFood && pheromone.type == 1)
            {
            sensorValue += pheromone.strength;
            Debug.Log("found red bubble");
            }
        }
        return sensorValue;
    }

}




