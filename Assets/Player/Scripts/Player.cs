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
        if (atacker == this) return; //dont damage self

        hp -= amount;

        if (hp <= 0)
        {
            _Death();
            return;
        }
        if (amount > 0)
        {
            PlayDamageAnimation();
        }
    }
    //override protected void _Death(bool playDeathAnim = true)
    //{
    //}
}
