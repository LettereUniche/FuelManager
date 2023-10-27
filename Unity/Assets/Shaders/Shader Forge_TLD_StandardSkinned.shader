Shader "Shader Forge/TLD_StandardSkinned" {
	Properties {
		_Color ("Main colour", Vector) = (1,1,1,1)
		[MaterialToggle] _UseVertexColour ("Use Vertex Colour", Float) = 0
		_MainTex ("MainTex", 2D) = "gray" {}
		_DetailOverlay ("DetailOverlay", 2D) = "gray" {}
		_DetailStrength ("DetailStrength", Float) = 0
		_DetailTiling ("DetailTiling", Float) = 1
		_Emissive ("Emissive", 2D) = "black" {}
		_EmissiveStrength ("EmissiveStrength", Range(0, 10)) = 0
		[MaterialToggle] _ColourizeEmissive ("Colourize Emissive", Float) = 1
		_EmissiveTint ("Emissive Tint", Vector) = (1,1,1,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "_LongDark/TLD_StandardShared"
}