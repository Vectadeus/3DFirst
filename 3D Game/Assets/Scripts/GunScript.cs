using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    //Damage and stuff
    public float Damage;
    public float FireRate;
    private float NextTimeToFire;
    public float ImpactForce;
    public Transform CameraTransform;
    public float Range;
    public Collider Nearest;


    //Rotations
    public Transform HandleTransform;
    public Transform ArmTransform;
    public Vector3 offset;
    public Vector3 handleOffset;
    public Quaternion RotOffset;


    //Particles and animations
    public ParticleSystem MuzzleFlash;
    public GameObject HitParticles;
    private Animator anim;


    private void OnEnable()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation
        //Quaternion offset = new Quaternion(transform.rotation.x + RotOffset.x, transform.rotation.y + RotOffset.y, transform.rotation.z + RotOffset.z, transform.rotation.w);
        //HandleTransform.rotation = transform.rotation * offset;

        Rotations();
        shoot();

        if (Input.GetKey(KeyCode.T))
        {
            Nearest = null;
        }
    }










    void shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(Time.time >= NextTimeToFire)
            {
                NextTimeToFire = Time.time + 1f / FireRate;
                MuzzleFlash.Play();

                //Animation

                anim.SetTrigger("Shoot");


                RaycastHit[] hits;

                hits = Physics.RaycastAll(CameraTransform.position, CameraTransform.transform.forward, Range);
 
                if (Physics.Raycast(CameraTransform.position, CameraTransform.transform.forward, Range))
                {
                    //FIND NEAREST TARGET TO HIT IT
                    foreach (RaycastHit rayhits in hits)
                    {
                        // PLAYER LAYER IS 8TH LAYER
                        if (rayhits.collider.gameObject.layer != 8)
                        {
                            //Check if previous was more near
                            if (Nearest != null && Vector3.Distance(transform.position, rayhits.transform.position) < Vector3.Distance(transform.position, Nearest.transform.position))
                            {
                                Nearest = rayhits.collider;
                            } //IF NEAREST IS 0 WE CAN SET IT TO ANYTHING
                            else
                            {
                                Nearest = rayhits.collider;
                            }
                        }
                    }



                    //DAMAGE NEAREST TARGET

                    if (Nearest != null)
                    {
                        EnemyHealthScript _EnemyHealth = Nearest.GetComponent<EnemyHealthScript>();
                        if (_EnemyHealth != null)
                        {

                            _EnemyHealth.TakeDamage(Damage);
                        }

                    }
                }
                Debug.Log(Nearest);
                Nearest = null;
            }
            

        }
  
    }




    void Rotations()
    {
        HandleTransform.rotation = transform.rotation * RotOffset;
        transform.position = Vector3.Lerp(transform.position, ArmTransform.position + transform.TransformDirection(offset), 1f);
        HandleTransform.position = Vector3.Lerp(transform.position, ArmTransform.position + transform.TransformDirection(handleOffset), 1f);
    }

}//CLASS
