using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject sun;
    public GameObject planet1;
    public GameObject planet2;
    public GameObject System;
    public Vector3 _trajectoryCenter, initialposition;
    public float _speed, distance, _speed1;
    public bool launch=false, _isInTravel=false, influence=false;
    public float Elliptic, Elliptic1;
    public float LaunchTimer;
    public float TravelTime;
    public float tt;
    void Start()
    {
            _trajectoryCenter = sun.transform.position;
            System = GameObject.Find("System");
            transform.position=planet1.transform.position;
           transform.SetParent(planet1.transform, true);
          
            
    }

    // Update is called once per frame
    void Update()
    {           
                
            if(launch){
                float pd1 = Vector3.Distance(planet1.transform.position,sun.transform.position);
            float pd2 = Vector3.Distance(planet2.transform.position,sun.transform.position);
            
          
           Elliptic = 1f;
            distance =(Mathf.PI*Vector2.Distance(transform.position,planet2.transform.position))/2f;

                  initialposition=transform.position; 
                    launch=false;
                    //_speed = planet1.GetComponent<PlanetData>().velocity;
                    LaunchTimer=0f;
                    transform.parent=null;
                    _isInTravel=true;
              

                        _speed1 =planet1.GetComponent<PlanetData>().velocity;
                        _speed=_speed1;
                    TravelTime=2*(distance/_speed1);

            }

                
            //transform.position=Vector2.MoveTowards(initialposition,planet2.transform.position,_speed*Time.deltaTime);
            if(_isInTravel){


                        if(LaunchTimer<=TravelTime){

                        if(LaunchTimer>TravelTime*0.06){
                                         tt = LaunchTimer/TravelTime;
                
                                Elliptic = Mathf.Lerp(Elliptic1,1f,tt);
                                if(influence){

                                 _speed = Mathf.Lerp(_speed1,planet2.GetComponent<PlanetData>().velocity,tt);
                                }
                                

                        }
                        LaunchTimer+=Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(planet2.transform.position.x, planet2.transform.position.y)*(Elliptic), _speed * Time.deltaTime);
                                if(!influence){
                                        float pd2 = Vector3.Distance(planet2.transform.position,sun.transform.position);
                                        Elliptic1=(((Vector3.Distance(planet2.transform.position,sun.transform.position)-Vector3.Distance(transform.position,sun.transform.position))/2f)+(Vector3.Distance(transform.position,sun.transform.position)))/pd2;
                                }
                LookAtDestiny();
                  
           
                
            

                

                        }

                        else{
                                        transform.position=planet2.transform.position;
                transform.SetParent(planet2.transform, true);
                _isInTravel=false;
                        }

            }

            

           



    }

     void LookAtDestiny()
    {

                                Vector3 diff = planet2.transform.position*Elliptic - transform.position;
                                
 
                                float rot_z = Mathf.Atan2(diff.normalized.y, diff.normalized.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }


    private void OnTriggerEnter2D(Collider2D other) {

        if(other.tag=="InfluenceOrbit"){

                if(other.gameObject.name==planet2.name){

                        influence=true;
                }
        }
        
    }
}
