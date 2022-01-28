using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    AnimationCurve reloadAnimationCurve;
    [SerializeField]
    private int magazineSize = 12;
    [SerializeField]
    private int ammo = 100;
    // Seconds per round
    [SerializeField]
    private float fireRate = 0.5f;
    // Seconds
    [SerializeField]
    private float reloadSpeed = 1f;
    private Transform barrel;
    private new Camera camera;
    private float? lastShot;
    private int ammoInMagazine = 0;
    private float? reloadStartTime;

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
        var canReload = ammo != 0 && (reloadStartTime == null || Time.time > reloadStartTime + reloadSpeed);
        if (ammoInMagazine == 0 && canReload) StartCoroutine(Reload());
        if (canReload && Input.GetButtonDown("Reload")) StartCoroutine(Reload());
    }

    void Shoot()
    {
        if (ammoInMagazine != 0 && (lastShot == null || Time.time > lastShot + fireRate))
        {
            var ray = camera.ScreenPointToRay(new Vector3(0.5f * Screen.width, 0.5f * Screen.height));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(camera.transform.position, hit.point, Color.red, 1f);
            }
            ammoInMagazine--;
            lastShot = Time.time;
        }
    }

    IEnumerator Reload()
    {
        if (ammoInMagazine == magazineSize || reloadStartTime + reloadSpeed > Time.time) yield return null;
        reloadStartTime = Time.time;
        while (Time.time < reloadStartTime + reloadSpeed)
        {
            var elapsedTime = Time.time - (float)reloadStartTime;
            transform.localPosition = new Vector3(0, -reloadAnimationCurve.Evaluate(elapsedTime / reloadSpeed), 0);
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = Vector3.zero;
        var reloadedAmmoCount = Mathf.Min(magazineSize - ammoInMagazine, ammo);
        ammoInMagazine += reloadedAmmoCount;
        ammo -= reloadedAmmoCount;
        yield return null;
    }
}
