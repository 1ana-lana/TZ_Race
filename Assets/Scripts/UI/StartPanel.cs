using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text startText;

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        int i = 3;
        while (i > 0)
        {
            yield return new WaitForSeconds(1f);
            i--;
            startText.text = i.ToString();
        }
        gameObject.SetActive(false);
    }
}
