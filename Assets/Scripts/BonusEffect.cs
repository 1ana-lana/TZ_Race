using System;
using System.Collections;
using UnityEngine;

public class BonusEffect : MonoBehaviour
{
    [SerializeField]
    protected float _activeTime = 15f;
    [SerializeField]
    protected SpriteRenderer _spriteRenderer;

    protected bool isBonusActive = false;
    protected Coroutine _lifeTimeRoutine = null;

    public bool IsBonusActive { get { return isBonusActive; } }

    public virtual void ActivateBonus(Action onAction, Action offAction)
    {
        if (isBonusActive)
        {
            StopCoroutine(_lifeTimeRoutine);
        }
        isBonusActive = true;
        _spriteRenderer.enabled = true;
        onAction?.Invoke();
        _lifeTimeRoutine = StartCoroutine(DeactivateBonus(offAction));
    }

    protected virtual IEnumerator DeactivateBonus(Action offAction)
    {
        yield return new WaitForSeconds(_activeTime);
        isBonusActive = false;
        _spriteRenderer.enabled = false;
        offAction?.Invoke();
    }
}
