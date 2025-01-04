
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

  Transform pauseMenu;
  AchievementMenuManager achivementMenuManager;

  PlayerInputActions pia;
  InputAction pauseInput;
  // Start is called before the first frame update

  void Start()
  {
    pauseMenu = transform.GetChild(0);
    achivementMenuManager = pauseMenu.GetComponentInChildren<AchievementMenuManager>(true);
  }

  private void Awake()
  {
    pia = new PlayerInputActions();
    pauseInput = pia.Player.Escape;
  }

  private void OnEnable()
  {
    pauseInput.Enable();
  }

  private void OnDisable()
  {
    pauseInput.Disable();
  }

  // Update is called once per frame
  void Update()
  {
    //TODO : add input
    if (pauseInput.triggered)
    {
      UpdateMenuState();
    }
  }

  void UpdateMenuState()
  {
    if (pauseMenu.gameObject.activeSelf)
    {
      UnPause();
    }
    else
    {
      Pause();
    }
  }

  void Pause()
  {
    pauseMenu.gameObject.SetActive(true);
    achivementMenuManager.SetAllAchievements(AchievementManager.instance.GetAchievements());
    Cursor.visible = true;
    Time.timeScale = 0;
  }
  void UnPause()
  {
    Time.timeScale = 1;
    pauseMenu.gameObject.SetActive(false);
    Cursor.visible = false;
  }
}
