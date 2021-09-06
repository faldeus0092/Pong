using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // rigidbody2d bola
    private Rigidbody2D rigidBody2D;
    // besar gaya awal
    public float xInitialForce;
    public float yInitialForce;

    // reset ke tengah layar
    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;
    }

    // inisiasi awal gerakan bola
    void PushBall()
    {
        // menentukan gaya komponen y secara random
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        // menentukan kiri kanan secara random
        float randomDirection = Random.Range(0, 2);

        // kita ingin resultan gayanya selalu sama, meskipun resultan y nya random
        // kita gunakan b^2=akar(a^2-c^2)
        // itu angka 5000 ngawur, yang penting hasilnya konstan
        xInitialForce = Mathf.Sqrt(5000 - (yRandomInitialForce * yRandomInitialForce));

        // gerak ke kiri
        if (randomDirection < 1.0f)
        {
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
        }
        else //gerak ke kanan
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
        }
    }

    void RestartGame()
    {
        //reset
        ResetBall();
        //berikan gaya setelah 2 detik
        Invoke("PushBall", 2);
    }

    // koordinat bola
    private Vector2 trajectoryOrigin;

    // Start is called before the first frame update
    void Start()
    {
        trajectoryOrigin = transform.position;
        rigidBody2D = GetComponent<Rigidbody2D>();
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ketika bola tabrakan
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    // untuk diakses kelas lain
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
