Shader "Cg shader with all built-in vertex input parameters" { 
   SubShader { 
      Pass { 
         CGPROGRAM 
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         
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
		    	
		    	if ( dist <= 9 )
		    		return float4(count, count/2, count, 1);
		    	else
		    		return float4(count,0,count*dist/9,1);
		    }
		}
		
		float4 frag( float4 fragCoord: WPOS ): COLOR
		{
			// Plane's resolution
			float2 res = float2(800,600);
			
			// Center the complex plane at initialization time
		    float2 c = float2(fragCoord/res); c.x += -1.5; c.y += -0.75;
		   	
		   	// Initializing the series
		   	float2 z = float2(0,0);
		   	
		   	// Moving the camera
		   	c *= 8;// - 8*cos(_Time.y);
		   	
		    // Check if the fragment pertain to the Mandelbrot set
		    int count = 0;
		    const int ITER = 256;
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