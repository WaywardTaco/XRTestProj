using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject bulletLoc;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletShootSpeed;
    [SerializeField] private float bulletDespawnTime;

    private void Start() {
        // StartCoroutine(ShootOnInterval(2));
    }

    private IEnumerator ShootOnInterval(float interval){
        yield return new WaitForSeconds(interval);

        this.FireBullet();

        StartCoroutine(ShootOnInterval(interval));
    }

    public void FireBullet(){
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletLoc.transform);
        bullet.GetComponent<Rigidbody>()?.AddForce(bulletLoc.transform.forward * bulletShootSpeed);
        StartCoroutine(DespawnBullet(bullet));
    }

    public void DestroyTurret(){
        // Destroy(this.gameObject);
    }

    private void OnDestroy() {
        Destroy(this.transform.parent.gameObject);
    }

    private IEnumerator DespawnBullet(GameObject bullet){
        yield return new WaitForSeconds(bulletDespawnTime);

        Destroy(bullet);
    }

}
