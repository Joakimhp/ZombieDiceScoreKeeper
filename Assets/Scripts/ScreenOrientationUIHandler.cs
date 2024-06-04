using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOrientationUIHandler : MonoBehaviour
{
    UIHandler[] uiHandlers;
    ScreenOrientation screenOrientation;

    public void Initialize() {
        uiHandlers = GetComponentsInChildren<UIHandler>();
        uiHandlers.Initialize();

        screenOrientation = Screen.orientation;
    }

    public void UpdateUI(int inputScore, int currentPlayerIndex) {
        if (Screen.orientation != screenOrientation) {
            if(Screen.orientation == ScreenOrientation.Portrait) {
                //Activate portrait layout
                uiHandlers[0].gameObject.SetActive(true);
                uiHandlers[1].gameObject.SetActive(false);
            }
            else {
                //Activate Landscape layout
                uiHandlers[0].gameObject.SetActive(false);
                uiHandlers[1].gameObject.SetActive(true);
            }
        }

        if(Screen.orientation == ScreenOrientation.Portrait) {
            uiHandlers[0].UpdateUI(inputScore, currentPlayerIndex);
        }
        else {
            uiHandlers[1].UpdateUI(inputScore, currentPlayerIndex);
        }
    }
}
