using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreButtonUIHandler : MonoBehaviour
{
    Button[] scoreButtons;
    Button deleteButton;

    public void Initialize(GameHandler gameHandler) {
        Button[] tmpButtons = GetComponentsInChildren<Button>();

        scoreButtons = tmpButtons.ToList().GetRange(1, tmpButtons.Length - 1).ToArray();
        deleteButton = tmpButtons[0];
        
        deleteButton.onClick.AddListener(() => {
            gameHandler.RemoveLastDigitFromInputScore();
        });

        for (int buttonIndex = 0; buttonIndex < scoreButtons.Length; buttonIndex++) {
            int score;
            if (buttonIndex == scoreButtons.Length - 1) {
                score = 0;
            }
            else {
                score = buttonIndex + 1;
            }

            scoreButtons[buttonIndex].onClick.AddListener(() => {
                gameHandler.AddToInputScore(score);
            });
        }
    }
}
