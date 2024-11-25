using UnityEngine;

public class WeaponFire : Weapon_base
{
    //https://www.youtube.com/watch?v=8TqY6p-PRcs
    //https://www.youtube.com/watch?v=LqrAbEaDQzc
    [SerializeField] Transform firePoint;
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected float bulletSpeed;

    // Update is called once per frame
    void Update()
    {
        if (weaponTimeout > 0f)
        {
            weaponTimeout-= Time.deltaTime;
        }
    }

    public override void Fire()
    {
        weaponTimeout = weaponDelay;

        Bullet bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        bullet.damage = weaponDamage;
        Rigidbody2D rbBull = bullet.GetComponent<Rigidbody2D>();
        rbBull.linearVelocity = -firePoint.right * bulletSpeed;

    }
}
