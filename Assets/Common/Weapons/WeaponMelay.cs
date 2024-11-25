using UnityEngine;

public class WeaponMelay : Weapon_base
{
    //https://www.youtube.com/watch?v=giJKCl-GVrU&list=PL3Qcy-_BC-b5n4B30Le94vdRf03kbD7V3&index=78
    //https://www.youtube.com/watch?v=oGW9UQQh-6w&list=PL3Qcy-_BC-b5n4B30Le94vdRf03kbD7V3&index=78
    //https://www.youtube.com/watch?v=sPiVz1k-fEs&list=PL3Qcy-_BC-b5n4B30Le94vdRf03kbD7V3&index=79

    //max range for melay attacks (we cheat the system: melay weapons will use the same system for calculating vectors, but instead of shooting a bullet, they will fire a raycast that has max range )
    [SerializeField] public float melayRange = 1;
    //[SerializeField] protected Transform GFX_Sword;

    private TrailRenderer trailRenderer;
    private Vector3 origScaleSword;


    public override void Fire()
    {
        Debug.Log("melay fire");
        weaponTimeout = weaponDelay;
        if (trailRenderer != null)
        {
            trailRenderer.emitting = false;
        }
        //GFX_Sword.localScale.Set(origScaleSword.x, melayRange, origScaleSword.z);
        //transform.get
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //origScaleSword = GFX_Sword.localScale;
        trailRenderer = gameObject.GetComponentInChildren<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trailRenderer != null)
        {
            trailRenderer.emitting = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision != null)
        {
            Entity entity = collision.GetComponent<Entity>();
            if (entity == null) return;
            if (entity == ignoreSelf) return;
            entity.TakeDamage(weaponDamage, ignoreSelf);
        }
    }
}
