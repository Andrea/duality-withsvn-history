<?xml version="1.0" encoding="utf-8"?>
<root>
  <object dataType="Class" type="Duality.Resources.FragmentShader" id="1">
    <source dataType="String">uniform sampler2D mainTex;

void main()
{
	vec4 clr = texture2D(mainTex, gl_TexCoord[0].st) * gl_Color;
	
	/* Kind-of-Tonemapping: Fade to white when going beyond visible hi-color. */
	float val = max(0.0, max(max(clr.r * 1.125, clr.g), clr.b * 1.35) - 1.0);
	clr.rgb = mix(clr.rgb, vec3(1.0, 1.0, 1.0), val / (1.0 + val));
	
	gl_FragColor = clr;
}</source>
    <sourcePath dataType="String">Source\Media\Tonemapping.frag</sourcePath>
  </object>
</root>