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
            <contentPath dataType="String">Default:Texture:DualityLogo256</contentPath>
          </mainTex>
        </customSerialIO>
      </textures>
      <uniforms dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.String],[System.Single[]]]" id="4" surrogate="true">
        <customSerialIO />
        <customSerialIO>
          <camRefDist dataType="Array" type="System.Single[]" id="5" length="1">
            <object dataType="Float">0</object>
          </camRefDist>
          <camWorldPos dataType="Array" type="System.Single[]" id="6" length="3">
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
          </camWorldPos>
          <lightCount dataType="Array" type="System.Single[]" id="7" length="1">
            <object dataType="Float">2</object>
          </lightCount>
          <lightPos dataType="Array" type="System.Single[]" id="8" length="16">
            <object dataType="Float">-500</object>
            <object dataType="Float">-500</object>
            <object dataType="Float">0</object>
            <object dataType="Float">1</object>
            <object dataType="Float">500</object>
            <object dataType="Float">0</object>
            <object dataType="Float">500</object>
            <object dataType="Float">1</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
          </lightPos>
          <lightRange dataType="Array" type="System.Single[]" id="9" length="4">
            <object dataType="Float">1500</object>
            <object dataType="Float">1500</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
          </lightRange>
          <lightColor dataType="Array" type="System.Single[]" id="10" length="12">
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">1</object>
            <object dataType="Float">1</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
            <object dataType="Float">0</object>
          </lightColor>
        </customSerialIO>
      </uniforms>
      <dirtyFlag dataType="Enum" type="Duality.Resources.BatchInfo+DirtyFlag" name="None" value="0" />
    </info>
  </object>
</root>