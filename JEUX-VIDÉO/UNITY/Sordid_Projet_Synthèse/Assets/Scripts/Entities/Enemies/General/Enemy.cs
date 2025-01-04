using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //Spawning:
    [SerializeField] private int powerPoints = 0;


    //base comps
    [SerializeField] protected LayerMask lm;
    [SerializeField] protected Transform player;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody2D rb;
    protected Collider2D enemyCollider;

    //Constitution
    [SerializeField] protected Health health;
    protected bool dead = false;

    //movement 
    [SerializeField] protected float speed;

    //Targeting
    protected bool targeting = false;
    protected float targetSign;
    [SerializeField] protected float losRange;

    //Attack
    [SerializeField] protected float range;
    [SerializeField] protected float baseAttackCooldown;
    protected bool attacking = false;
    protected SoundManager soundManager;

    protected float attackCooldownTimer = 0.0f;
    protected float attackTimer = 0.0f;
    protected float attackTime;

    //Score
    //Bonus Drop
    protected BonusManager bonusPool;

    [SerializeField] protected int score;
    protected virtual void Awake(){
        player = GameObject.Find("Player").transform;
        bonusPool = GameObject.Find("GameManager").GetComponent<BonusManager>();
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
       soundManager = SoundManager.Instance;
    }
    protected virtual void OnEnable()
    {
        if (dead)
        {
            gameObject.SetActive(false);
        }
        targeting = false;
    }
    protected virtual void OnDisable()
    {

    }


    public void OnDeath()
    {
        DropLoot();
        gameObject.GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("dead");
        dead = true;
        StartCoroutine(GoFar());
        ScoreManager.instance.IncrementKills();
    }

    IEnumerator GoFar()
    {
        //TODO : change to actual value
        yield return new WaitForSeconds(1);
        transform.position = new Vector2(10000, 10000);
        gameObject.SetActive(false);

    }

    public void TakeDmg(float DMG, SpellBehaviour.SpellElements Element)
    {
        float ActualDamageTaken = DMG;
        //Ici Irait la logique de faiblesse qui changerait le dmg pris d�pendament de l'�lements comparer au faiblesse/force de l'enemy
        health.TakeDmg(ActualDamageTaken);
    }
    protected void CheckToAttack()
    {
        if (InAttackRange())
        {
            if (Time.time > attackCooldownTimer)
            {
                BasicAttack();
            }
            attacking = true;
        }
        else
        {
            attacking = false;
        }
    }
    protected abstract IEnumerator StartAttack();

    public virtual void BasicAttack()
    {
        attackCooldownTimer = Time.time + baseAttackCooldown;
        animator.SetTrigger("attack");
        StartCoroutine(StartAttack());
    }

    protected bool InAttackRange()
    {
        float dis = Vector2.Distance(transform.position, player.position);
        if (dis < range)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.position - transform.position), dis, lm);
            if (hit.collider == null)
            {

                return true;
            }
        }
        return false;
    }

    void DropLoot()
    {
        int nbCoinsDrop = Random.Range(0, 10);
        int nbPotionDrop = Random.Range(0, 2);
        int nbPowerDrop = Random.Range(0, 5);
        DropCoins(nbCoinsDrop);
        DropPotion(nbPotionDrop);
        DropPower(nbPowerDrop);
    }

    void DropCoins(int nbCoins)
    {
        GameObject currentCoin = null;
        for (int i = 0; i < nbCoins; i++)
        {
            currentCoin = bonusPool.FindCoin();
            currentCoin.SetActive(true);
            currentCoin.transform.position = gameObject.transform.position;
        }
    }

    void DropPotion(int nbPotion)
    {
        GameObject currentPotion = null;
        if (nbPotion == 1)
        {
            currentPotion = bonusPool.FindPotion();
            currentPotion.SetActive(true);
            currentPotion.transform.position = gameObject.transform.position;
        }
    }

    void DropPower(int nbPower)
    {
        GameObject currentPower = null;
        for (int i = 0; i < nbPower; i++)
        {
            currentPower = bonusPool.FindPower();
            currentPower.SetActive(true);
            currentPower.transform.position = gameObject.transform.position;
        }
    }


    public void SetPlayer(Transform _player)
    {
        player = _player;
    }

    public int GetPowerPoints()
    {
        return powerPoints;
    }

    public void Spawn(Vector2 pos)
    {
        gameObject.SetActive(true);
        dead = false;
        transform.position = pos;
    }

    public void Spawn(Vector2 pos, Transform player)
    {
        Spawn(pos);
        SetPlayer(player);
    }

}
