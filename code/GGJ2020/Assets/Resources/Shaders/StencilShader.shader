Shader "Unlit/StencilShader"
{
    Properties
    {
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Stencil 
        {
            Ref 1
            Comp Never
            CompFront Never
            CompBack Never          
            Fail IncrSat
            FailBack IncrSat
        }

        Pass
        {
        }
    }
}
