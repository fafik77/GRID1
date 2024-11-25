using UnityEngine;

public class Player : Entity
{

    //[Header("Movemt Attribs")]
    //[SerializeField] protected float moveSpeed = 50;

    //[Header("Dependencies")]
    //[SerializeField] protected Rigidbody2D rb2;
    //[SerializeField] protected Transform weapon_FireArm;
    //[SerializeField] protected Transform weapon_MelayArm;
    //[SerializeField] protected Animation anim_SwordSwing;

    public override void TakeDamage(double amount, Entity atacker = null)
    {

        if (hp <= 0)
        {
            _Death();
            return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
