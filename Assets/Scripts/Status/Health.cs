using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    public bool BoxDie = false;
    public bool MonsyerDie = false;
    public int Monstermun;

    public static bool MonsterRatDie = false;
    public static bool MonsterSkullDie = false;
    public static bool MonsterNineTailDie = false;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        transform.GetComponentInChildren<Text>().text = currentHealth.ToString();

        Debug.Log(currentHealth);

        if (currentHealth > 0)
        {
            //anim.SetTrigger("Hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                //anim.SetTrigger("die");

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true;
                if (gameObject.name == "Box(Clone)")
                {
                    BoxDie = true;
                }
                else
                {
                    MonsyerDie = true;
                    if (gameObject.name == "Monster_Rat(Clone)")
                    {
                        MonsterRatDie = true;
                    }
                    else if (gameObject.name == "Monster_Skull(Clone)")
                    {
                        MonsterSkullDie = true;
                    }
                    else if (gameObject.name == "Monster_NineTail(Clone)")
                    {
                        MonsterNineTailDie = true;
                    }
                    Destroy(this.gameObject);
                }

                


            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white; 
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
}