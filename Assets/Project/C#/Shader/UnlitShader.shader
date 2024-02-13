Shader "Costume/UnlitShader"
{

    SubShader
    {

        ZWrite off
        ColorMask 0 //Transparent Mask

        Stencil{
            Ref 1
            Pass replace
        }

        Pass
        {

        }
    }
}
