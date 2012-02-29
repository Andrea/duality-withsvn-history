<?xml version="1.0" encoding="utf-8"?>
<root>
  <object dataType="Class" type="Duality.Resources.Prefab" id="1">
    <objTree dataType="Class" type="Duality.GameObject" id="2">
      <prefabLink />
      <parent />
      <children />
      <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="3" surrogate="true">
        <customSerialIO />
        <customSerialIO>
          <keys dataType="Array" type="System.Type[]" id="4" length="3">
            <object dataType="Type" id="5" value="Duality.Components.Transform" />
            <object dataType="Type" id="6" value="Duality.Components.Renderers.TextRenderer" />
            <object dataType="Type" id="7" value="GamePlugin.PowerupEffect" />
          </keys>
          <values dataType="Array" type="Duality.Component[]" id="8" length="3">
            <object dataType="Class" type="Duality.Components.Transform" id="9">
              <pos dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">0</Y>
                <Z dataType="Float">0</Z>
              </pos>
              <vel dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">0</Y>
                <Z dataType="Float">0</Z>
              </vel>
              <angle dataType="Float">0</angle>
              <angleVel dataType="Float">0</angleVel>
              <scale dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">1</X>
                <Y dataType="Float">1</Y>
                <Z dataType="Float">1</Z>
              </scale>
              <deriveAngle dataType="Bool">true</deriveAngle>
              <extUpdater />
              <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
              <parentTransform />
              <posAbs dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">0</Y>
                <Z dataType="Float">0</Z>
              </posAbs>
              <velAbs dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">0</Y>
                <Z dataType="Float">0</Z>
              </velAbs>
              <angleAbs dataType="Float">0</angleAbs>
              <angleVelAbs dataType="Float">0</angleVelAbs>
              <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">1</X>
                <Y dataType="Float">1</Y>
                <Z dataType="Float">1</Z>
              </scaleAbs>
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
            <object dataType="Class" type="Duality.Components.Renderers.TextRenderer" id="10">
              <align dataType="Enum" type="Duality.Alignment" name="Center" value="0" />
              <text dataType="Class" type="Duality.FormattedText" id="11">
                <sourceText dataType="String">Powerup!</sourceText>
                <icons />
                <flowAreas />
                <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="12" length="1">
                  <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                    <contentPath dataType="String">Data\Fonts\HUD_Small.Font.res</contentPath>
                  </object>
                </fonts>
                <maxWidth dataType="Int">0</maxWidth>
                <maxHeight dataType="Int">0</maxHeight>
                <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                <displayedText dataType="String">Powerup!</displayedText>
                <fontGlyphCount dataType="Array" type="System.Int32[]" id="13" length="1">
                  <object dataType="Int">8</object>
                </fontGlyphCount>
                <iconCount dataType="Int">0</iconCount>
                <elements dataType="Array" type="Duality.FormattedText+Element[]" id="14" length="1">
                  <object dataType="Class" type="Duality.FormattedText+TextElement" id="15">
                    <text dataType="String">Powerup!</text>
                  </object>
                </elements>
              </text>
              <customMat dataType="Class" type="Duality.Resources.BatchInfo" id="16">
                <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
                  <contentPath dataType="String">Default:DrawTechnique:Add</contentPath>
                </technique>
                <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                  <r dataType="Byte">255</r>
                  <g dataType="Byte">255</g>
                  <b dataType="Byte">255</b>
                  <a dataType="Byte">255</a>
                </mainColor>
                <textures dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.String],[Duality.ContentRef`1[[Duality.Resources.Texture]]]]" id="17" surrogate="true">
                  <customSerialIO />
                  <customSerialIO>
                    <mainTex dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Texture]]">
                      <contentPath />
                    </mainTex>
                  </customSerialIO>
                </textures>
                <uniforms />
              </customMat>
              <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                <r dataType="Byte">255</r>
                <g dataType="Byte">255</g>
                <b dataType="Byte">255</b>
                <a dataType="Byte">255</a>
              </colorTint>
              <iconMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                <contentPath />
              </iconMat>
              <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
              <visibilityGroup dataType="UInt">1</visibilityGroup>
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
            <object dataType="Class" type="GamePlugin.PowerupEffect" id="18">
              <fade dataType="Float">0</fade>
              <baseColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                <r dataType="Byte">0</r>
                <g dataType="Byte">0</g>
                <b dataType="Byte">0</b>
                <a dataType="Byte">0</a>
              </baseColor>
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
          </values>
        </customSerialIO>
      </compMap>
      <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="19">
        <_items dataType="Array" type="Duality.Component[]" id="20" length="4">
          <object dataType="ObjectRef">9</object>
          <object dataType="ObjectRef">10</object>
          <object dataType="ObjectRef">18</object>
          <object />
        </_items>
        <_size dataType="Int">3</_size>
        <_version dataType="Int">3</_version>
      </compList>
      <name dataType="String">PowerupEffect</name>
      <active dataType="Bool">true</active>
      <disposed dataType="Bool">false</disposed>
      <compTransform dataType="ObjectRef">9</compTransform>
      <EventComponentAdded />
      <EventComponentRemoving />
      <EventCollisionBegin />
      <EventCollisionEnd />
      <EventCollisionSolve />
    </objTree>
  </object>
</root>