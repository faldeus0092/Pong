using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Pemain 1
    public PlayerControl player1; // skrip
    private Rigidbody2D player1Rigidbody;

    // Pemain 2
    public PlayerControl player2; // skrip
    private Rigidbody2D player2Rigidbody;

    // Bola
    public BallControl ball; // skrip
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D ballCollider;

    // Objek untuk menggambar prediksi lintasan bola
    public Trajectory trajectory;

    // Skor maksimal
    public int maxScore;

    // Apakah debug window ditampilkan?
    private bool isDebugWindowShown = false;

    private void Start()
    {
        player1Rigidbody = player1.GetComponent<Rigidbody2D>();
        player2Rigidbody = player2.GetComponent<Rigidbody2D>();
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    void OnGUI()
    {
        // player 1 di kiri
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        // player 2 di kanan
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);

        // restart game button
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            player1.ResetScore();
            player2.ResetScore();
            // restart bola
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        // jika player 1 max score
        if (player1.Score == maxScore)
        {
            // ...tampilkan teks "PLAYER ONE WINS" di bagian kiri layar...
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");
            // restart bola
            ball.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        } else if (player2.Score == maxScore)
        {
            // ...tampilkan teks "PLAYER ONE WINS" di bagian kiri layar...
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");
            // restart bola
            ball.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        }

        // Toggle nilai debug window ketika pemain mengeklik tombol ini.
        // if button = true dipencet
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
        }

        // untuk toggle debug window
        if (isDebugWindowShown)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            // Simpan variabel-variabel fisika yang akan ditampilkan. 
            float ballMass = ballRigidbody.mass;
            Vector2 ballVelocity = ballRigidbody.velocity;
            float ballSpeed = ballRigidbody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            // ambil impuls saat tumbukan
            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            // Tentukan debug text-nya
            string debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            // style untuk gui
            GUIStyle guistyle = new GUIStyle(GUI.skin.textArea); //deklarasi guistyle
            guistyle.alignment = TextAnchor.UpperCenter; // alignment tengah
            //buat gui nya
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guistyle);
            // kembalikan warna ui
            GUI.backgroundColor = oldColor;
            trajectory.enabled = !trajectory.enabled;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
