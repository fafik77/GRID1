using Unity.VisualScripting;
using UnityEngine;

abstract public class Entity : MonoBehaviour
{
    [SerializeField] public double hp;
    [SerializeField] protected ParticleSystem OnDeathParticleSystem;
    [SerializeField] protected GameObject OnDamageParticleSystem;
    //[SerializeField] protected ParticleSystem OnDamageParticleSystem;
    //https://www.youtube.com/watch?v=_z68_OoC_0o&list=PL3Qcy-_BC-b5n4B30Le94vdRf03kbD7V3&index=80
    //https://www.youtube.com/watch?v=faKPt0cM2VI&list=PL3Qcy-_BC-b5n4B30Le94vdRf03kbD7V3&index=81
    protected ParticleSystem particleDamage;

    //check if hp is <=0 and apply death
    public abstract void TakeDamage(double amount, Entity atacker=null);
    public double GetHp(){ return hp; }
    public void SetHp(double hp) { this.hp = hp;if (hp <= 0) { TakeDamage(0); } }

    private void Start()
    {
        if (OnDamageParticleSystem != null)
        {
            particleDamage = OnDamageParticleSystem.GetComponent<ParticleSystem>();
            if (particleDamage != null)
            {
                particleDamage.Pause();
            }
            OnDamageParticleSystem.SetActive(true);
        }
    }

    //apply death
    virtual protected void _Death(bool playDeathAnim=true)
    {
        if (playDeathAnim)
        {
            PlayDeathAnimation();
        }

        GameObject.Destroy(gameObject);
    }
    public void PlayDamageAnimation()
    {
        if (particleDamage != null)
        {
            particleDamage.Play();
        }
    }
    public void PlayDeathAnimation()
    {
        if (OnDeathParticleSystem != null)
        {
            ParticleSystem particle = Instantiate(OnDeathParticleSystem, gameObject.transform.position, gameObject.transform.rotation);
            particle.time = 0;
            particle.Play();
            GameObject.Destroy(particle.gameObject, 2);
        }
    }
}
