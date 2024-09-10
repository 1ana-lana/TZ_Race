using System.Collections;
using TMPro;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _startText;

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
            _startText.text = i.ToString();
        }
        gameObject.SetActive(false);
    }
}
