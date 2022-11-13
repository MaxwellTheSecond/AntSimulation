using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    bool isMoving = false;
    bool isBeingCarried = false;
    bool isWanted = false;
    Vector3 clickPosition;
    
    public void ToggleCarry(){if(isBeingCarried) isBeingCarried=false; else isBeingCarried=true;}
    public void ToggleWanted(){if(isWanted) isWanted=false; else isWanted=true;}
    public bool IsBeingCarried{get { return isBeingCarried;}}
    public bool IsWanted{get { return isWanted;}}
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0;
            isMoving = true;
        }

        if(isMoving && transform.position != clickPosition)
        {
             transform.position = Vector2.MoveTowards(transform.position, clickPosition, speed * Time.deltaTime);
             isMoving = true;
        }
            else isMoving=false;
    }
}
