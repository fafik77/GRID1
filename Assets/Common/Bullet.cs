using UnityEngine;

public class Bullet : Entity
{
    //https://www.youtube.com/watch?v=8TqY6p-PRcs
    [SerializeField] public double damage;

    Entity owner;
    public Bullet(double damage=0, Entity owner = null)
    {
        SetHp(0.1f);
        this.damage = damage;
        this.owner = owner;
    }

    //yes we want damageble bullets, that way 2 bullets coliding will destroy each other
    public override void TakeDamage(double amount, Entity atacker)
    {
        //dont destroy own balls
        if (owner != null && atacker == owner) return;

        //test if atacker is ownere or owner's bullet
        if (atacker != null)
        {
            Bullet bulletAtck = atacker.GetComponent<Bullet>();
            if (bulletAtck != null)
            {
                if (bulletAtck.owner == owner) return; //this is owner's bullet, dont run 
            }
        }

        hp -= amount;

        if (hp <= 0)
        {
            _Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        Entity entity = collision.collider.GetComponent<Entity>();
        if (entity != null)
        {
            Debug.Log("collision with: " + entity.name);
            if (entity == owner) return; //dont damage owner
            //if (owner && owner is Player && entity is Player) return;//
            if (owner && owner is Enemy && entity is Enemy) return; //no (un)friendly fire
            entity.TakeDamage(damage, owner);
            _Death(false);
            return;
        }
        _Death(true);
    }

}
