Shader "Custom/Post Outline"
{
    Properties
    {
        //Graphics.Blit() sets the "_MainTex" property to the source texture
        _MainTex("Main Texture",2D)="black"{}
    }
    SubShader 
    {
        Pass 
        {
            CGPROGRAM
            sampler2D _MainTex;
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
             
            struct v2f 
            {
                float4 pos : SV_POSITION;
                float2 uvs : TEXCOORD0;
            };
             
            v2f vert (appdata_base v) 
            {
                v2f o;
                 
                //Despite only drawing a quad to the screen from -1,-1 to 1,1, Unity altered our verts, and requires us to use UnityObjectToClipPos.
                o.pos = UnityObjectToClipPos(v.vertex);
                 
                //Also, the UVs show up in the top right corner for some reason, let's fix that.
                o.uvs = o.pos.xy / 2 + 0.5;
                 
                return o;
            }
             
             
            half4 frag(v2f i) : COLOR 
            {
                //return the texture we just looked up
                return tex2D(_MainTex,i.uvs.xy);
            }
             
            ENDCG
 
        }
        //end pass        
    }
    //end subshader
}
//end shader