using UnityEngine;
using UnityEngine.AI;

public class CreateAgent : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
    }
}
