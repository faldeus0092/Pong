using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    public PlayerControl player;
    // Skrip GameManager untuk mengakses skor maksimal
    [SerializeField]
    private GameManager gameManager;
    // ketika objek lain collide dengan wall
    void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        // jika objek bernama Ball
        if (anotherCollider.name == "Ball")
        {
            // increment score
            player.IncrementScore();

            // jika belum sampai skor maks, restart
            if (player.Score < gameManager.maxScore)
            {
                // restar setelah mengenai dinding
                anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
