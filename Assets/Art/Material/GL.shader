Shader "Custom/GL" {
	
	SubShader {
		pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off Cull Off Fog { Mode Off }
			
		}
	}
	FallBack "Diffuse"
}
