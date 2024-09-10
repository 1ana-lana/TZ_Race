using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillImageController : MonoBehaviour
{
    [SerializeField]
    private Image _filledImage;
    [SerializeField]
    private float _fillDuration; 

    public void SetNewState(float currentPoints)
    {
        StartCoroutine(AnimateFill(_filledImage.fillAmount, currentPoints));
    }

    private IEnumerator AnimateFill(float startFill, float targetFill)
    {
        float timeElapsed = 0f;

        while (timeElapsed < _fillDuration)
        {
            _filledImage.fillAmount = Mathf.Lerp(startFill, targetFill, timeElapsed / _fillDuration);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        _filledImage.fillAmount = targetFill;
    }
}
