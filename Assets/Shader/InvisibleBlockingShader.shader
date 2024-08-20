Shader "Custom/InvisibleBlockingShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue" = "Geometry-1" }
        Pass
        {
            // Don't draw in the color buffer
            ColorMask 0

            // Draw into the stencil buffer
            Stencil
            {
                Ref 1
                Comp always
                Pass replace
            }

            // This will ensure the object doesn't render but affects the stencil buffer
            ZWrite On
        }
    }
    FallBack "Diffuse"
}