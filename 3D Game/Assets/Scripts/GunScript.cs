using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{


    public Transform CameraTransform;
    public Transform HandleTransform;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {

        //transform.position = Vector3.Lerp(transform.position, CameraTransform.position + transform.TransformDirection(offset), 1f);
        //HandleTransform.position = Vector3.Lerp(HandleTransform.position, CameraTransform.position + HandleTransform.TransformDirection(offset), 1f);
    }


}//CLASS
