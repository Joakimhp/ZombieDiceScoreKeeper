using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerUIHandler : MonoBehaviour
{
    private float delay = 2f;
    TextMeshProUGUI winnerNameText;

    public void Initialize() {
        winnerNameText = GetComponentInChildren<TextMeshProUGUI>();

        gameObject.SetActive(false);
    }

    public void ShowWinner(string winnerName) {
        winnerNameText.text = winnerName;
        gameObject.SetActive(true);
        StartCoroutine(CloseWindowAfterDelay(winnerName));
    }
    
    private IEnumerator CloseWindowAfterDelay(string winnerName) {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
