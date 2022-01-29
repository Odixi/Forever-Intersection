using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve reloadAnimationCurve;
    [SerializeField]
    private List<GameObject> bulletDecals;
    [SerializeField]
    private int magazineSize = 12;
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
    private Transform barrel;
    private new Camera camera;
    private float? lastShot;
    public int ammoInMagazine = 0;
    private bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        barrel = transform.GetChild(0);
        camera = Camera.main;
        ammoInMagazine = Mathf.Min(magazineSize, ammo);
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
            var ray = raycastCamera.ScreenPointToRay(new Vector3(0.5f * Screen.width, 0.5f * Screen.height));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                InstantiateBulletDecal(hit.point, Quaternion.LookRotation(hit.point - transform.position));
            }
            ammoInMagazine--;
            lastShot = Time.time;
        }
    }

    void InstantiateBulletDecal(Vector3 position, Quaternion rotation)
    {
        var decalIndex = Random.Range(0, bulletDecals.Count);
        Instantiate(bulletDecals[decalIndex], position, rotation);
    }

    bool canReload()
    {
        return !reloading && ammo != 0 && ammoInMagazine != magazineSize;
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
        yield return null;
    }
}
