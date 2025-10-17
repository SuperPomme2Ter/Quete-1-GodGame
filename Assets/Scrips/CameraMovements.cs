using UnityEngine;

public class CameraMovements : MonoBehaviour
{
    internal Vector3 movement;

    // Update is called once per frame
    void Update()
    {
        Vector3 horizontalForward=new Vector3(transform.forward.x,0,transform.forward.z);
        Vector3 horizontalRight=new Vector3(transform.right.x,0,transform.right.z);
        transform.position+=horizontalForward*movement.z;
        transform.position+=horizontalRight*movement.x;
    }
}
