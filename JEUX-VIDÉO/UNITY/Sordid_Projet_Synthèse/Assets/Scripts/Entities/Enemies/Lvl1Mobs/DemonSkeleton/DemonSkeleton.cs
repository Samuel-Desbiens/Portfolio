using Harmony;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DemonSkeleton : GroundedEnemyBehavior
{
    //TODO : Change to Timer
    [SerializeField] float teleportCD = 8;
    [SerializeField] float teleportRange = 30;
    [SerializeField] float minTpDistanceFromPlayer = 10;
    [SerializeField] List<GameObject> portals;

    
    [SerializeField] Transform castSpellPos;
    [SerializeField] ProjectilePool pool;
    private bool firstEnabled = false;

    private static bool canSummon = false;

    protected override IEnumerator StartAttack()
    {
        
        yield return new WaitForSeconds(attackTime);
        GameObject projectile = pool.GetProjectile();
        Vector2 projDir = (player.position - transform.position).normalized;
        projectile.SetActive(true);
        projectile.transform.position = castSpellPos.position;
        projectile.GetComponent<Projectile>().Shoot(projDir);
        soundManager.PlayAudio(soundManager.magicAttackClip, transform.position);
        yield return null;
    }
    float nextTpTime = 0;



    void Teleport(Vector2 position)
    {
        Debug.Log("TP");
        animator.SetTrigger("attack");
        portals[0].transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        portals[0].gameObject.SetActive(true);

        portals[1].transform.position = position;
        portals[1].gameObject.SetActive(true);

        nextTpTime = Time.time + teleportCD;


    }

    bool CheckTeleportRaycastConditions(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(player.position,dir, teleportRange, lm);
        if (hit.collider && Vector2.Distance(hit.point, player.position) > minTpDistanceFromPlayer)
        {
            Teleport(new Vector2(hit.point.x - dir.x, hit.point.y));
            return true;
        }
        return false;
    }

    bool CheckTeleport()
    {
        if (canSummon && firstEnabled)
        {
            if (Vector2.Distance(transform.position, player.position) < losRange && Time.time > nextTpTime)
            {
                int xDir = Random.Range(0, 2) * 2 - 1;
                if (CheckTeleportRaycastConditions(new Vector2(xDir, 0))) return true;
                else if (CheckTeleportRaycastConditions(new Vector2(-xDir, 0))) return true;
                else return false;
            }
        }
        return false;

    }

    protected override void Start()
    {
        pool = GameObject.Find("DemonSkeletonPool").GetComponent<ProjectilePool>();

        attackTime = 1;

        base.Start();
    }
    protected override void OnEnable()
    {
        if (!canSummon)
        {
            canSummon = true;
            firstEnabled = true;
        }
        Transform linkedPortal = FindFirstObjectByType<PortalParent>().GetAvailableLinkedPortal().transform;
        linkedPortal.gameObject.SetActive(true);
        portals = linkedPortal.Children().ToList();
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        portals[0].Parent().gameObject.SetActive(false);
        if (firstEnabled)
        {
            canSummon = false;
        }
        firstEnabled = false;
        base.OnDisable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckTeleport())
        {
            CheckLoS();
            CheckToAttack();
            CheckWalls();
            Move();
        } 
    }
}
