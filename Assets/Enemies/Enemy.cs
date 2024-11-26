using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Entity
{

    //https://www.youtube.com/watch?v=2SXa10ILJms&list=PL3Qcy-_BC-b5s9jGuUiuTp2e6cyi8pC1I
    //https://www.youtube.com/watch?v=NZnQ8rw5xjc&list=PL3Qcy-_BC-b5s9jGuUiuTp2e6cyi8pC1I&index=2   (with Events)
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
    private void Awake()
    {
        if (Singletone.instance.enemyHp > 0)
            this.hp = Singletone.instance.enemyHp;
    }
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{ 
    //}

    private void OnDestroy()
    {
        SceneManager.LoadScene("Chest"); //przejscie do poprzedniej sceny
    }
}
