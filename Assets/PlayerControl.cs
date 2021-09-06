using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Tombol untuk menggerakkan ke atas
    public KeyCode upButton = KeyCode.W;

    // Tombol untuk menggerakkan ke bawah
    public KeyCode downButton = KeyCode.S;

    // Kecepatan gerak
    public float speed = 10.0f;

    // Batas atas dan bawah game scene (Batas bawah menggunakan minus (-))
    public float yBoundary = 9.0f;

    // Rigidbody 2D raket ini
    private Rigidbody2D rigidBody2D;

    // Skor pemain
    private int score;

    // var nyimpen titik collision terakhir
    private ContactPoint2D lastContactPoint;

    // class lain mengakses titik collision terakhir
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // speed
        Vector2 velocity = rigidBody2D.velocity;

        // Jika pemain menekan tombol ke atas, beri kecepatan positif ke komponen y (ke atas).
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }

        // Jika pemain menekan tombol ke bawah, beri kecepatan negatif ke komponen y (ke bawah).
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }

        // Jika pemain tidak menekan tombol apa-apa, kecepatannya nol.
        else
        {
            velocity.y = 0.0f;
        }

        // Masukkan kembali kecepatannya ke rigidBody2D.
        rigidBody2D.velocity = velocity;

        //position
        Vector3 position = transform.position;

        // jika melewati boundary y
        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }

        // jika melewati boundary -y
        if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        transform.position = position;

    }

    public void IncrementScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int Score
    {
        get { return score;  }
    }

    // buat naruh angka kontak terakhir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            lastContactPoint = collision.GetContact(0);
        }
    }
}
