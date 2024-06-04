using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private bool disableSleepTimeout;
    private void Awake() {
        if (disableSleepTimeout) {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}
