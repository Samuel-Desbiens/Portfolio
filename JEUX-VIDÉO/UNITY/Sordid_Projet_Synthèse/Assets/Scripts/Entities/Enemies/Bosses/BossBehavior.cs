using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
public class BossBehavior : MonoBehaviour
{
  [SerializeField] Transform player;
  [SerializeField] LayerMask lm;
  [SerializeField] Animator animator;

  //Movement
  bool blocked = false;
  [SerializeField] float speed;
  [SerializeField] float waitTime = 2.0f;
  float waitTimer = 0.0f;

  //Targeting
  bool targeting = false;
  float targetSign;
  [SerializeField] float losRange;


  //Attack
  [SerializeField] float range;
  [SerializeField] float attackCooldown = 2.0f;
  float attackCooldownTimer = 4.0f;
  float attackTime = 0.8f;
  float betweenAttackCooldown = 0.2f;

  //Constitution
  [SerializeField]float health = 20;
  bool dead = false;
  float deathTimer = 3.0f;

  //Bullets
  [SerializeField] GameObject bullet;
  private static int bulletCount = 50;
  private GameObject[] bullets = new GameObject[bulletCount];

    //Score
    [SerializeField] private int score;

  private void Start()
  {
    for (int i = 0; i < bulletCount; i++)
    {
      bullets[i] = Instantiate(bullet);
      bullets[i].SetActive(false);
    }
  }
  void Update()
  {
    CheckLife();
    CheckLoS();
    CheckInRange();
    CheckBlocked();
    FlipSprite();
    Move();
  }

  void Move()
  {
    animator.SetBool("moving", false);
    if (!targeting && !blocked && !dead)
    {
      
      animator.SetBool("moving", true);
      transform.Translate(new Vector2(0, speed * Time.deltaTime));
    }
    else if (blocked && !targeting && !dead)
    {
      waitTimer += Time.deltaTime;
      if (waitTimer >= waitTime)
      {
        speed = speed * -1;
        blocked = false;
        waitTimer = 0;
      }

    }
  }

  void CheckBlocked()
  {
    if (transform.position.y >= 12.5f && speed > 0)
    {
      blocked = true;
    }
    if (transform.position.y <= 1.35f && speed < 0)
    {
      blocked = true;
    }
  }

  void CheckLife()
  {
    if (health <= 0 && !dead)
    {
      dead = true;
      animator.SetTrigger("dead");
    }
    if (dead)
    {
      deathTimer -= Time.deltaTime;
      if (deathTimer <= 0)
      {
        GameManager.Instance.AddScore(GameManager.Instance.getBonus() + score);
        GameManager.Instance.StartNextlevel(2.0f); 
        gameObject.SetActive(false);
      }
    }
  }
  void CheckLoS()
  {
    float dis = Vector2.Distance(transform.position, player.position);
    if (dis < losRange)
    {
      RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.position - transform.position), dis, lm);
      if (hit.collider == null)
      {
        targeting = true;
        float disX = player.position.x - transform.position.x;
        targetSign = disX / Mathf.Abs(disX);
        return;
      }
    }
    targeting = false;
  }

  void CheckInRange()
  {
    attackCooldownTimer += Time.deltaTime;
    if (InAttackRange())
    {
      if (attackCooldownTimer >= attackCooldown)
      {
        attackCooldownTimer = 0;
        animator.SetBool("attack", true);
        StartCoroutine(StartAttack());
      }
    }
  }

  private bool InAttackRange()
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

  IEnumerator StartAttack()
  {
    yield return new WaitForSeconds(attackTime);
    Attack();
    yield return new WaitForSeconds(betweenAttackCooldown);
    Attack();
    yield return new WaitForSeconds(betweenAttackCooldown);
    Attack();
    yield return new WaitForSeconds(betweenAttackCooldown);
    Attack();
  }

  private GameObject GetBullet()
  {
    for (int i = 0; i < bulletCount; i++)
    {
      if (!bullets[i].activeSelf)
      {
        return bullets[i];
      }
    }
    return null;
  }
  void Attack()
  {
    GameObject bullet = GetBullet();
    bullet.GetComponent<BossBullet>().SetTarget(player.transform);
    bullet.transform.position = transform.position;
    bullet.SetActive(true);
  }

  void FlipSprite()
  {
    if ((player.position.x - transform.position.x) < 0)
    {
      GetComponent<SpriteRenderer>().flipX = true;
    }
    else if ((player.position.x - transform.position.x) > 0)
    {
      GetComponent<SpriteRenderer>().flipX = false;
    }
  }

public void TakeDamage(int damage)
{
    health -= damage;
}
}
*/