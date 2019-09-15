Shader "Hidden/OnebyoneDesign/PostFX/Ripple"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

        half _CenterX;
        half _CenterY;
        half _Amount;
        half _WaveSpeed;
        half _WaveAmount;

        half4 Frag(VaryingsDefault i) : SV_Target
        {
            half2 center = half2(_CenterX, _CenterY);
            half time = _Time.y *  _WaveSpeed;
            half amt = _Amount/_ScreenParams.x;
 
            half2 uv = center.xy - i.texcoord;
            uv.x *= _ScreenParams.x / _ScreenParams.y;
 
            half dist = sqrt(dot(uv,uv));
            half ang = dist * _WaveAmount - time;
            uv = i.texcoord + normalize(uv) * sin(ang) * amt;
 
            return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}