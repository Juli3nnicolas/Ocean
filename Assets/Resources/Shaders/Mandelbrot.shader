Shader "Cg shader with all built-in vertex input parameters" {
	Properties
	{
		_ColorRatio("Color tint modifier ratio", Vector) = (1,1,1,1)
	}
	SubShader
	{
		Pass
		{

		CGPROGRAM

		#pragma vertex vert  
		#pragma fragment frag 


		uniform float3 _ColorRatio;

        struct vertexOutput {
         	float4 pos: POSITION;
         };
 
         vertexOutput vert(float4 pos: POSITION) 
         {
            vertexOutput output;
            output.pos =  mul(UNITY_MATRIX_MVP, pos);

            return output;
         }
         
         ///////////////////////////////////////////////////////////
        
        float2 squareCpx(float2 a)
        {
        	return float2(a.x*a.x - a.y*a.y, 2*a.x*a.y);
        }
        
        float cpxSquareMod(float2 z)
        {
        	return dot(z,z);
        }

		float4 setFragColor(float dist, float ITER, float count)
		{
			// Set the fragments' color
		    if ( dist <= 4 )
		    	return float4(0,0,0,1);	// The fragment is part of the set
		    else
		    {
		    	count /= ITER/8;
		    	
				if (dist <= 9)
				{
					float4 color = float4(count, count / 2, count, 1);
					color.rgb *= _ColorRatio;
					return color;
				}
				else
				{
					float4 color = float4(count, 0, count*dist / 9, 1);
					color.rgb *= _ColorRatio;
					return color;
				}
		    }
		}

		float2 collapsePixels(float2 px, float2 target)
		{
			// The following value is an interesting target
			//float2 target = float2(-3.0/4.0, 0.25);
			
			px += abs(sin(0.5*_Time.y)) * normalize(-target + px);

			return px;
		}

		float4 frag( float4 fragCoord: WPOS ): COLOR
		{
			// Frame settings
			//float2 target = float2(-3.0 / 4.0, 0.25);
			//float2 target = float2(-0.04524074130409, 0.9868162207157852);
			float2 target = float2(0.281717921930775, 0.5771052841488505);
			float h       = 0.0000000000000001;
			float xl      = target.x-h, xr = target.x+h, yb = target.y-h, yt = target.y+h;
			float y_intercept = 2000;
			float zoom = y_intercept + 400*pow(_Time.y,2);
			
			// Convert pixels from pixel coordinates to complex coordinates
			float2 c = fragCoord / zoom;
			c.x += xl; c.y += yb;

		   	// Initializing the Mandelbrot series
		   	float2 z = float2(0,0);
		   	
		    // Check if the fragment pertains to the Mandelbrot set
		    int count = 0;
			const int ITER = 4196; // The greater it is, the more accurate the result becomes
		    while ( cpxSquareMod(z) <= 4 && count < ITER ) // While |z| <= 2, the pixel is assumed to be part of the Mandelbrot set
		    {
		    	z = squareCpx(z) + c;
		    	count++;
		    }
		   
		    return setFragColor( cpxSquareMod(z), ITER, count ); 
		}

         ENDCG  
      }
   }
}