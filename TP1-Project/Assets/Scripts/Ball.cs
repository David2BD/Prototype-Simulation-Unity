using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = System.Random;
using Vector3 = UnityEngine.Vector3;

public class Ball : MonoBehaviour
{
    public Vector3 gravitation = new Vector3(0f, -9.81f, 0f);
    public Vector3 acceleration = new Vector3(0f,0f,0f);
    public Vector3 speed = new Vector3(0f,0f,0f);
    public Vector3 force = new Vector3(0f,0f,0f);
    public Vector3 normalMur = new Vector3(1f, 0f, 0f);
    public float coeficientConservation = 0.8f;
    public float masse = 1f;
    private float radius = 0.5f;
    private GameObject midWall;
    
    //public GameLoop gameLoopScript;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // velocite initiale
        speed = 
            (transform.position.x > 0) ? 
            randomVector(-20f,20f,10f,10f) : 
            randomVector(15f,25f,10f,10f);
        acceleration = (gravitation) / masse;
        midWall = GameObject.Find("Wall");
    }
    // Update is called once per frame
    void Update()
    {
        collideWithWall();
        collideGround();
        collideOutsideWall(10, 10);
        
        transform.position += speed * Time.deltaTime + (0.5f * acceleration * (Time.deltaTime*Time.deltaTime));
        speed += acceleration * Time.deltaTime;
        acceleration = (gravitation) / masse;
        
        //ligne bleu pour voir le vecteur de velocite
        //Debug.DrawLine(transform.position, speed, Color.blue);
    }

    Vector3 randomVector(float minX, float maxX, float y, float z)
    {
        Vector3 vector = new Vector3(UnityEngine.Random.Range(minX, maxX),
            UnityEngine.Random.Range(-y, y),
            UnityEngine.Random.Range(-z, z));
        return vector;
    }

    void collideWithWall()
    {
        //les dimension du mur
        if (
                transform.position.x - radius <= midWall.transform.position.x + 0.5 && transform.position.x + radius >= midWall.transform.position.x - 0.5
                && transform.position.y - radius <= midWall.transform.position.y + 5 && transform.position.y + radius >= midWall.transform.position.y - 5
                && transform.position.z - radius <= midWall.transform.position.z + 5 && transform.position.z + radius >= midWall.transform.position.z - 5
           )
        {
            //pour ressortir du mur pour pas etre ne collision le prochain frame
            if (transform.position.x > 0.9)
            {
                Vector3 newPosition = new Vector3(1.505f, transform.position.y, transform.position.z);
                transform.position = newPosition;
            }
            else
            {
                Vector3 newPosition = new Vector3(-0.505f, transform.position.y, transform.position.z);
                transform.position = newPosition;
            }
            
            force = CalculRebound(speed, normalMur);
            speed = (force) / masse;
            //pour reset la vitesse avant dappliquer la nouvelle force
            //speed = new Vector3(0, 0, 0);
        }
        
    }

    void collideOutsideWall(float limitX, float limitZ)
    {
        // on considere les murs limites comme des plans sur les axes
        if (transform.position.x - radius < -limitX || transform.position.z - radius < -limitZ ||
            transform.position.x + radius > limitX || transform.position.z + radius > limitZ)
        {
            Vector3 newPosition;
            Vector3 normale;
            if (transform.position.x + radius > limitX)
            {
                newPosition = new Vector3(limitX - radius, transform.position.y, transform.position.z);
                normale = new Vector3(1, 0, 0);
            }
            else if (transform.position.x - radius < -limitX)
            {
                newPosition = new Vector3(-limitX + radius, transform.position.y, transform.position.z);
                normale = new Vector3(1, 0, 0);
            }
            else if (transform.position.z + radius > limitZ)
            {
                newPosition = new Vector3(transform.position.x, transform.position.y, limitZ - radius);
                normale = new Vector3(0, 0, 1);
            }
            else
            {
                newPosition = new Vector3(transform.position.x, transform.position.y, -limitZ + radius);
                normale = new Vector3(0, 0, 1);
            }

            transform.position = newPosition;
            speed = calculGlissement(speed, normale);
        }
    }

    void collideGround()
    {
        // le sol est un plan sur axe XZ avec une normale en y
        if (transform.position.y - radius <= 0)
        {
            Vector3 newPosition = new Vector3(transform.position.x,radius, transform.position.z);
            transform.position = newPosition;
            speed = new Vector3(0, 0, 0);
            acceleration = new Vector3(0, 0, 0);
            Destroy(gameObject);
        }
    }

    private Vector3 calculGlissement(Vector3 initialVelocity, Vector3 normal)
    {
        float dotProduct = Vector3.Dot(initialVelocity, normal);
        Vector3 newVelocity = initialVelocity - dotProduct * normal;
        return newVelocity;
    }
    
    //formule de rebound des notes de cours
    private Vector3 CalculRebound(Vector3 initialVelocity, Vector3 normal)
    {
        float dotProduct = Vector3.Dot(initialVelocity, normal);
        Vector3 reflection = initialVelocity - 2 * dotProduct * normal;
        Vector3 reboundVelocity = coeficientConservation * reflection;
        
        return reboundVelocity;
    }
    
}
