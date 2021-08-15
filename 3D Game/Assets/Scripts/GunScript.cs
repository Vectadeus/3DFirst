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
    private RaycastHit NearestRayHit;

    //Rotations
    //public Transform HandleTransform;
    //public Transform GunHandlePos;
    //public Vector3 offset;
    //public Vector3 handleOffset;
    //public Quaternion RotOffset;


    //WeaponSway
    [SerializeField] private float SwayAmount;
    [SerializeField] private float SwaySmoothing;
    [SerializeField] private Transform HandTransform;
    private Vector3 initialPos;

    //Particles and animations
    public ParticleSystem MuzzleFlash;
    public GameObject HitParticles;
    public GameObject EnemyHitParticles;
    [SerializeField] private  Animator anim;



    //TEST
    float PrevDistance;



    private void Start()
    {
        initialPos = HandTransform.localPosition;
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

        WeaponSway();
    }







    void WeaponSway()
    {
        float swayX = Input.GetAxis("Mouse X") * SwayAmount;
        float swayY = Input.GetAxis("Mouse Y") * SwayAmount;

        Vector3 finalPos = new Vector3(swayX, swayY, 0);
        HandTransform.localPosition = Vector3.Lerp(HandTransform.localPosition, finalPos + initialPos, Time.deltaTime * SwaySmoothing);

    }


    void shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(Time.time >= NextTimeToFire)
            {
                PrevDistance = Range;
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
                            if (Vector3.Distance(transform.position, rayhits.transform.position) < PrevDistance)
                            {
                                Nearest = rayhits.collider;
                                NearestRayHit = rayhits;
                                PrevDistance = Vector3.Distance(transform.position, Nearest.transform.position);
                            }
                        }
                    }



                    //DAMAGE NEAREST TARGET
                    if (Nearest != null)
                    {
                        // 9TH LAYER IS ENEMY LAYER
                        if(NearestRayHit.transform.gameObject.layer != 9)
                        {
                            GameObject Particles = Instantiate(HitParticles, NearestRayHit.point, Quaternion.identity);
                            Destroy(Particles, 2f);

                        }

                        EnemyHealthScript _EnemyHealth = Nearest.GetComponent<EnemyHealthScript>();
                        if(_EnemyHealth == null)
                        {
                            _EnemyHealth = Nearest.GetComponentInParent<EnemyHealthScript>();
                        }

                        if (_EnemyHealth != null)
                        {

                            _EnemyHealth.TakeDamage(Damage);
                            GameObject particles = Instantiate(EnemyHitParticles, NearestRayHit.point, Quaternion.identity);
                            Destroy(particles, 2f);
                        }
                        else
                        {

                        }


                    }
                }
                Debug.Log(Nearest);
                
            }
            

        }
  
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(CameraTransform.position, CameraTransform.transform.forward);
    }

    void Rotations()
    {
        //HandleTransform.rotation = transform.rotation * RotOffset;
        //transform.position = Vector3.Lerp(transform.position, ArmTransform.position + transform.TransformDirection(offset), 1f);
        //HandleTransform.position = Vector3.Lerp(transform.position, GunHandlePos.position + transform.TransformDirection(handleOffset), 50f);
        //HandleTransform.position = GunHandlePos.position;
    }

}//CLASS
