using System;
using UnityEngine;
using UnityEngine.UI;

public class Singletone : MonoBehaviour
{
     //allow only one instance to exist
    public static Singletone instance;
    private void Awake()
    {
        if (instance != null) return;
        instance = this;
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        DontDestroyOnLoad(this.gameObject);
    }

    public double playerHp;
    public float playerMaxHp = 20;

    //public GameObject player1;
    public float enemyHp;
    public float player_melayDamage = 1;
    public float player_melaySpeed = 1;
    public float player_gunDamage = 1;
    public float player_gunSpeed = 1;

    /// <summary>
    /// passed in values can be null
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="stats"></param>
    /// <param name="hpVal"></param>
    public void UpdateHud(Text hp, Text stats, double hpVal=0)
    {
        if (hp != null)
            hp.text = "HP " + Math.Round((hpVal > 0 ? hpVal : Singletone.instance.playerHp), 1, MidpointRounding.AwayFromZero) + " / " + Singletone.instance.playerMaxHp;
        if (stats != null)
        {
            string newText;
            newText = "Melay Dmg: " + Singletone.instance.player_melayDamage + "\n";
            newText += "Melay rearm time: " + Singletone.instance.player_melaySpeed + "\n";
            newText += "Gun Dmg: " + Singletone.instance.player_gunDamage + "\n";
            newText += "Gun reload time: " + Singletone.instance.player_gunSpeed + "\n";
            stats.text = newText;
        }
    }

}
