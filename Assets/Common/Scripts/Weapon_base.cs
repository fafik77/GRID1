using UnityEngine;

public abstract class Weapon_base : MonoBehaviour
{
    //damage dealt when hit
    [SerializeField] public float weaponDamage = 1;
    //fireDelay= 1/fireRate
    [SerializeField] public float weaponDelay = 1;
    //we dont want to hit the attacker, only the reciving end
    protected Entity ignoreSelf;
    //swing a sword, shoot a weapon
    public abstract void Fire();
    protected float weaponTimeout = 0;

    //true when no timeout
    public bool IsWeaponNotTimeout() {  return weaponTimeout <= 0f; }
    public bool IsOnTimeout() {  return weaponTimeout > 0f; }
    //true if 
    public bool IsWeaponAvailable() { return weaponDamage > 0f; }

    public void SetIgnoreSelf(Entity ignoreSelf) { this.ignoreSelf = ignoreSelf; }
    public void SetweaponDamage(float weaponDamage) { this.weaponDamage = weaponDamage; }
    public void SetweaponDelay(float weaponDelay) { this.weaponDelay = weaponDelay; }

    private void FixedUpdate()
    {
        weaponTimeout -= Time.fixedDeltaTime;
    }

}
