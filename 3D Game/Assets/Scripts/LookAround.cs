using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public float MouseSensitivity;
    public Transform CameraTransform;
    public Transform PlayerTransform;
    float Xrotation;
    public Transform PlayerHead;
    public float LerpSpeed;
    public float HeadOffsetY;
    public Vector3 CameraOffset;


    void Look()
    {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        PlayerTransform.Rotate(Vector3.up * MouseX);
        Xrotation -= MouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90);
        transform.localRotation = Quaternion.Euler(Xrotation, 0f, 0f);
    }
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame

    private void LateUpdate()
    {
        Look();

        //TEST
        CameraTransform.position = Vector3.Lerp(CameraTransform.position, PlayerHead.position + PlayerHead.transform.TransformDirection(CameraOffset), LerpSpeed);

        //gasrialebisas
        // X = -0.3
        // Y = 
    }




}//CLAASS
