using UnityEngine;

public class Enemy : Entity
{
    public override void TakeDamage(double amount, Entity atacker = null)
    {
        if (atacker == this) return; //dont damage self
        if (atacker != null)
        {
            if (atacker.GetComponent<Enemy>() != null) return; //is another Enemy
        }

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

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{ 
    //}
}
