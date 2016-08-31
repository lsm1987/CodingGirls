Shader "Lines/HitArea" {
	SubShader {
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			ZWrite Off
			ZTest Less
			Fog { Mode Off }
			BindChannels {
				Bind "Vertex", vertex
				Bind "Color", color
			}
		}
	}
}