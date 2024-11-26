using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Entity
{

    //[Header("Movemt Attribs")]
    //[SerializeField] protected float moveSpeed = 50;

    [Header("Player Hud")]
    [SerializeField] private Text hpText;
    [SerializeField] private Text statsText;
    //[SerializeField] protected Rigidbody2D rb2;
    //[SerializeField] protected Transform weapon_FireArm;
    //[SerializeField] protected Transform weapon_MelayArm;
    //[SerializeField] protected Animation anim_SwordSwing;

    public override void TakeDamage(double amount, Entity atacker = null)
    {
        if (atacker == this) return; //dont damage self

        hp -= amount;

        //hpText.text = "HP " + hp.ToString() + " / " + Singletone.instance.playerMaxHp;
        Singletone.instance.UpdateHud(hpText, null, this.hp);

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
    private void Awake()
    {
        if (Singletone.instance.playerHp > 0)
        {
            this.hp = Singletone.instance.playerHp;
        }
        else if (Singletone.instance.playerMaxHp > 0)
            this.hp = Singletone.instance.playerMaxHp;
        Singletone.instance.UpdateHud(hpText, statsText, this.hp);
        //hpText.text = "HP " + hp.ToString() + " / " + Singletone.instance.playerMaxHp;
    }
    override protected void _Death(bool playDeathAnim = true)
    {
        SceneManager.LoadScene("Chest"); //przejscie do poprzedniej sceny
    }

    public void SaveHp()
    {
        Singletone.instance.playerHp = this.hp;
    }
}
