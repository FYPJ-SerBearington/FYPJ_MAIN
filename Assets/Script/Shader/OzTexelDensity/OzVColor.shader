Shader "Oz/OzVColor" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float4 color: COLOR;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = IN.color.rgb;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
