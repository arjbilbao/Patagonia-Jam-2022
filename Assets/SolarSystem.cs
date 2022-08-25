using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{       public float G = 100f;
        [SerializeField]
        GameObject [] Celestials;
    // Start is called before the first frame update
    void Start()
    {
        Celestials = GameObject.FindGameObjectsWithTag("Celestial");
        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void FixedUpdate() {
        Gravity();
    }

    void Gravity()
    {

        foreach(GameObject a in Celestials){
                    foreach(GameObject b in Celestials){

                        if(!a.Equals(b)){

                                float m1 = a.GetComponent<Rigidbody2D>().mass;
                                float m2 = b.GetComponent<Rigidbody2D>().mass;
                                float r = Vector2.Distance(a.transform.position,b.transform.position);
                               

                                a.GetComponent<Rigidbody2D>().AddForce((b.transform.position - a.transform.position).normalized*(G*(m1*m2)/(r*r)));


                        }
                    }

        }
    }

    void InitialVelocity(){

        
        foreach(GameObject a in Celestials){
                    foreach(GameObject b in Celestials){

                        if(!a.Equals(b)){
                                float m2 = b.GetComponent<Rigidbody2D>().mass;
                                float r = Vector2.Distance(a.transform.position,b.transform.position);
                                Vector3 diff = b.transform.position - a.transform.position;
                                
 
                                float rot_z = Mathf.Atan2(diff.normalized.y, diff.normalized.x) * Mathf.Rad2Deg;
                                a.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                               
                                a.GetComponent<Rigidbody2D>().velocity+= new Vector2(0f,Mathf.Sqrt((G*m2)/r));

                        }
                    }

        }
    }
}
