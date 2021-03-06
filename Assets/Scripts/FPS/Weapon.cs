using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve reloadAnimationCurve;
    [SerializeField]
    private AnimationCurve recoilAnimationCurve;
    [SerializeField]
    private List<GameObject> bulletDecals;
    [SerializeField]
    private AudioSource shootAudioSource;
    [SerializeField]
    private AudioSource reloadAudioSource;
    [SerializeField]
    private int bulletsPerShot = 1;
    [SerializeField]
    private float damagePerBullet = 20;
    [SerializeField]
    private float spread = 0;
    [SerializeField]
    public int magazineSize = 12;
    [SerializeField]
    public int ammo = 100;
    // Seconds per round
    [SerializeField]
    private float fireRate = 0.5f;
    // Seconds
    [SerializeField]
    private float reloadSpeed = 1f;
    [SerializeField]
    private Camera raycastCamera;
    [SerializeField]
    private float recoil = 0.4f;
    [SerializeField]
    private Transform barrelEnd;
    private float? lastShot;
    private Quaternion initialLocalRotation;
    public int ammoInMagazine = 0;
    private bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        ammoInMagazine = Mathf.Min(magazineSize, ammo);
        initialLocalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")) Shoot();
        if ((ammoInMagazine == 0 || Input.GetButtonDown("Reload")) && canReload()) StartCoroutine(Reload());
    }

    void Shoot()
    {
        if (ammoInMagazine != 0 && (lastShot == null || Time.time > lastShot + fireRate))
        {
            shootAudioSource.Play();
            for (var i = 0; i < bulletsPerShot; i++)
            {
                var ray = raycastCamera.ScreenPointToRay(new Vector3(0.5f * Screen.width, 0.5f * Screen.height));
                var x = Random.Range(-spread / 90, spread / 90);
                var y = Random.Range(-spread / 90, spread / 90);
                ray.direction = ray.direction + new Vector3(x, y, 0);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    InstantiateBulletDecal(hit.point, Quaternion.LookRotation(hit.point - transform.position), hit.collider.transform);
                    BulletTrail.Create(barrelEnd.position, hit.point);
                    if (hit.collider.tag == "Enemy")
                    {
                        var enemy = hit.collider.gameObject.GetComponent<Enemy>();
                        if (enemy.Health <= damagePerBullet) HUDElementController.Instance.GetExcited();
                        enemy.TakeDamage(damagePerBullet);
                    }
                    if (hit.collider.tag == "Barrel")
                    {
                        hit.collider.gameObject.GetComponent<ExplosiveBarrel>().GetHit();
                    }
                }
            }
            ammoInMagazine--;
            lastShot = Time.time;
            StartCoroutine(Recoil());
        }
    }

    IEnumerator Recoil()
    {
        while (Time.time < lastShot + fireRate)
        {
            var scaledTimeSinceShot = (Time.time - (float)lastShot) / fireRate;
            var appliedRecoil = recoil * recoilAnimationCurve.Evaluate(scaledTimeSinceShot);
            transform.localPosition = new Vector3(0, 0, -appliedRecoil);
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = Vector3.zero;
        yield return null;
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
        reloading = false;
    }

    void InstantiateBulletDecal(Vector3 position, Quaternion rotation, Transform parent)
    {
        var decalIndex = Random.Range(0, bulletDecals.Count);
        Instantiate(bulletDecals[decalIndex], position, rotation, parent);
    }

    bool canReload()
    {
        return !reloading && ammo != 0 && ammoInMagazine != magazineSize && Time.time > lastShot + fireRate;
    }
    

    IEnumerator Reload()
    {
        reloading = true;
        var reloadStartTime = Time.time;
        while (Time.time < reloadStartTime + reloadSpeed)
        {
            var elapsedTime = Time.time - reloadStartTime;
            transform.localPosition = new Vector3(0, -reloadAnimationCurve.Evaluate(elapsedTime / reloadSpeed), 0);
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = Vector3.zero;
        var reloadedAmmoCount = Mathf.Min(magazineSize - ammoInMagazine, ammo);
        ammoInMagazine += reloadedAmmoCount;
        ammo -= reloadedAmmoCount;
        reloading = false;
        reloadAudioSource.Play();
        yield return null;
    }
}
