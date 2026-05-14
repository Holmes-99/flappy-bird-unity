using UnityEngine;
using UnityEngine.InputSystem;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D Myrigid;
    public int flashspeed;
    public LogicManader logic;
    public bool birdIsAlive = true;

    private AudioSource audioSource;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicManader>();

        // Load game over sound from Resources folder
        audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("gameOverSound");

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.playOnAwake = false;
        }
        else
        {
            Debug.LogWarning("gameOverSound not found! Make sure it's in Assets/Resources/gameOverSound.wav");
        }
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && birdIsAlive)
        {
            Myrigid.linearVelocity = Vector2.up * flashspeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        birdIsAlive = false;

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        logic.gameOver();
    }
}