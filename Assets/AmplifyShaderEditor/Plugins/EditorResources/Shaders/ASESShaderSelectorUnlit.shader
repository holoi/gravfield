Shader "Hidden/ASESShaderSelectorUnlit"
{
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct MeshData
			{
				float4 vertex : POSITION;
			};

			struct V2FData
			{
				float4 vertex : SV_POSITION;
			};
			
			uniform fixed4 _Color;

			V2FData vert (MeshData v)
			{
				V2FData o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (V2FData i) : SV_Target
			{
				return _Color;
			}
			ENDCG
		}
	}
}
