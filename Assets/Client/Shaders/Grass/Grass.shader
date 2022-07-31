Shader "Ground/Grass"
{
	Properties
	{
		         
		_BaseColor("Base Color", Color) = (1,1,1,1)
		_TipColor("Tip Color", Color) = (1,1,1,1)
		_BladesTexture("Blades Texture", 2D) = "white" {}

		_BladeWidthMin("Blades width min", float) = 0.01
		_BladeWidthMax("Blades width max", float) = 0.01
		_BladeHeightMin("Blades height min", float) = 0.1
		_BladeHeightMax("Blades height max", float) = 0.2

		_BladeSegments("Blade segments", Range(1, 10)) = 3
		_BladeBendDistance("Blades bend distance", float) = 0.38
		_BladeBendCurve("Blades bend curve", Range(1,4)) = 2

		_BendDelta("Bend variations", Range(0,1)) = 0.2

		_TessellationUniform("Tessellation Uniform", Range(1, 64)) = 1

		_GrassMap("Grass map",2D) = "white"{}
		_GrassTreshold("Grass trashold",Range(-0.1,1)) = 0.5
		_GrassFalloff("Grass Fade-In falloff",Range(0,0.5)) = 0.05

		_WindMap("Wind map", 2D) = "bump"{}
		_WindVelocity("Wind velocity",vector) = (1,0,0,0)
		_WindFrequency("Wind pulse frequency",Range(0,1)) = 0.01
	}

	HLSLINCLUDE
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
		#include "Assets/Client/Shaders/Tessellation/CustomTessellation.hlsl"
		#define BLADE_SEGMENTS 3
		#define UNITY_PI 3.14159265359f
		#define UNITY_TWO_PI 6.28318530718f

	CBUFFER_START(UnityPerMaterial)

		float4 _BaseColor;
		float4 _TipColor;
		sampler2D _BladeTexture;

		float _BladeWidthMin;
		float _BladeWidthMax;
		float _BladeHeightMin;
		float _BladeHeightMax;

		float _BladeBendDistance;
		float _BladeBendCurve;
		float _BendDelta;

		sampler2D _GrassMap;
		float4 _GrassMap_ST;
		float _GrassTreshold;
		float _GrassFalloff;

		sampler2D _WindMap;
		float4 _WindMap_ST;
		float4 _WindVelocity;
		float _WindFrequency;

		float _ShadowColor;

	CBUFFER_END
	

	struct GeomData
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		float3 worldPos : TEXCOORD1;
	};


	VertexOutput geomVert(VertexInput v)
	{
		VertexOutput o;
		o.vertex = float4(TransformObjectToWorld(v.vertex),0.0f);
		o.normal = TransformObjectToWorldNormal(v.normal);
		o.tangent = v.tangent;
		o.uv = TRANSFORM_TEX(v.uv, _GrassMap);

		return o;
	}

	GeomData TransformGeomToClip(float3 pos, float3 offset, float3x3 transformationMatrix, float2 uv)
	{
		GeomData d;
		d.pos = TransformObjectToHClip(pos + mul(transformationMatrix, offset));
		d.uv = uv;
		d.worldPos = TransformObjectToWorld(pos + mul(transformationMatrix, offset));

		return d;
	}
	// Returns a number in the 0...1 range.
	float rand(float3 co)
	{
		return frac(sin(dot(co.zzx, float3(12.9898, 78.233, 53.539))) * 43758.5453);
	}

	float3x3 AngleAxis3x3(float angle, float3 axis)
	{
		float c, s;
		sincos(angle, s, c);

		float t = 1 - c;
		float x = axis.x;
		float y = axis.y;
		float z = axis.z;

		return float3x3(
			t * x * x + c, t * x * y - s * z, t * x * z + s * y,
			t * x * y + s * z, t * y * y + c, t * y * z - s * x,
			t * x * z - s * y, t * y * z + s * x, t * z * z + c
			);
	}
	[maxvertexcount(BLADE_SEGMENTS * 2 + 1)]
	void geom(point VertexOutput input[1], inout TriangleStream<GeomData> triStream)
	{
		float grassVisibility = tex2Dlod(_GrassMap, float4(input[0].uv, 0, 0)).r;

		if (grassVisibility >= _GrassTreshold)
		{
			float3 pos = input[0].vertex.xyz;
			float3 normal = input[0].normal;
			float4 tangent = input[0].tangent;
			
			float falloff = smoothstep(_GrassTreshold, _GrassTreshold + _GrassFalloff, grassVisibility);

			float width = lerp(_BladeWidthMin, _BladeWidthMax, rand(pos.xzy) * falloff);
			float height = lerp(_BladeHeightMin, _BladeHeightMax, rand(pos.zyx) * falloff);
			float forward = rand(pos.yyz) * _BladeBendDistance;

			float3 bitangent = cross(normal, tangent.xyz) * tangent.w;

			float3x3 tangentToLocal = float3x3
			(
				tangent.x, bitangent.x, normal.x,
				tangent.y, bitangent.y, normal.y,
				tangent.z, bitangent.z, normal.z
			);

			//Rotate around the y-axis a random amount.
			float3x3 randRotMatrix = AngleAxis3x3(rand(pos) * UNITY_TWO_PI, float3(0, 0, 1.0f));
			//Rotate around the bottom of the blade a random amount.
			float3x3 randBendMatrix = AngleAxis3x3((rand(pos.xyz) - 0.5f) * _BendDelta * UNITY_PI, float3(-1.0f, 0, 0));


			float2 windUV = pos.xz * _WindMap_ST.xy + _WindMap_ST.zw + normalize(_WindVelocity.xzy) * _WindFrequency * _Time.y;
			float2 windSample = (tex2Dlod(_WindMap, float4(windUV, 0, 0)).xy * 2 - 1) * length(_WindVelocity);

			float3 windAxis = normalize(float3(windSample.x, windSample.y, 0));
			float3x3 windMatrix = AngleAxis3x3(UNITY_PI * windSample, windAxis);

			//Transform the grass blades to the  correct tangent space.
			float3x3 baseTransformationMatrix = mul(tangentToLocal, randRotMatrix);
			float3x3 tipTransformationMatrix = mul(mul(mul(tangentToLocal, windMatrix), randBendMatrix), randRotMatrix);


			

			for (int i = 0; i < BLADE_SEGMENTS; ++i)
			{
				float t = i / (float)BLADE_SEGMENTS;
				float3 offset = float3(width * (1 - t), pow(t, _BladeBendCurve) * forward, height * t);

				float3x3 transformationMatrix = (i == 0) ? baseTransformationMatrix : tipTransformationMatrix;

				triStream.Append(TransformGeomToClip(pos, float3(offset.x, offset.y, offset.z), transformationMatrix, float2(0, t)));
				triStream.Append(TransformGeomToClip(pos, float3(-offset.x, offset.y, offset.z), transformationMatrix, float2(1, t)));
			}

			triStream.Append(TransformGeomToClip(pos, float3(0, forward, height), tipTransformationMatrix, float2(0.5f, 1.0f)));

		}
		
	}

	ENDHLSL
		Subshader
	{
		Tags
		{
				"RenderType" = "Opaque"
				"Queue" = "Geometry"
				"RenderPipeline" = "UniversalPipeline"
		}
		LOD 100
		Cull off

		Pass
		{
			Name "GrassShade"
			Tags{"LightMode" = "UniversalForward"}

			HLSLPROGRAM
			#pragma vertex geomVert;
			#pragma fragment frag;
			#pragma require geometry
			#pragma geometry geom

			
			
			float4 frag(GeomData d) : SV_Target
			{
				float4 color = tex2D(_BladeTexture, d.uv);

				return color * lerp(_BaseColor, _TipColor, d.uv.y);
			}
			ENDHLSL
		}
    }
}