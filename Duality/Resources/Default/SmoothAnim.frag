uniform sampler2D mainTex;
varying float animBlendVar;

void main()
{
	vec4 texClrOld = texture2D(mainTex, gl_TexCoord[0].st);
	vec4 texClrNew = texture2D(mainTex, gl_TexCoord[0].pq);
	gl_FragColor = gl_Color * (texClrNew * animBlendVar + texClrOld * (1.0 - animBlendVar));
}