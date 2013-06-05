<?xml version="1.0" encoding="utf-8"?>
<root>
  <object dataType="Class" type="Duality.Resources.VertexShader" id="1">
    <source dataType="String">attribute vec4 lightAttrib;
attribute float animBlend;

varying vec3 lightIntensity;
varying float animBlendVar;

void main()
{
	gl_Position = ftransform();
	gl_TexCoord[0] = gl_MultiTexCoord0;
	gl_FrontColor = gl_Color;
	lightIntensity = lightAttrib;
	animBlendVar = animBlend;
}</source>
    <sourcePath dataType="String">Source\Media\PerVertexLighting\SmoothAnim\VertexLight.vert</sourcePath>
  </object>
</root>