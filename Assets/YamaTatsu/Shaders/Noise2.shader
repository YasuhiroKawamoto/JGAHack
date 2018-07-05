﻿Shader "Custom/Noise2" 
{
	Properties 
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}

	CGINCLUDE

	#include "UnityCG.cginc"

	sampler2D _MainTex;

	//0~1
	float _HorizonValue = 1;
	//乱数シード
	int _Seed;

	// 乱数生成
	// http://neareal.net/index.php?ComputerGraphics%2FHLSL%2FCommon%2FGenerateRandomNoiseInPixelShader
	float rnd(float2 value, int Seed)
	{
		//x の小数部 (10 進数値) を返します
		return frac((sin(dot(value.xy, float2(12.9898, 78.233)) + Seed)*43758.5453));
	}

	fixed4 frag(v2f_img i) :SV_Target
	{
		float rndValue = rnd(i.uv,_Seed);
	//-1 or 1 左右どちらかにずれる
	int tmp = step(rndValue, 0.5) * 2 - 1;
	//ピクセルジャンプ地
	float rndU = _HorizonValue * tmp * rndValue;
	float uv = float2(frac(i.uv.x + rndU), i.uv.y);
	fixed4 col = tex2D(_MainTex, uv);
	return col;
	}


		ENDCG

		SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			ENDCG
		}
	}
}
