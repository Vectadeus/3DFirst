using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    [SerializeField] private float MouseSensitivity;

    [SerializeField] private Vector3 CameraOffset;
    [SerializeField] private float HeadOffsetY;
    [SerializeField] private float LerpSpeed;
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Transform PlayerHead;
    private float Xrotation;



    void Look()
    {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        PlayerTransform.Rotate(Vector3.up * MouseX);
        Xrotation -= MouseY;
        Xrotation = Mathf.Clamp(Xrotation, -72f, 80f);
        transform.localRotation = Quaternion.Euler(Xrotation, 0f, 0f);

        CameraTransform.position = Vector3.Lerp(CameraTransform.position, PlayerHead.position + PlayerHead.transform.TransformDirection(CameraOffset), LerpSpeed);
    }
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void LateUpdate()
    {
        Look();
    }




}//CLAASS
