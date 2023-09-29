using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public int playerTurn;
    public GameObject ball;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerTurn = 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Respawn"))
        {
            if (playerTurn == 1 && Input.GetMouseButtonDown(0))
            {
                Instantiate(ball, new Vector3(-5, 3, 0), Quaternion.identity);
                playerTurn = 2;

            }
            else if (playerTurn == 2 && Input.GetMouseButtonDown(0))
            {
                Instantiate(ball, new Vector3(5, 3, 0), Quaternion.identity);
                playerTurn = 1;
            }
        }

    }
    
    public int GetplayerTurn()
    {
        return playerTurn;
    }
    
    public void SetplayerTurn(int player)
    {
        playerTurn = player;
    }
    
}
