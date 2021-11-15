using UnityEngine;
using DG.Tweening;

public class FadeWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _restartWindow;
    private Tween fadeTween;
    
    public void FadeIn(float duration)
    {
        _restartWindow.gameObject.SetActive(true);

        Fade(1f, duration, () =>
        {
            _restartWindow.interactable = true;
            _restartWindow.blocksRaycasts = true;
        });
    }

    public void FadeOut(float duration)
    {
        Fade(0f, duration, () =>
        {
            _restartWindow.interactable = false;
            _restartWindow.blocksRaycasts = false;
            _restartWindow.gameObject.SetActive(false);
        });
    }

    public void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = _restartWindow.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }
}
