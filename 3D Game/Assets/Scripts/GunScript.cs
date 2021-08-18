using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    //Damage and stuff
    [SerializeField] private float Damage;
    [SerializeField] private float FireRate;
    [SerializeField] float ImpactForce;
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private float Range;

    private float PrevDistance;
    private float NextTimeToFire;
    private Collider Nearest;
    private RaycastHit NearestRayHit;



    //WeaponSway
    [SerializeField] private float SwayAmount;
    [SerializeField] private float SwaySmoothing;
    [SerializeField] private Transform HandTransform;
    private Vector3 initialPos;


    //Particles and animations
    [SerializeField] private ParticleSystem MuzzleFlash;
    [SerializeField] private GameObject HitParticles;
    [SerializeField] private GameObject EnemyHitParticles;
    [SerializeField] private Animator handAnim;



    //RELOADING STUFF
    [SerializeField] private bool isReloading;
    [SerializeField] private float reloadTime;

    private void Start()
    {
        initialPos = HandTransform.localPosition;
        isReloading = false;

    }


    // Update is called once per frame
    void Update()
    {

        shoot();
        WeaponSway();
        Reload();
    }







    void WeaponSway()
    {
        float swayX = Input.GetAxis("Mouse X") * SwayAmount;
        float swayY = Input.GetAxis("Mouse Y") * SwayAmount;

        Vector3 finalPos = new Vector3(swayX, swayY, 0);
        HandTransform.localPosition = Vector3.Lerp(HandTransform.localPosition, finalPos + initialPos, Time.deltaTime * SwaySmoothing);

    }


    void FindTarget()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(CameraTransform.position, CameraTransform.transform.forward, Range);

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
    }
    void DamageNearest()
    {
        if (Nearest != null)
        {
            // 9TH LAYER IS ENEMY LAYER
            if (NearestRayHit.transform.gameObject.layer != 9)
            {
                GameObject Particles = Instantiate(HitParticles, NearestRayHit.point, Quaternion.identity);
                Destroy(Particles, 2f);

            }

            EnemyHealthScript _EnemyHealth = Nearest.GetComponent<EnemyHealthScript>();
            if (_EnemyHealth == null)
            {
                _EnemyHealth = Nearest.GetComponentInParent<EnemyHealthScript>();
            }

            if (_EnemyHealth != null)
            {

                _EnemyHealth.TakeDamage(Damage);
                GameObject particles = Instantiate(EnemyHitParticles, NearestRayHit.point, Quaternion.identity);
                Destroy(particles, 2f);
            }
        }
    }

    void shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(Time.time >= NextTimeToFire && !isReloading)
            {
                if (!handAnim.GetCurrentAnimatorStateInfo(0).IsName("ShootAnim"))
                {
                    //Animations
                    handAnim.SetTrigger("Shoot");

                    PrevDistance = Range;
                    NextTimeToFire = Time.time + 1f / FireRate;
                    MuzzleFlash.Play();


                    if (Physics.Raycast(CameraTransform.position, CameraTransform.transform.forward, Range))
                    {

                        FindTarget();
                        DamageNearest();
                    }
                }
                
            }
            

        }
  
    }


    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
    }

    void Reload1()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            handAnim.SetBool("Reloadaing", true);

            Debug.Log("Sup");
        }
        if (isReloading && !handAnim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            Debug.Log("AH");
            isReloading = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(CameraTransform.position, CameraTransform.transform.forward);
    }

}//CLASS
