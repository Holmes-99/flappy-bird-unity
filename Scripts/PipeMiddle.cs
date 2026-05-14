using UnityEngine;

public class PipeMiddle : MonoBehaviour
{
    public LogicManader logic;
    private AudioSource audioSource;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicManader>();

        audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("scoreSound");

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.playOnAwake = false;
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logic.addScore(1);

            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
        }
    }
}