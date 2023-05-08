using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{


    //Gun Variables
    public float damage = 20f;
    public float range = 100f;
    public float impactForce = 25f;
    public float fireRate = 15f;
    private float timeToFire = 0f;

    //Ammo Variables
    public int ammoReserves;
    public int maxAmmoReserves;
    public int maxAmmo;
    public int currentAmmo;
    public Text ammoDisplay;
    public Text ammoReservesText;

    public float reloadTime = 3f;
    private bool isReloading = false;


    //Sound Variables
    AudioSource gunSound;

    //Camera Variables
    public Camera shootCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public Animator animator;

    private void Start()
    {
        currentAmmo = maxAmmo;
        gunSound = GetComponent<AudioSource>();
    }   

    void Update()
    {
        if (currentAmmo >= 20)
            ammoDisplay.color = Color.green;
        else if (currentAmmo < 20 && currentAmmo >= 10)
            ammoDisplay.color = Color.yellow;
        else if (currentAmmo < 10)
            ammoDisplay.color = Color.red;

        ammoDisplay.text = currentAmmo.ToString();
        ammoReservesText.text = ammoReserves.ToString();

        if(isReloading)
            return;

        if (currentAmmo <= 0 && ammoReserves != 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo != maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            if (currentAmmo > 0 || currentAmmo == 0 && ammoReserves > 0)
            {
                timeToFire = Time.time + 1f / fireRate;
                Shoot();
                gunSound.Play();
            }
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;

        currentAmmo--;

        if (Physics.Raycast(shootCam.transform.position, shootCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }
    }

    IEnumerator Reload()
    {
        if (ammoReserves >= 0)
        {
            isReloading = true;
            ammoReserves -= (maxAmmo - currentAmmo);

            if (ammoReserves < 0)
                ammoReserves = 0;

            animator.SetBool("Reloading", true);
            yield return new WaitForSeconds(reloadTime - .5f);
            animator.SetBool("Reloading", false);
            yield return new WaitForSeconds(.3f);

            currentAmmo = maxAmmo;

            isReloading = false;
        }
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    public void IncreaseDamage()
    {
        damage *= 1.25f;
    }

    public void OnDamageButtonClicked()
    {
        IncreaseDamage();
    }

    public void RefillAmmo()
    {
        currentAmmo = maxAmmo;
        ammoReserves = maxAmmoReserves;
    }

    public void OnAmmoButtonClicked()
    {
        RefillAmmo();
    }
}
