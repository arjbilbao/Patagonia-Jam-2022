using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchData : MonoBehaviour
{

    public GameObject planet1, planet2, earth, sun;
     [HideInInspector]
    public float AU, AxisMajorP1toP2,DistanceP1P2, P1DotP2,P1Mag,P2Mag, distancePlanet1FromSun, distancePlanet2FromSun, SAMP1, SAMP2, PP1, earthP, timer=0f;
    public float K, PHTO=0f,PHTOV,HTOD,HTODV, SAMH1, SAMH2=0f,P2Angle,P2AngleV, PP2, AngleP1toP2, number2;
    [HideInInspector]
    public Vector3 planet1FromSun, planet2FromSun;
    public LineRenderer LinePlanet1, LinePlanet2;
    public bool minimunSAM=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {            


         if(planet2.GetComponent<PlanetData>().calculated){

                     LaunchLines();
                //Astronomy Unit
               AU = Vector3.Distance(earth.transform.position,sun.transform.position);


               //Distances Between Planet1 and the Sun
               planet1FromSun = planet1.transform.position-sun.transform.position;
               distancePlanet1FromSun = Vector3.Distance(planet1.transform.position,sun.transform.position);
               //Planet1 Semi-axis Major
               SAMP1 = (distancePlanet1FromSun/AU);
               //***********************************

               //Distances Between Planet1 and the Sun
               planet2FromSun = planet2.transform.position-sun.transform.position;
               distancePlanet2FromSun = Vector3.Distance(planet2.transform.position,sun.transform.position);
                //Planet2 Semi-axis Major
               SAMP2 = (distancePlanet2FromSun/AU);
               //***********************************


               //Angle between Planets
               P1DotP2 = Vector3.Dot(planet1FromSun,planet2FromSun);
               P1Mag= planet1FromSun.magnitude;
               P2Mag = planet2FromSun.magnitude;
               AngleP1toP2 = Mathf.Acos(P1DotP2/(P1Mag*P2Mag))*(180/Mathf.PI);

               //Hohmann Transfer Orbit
                earthP=earth.GetComponent<PlanetData>().P;
                PP1=planet1.GetComponent<PlanetData>().P/earthP;
                
                K=(PP1*PP1)/(SAMP1*SAMP1*SAMP1);
                SAMH1=(SAMP1+SAMP2)/2;
                
                    
                  
                      
                   SAMH2=Minimun(SAMH1,planet2.GetComponent<PlanetData>().P);    
                
    
                HTODV = Vector3.Distance(planet1.transform.position,planet2.transform.position);
                if(SAMH2>0)
                {
                    minimunSAM=false;
                    timer=0;
                         PHTO=((K*Mathf.Sqrt(Mathf.Pow(SAMH2,3)))/2)*365f;
                         HTOD = Vector3.Distance(planet1.transform.position,planet2.transform.position);
                }
                PHTOV =((K*Mathf.Sqrt(Mathf.Pow(SAMH1,3)))/2)*365f;


                PP2=((planet2.GetComponent<PlanetData>().P)/earthP)*365;


                P2Angle=180-(PHTO*(360/PP2));
                if(P2Angle<0)
                {

                    P2Angle=-P2Angle;
                }
                P2AngleV=180-(PHTOV*(360/PP2));

                if(P2AngleV<0)
                {

                    P2AngleV=-P2AngleV;
                }
            //Debug.Log(AngleP1toP2);
         }

               

    }

    private float Minimun(float number, float period)
    {
      
        float number3;
       
       

       
        if(period>timer)
        {
            if(number<number2){
                
                number2=number;
                
            }
            
              number3=0f;
            
              timer+=Time.deltaTime;
        }
        else{
             
            number3=number2;
        }
       
            
        return number3;

    }


    void LaunchLines()
    {

        LinePlanet1.SetPosition(0,transform.position);
        LinePlanet1.SetPosition(1,planet1.transform.position);
        LinePlanet2.SetPosition(0,transform.position);
        LinePlanet2.SetPosition(1,planet2.transform.position);
    }

        void OnDrawGizmos()
{
      Gizmos.color = Color.yellow;
    float rayRange = 10.0f;
    float halfFOV = P2AngleV / 2.0f;
    float coneDirection = 180;

    Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV + coneDirection, planet1FromSun);
    Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV + coneDirection, planet2FromSun);

    Vector3 upRayDirection = upRayRotation * planet1FromSun;
    Vector3 downRayDirection = downRayRotation * planet2FromSun;

    Gizmos.DrawRay(transform.position, upRayDirection);
    Gizmos.DrawRay(transform.position, downRayDirection);
   
}

        

}
