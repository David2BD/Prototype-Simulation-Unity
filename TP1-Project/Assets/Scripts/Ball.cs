using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

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
    //public GameLoop gameLoopScript;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        // genere une force aleatoire
        if (transform.position.x > 0)
        {
            force = new Vector3(UnityEngine.Random.Range(-30f, -20f),
                UnityEngine.Random.Range(15f, 15f),
                UnityEngine.Random.Range(0f, 0f));
        }
        else
        {
            force = new Vector3(UnityEngine.Random.Range(20f, 30f),
                UnityEngine.Random.Range(15f, 15f),
                UnityEngine.Random.Range(0f, 0f));
        }
        
        acceleration += (force + gravitation) / masse;
    }
    // Update is called once per frame
    void Update()
    {
        //masse = UnityEngine.Random.Range(1f, 5f);
        
        
        
        if (transform.position.x < -20 || transform.position.y < -20 || transform.position.z < -20 ||
            transform.position.x > 20 || transform.position.y > 20 || transform.position.z > 20)
        {
            Destroy(gameObject);
        }
        
        collideWithWall();
        
        transform.position += speed * Time.deltaTime + (0.5f * acceleration * (Time.deltaTime*Time.deltaTime));
        speed += acceleration * Time.deltaTime;
        acceleration = (force + gravitation) / masse;
        
        //ligne bleu pour voir le vecteur de velocite
        Debug.DrawLine(transform.position, speed, Color.blue);
    }

    void collideWithWall()
    {
        //les dimension du mur
        if (
                transform.position.x - radius <= 1 && transform.position.x + radius >= 0
                && transform.position.y - radius <= 10 && transform.position.y + radius >= 0
                && transform.position.z - radius <= 10 && transform.position.z + radius >= 0
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
            acceleration = (force + gravitation) / masse;
            //pour reset la vitesse avant dappliquer la nouvelle force
            speed = new Vector3(0, 0, 0);
        }
        
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
