using NUnit.Framework;
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
        Vector3 startPos = new Vector3();
        Vector3 endPos = new Vector3();
        public bool isUp;
        [PositiveNonZero] public float speed = 0.2f;


        void Start() 
        {
            startPos = transform.position;
            endPos = new Vector3(transform.position.x, transform.position.y,upPosY);
        
        }

        public void Move(bool up)
        {
            if (up)
            {
                while (transform.position.z < endPos.z)
                {
                    transform.position = Vector3.Lerp(startPos, endPos, speed);
                    if (transform.position.z - upPosY < 0.01f)      // ganz komischer Fehler darum z anstatt y für vertikale Bewegung
                    {
                        transform.position = endPos; 
                    }
                }
                isUp = true;
            }
            else if (!up)
            {
                while (transform.position.z < upPosY)
                {
                    transform.position = Vector3.Lerp(endPos, startPos, speed);
                    if (transform.position.z - downPosY < 0.01f)        // ganz komischer Fehler darum z anstatt y für vertikale Bewegung
                    {
                        transform.position = startPos; 
                    }
                }
                isUp = false;
            }

        }
    }
}