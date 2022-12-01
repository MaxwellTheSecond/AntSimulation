using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    bool isBeingCarried = false;
    bool isWanted = false;
    Vector3 clickPosition;
    
    public void ToggleCarry(){if(isBeingCarried) isBeingCarried=false; else isBeingCarried=true;}
    public void ToggleWanted(){if(isWanted) isWanted=false; else isWanted=true;}
    public bool IsBeingCarried{get { return isBeingCarried;}}
    public bool IsWanted{get { return isWanted;}}
}
