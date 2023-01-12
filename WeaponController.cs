using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootType
{
    Manual,
    Automatic
}
public class WeaponController : MonoBehaviour
{
    [Header("References")]
    public Transform  weaponMuzzle;

    [Header("General")]
    public LayerMask hittableLayers;
    public GameObject bulletHolePrefab;

    [Header("Shoot Paramaters")]
    public ShootType shootType;
    public float fireRange = 200;
    public float recoilForce = 4f;
    public float fireRate = 0.6f;
    public int maxAmmo = 30;

    [Header ("Reload Parameters")]
    public float reloadTime = 1.5f;

    public int currentAmmo { get; private set; }

    private float lastTimeShoot = Mathf.NegativeInfinity;

    [Header("Sounds & Visuals")]
    public GameObject flashEffect;

    public GameObject owner { set; get; }

    private Transform cameraPlayerTransform;

    private void Awake()
        {
           currentAmmo = maxAmmo;
        }
    

    private void Start()
    {
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        if(shootType == ShootType.Manual)
        {
        if (Input.GetButtonDown("Fire1"))
        {
             TryShoot();
        }
        } 
        else if(shootType == ShootType.Automatic)
        {
        if (Input.GetButton("Fire1"))
        {
             TryShoot();
        }
        }
        

        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    private bool TryShoot()
    {
        if(lastTimeShoot + fireRate < Time.time)
        {
            if(currentAmmo >= 1)
            {
                HandleShoot();
                currentAmmo -= 1;
                return true;
            }
        }
        

        return false;
    }

    private void HandleShoot()
    {
        
            Instantiate(flashEffect, weaponMuzzle.position, Quaternion.Euler(weaponMuzzle.forward), transform);

            AddRecoil();  
   
            RaycastHit[] hits;
            EnemyHealth enemyHealth;
            hits = Physics.RaycastAll(cameraPlayerTransform.position, cameraPlayerTransform.forward, fireRange, hittableLayers);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject != owner)
                {
                    GameObject bulletHoleClone = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                    Destroy(bulletHoleClone, 4f);
                }
                if (hit.transform.tag == "Enemy")
                {
                   
                    enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                    enemyHealth.TakeDamage(100);
                    
                }
            }

            lastTimeShoot = Time.time;
    }

    private void AddRecoil()
    {
        transform.Rotate(-recoilForce, 0f, 0f);
        transform.position = transform.position - transform.forward * (recoilForce/50f);
    }

    IEnumerator Reload ()
    {
        Debug.Log("Recargando...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        Debug.Log("Recargado");
    }
}
