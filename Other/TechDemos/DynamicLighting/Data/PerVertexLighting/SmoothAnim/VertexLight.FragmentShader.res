<?xml version="1.0" encoding="utf-8"?>
<root>
  <object dataType="Class" type="Duality.Resources.FragmentShader" id="1">
    <source dataType="String">uniform sampler2D mainTex;

varying vec3 lightIntensity;
varying float animBlendVar;

void main()
{
	vec4 texClrOld = texture2D(mainTex, gl_TexCoord[0].st);
	vec4 texClrNew = texture2D(mainTex, gl_TexCoord[0].pq);
	vec4 color = gl_Color * (texClrNew * animBlendVar + texClrOld * (1.0 - animBlendVar));
	color.rgb *= lightIntensity;
	gl_FragColor = color;
}</source>
    <sourcePath dataType="String">Source\Media\PerVertexLighting\SmoothAnim\VertexLight.frag</sourcePath>
  </object>
</root>