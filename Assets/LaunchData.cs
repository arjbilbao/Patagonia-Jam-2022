using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchData : MonoBehaviour
{

    public GameObject planet1, planet2, earth, sun;
    private float AU, AxisMajorP1toP2,DistanceP1P2, AngleP1toP2, P1DotP2,P1Mag,P2Mag, distancePlanet1FromSun, distancePlanet2FromSun, SAMP1, SAMP2, PP1, earthP, timer=0f, number2=361f;
    public float K, PHTO,PHTOV, SAMH1, SAMH2,P2Angle,P2AngleV, PP2;
    private Vector3 planet1FromSun, planet2FromSun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {           
        
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
                
                
                SAMH2=Minimun(SAMH1,3*planet2.GetComponent<PlanetData>().P);
                
                if(SAMH2>0)
                {
                     
                         PHTO=((K*Mathf.Sqrt(Mathf.Pow(SAMH2,3)))/2)*365f;
                }
                PHTOV =((K*Mathf.Sqrt(Mathf.Pow(SAMH1,3)))/2)*365f;


                PP2=((planet2.GetComponent<PlanetData>().P)/earthP)*365;
                P2Angle=180-(PHTO*(360/PP2));
                P2AngleV=180-(PHTOV*(360/PP2));
               

            //Debug.Log(AngleP1toP2);

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



}
