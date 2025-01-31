using UnityEngine;
public class GroundDetection : MonoBehaviour
{
    [SerializeField] LayerMask hitLayers;
    [SerializeField] float hitDistance;
    [SerializeField] float hitRadius = 0.5f;
    [SerializeField] bool debugMode = false;
    static public GroundDetection instance;
    Vector3 debugPosition;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool isGrounded(Vector3 position)
    {
        if (Physics.SphereCast(position, hitRadius, Vector3.down, out RaycastHit hit, hitDistance, hitLayers))
        {
            if (debugMode)
            {
                Debug.DrawRay(position, Vector3.down * hitDistance, Color.red, 0.1f);
                print("hit ground");
                debugPosition = hit.point;
            }
            return true;
        }
        if (debugMode) debugPosition = position;
        
        return false;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!debugMode) return;
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawSphere(debugPosition, hitRadius);
    }
#endif
}
