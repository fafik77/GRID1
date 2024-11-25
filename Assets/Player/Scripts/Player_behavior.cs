using UnityEngine;

[SelectionBase]
public class Player_behavior : MonoBehaviour
{
    [Header("Movemt Attribs")]
    [SerializeField] float moveSpeed = 50;

    [Header("Depends")]
    [SerializeField] private Rigidbody2D rb2;
    [SerializeField] private Animator animator;

    [Header("Weapons")]
    [SerializeField] Transform weapon_FireArm;
    [SerializeField] Transform weapon_MelayArm;


    private Vector2 moveDirection = Vector2.zero;
    private Vector2 mousePosition;
    private Player playerEntity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerEntity = gameObject.GetComponent<Player>();
        if (rb2 == null)
        {
            rb2 = this.GetComponent<Rigidbody2D>();
        }
        if (animator == null)
        {
            animator = gameObject.GetComponent<Animator>();
        }

        if (weapon_MelayArm != null) {
            Weapon_base weapon = weapon_MelayArm.GetComponent<Weapon_base>();
            if (weapon != null)
            {
                weapon.SetIgnoreSelf(playerEntity);
            }
        }
        if (weapon_FireArm != null) {
            Weapon_base weapon = weapon_FireArm.GetComponent<Weapon_base>();
            if (weapon != null)
            {
                weapon.SetIgnoreSelf(playerEntity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    private void FixedUpdate()
    {
        MovementUpdate();
    }

    private void GetInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        //rotate to heading direction
        if(moveDirection.x > 0f)
        {
            transform.rotation = Quaternion.Euler( 0,180f,0);
        }
        else if (moveDirection.x < 0f)
        {
            transform.rotation = Quaternion.Euler(0, 0f, 0);
        }

        if (Input.GetMouseButtonDown(1) && weapon_FireArm!=null)    //Rmb = gun
        {
            WeaponFire weaponFire = weapon_FireArm.GetComponent<WeaponFire>();
            if (weaponFire != null && !weaponFire.IsOnTimeout())
            {
                weaponFire.Fire();
            }
        }
        if (Input.GetMouseButtonDown(0) && weapon_MelayArm != null) //Lmb = sword
        {
            WeaponMelay weaponMelay = weapon_MelayArm.GetComponent<WeaponMelay>();
            if (weaponMelay != null && !weaponMelay.IsOnTimeout())
            {
                animator.SetTrigger("Attack");
                weaponMelay.Fire();
            }
        }
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void MovementUpdate()
    {
        rb2.linearVelocity = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;

        //Vector2 posFrom= Vector2.zero;
        //posFrom.x = weapon_FireArm.position.x;
        //posFrom.y = weapon_FireArm.position.y;
        //Vector2 aimDirection = mousePosition - posFrom;

        Vector2 aimDirection = mousePosition - rb2.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 180f;

        weapon_FireArm.rotation = Quaternion.Euler(0, 0, aimAngle);
    }

}
