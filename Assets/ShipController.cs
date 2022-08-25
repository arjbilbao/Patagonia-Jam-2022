using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject sun;
    public GameObject planet1;
    public GameObject planet2;
    public Vector3 _trajectoryCenter, initialposition;
    public float _speed;
    private bool launch=true;
    void Start()
    {
            _trajectoryCenter = sun.transform.position;
            
    }

    // Update is called once per frame
    void Update()
    {
            if(launch){

                  initialposition=transform.position; 
                    launch=false;

            }
            //transform.position=Vector2.MoveTowards(initialposition,planet2.transform.position,_speed*Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(planet2.transform.position.x, planet2.transform.position.y), _speed * Time.deltaTime);



    }
}
