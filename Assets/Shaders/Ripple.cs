using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable, PostProcess(typeof(RippleRenderer), PostProcessEvent.AfterStack, "OnebyoneDesign/Ripple")]
public sealed class Ripple : PostProcessEffectSettings
{
    [Range(0,1), Tooltip("Horizontal center of effect")]
    public FloatParameter CenterX = new FloatParameter { value = 0.5f };

    [Range(0,1), Tooltip("Vertical center of effect")]
    public FloatParameter CenterY = new FloatParameter { value = 0.5f };

    [Tooltip("Amount/Strength of effect")]
    public FloatParameter Amount = new FloatParameter { value = 10f };

    [Tooltip("Speed of ripple waves")]
    public FloatParameter WaveSpeed = new FloatParameter { value = 10f };

    [Range(0, 50), Tooltip("Amount of waves")]
    public FloatParameter WaveAmount = new FloatParameter { value = 20f };
}

public sealed class RippleRenderer : PostProcessEffectRenderer<Ripple>
{
    public override void Render(PostProcessRenderContext context)
    {
        PropertySheet sheet = context.propertySheets.Get(Shader.Find("Hidden/OnebyoneDesign/PostFX/Ripple"));

        sheet.properties.SetFloat("_CenterX", settings.CenterX);
        sheet.properties.SetFloat("_CenterY", settings.CenterY);
        sheet.properties.SetFloat("_Amount", settings.Amount);
        sheet.properties.SetFloat("_WaveSpeed", settings.WaveSpeed);
        sheet.properties.SetFloat("_WaveAmount", settings.WaveAmount);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}