using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField] GameObject chaseTargetPlayer;
    //[SerializeField] Transform shootPoint;
    [SerializeField] float chaseSpeed;
    [SerializeField] float chaseWhenFurtherThan;
    [SerializeField] float chaseHysterisis;
    [SerializeField] float secondsPerAction;

    [Header("Weapons")]
    [SerializeField] Transform weapon_FireArm;

    private float distanceDelta;
    private bool chaseOnHysterisis;
    private Vector2 directionHeading;
    private WeaponFire weapon_FireArmScript;
    private Enemy entityThisEnemy;
    private float actionTimeout;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        entityThisEnemy = this.GetComponent<Enemy>();
        if (weapon_FireArm != null)
        {
            weapon_FireArmScript= weapon_FireArm.GetComponent<WeaponFire>();
            weapon_FireArmScript?.SetIgnoreSelf(entityThisEnemy);
            
            //debug ?
            weapon_FireArmScript?.SetweaponDamage(1);
        }
        actionTimeout = 2; //start with 2sec of timeout
    }

    // Update is called once per frame
    void Update()
    {
        distanceDelta = Vector2.Distance(this.transform.position, chaseTargetPlayer.transform.position);
        directionHeading = chaseTargetPlayer.transform.position - this.transform.position;
        directionHeading.Normalize();
        if (distanceDelta >= (chaseWhenFurtherThan + chaseHysterisis)
            || (chaseOnHysterisis && distanceDelta >= chaseWhenFurtherThan))
        {
            if (chaseOnHysterisis == true && actionTimeout <= secondsPerAction) 
            {
                actionTimeout += Time.deltaTime;
            }
            chaseOnHysterisis = true;
            transform.position = Vector2.MoveTowards(this.transform.position, chaseTargetPlayer.transform.position, chaseSpeed * Time.deltaTime);
        }
        else
        {
            chaseOnHysterisis = false;
        }

        if (actionTimeout > 0) actionTimeout -= Time.deltaTime;
        if (weapon_FireArm != null) //get ready to shoot
        {
            float angle = Mathf.Atan2(directionHeading.y, directionHeading.x) * Mathf.Rad2Deg - 180f;
            weapon_FireArm.rotation = Quaternion.Euler(Vector3.forward * angle);
            if (actionTimeout <= 0f)
            {
                actionTimeout = secondsPerAction;
                if (weapon_FireArmScript != null && !weapon_FireArmScript.IsOnTimeout())
                {
                    weapon_FireArmScript.Fire();
                }
            }
        }
        
    }
    private void OnDestroy()
    {
        if (chaseTargetPlayer != null)
        {
            Player playerScript = chaseTargetPlayer.GetComponentInChildren<Player>(false);
            if (playerScript != null)
            {
                playerScript.SaveHp();
            }
        }
    }
}
