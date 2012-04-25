uniform extern float4x4 WVPMatrix;
uniform extern texture SpriteTexture;

struct VS_INPUT
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
	float Size : PSIZE;
	float Rotation : COLOR1;
};

struct VS_OUTPUT
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
	float Size : PSIZE;
	float Rotation : COLOR1;
};

struct PS_INPUT
{
	float4 Color : COLOR0;
	float Rotation : COLOR1;

    #ifdef XBOX
        float2 TexCoord : SPRITETEXCOORD;
    #else
        float2 TexCoord : TEXCOORD0;
    #endif       
};

sampler Sampler = sampler_state
{
    Texture = <SpriteTexture>;
    
    MinFilter = LINEAR;
    MagFilter = LINEAR;
    MipFilter = POINT;
    
    AddressU = CLAMP;
    AddressV = CLAMP;
};                        

VS_OUTPUT VertexShader(VS_INPUT input)
{
	VS_OUTPUT output = (VS_OUTPUT)0;
	
	output.Position = mul(input.Position, WVPMatrix);
	output.Color = input.Color;
	output.Size = input.Size;
	output.Rotation = input.Rotation;

	return output;
}

float4 PixelShader_2_0(PS_INPUT input) : COLOR0
{
	float2 texCoord;
	
	float2 cCoord = input.TexCoord;
	
	cCoord += 0.5f;
	
	float ca = cos(input.Rotation);
	float sa = sin(input.Rotation);
	
	texCoord.x = cCoord.x * ca - cCoord.y * sa;
	texCoord.y = cCoord.x * sa + cCoord.y * ca;
	
	texCoord -= 0.5f;

    return tex2D(Sampler, texCoord) * input.Color;
}

float4 PixelShader_1_1(PS_INPUT input) : COLOR0
{
	return tex2D(Sampler, input.TexCoord) * input.Color;
}

technique PointSprite_2_0
{
    pass P0
    {
        vertexShader = compile vs_2_0 VertexShader();
        pixelShader = compile ps_2_0 PixelShader_2_0();
    }
}

technique PointSprite_1_1
{
	pass P0
	{
		vertexShader = compile vs_1_1 VertexShader();
		pixelShader = compile ps_1_1 PixelShader_1_1();
	}
}