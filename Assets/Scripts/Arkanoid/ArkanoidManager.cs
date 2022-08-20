using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkanoidManager : MonoBehaviour
{
    public GameObject crossHair;
    public GameObject player;
    public GameObject bullet;
    public Transform shootPos;
    public LineRenderer mouseLR;

    public MonsterRandom spawn;

    public Animator playerAni;
    public Health playerHP;

    public AudioClip clip;

    private float bulletSpeed = 12.0f;

    private Vector3 target;
    private bool playerMovePhase;
    private bool playerAttackPhase;
    private bool ableMove;
    private bool ableAttack;

    public float bulletAmmo = 4.0f;
    private float bulletAmount = 0.0f;
    private float fireRate = 0.1f;
    private float fireCoolTime = 0.1f;
    public float damage = 1.0f;

    private float bulletCount;
    private float angle;
    float distance;
    Vector2 direction = new Vector2(0.0f, 0.0f);

    private float timer = 0.0f;

    public bool hit = false;
    public bool monsterMove = false;
    void Awake()    
    {
        bulletCount = 0.0f;
        playerMovePhase = true;
        playerAttackPhase = false;
        ableMove = true;
        ableAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crossHair.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.z) * Mathf.Rad2Deg;

        if (playerMovePhase && bulletCount <= 0.0f)
            StartCoroutine(movePhase(rotationZ));
        else if (!playerMovePhase)
            StopCoroutine(movePhase(rotationZ));

        if (playerAttackPhase)
            StartCoroutine(attackPhase(difference, rotationZ));
        else if(!playerAttackPhase)
        {
            StopCoroutine(attackPhase(difference, rotationZ));

            if (monsterMove)
            {
                //spawn.SpawnMonster();
                monsterMove = false;
            }
        }

        if (hit)
            playerHP.TakeDamage(1);
    }

    void Shoot(Vector2 direction, float rotationZ)
    {
        GameObject bulletIns;

        if (bulletAmount < bulletAmmo)
        {
            fireRate = 0.0f;

            //bulletIns = Instantiate(bullet, shootPos.position, Quaternion.Euler(0.0f, 0.0f, rotationZ), transform);
            bulletIns = Instantiate(bullet, shootPos.position, Quaternion.Euler(0.0f, 0.0f, angle), transform);
            bulletIns.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            var bulletComponent = bulletIns.GetComponent<Bullets>();
            bulletComponent.pointShoot = this;

            bulletAmount++;
        }
    }

    public void CountOnGround()
    {
        bulletCount--;
        //Debug.Log(bulletCount);
    }

    public void IncreaseAmmo()
    {
        bulletAmmo++;
    }
    public void IncreaseDamage()
    {
        damage++;
    }
    
    IEnumerator movePhase(float rotZ)
    {
        yield return new WaitForSeconds(0.2f);

        ableMove = true;
        bulletAmount = 0.0f;
        //Debug.Log("Start Move Phase");

        if (ableMove)
        {
            //player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            player.transform.position = new Vector2(player.transform.position.x, target.y);

            if (player.transform.position.y > 4.0f)
            {
                player.transform.position = new Vector2(player.transform.position.x, 4.0f);
            }
            if (player.transform.position.y < -4.0f)
            {
                player.transform.position = new Vector2(player.transform.position.x, -4.0f);
            }
        }

        timer += Time.deltaTime;

        if(Input.GetMouseButtonDown(1) || timer >= 10.0f)
        {
           // Debug.Log("Stop Move Phase");
            bulletCount = bulletAmmo;
            ableMove = false;
            playerMovePhase = false;
            playerAttackPhase = true;

            timer = 0.0f;
        }
    }

    IEnumerator attackPhase(Vector3 dif, float rotZ)
    {
        mouseLR.SetPosition(0, shootPos.position);
        mouseLR.SetPosition(1, new Vector3(target.x, target.y, 0.0f));

        //player.transform.rotation = Quaternion.Euler(target);

       // Debug.Log("Attack Move Phase");

        if (Input.GetMouseButtonDown(0) && ableAttack)
        {
            angle = Mathf.Atan2(target.y - player.GetComponent<Transform>().position.y, target.x - player.GetComponent<Transform>().position.x) * Mathf.Rad2Deg;

            ableAttack = false;

            distance = dif.magnitude;
            direction = dif / distance;
            direction.Normalize();

            playerAni.SetBool("IsAttack", true);

            SoundManager.instance.SFXPlay("Slash", clip);

        }

        if (!ableAttack && fireRate > fireCoolTime)
            Shoot(direction, rotZ);

        fireRate += Time.deltaTime;

        if(bulletCount <= 0.0f)
        {
            mouseLR.SetPosition(0, Vector3.zero);
            mouseLR.SetPosition(1, Vector3.zero);

            ableAttack = true;
            playerMovePhase = true;
            playerAttackPhase = false;

            playerAni.SetBool("IsAttack", false);

            monsterMove = true;

            spawn.SpawnMonster();
            yield return null;
        }
    }
}