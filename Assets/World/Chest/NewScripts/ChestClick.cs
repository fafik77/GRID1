using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChestClick : MonoBehaviour
{
    [SerializeField] private Transform chest_closed;
    [SerializeField] private Transform chest_open;
    [SerializeField] private Canvas canvas;

    [Header("Player Hud")]
    [SerializeField] private Text hpText;
    [SerializeField] private Text statsText;

    private bool chestState_isOpen;
    private bool chestLooted;
    private float actionCountdown;
    private Text canvasText;
    private struct RollResults
    {
        public RollResults(int x=0)
        {
            lootType = 0;
            v_float = new List<float>();
            v_int = new List<int>();
        }
        public int lootType;
        public List<float> v_float;
        public List<int> v_int;
        public void Reset()
        {
            lootType = 0;
            v_float.Clear();
            v_int.Clear();
        }
        public void Add(float f, bool round = true) { v_float.Add(round ? (float)Math.Round(f, 2, MidpointRounding.AwayFromZero) : f); }
        public void Add(int i) { v_int.Add(i); }
        public void SetLoot(int loot) { lootType = loot; }
    }
    private RollResults rollResults;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rollResults = new RollResults(0);
        rollResults.Reset();
        //chest_open.gameObject.SetActive(false);
        //chest_closed.gameObject.SetActive(true);
        chestState_isOpen = false;
        UpdateChestState();
        if (canvas != null)
        {
            canvasText = canvas.GetComponentInChildren<Text>(true);
        }
        canvas.gameObject.SetActive(false);
        Singletone.instance.UpdateHud(hpText, statsText);
    }
    private void UpdateChestState()
    {
        chest_open.gameObject.SetActive(chestState_isOpen);
        chest_closed.gameObject.SetActive(!chestState_isOpen);
        if (chestState_isOpen == false && canvas != null) 
        {
            canvas.gameObject.SetActive(false);
            rollResults.Reset();
        }
    }

    public void TakeLoot()
    {
        canvas.gameObject.SetActive(false);
        switch (rollResults.lootType)
        {
            case 1: //add melay point
                {
                    Singletone.instance.player_melayDamage = rollResults.v_float[0];
                    Singletone.instance.player_melaySpeed = rollResults.v_float[1];
                    break;
                }
            case 2: //add firearm point
                {
                    Singletone.instance.player_gunDamage = rollResults.v_float[0];
                    Singletone.instance.player_gunSpeed = rollResults.v_float[1];
                    break;
                }
        }
        rollResults.Reset();
        Singletone.instance.UpdateHud(hpText, statsText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            /*if (GetComponent<Collider2D>().OverLapPoint(mousePosition))
            {
                //do great stuff
            }*/
            if (GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                chestState_isOpen = !chestState_isOpen;
                UpdateChestState();
                actionCountdown = 1;
                chestLooted = false;
                //Instantiate(prefabopen, new Vector2(0, 0), Quaternion.identity);
                // Destroy(prefabclosed);
            }
        }
        if (actionCountdown > 0f)
        {
            actionCountdown -= Time.deltaTime;
        }
        if (chestLooted==false && actionCountdown <= 0f && chestState_isOpen)
        {
            chestLooted = true;
            canvas.gameObject.SetActive(true);
            rollResults.Reset();

            int action = UnityEngine.Random.Range(0, 3);
            rollResults.SetLoot(action);
            switch (action)
            {
                case 0: //enemy and their hp
                    {
                        canvasText.text = "There was 99.9% chance\nof it being a Mimic\nand you still opened it";
                        Singletone.instance.enemyHp = UnityEngine.Random.Range(5, 10) + (Singletone.instance.player_melayDamage + Singletone.instance.player_gunDamage)/2 + (Singletone.instance.playerMaxHp-20);
                        SceneManager.LoadScene("Arena"); //load Arena scene
                        break;
                    }
                 case 1: //add melay point
                    {
                        rollResults.Add( Math.Max(1, UnityEngine.Random.Range(Singletone.instance.player_melayDamage - 1, Singletone.instance.player_melayDamage + 2)));
                        rollResults.Add (Math.Max(0.05f, UnityEngine.Random.Range(Singletone.instance.player_melaySpeed - 0.1f, Singletone.instance.player_melaySpeed + 0.1f)));
                        canvasText.text = "Take Sword\n" + rollResults.v_float[0] + " damage, " + rollResults.v_float[1] + " rearm time";
                        //Singletone.instance.player_melayDamage += 0.5f;
                        break;
                    }
                 case 2: //add firearm point
                    {
                        rollResults.Add(Math.Max(1, UnityEngine.Random.Range(Singletone.instance.player_gunDamage - 1, Singletone.instance.player_gunDamage + 2)));
                        rollResults.Add(Math.Max(0.05f, UnityEngine.Random.Range(Singletone.instance.player_gunSpeed - 0.1f, Singletone.instance.player_gunSpeed + 0.1f)));
                        canvasText.text = "Take Gun\n" + rollResults.v_float[0] + " damage, " + rollResults.v_float[1] + " reload time";
                        //Singletone.instance.player_gunDamage += 0.5f;
                        break;
                    }
                 case 3: //set player max hp and renew hp
                    {
                        canvasText.text = "Max Hp increased by 0.5\n Max Hp: " + Singletone.instance.playerMaxHp;
                        Singletone.instance.playerHp = Singletone.instance.playerMaxHp += 0.5f;
                        break;
                    }
            }
            Singletone.instance.UpdateHud(hpText, statsText);
        }
    }
}
