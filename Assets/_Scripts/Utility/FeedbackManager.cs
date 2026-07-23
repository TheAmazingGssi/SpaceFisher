using DG.Tweening;
using UnityEngine;
public class FeedbackManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform trans;
    [SerializeField] private ParticleSystem particles;
    [Header("Squash & Stretch")]
    [SerializeField] private float squashAmount = 0.3f;
    [SerializeField] private float squashDuration = 0.15f;
    [SerializeField] private Ease squashEase = Ease.OutQuad;
    [Header("Ease In")]
    [SerializeField] private Transform start;
    [SerializeField] private Transform finish;
    [SerializeField] private float easeInDuration = 0.25f;
    [SerializeField] private Ease easeInEase = Ease.OutBack;
    [Header("Ease Out")]
    [SerializeField] private float easeOutDuration = 0.25f;
    [SerializeField] private Ease easeOutEase = Ease.InBack;
    [Header("Spring")]
    [SerializeField] private float springDuration = 0.4f;
    [SerializeField] private float springElasticity = 0.6f;
    [SerializeField] private float springPeriod = 0.2f;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Sequence sequence;
    private Tween tween;
    private void Awake()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }
    private void OnDestroy()
    {
        sequence?.Kill();
        tween?.Kill();
    }

    [ContextMenu("SquashStretch")]
    public void SquashStretch()
    {
        sequence?.Kill();
        trans.localScale = originalScale;
        sequence = DOTween.Sequence();
        sequence.Append(trans.DOScale(new Vector3(originalScale.x * (1 + squashAmount), originalScale.y * (1 - squashAmount), originalScale.z), squashDuration).SetEase(squashEase));
        sequence.Append(trans.DOScale(new Vector3(originalScale.x * (1 - squashAmount * 0.5f), originalScale.y * (1 + squashAmount * 0.5f), originalScale.z), squashDuration).SetEase(squashEase));
        sequence.Append(trans.DOScale(originalScale, squashDuration).SetEase(Ease.OutElastic));
        sequence.SetLink(gameObject);
    }
    [ContextMenu("EaseIn")]
    public void EaseIn()
    {
        tween?.Kill();
        trans.position = start.position;
        tween = trans.DOMove(finish.position, easeInDuration).SetEase(easeInEase);
        tween.SetLink(gameObject);
    }
    [ContextMenu("EaseOut")]
    public void EaseOut()
    {
        tween?.Kill();
        tween = trans.DOMove(start.position, easeOutDuration).SetEase(easeOutEase);
        tween.SetLink(gameObject);
    }
    [ContextMenu("SpringMovement")]
    public void SpringMovement()
    {
        tween?.Kill();
        trans.position = start.position;
        tween = trans.DOMove(finish.position, springDuration).SetEase(Ease.OutElastic, springElasticity, springPeriod);
        tween.SetLink(gameObject);
    }
    [ContextMenu("PlayParticleEffect")]
    public void PlayParticleEffect()
    {
        if (particles != null)
            particles.Play();
    }
    public void PlayParticleEffect(Vector3 position)
    {
        if (particles == null) return;
        particles.gameObject.transform.position = position;
        particles.Play();
    }
}