using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
  private static SoundManager instance = null;
  public static SoundManager Instance { get { return instance; } }

  [SerializeField] private AudioSource audioSourcePrefab;
  [SerializeField] private static int audioSourcesAmount = 20;
  private AudioSource[] audioSources = new AudioSource[audioSourcesAmount];
  private AudioSource playerAudio;
  [SerializeField] private GameObject mainCam;


  #region AudioClips
  [SerializeField] private AudioClip musicNormal;
  [SerializeField] private AudioClip musicAction;
  [SerializeField] private AudioClip bowAttack;
  [SerializeField] private AudioClip chest1;
  [SerializeField] private AudioClip hit1;
  [SerializeField] private AudioClip hit2;
  [SerializeField] private AudioClip hit3;
  [SerializeField] private AudioClip jump;
  [SerializeField] private AudioClip airSound;
  [SerializeField] private AudioClip airBoomrang;
  [SerializeField] private AudioClip fireBallExplosion;
  [SerializeField] private AudioClip fireBallProj;
  [SerializeField] private AudioClip fireBolt;
  [SerializeField] private AudioClip iceSwordHit;
  [SerializeField] private AudioClip iceSwordSpawn;
  [SerializeField] private AudioClip leaf1;
  [SerializeField] private AudioClip spearSkeleton;
  [SerializeField] private AudioClip monster1;
  [SerializeField] private AudioClip monster2;
  [SerializeField] private AudioClip monster3;
  [SerializeField] private AudioClip monsterGrowl;
  [SerializeField] private AudioClip wallSound;
  [SerializeField] private AudioClip wallHit;
  [SerializeField] private AudioClip wave;
  [SerializeField] private AudioClip potion;
  [SerializeField] private AudioClip teleport;
  [SerializeField] private AudioClip whip;
  [SerializeField] private AudioClip magicAttack;
  [SerializeField] private AudioClip dash;
  [SerializeField] private AudioClip laser;
  [SerializeField] private AudioClip coinPickup;

  public AudioClip chest1Clip { get { return chest1; } }
  public AudioClip hit1Clip { get { return hit1; } }
  public AudioClip hit2Clip { get { return hit2; } }
  public AudioClip hit3Clip { get { return hit3; } }
  public AudioClip airSoundClip { get { return airSound; } }
  public AudioClip airBoomrangClip { get { return airBoomrang; } }
  public AudioClip bowAttackClip { get { return bowAttack; } }
  public AudioClip fireBallExplosionClip { get { return fireBallExplosion; } }
  public AudioClip fireBallProjClip { get { return fireBallProj; } }
  public AudioClip fireBoltClip { get { return fireBolt; } }
  public AudioClip iceSwordHitClip { get { return iceSwordHit; } }
  public AudioClip iceSwordSpawnClip { get { return iceSwordSpawn; } }
  public AudioClip leaf1Clip { get { return leaf1; } }
  public AudioClip spearSkeletonClip { get { return spearSkeleton; } }
  public AudioClip monster1Clip { get { return monster1; } }
  public AudioClip monster2Clip { get { return monster2; } }
  public AudioClip monster3Clip { get { return monster3; } }
  public AudioClip monsterGrowlClip { get { return monsterGrowl; } }
  public AudioClip wallSoundClip { get { return wallSound; } }
  public AudioClip wallHitClip { get { return wallHit; } }
  public AudioClip potionClip { get { return potion; } }
  public AudioClip musicNormalClip { get { return musicNormal; } }
  public AudioClip musicActionClip { get { return musicAction; } }
  public AudioClip teleportClip { get { return teleport; } }
  public AudioClip waveClip { get { return wave; } }
  public AudioClip jumpClip { get { return jump; } }
  public AudioClip whipClip { get { return whip; } }
  public AudioClip magicAttackClip { get { return magicAttack; } }
  public AudioClip dashClip { get { return dash; } }
  public AudioClip laserClip { get { return laser; } }
  public AudioClip coinPickupClip { get { return coinPickup; } }

  void Start()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }

    SceneManager.sceneLoaded += OnSceneLoaded;
    InstantiateAudioSources();
  }
    #endregion
    public void PlayAudio(AudioClip clip, Vector2 position)
  {
    AudioSource audio = findAudioSource();
    audio.gameObject.SetActive(true);
    audio.clip = clip;
    audio.transform.position = position;
    audio.Play();
    StartCoroutine(DeactivateSource(audio, clip.length));
  }

  public void ChangeMusic(AudioClip clip)
  {
    playerAudio.clip = clip;
    playerAudio.Play();
  }

  IEnumerator DeactivateSource(AudioSource audio, float time)
  {
    yield return new WaitForSeconds(time);
    audio.gameObject.SetActive(false);
  }



  private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
  {
    mainCam = Camera.main.gameObject;
    playerAudio = GameObject.FindWithTag("Player").AddComponent<AudioSource>();
    playerAudio.clip = musicNormal;
    playerAudio.Play();
  }

  private AudioSource findAudioSource()
  {
    foreach (AudioSource audio in audioSources)
    {
      if (!audio.gameObject.activeSelf)
      {
        return audio;
      }
    }
    return null;
  }

  private void InstantiateAudioSources()
  {
    GameObject pool = new("AudioSources");
    DontDestroyOnLoad(pool);
    for (int i = 0; i < audioSourcesAmount; i++)
    {
      audioSources[i] = Instantiate(audioSourcePrefab);
      audioSources[i].name += i;
      audioSources[i].transform.SetParent(pool.transform);
      audioSources[i].gameObject.SetActive(false);
    }
  }



}
