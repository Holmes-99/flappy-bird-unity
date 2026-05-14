using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class PipeMoveScript : MonoBehaviour
{
    public float speed = 5f;
    public float deadzone = -43;

    void Update()
    {
        transform.position += (Vector3.left * speed) * Time.deltaTime;
        if (transform.position.x <= deadzone)
            Destroy(gameObject);

    }
}
