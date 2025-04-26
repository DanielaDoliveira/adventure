using UnityEngine;

public class Centralize : MonoBehaviour
{

    private float speed = 5;
    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.parent.position - transform.position) * speed* Time.deltaTime;
    }
}
