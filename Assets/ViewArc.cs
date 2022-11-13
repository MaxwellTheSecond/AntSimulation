using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

       public class ViewArc : MonoBehaviour
    {
        public float shieldArea;

    [CustomEditor(typeof(ViewArc))]
    public class DrawWireArc : Editor
    {
        void OnSceneGUI()
        {
            Handles.color = Color.green;
            ViewArc myObj = (ViewArc)target;
            Handles.DrawSolidArc(myObj.transform.position, myObj.transform.up, -myObj.transform.right, 180, myObj.shieldArea);
            myObj.shieldArea = (float)Handles.ScaleValueHandle(myObj.shieldArea, myObj.transform.position + myObj.transform.forward * myObj.shieldArea, myObj.transform.rotation, 1, Handles.ConeHandleCap, 1);
        }
    }
    }
