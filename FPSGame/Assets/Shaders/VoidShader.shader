Shader "Custom/VoidShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Emmission ("Emmission", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        
        Pass
        {
            CGPROGRAM
            #pragma vertex vertexFunction
            #pragma fragment fragmentFunction
            #pragma geometry geometryFunction
            #include "UnityCG.cginc"
       
            ENDCG
        }
        
    }
    FallBack "Diffuse"
}
