using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestClick : MonoBehaviour
{
    [SerializeField] private Transform chest_closed;
    [SerializeField] private Transform chest_open;
  
    private bool chestState_isOpen;
    private float actionCountdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //chest_open.gameObject.SetActive(false);
        //chest_closed.gameObject.SetActive(true);
        chestState_isOpen = false;
        updateChestState();
    }
    private void updateChestState()
    {
        chest_open.gameObject.SetActive(chestState_isOpen);
        chest_closed.gameObject.SetActive(!chestState_isOpen);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            /*if (GetComponent<Collider2D>().OverLapPoint(mousePosition))
            {
                //do great stuff
            }*/
            if (GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                chestState_isOpen = !chestState_isOpen;
                updateChestState();
                actionCountdown = 1;
                //Instantiate(prefabopen, new Vector2(0, 0), Quaternion.identity);
                // Destroy(prefabclosed);
            }
        }
        if (actionCountdown > 0f)
        {
            actionCountdown -= Time.deltaTime;
        }
        if (actionCountdown <= 0f && chestState_isOpen)
        {
            SceneManager.LoadScene("Arena"); //load Arena scene
        }
    }
}
