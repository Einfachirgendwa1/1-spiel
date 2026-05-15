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
        public bool isUp;
        [PositiveNonZero] public float speed = 0.2f;

        public void Move(bool up)
        {
            if (up)
            {
                while (transform.position.z < upPosY)
                {
                    //Lerpen
                    if(transform.position.z - upPosY < 0.01f)
                    {
                        transform.position = new Vector3(transform.position.x,transform.position.y, upPosY); // ganz komischer Fehler darum z anstatt y für vertikale Bewegung
                    }
                }
                isUp = true;
            }
            else if (!up)
            {
                while (transform.position.z < upPosY)
                {
                    //runter Lerpen
                    if (transform.position.z - downPosY < 0.01f)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, downPosY); // ganz komischer Fehler darum z anstatt y für vertikale Bewegung
                    }
                }
                isUp = false;
            }

        }
    }
}