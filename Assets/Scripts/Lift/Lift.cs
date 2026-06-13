using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Validation;

/*
 * Attach to
 */
namespace Lift {
    public class Lift : MonoBehaviour {
        public float downPosY = 0.6f;
        public float upPosY = 3f; // ganz komisch darum z anstatt y für vertikale Bewegung
        float delta = 10;
        public Vector3 startPos = new Vector3();
        public Vector3 endPos = new Vector3();
        public bool isUp;
        [PositiveNonZero] public float speed = 0.2f;


        void Start() 
        {
            startPos = transform.position;
            endPos = new Vector3(transform.position.x, startPos.y + delta, transform.position.z);
        
        }

        public void Move(bool up)
        {
            if (up)
            {
                Debug.Log("jetz soll hochfahren");
                Debug.Log(transform.position.y + " sollte kleiner sein als " + endPos.y);
                
                transform.position = endPos;
                /*while (transform.position.y < endPos.y)
                {
                    transform.position = Vector3.Lerp(startPos, endPos, speed);
                    if (Math.Abs(transform.position.y - endPos.y) < 0.2f)      // ganz komischer Fehler darum z anstatt y für vertikale Bewegung
                    {
                        transform.position = endPos; 
                    }
                }
                */
                isUp = true;
            }
            else if (!up)
            {
                Debug.Log("jetz soll runterfahren"); 
                while (transform.position.y < startPos.y)
                {
                    transform.position = Vector3.Lerp( endPos, startPos, speed);
                    if (Math.Abs(transform.position.y - startPos.y) < 0.1f)      // ganz komischer Fehler darum z anstatt y für vertikale Bewegung
                    {
                        transform.position = endPos;
                    }
                }
                isUp = false;
            }

        }
    }
}