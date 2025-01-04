using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public void EndGameBroadcast()
    {
        BroadcastMessage("Deactivate");
    }
}
