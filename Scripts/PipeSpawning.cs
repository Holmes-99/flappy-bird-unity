using UnityEngine;

public class PipeSpawning : MonoBehaviour
{
    public GameObject Pipe;
    public float spawnRate = 3f;
    private float timer = 0;
    float hightOffset = 5.7f;
    void Start()
    {
        spawn();

    }

    void Update()
    {
        if
           (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawn();
            timer = 0;
        }

    }

    void spawn()
    {
        float lowespoint = transform.position.y - hightOffset;
        float highestpoint = transform.position.y + hightOffset;

        Instantiate(Pipe, new Vector3(transform.position.x, Random.Range(lowespoint, highestpoint), 0), transform.rotation);

    }
}
