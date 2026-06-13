using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Validation;

/*
 * Attach to Fahrstuhlkabine
 */
namespace Lift {
    public class Lift : MonoBehaviour {
        //public float downPosY = 0.6f;
        //public float upPosY = 3f; // ganz komisch darum z anstatt y für vertikale Bewegung
        [SerializeField]
        float delta = 6;
        public Vector3 startPos = new Vector3();
        public Vector3 endPos = new Vector3();
        public bool isUp;
        public bool isMoving = false;
        [PositiveNonZero] public float speed = 0.003f;


        void Start() 
        {
            //Startposition ist aktuelle Position, Endposition ist aktuelle Poaition plus Höhenunterschied
            startPos = transform.position;
            endPos = new Vector3(transform.position.x, startPos.y + delta, transform.position.z);
        
        }

        public void Move(bool up)
        {
            //Untescheidung ob hoch-oder runterfahren
            if (up && !isMoving)
            {
                Debug.Log("jetz soll hochfahren");
                //Fahren wird in Coroutine ausgeführt - sonst fehler(Endlosschleife)
                StartCoroutine(MoveLiftUp());
            }
            else if (!up && !isMoving)
            {
                Debug.Log("jetz soll runterfahren");
                StartCoroutine(MoveLiftDown());               
            }

        }

        IEnumerator MoveLiftUp() 
        {
            isMoving = true;
            //solange aktuelle Y Koordinate nicht gleich ist als Ziel Y Koordinate...
            while (transform.position.y != endPos.y)
            {
                //wird die position auf eine Position, die zwischen Start- und Endpunkt liegt gesetzt
                transform.position = Vector3.Lerp(transform.position, endPos, speed);
                //sind die Positionen nah genug aneinander wird die Position auf die Zielposition gesetzt
                if (Math.Abs(transform.position.y - endPos.y) < 0.1f)     
                {
                    Debug.Log(transform.position.y);
                    transform.position = endPos;
                    isUp = true;
                    isMoving = false;
                    break;
                }
                yield return null;
            }
           

        }

        IEnumerator MoveLiftDown()
        {
            isMoving = true;
            while (transform.position.y != startPos.y)
            {
                transform.position = Vector3.Lerp(transform.position, startPos, speed);
                if (Math.Abs(transform.position.y - startPos.y) < 0.1f)
                {
                    Debug.Log(transform.position.y);
                    transform.position = startPos;
                    isUp = false;
                    isMoving = false;
                    break;
                }
                yield return null;
            }


        }
    }
}