using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData : MonoBehaviour
{       private Vector3 InitialPosition;
        public float P;
        private int count;
       private bool calculated;
       public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = transform.position;
        calculated=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(calculated==false){

            P+=Time.deltaTime;
        }
            velocity= GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.tag=="SideralPeriod"){
                //Debug.Log("Sideral Period Reached");
            if(calculated==false&&count>0){

                calculated=true;
            }
            if(count==0){

                    count+=1;
            }
            
        }
    }
}
