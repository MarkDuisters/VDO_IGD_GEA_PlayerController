using UnityEngine;

public class Blackhole : MonoBehaviour
{

    float gravity = -9.81f;
    [SerializeField] float attractionForce = 9.81f;//default is regular gravity force.
    Rigidbody rb;


    //Haal de rigibody van de speler op.
    void OnTriggerEnter(Collider col)
    {
        if (!col.CompareTag("Player")) return;

        rb = col.GetComponent<Rigidbody>();
    }

    void OnTriggerStay(Collider col)
    {
        if (!col.CompareTag("Player")) return;
        if (rb == null) return;

        Vector3 directionalGravity = transform.position - col.transform.position;//directional gravity force.
        directionalGravity = directionalGravity * attractionForce;//Apply force to direction.
        directionalGravity += -gravity * Vector3.up;//Cancel out regular gravity.
        // rb.AddForce(-gravity * Vector3.up, ForceMode.Force);//Cancel out regular gravity force.
        rb.AddForce(directionalGravity, ForceMode.Acceleration);//Add directional gravity force.

    }




}
