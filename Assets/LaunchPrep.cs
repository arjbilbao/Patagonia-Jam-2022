using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class LaunchPrep : MonoBehaviour
{   public LaunchData L;
    public float tangentLength;
    public SpriteShapeController spriteShapeController;
    public SpriteShapeRenderer Render;
    private float lastpos;
    private bool decreasing;
    
    public List<GameObject> AnglePoints;

    private void Start() {

        AnglePoints = new List<GameObject>();
        AnglePoints.Add(L.planet1);
        AnglePoints.Add(L.planet2);
        AnglePoints.Add(L.sun);

        
    }

    public void Update()
    {
       
          AnglePoints[0]=L.sun;   
         AnglePoints[1]=L.planet1;
         AnglePoints[2]=L.planet2;

        tangentLength = 1f;
        SetSpline();
    }

    public void SetSpline()
    {       checkPos();
            Color verde, rojo;
            verde = new Color(0f, 0.8784314f, 0.05288469f, 0.2078f);
            rojo = new Color(0.8784f, 0f, 0.1208f, 0.2078f);
          
            if (L.AngleP1toP2<=L.P2Angle&&decreasing==true){

                 Render.color=verde;
            }

            else {
                 Render.color=rojo;

            }
        Spline spline = spriteShapeController.spline;
        spline.Clear();

            
            spline.InsertPointAt(0, AnglePoints[0].transform.position);
            spline.SetTangentMode(0, ShapeTangentMode.Continuous);
            spline.SetRightTangent(0, AnglePoints[0].transform.rotation * Vector3.down);
            spline.SetLeftTangent(0, AnglePoints[0].transform.rotation * Vector3.up);


            spline.InsertPointAt(1, AnglePoints[1].transform.position*0.7f);
            spline.SetTangentMode(1, ShapeTangentMode.Continuous);
            spline.SetRightTangent(1, AnglePoints[1].transform.rotation * Vector3.down);
            spline.SetLeftTangent(1, AnglePoints[1].transform.rotation * Vector3.up);


             spline.InsertPointAt(2, AnglePoints[2].transform.position*(L.distancePlanet1FromSun/L.distancePlanet2FromSun)*0.7f);
            spline.SetTangentMode(2, ShapeTangentMode.Continuous);
            spline.SetRightTangent(2, AnglePoints[2].transform.rotation * Vector3.down);
            spline.SetLeftTangent(2, AnglePoints[2].transform.rotation * Vector3.up);
      
      

        spriteShapeController.RefreshSpriteShape();
    }

    void checkPos(){
        if(L.AngleP1toP2 < lastpos){
                decreasing =true;
                lastpos=L.AngleP1toP2;
        }
        if(L.AngleP1toP2 > lastpos){
                decreasing =false;
                lastpos=L.AngleP1toP2;
        }
         }
       
    
}
