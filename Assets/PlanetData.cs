using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData : MonoBehaviour
{       private Vector3 InitialPosition;
        public float P;
        public InstantiatePrefab Trail;
        public float trailTime, trailTimer;
        private int count;
       public bool calculated=false;
       public float velocity;
       public int Q;
       public CircleCollider2D influenceOrbit;
       public GameObject Sun;
    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = transform.position;
        
        Sun = GameObject.Find("Sun");
        influenceOrbit.radius = Vector3.Distance(transform.position,Sun.transform.position)*Mathf.Pow((GetComponent<Rigidbody2D>().mass/Sun.GetComponent<Rigidbody2D>().mass),2f/5f);
              calculated=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(calculated==false){

            P+=Time.deltaTime;
        }
            

        if(trailTimer<trailTime){

            trailTimer+=Time.deltaTime;
        }
        else{

            trailTimer=0;
            Trail.Instantiate();
        }
     LookAtTheSun();
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

    void OnTriggerStay2D(Collider2D other) {

        if(other.tag=="Quadrant"){

                    if(other.gameObject.name=="Q1"){
                            Q=1;

                    }
                     if(other.gameObject.name=="Q2"){
                            Q=2;

                    }
                     if(other.gameObject.name=="Q3"){
                            Q=3;

                    }
                     if(other.gameObject.name=="Q4"){
                            Q=4;

                    }
        }
        
    }


    void LookAtTheSun()
    {

                                Vector3 diff = Sun.transform.position - transform.position;
                                
 
                                float rot_z = Mathf.Atan2(diff.normalized.y, diff.normalized.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
