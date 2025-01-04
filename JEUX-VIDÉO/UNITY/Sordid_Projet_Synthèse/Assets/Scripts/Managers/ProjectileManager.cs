using Harmony;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProjectileManager : MonoBehaviour
{
  //Spells
  private static ProjectileManager instance;
  [SerializeField] List<GameObject> spellTypes = new List<GameObject>();
  IDictionary<string, int> spellSpawnAmount = new Dictionary<string, int>();
  KeyValuePair<string, int> airDiscAmount = new("AirDisc", 100);
  KeyValuePair<string, int> airDiscUpgradeAmount = new("AirDiscUpgrade", 100);
  KeyValuePair<string, int> airSlashAmount = new("AirSlash", 30);
  KeyValuePair<string, int> airSlashUpgradeAmount = new("AirSlashUpgrade", 30);
  KeyValuePair<string, int> fireBoltAmount = new("Firebolt", 50);
  KeyValuePair<string, int> fireBoltUpgradeAmount = new("FireboltUpgrade", 50);
  KeyValuePair<string, int> fireBallAmount = new("Fireball", 20);
  KeyValuePair<string, int> fireBallUpgradeAmount = new("FireballUpgrade", 20);
  KeyValuePair<string, int> iceSlashAmount = new("IceSlash", 4);
  KeyValuePair<string, int> iceSlashUpgradeAmount = new("IceSlashUpgrade", 4);
  KeyValuePair<string, int> iceWaveAmount = new("IceWave", 10);
  KeyValuePair<string, int> iceWaveUpgradeAmount = new("IceWaveUpgrade", 10);
  KeyValuePair<string, int> leafShieldAmount = new("LeafShields", 4);
  KeyValuePair<string, int> leafShieldUpgradeAmount = new("LeafShieldsUpgrade", 4);
  KeyValuePair<string, int> slashingAirAmount = new("SlashingAir", 10);
  KeyValuePair<string, int> slashingAirUpgradeAmount = new("SlashingAirUpgrade", 10);
  KeyValuePair<string, int> vineWhipAmount = new("VineWhip", 4);
  KeyValuePair<string, int> vineWhipUpgradeAmount = new("VineWhipUpgrade", 4);



  Dictionary<string, List<GameObject>> spellPool = new Dictionary<string, List<GameObject>>();

  //Boss1 Projectiles
  [SerializeField] private BossBullet boss1BulletPrefab;
  [SerializeField] private static int boss1BulletAmount = 100;
  private BossBullet[] boss1Bullets = new BossBullet[boss1BulletAmount];

  //Boss1 Roof Projectiles
  [SerializeField] private RoofBullet boss1RoofBulletPrefab;
  [SerializeField] private static int boss1RoofBulletAmount = 100;
  private RoofBullet[] roofBullets = new RoofBullet[boss1RoofBulletAmount];

  void Awake()
  {
    SetSpellSpawnAmount();
    if (instance == null)
    {
      Cursor.visible = false;
      instance = this;
      DontDestroyOnLoad(gameObject);
      InstantiateSpellProjectiles();
      InstantiateBoss1Projectiles();
      InstantiateBoss1RoofProjectiles();
    }
    else
    {
      Destroy(gameObject);
    }
  }

  

  public BossBullet findBoss1Bullet()
  {
    foreach (BossBullet bullet in boss1Bullets)
    {
      if (!bullet.gameObject.activeSelf)
      {
        return bullet;
      }
    }
    return null;
  }

  private void InstantiateBoss1Projectiles()
  {
    GameObject boss1Pool = new("BossProj");
    DontDestroyOnLoad(boss1Pool);
    for (int i = 0; i < boss1BulletAmount; i++)
    {
      boss1Bullets[i] = Instantiate(boss1BulletPrefab);
      boss1Bullets[i].name += i;
      boss1Bullets[i].transform.SetParent(boss1Pool.transform);
      boss1Bullets[i].gameObject.SetActive(false);
    }
  }

  public RoofBullet findBoss1RoofBullet()
  {
    foreach (RoofBullet bullet in roofBullets)
    {
      if (!bullet.gameObject.activeSelf)
      {
        return bullet;
      }
    }
    return null;
  }

  private void InstantiateBoss1RoofProjectiles()
  {
    GameObject pool = new("BossRoofProj");
    DontDestroyOnLoad(pool);
    for (int i = 0; i < boss1RoofBulletAmount; i++)
    {
      roofBullets[i] = Instantiate(boss1RoofBulletPrefab);
      roofBullets[i].name += i;
      roofBullets[i].transform.SetParent(pool.transform);
      roofBullets[i].gameObject.SetActive(false);
    }
  }

  private void InstantiateSpellProjectiles()
  {
    GameObject spellPoolGO = new("Projectiles");
    spellPoolGO.transform.position = new(0, 0, 0);
    DontDestroyOnLoad(spellPoolGO);
    foreach (GameObject GO in spellTypes)
    {
      spellPool.Add(GO.name, InstantiateSpell(GO, spellPoolGO));
    }
  }

  private List<GameObject> InstantiateSpell(GameObject spellPrefab, GameObject parent)
  {
    List<GameObject> spellList = new List<GameObject>();
    for (int i = 0; i < spellSpawnAmount[spellPrefab.name]; i++)
    {
      GameObject tempPro = Instantiate(spellPrefab);
      tempPro.SetActive(false);
      spellList.Add(tempPro);
      tempPro.name = spellPrefab.name + i.ToString();
      tempPro.transform.SetParent(parent.transform);
    }
    return spellList;
  }

  public GameObject GetSpell(string spellName)
  {
    if (spellName == null) return null;
    foreach (GameObject spell in spellPool[spellName])
    {
      if (!spell.activeSelf)
      {
        return spell;
      }
    }
    return null;
  }

  private void SetSpellSpawnAmount()
  {
    spellSpawnAmount.Add(airDiscAmount);
    spellSpawnAmount.Add(airDiscUpgradeAmount);
    spellSpawnAmount.Add(airSlashAmount);
    spellSpawnAmount.Add(airSlashUpgradeAmount);
    spellSpawnAmount.Add(fireBoltAmount);
    spellSpawnAmount.Add(fireBoltUpgradeAmount);
    spellSpawnAmount.Add(fireBallAmount);
    spellSpawnAmount.Add(fireBallUpgradeAmount);
    spellSpawnAmount.Add(iceSlashAmount);
    spellSpawnAmount.Add(iceSlashUpgradeAmount);
    spellSpawnAmount.Add(iceWaveAmount);
    spellSpawnAmount.Add(iceWaveUpgradeAmount);
    spellSpawnAmount.Add(leafShieldAmount);
    spellSpawnAmount.Add(leafShieldUpgradeAmount);
    spellSpawnAmount.Add(slashingAirAmount);
    spellSpawnAmount.Add(slashingAirUpgradeAmount);
    spellSpawnAmount.Add(vineWhipAmount);
    spellSpawnAmount.Add(vineWhipUpgradeAmount);
  }
}
