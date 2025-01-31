using UnityEngine;

public class FallAcceleration : MonoBehaviour
{
    [SerializeField] float fallAcceleration = 1f;
    Rigidbody rb => GetComponent<Rigidbody>();
    Vector3 oldPos;
    // Update is called once per frame
    void LateUpdate()
    {
        if (rb.linearVelocity.y < 0f)
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallAcceleration * Time.deltaTime;
    }
}
