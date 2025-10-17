using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CameraControls : MonoBehaviour
{
    Camera cam;
    [SerializeField] float zoomspeed=1;
    [SerializeField] float movementSpeed=1;
    [SerializeField] float rotationSpeed=1;
    private bool moving = false;
    CameraMovements movements;
    void Start()
    {
        cam = GetComponent<Camera>();
        movements = GetComponent<CameraMovements>();
    }

    private void Update()
    {
    }

    // Update is called once per frame
    public void Zoom(InputAction.CallbackContext context)
    {
        float zoomValue=context.ReadValue<float>();
        
        transform.position+= cam.transform.forward*zoomValue*zoomspeed*Time.deltaTime;
    }

    public void MoveCamera(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            moving = true;
            Vector2 moveInput = context.ReadValue<Vector2>();
            movements.enabled = true;
            movements.movement=new Vector3(moveInput.x,0,moveInput.y)*movementSpeed*Time.deltaTime;;
        
            
        }

        if (context.performed)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            movements.movement=new Vector3(moveInput.x,0,moveInput.y)*movementSpeed*Time.deltaTime;
        }

        if (context.canceled)
        {
            movements.enabled = false;
        }
        
    }

    public void RotateCamera(InputAction.CallbackContext context)
    {
        Vector2 mouseMovement=context.ReadValue<Vector2>();
        Vector3 actualRotation=transform.rotation.eulerAngles;
        Vector3 newRotation=actualRotation+(new Vector3(-mouseMovement.y,mouseMovement.x,0)*rotationSpeed*Time.deltaTime);
        transform.rotation=Quaternion.Euler(newRotation);
    }
    
}
