using System.Collections;

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RippleController : MonoBehaviour
{
    [SerializeField] private float maxAmount = 25f;
    [SerializeField] private float friction = .95f;

    private Coroutine rippleRoutine;
    private Ripple ripple;
    private PostProcessVolume rippleVolume;

    private void Start()
    {
        ripple = ScriptableObject.CreateInstance<Ripple>();
        ripple.enabled.Override(false);
        ripple.Amount.Override(0f);
        ripple.WaveAmount.Override(10f);
        ripple.WaveSpeed.Override(15f);
        rippleVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, ripple);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        RuntimeUtilities.DestroyVolume(rippleVolume, true, true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position = Input.mousePosition;

            if(rippleRoutine != null)
                StopCoroutine(rippleRoutine);

            ripple.CenterX.Override(position.x / Screen.width);
            ripple.CenterY.Override(position.y / Screen.height);

            rippleRoutine = StartCoroutine(DoRipple());
        }
    }

    private IEnumerator DoRipple()
    {
        ripple.enabled.Override(true);

        float amount = maxAmount;
        while(amount > .5f)
        {
            ripple.Amount.value = amount;
            amount *= friction;
            yield return null;
        }

        ripple.enabled.Override(false);
    }
}
