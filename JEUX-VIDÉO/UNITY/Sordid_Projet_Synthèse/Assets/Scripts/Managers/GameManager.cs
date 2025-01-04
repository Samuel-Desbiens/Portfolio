using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
  private PlayerInputActions inputs;

  private void Awake()
  {
    inputs = new PlayerInputActions();
  }

  public PlayerInputActions GetInputs()
  {
    return inputs;
  }
}
