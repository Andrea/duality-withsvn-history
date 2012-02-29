<?xml version="1.0" encoding="utf-8"?>
<root>
  <object dataType="Class" type="Duality.Resources.Material" id="1">
    <info dataType="Class" type="Duality.Resources.BatchInfo" id="2">
      <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
        <contentPath dataType="String">Data\PerPixelLighting\Mask.LightingTechnique.res</contentPath>
      </technique>
      <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
        <r dataType="Byte">255</r>
        <g dataType="Byte">255</g>
        <b dataType="Byte">255</b>
        <a dataType="Byte">255</a>
      </mainColor>
      <textures dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.String],[Duality.ContentRef`1[[Duality.Resources.Texture]]]]" id="3" surrogate="true">
        <customSerialIO />
        <customSerialIO>
          <mainTex dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Texture]]">
            <contentPath dataType="String">Data\ship3_c.Texture.res</contentPath>
          </mainTex>
          <normalTex dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Texture]]">
            <contentPath dataType="String">Data\ship3_n.Texture.res</contentPath>
          </normalTex>
          <specularTex dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Texture]]">
            <contentPath dataType="String">Data\ship3_s.Texture.res</contentPath>
          </specularTex>
        </customSerialIO>
      </textures>
      <uniforms dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.String],[System.Single[]]]" id="4" surrogate="true">
        <customSerialIO />
        <customSerialIO>
          <_lightColor dataType="Array" type="System.Single[]" id="5" length="24">
            <object dataType="Float">1</object>
            <object dataType="Float">1</object>
            <object dataType="Float">1</object>
            <object dataType="Float">1</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">1</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">1</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
          </_lightColor>
          <_lightPos dataType="Array" type="System.Single[]" id="6" length="32">
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">1000</object>
            <object dataType="Float">500</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">1000</object>
            <object dataType="Float">0</object>
            <object dataType="Float">500</object>
            <object dataType="Float">0</object>
            <object dataType="Float">1000</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">500</object>
            <object dataType="Float">1000</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
          </_lightPos>
          <_lightCount dataType="Array" type="System.Single[]" id="7" length="1">
            <object dataType="Float">1</object>
          </_lightCount>
          <_camWorldPos dataType="Array" type="System.Single[]" id="8" length="3">
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
          </_camWorldPos>
          <_camRefDist dataType="Array" type="System.Single[]" id="9" length="1">
            <object dataType="Float">0</object>
          </_camRefDist>
          <_lightDir dataType="Array" type="System.Single[]" id="10" length="32">
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
          </_lightDir>
        </customSerialIO>
      </uniforms>
      <dirtyFlag dataType="Enum" type="Duality.Resources.BatchInfo+DirtyFlag" name="None" value="0" />
    </info>
  </object>
</root>