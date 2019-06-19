Shader "Unlit/UVAnimation"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

	}
		SubShader
		{
			Tags { "RenderType" = "Transparent" }

			Pass
			{
			Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag


				#include "UnityCG.cginc"  

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 worldPosition : TEXCOORD1;

			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.worldPosition = v.vertex;

				o.vertex = UnityObjectToClipPos(v.vertex);
				//o.vertex = (v.vertex);
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//o.uv.x = o.uv.x-(int)o.uv.x;
				//o.uv.y = o.uv.y-(int)o.uv.y;
				
				
				
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//fixed4 col = tex2D(_MainTex, i.uv);
				i.uv = TRANSFORM_TEX(i.uv, _MainTex);
				i.uv.x -= (int)i.uv.x;
				i.uv.y -= (int)i.uv.y;

				fixed4 col = tex2D(_MainTex, i.uv);
			
				return col;
			}
			ENDCG
		}
	}
}
