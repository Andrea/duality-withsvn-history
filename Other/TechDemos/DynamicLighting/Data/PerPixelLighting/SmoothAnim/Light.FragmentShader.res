<?xml version="1.0" encoding="utf-8"?>
<root>
  <object dataType="Class" type="Duality.Resources.FragmentShader" id="1">
    <source dataType="String">#version 120

uniform sampler2D mainTex;
uniform sampler2D normalTex;
uniform sampler2D specularTex;

uniform vec3 _camWorldPos;

uniform int _lightCount;
uniform vec4 _lightPos[8];
uniform vec4 _lightDir[8];
uniform vec3 _lightColor[8];

varying vec3 worldSpacePos;
varying mat2 objTransform;
varying float animBlendVar;

void main()
{
	vec3 eyeDir = normalize(_camWorldPos - worldSpacePos);
  
	vec4 clrDiffuseOld = gl_Color * texture2D(mainTex, gl_TexCoord[0].st);
	vec4 clrNormalOld = texture2D(normalTex, gl_TexCoord[0].st);
	vec4 clrSpecularOld = texture2D(specularTex, gl_TexCoord[0].st);
	vec4 finalColorOld = vec4(0.0, 0.0, 0.0, clrDiffuseOld.a);
	
	vec4 clrDiffuseNew = gl_Color * texture2D(mainTex, gl_TexCoord[0].pq);
	vec4 clrNormalNew = texture2D(normalTex, gl_TexCoord[0].pq);
	vec4 clrSpecularNew = texture2D(specularTex, gl_TexCoord[0].pq);
	vec4 finalColorNew = vec4(0.0, 0.0, 0.0, clrDiffuseNew.a);
	
	vec3 normalOld = normalize(clrNormalOld.xyz - vec3(0.5, 0.5, 0.5));
	vec3 normalNew = normalize(clrNormalNew.xyz - vec3(0.5, 0.5, 0.5));
	normalOld.z = -normalOld.z;
	normalNew.z = -normalNew.z;
	
	vec3 lightDir;
	float attenFactor;
	for (int i = 0; i &lt; _lightCount; i++)
	{
		if (_lightPos[i].w &gt; 0.0)
		{
			// positional light source (pos.w encodes range)
			float dist	= distance(_lightPos[i].xyz, worldSpacePos);
			attenFactor	= 1.0 - min(dist / _lightPos[i].w, 1.0);
			lightDir	= normalize(_lightPos[i].xyz - worldSpacePos);
			
			attenFactor = attenFactor * pow(max(dot(lightDir, -_lightDir[i].xyz), 0.000001), _lightDir[i].w);
		}
		else 
		{
			// directional light source	(pos.xyz encodes an ambient term)
			attenFactor	= 1.0;
			lightDir	= -_lightDir[i].xyz;
			
			finalColorOld.rgb += _lightPos[i].xyz * clrDiffuseOld.rgb;
			finalColorNew.rgb += _lightPos[i].xyz * clrDiffuseNew.rgb;
		} 
		
		// Apply rotation to the light direction to match rotated normal map.
		lightDir.xy = lightDir.xy * objTransform;
		
		// Diffuse lighting
		float diffuseFactorOld = max(dot(normalOld, lightDir), 0.0);
		float diffuseFactorNew = max(dot(normalNew, lightDir), 0.0);
		finalColorOld.rgb += attenFactor * _lightColor[i] * clrDiffuseOld.rgb * diffuseFactorOld;
		finalColorNew.rgb += attenFactor * _lightColor[i] * clrDiffuseNew.rgb * diffuseFactorNew;
		
		// Specular lighting
		float specularFactorOld = pow(max(dot(normalOld, normalize(eyeDir + lightDir)), 0.000001), clrSpecularOld.a * 64.0);
		float specularFactorNew = pow(max(dot(normalNew, normalize(eyeDir + lightDir)), 0.000001), clrSpecularNew.a * 64.0);
		finalColorOld.rgb += _lightColor[i] * clrSpecularOld.rgb * specularFactorOld * diffuseFactorOld * attenFactor;
		finalColorNew.rgb += _lightColor[i] * clrSpecularNew.rgb * specularFactorNew * diffuseFactorNew * attenFactor;
	}
	
	finalColorOld.rgb = max(finalColorOld.rgb, mix(clrDiffuseOld.xyz, finalColorOld.rgb, clrNormalOld.a));
	finalColorNew.rgb = max(finalColorNew.rgb, mix(clrDiffuseNew.xyz, finalColorNew.rgb, clrNormalNew.a));
	
	gl_FragColor = (finalColorNew * animBlendVar + finalColorOld * (1.0 - animBlendVar));;
}</source>
    <sourcePath dataType="String">Source\Media\PerPixelLighting\SmoothAnim\Light.frag</sourcePath>
  </object>
</root>