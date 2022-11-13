using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    [SerializeField] float lookSpeed = 5f;
    [SerializeField] float moveSpeed = 5f;

    Transform anthillTransform;
    Food closestFood = null;
    bool carryingFood = false;
    
    private void Start() {
        anthillTransform = GameObject.FindGameObjectWithTag("Anthill").transform;
    }
    void Update()
    {
        if(closestFood == null) FindClosestFood();

        if(!carryingFood && closestFood != null)
        {
            LookTowards(closestFood.transform);
            MoveTowards(closestFood.transform);
        }

        if(carryingFood) 
        {
            LookTowards(anthillTransform);
            MoveTowards(anthillTransform);
        }
    }


    private void FindClosestFood()
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
	}
    
    private void LookTowards(Transform objectToLookAt)
    {
        Vector2 direction = objectToLookAt.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
    }


    private void MoveTowards(Transform objectToMoveTowards)
    {
        transform.position = Vector2.MoveTowards(transform.position, objectToMoveTowards.position, moveSpeed * Time.deltaTime);
    }

    private void MoveAndLookTowards(Transform objectToMoveTowards)
    {
        Vector2 direction = objectToMoveTowards.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.SetPositionAndRotation(direction, Quaternion.Euler(0,0,angle));
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
        Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void TransportFoodToAnthill()
    {
        transform.position = Vector2.MoveTowards(transform.position, anthillTransform.position, moveSpeed * Time.deltaTime);
    }

   
}
