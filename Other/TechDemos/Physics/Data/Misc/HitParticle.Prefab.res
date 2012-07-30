<?xml version="1.0" encoding="utf-8"?>
<root>
  <object dataType="Class" type="Duality.Resources.Prefab" id="1">
    <objTree dataType="Class" type="Duality.GameObject" id="2">
      <name dataType="String">HitParticle</name>
      <prefabLink />
      <parent />
      <children />
      <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="3" surrogate="true">
        <customSerialIO />
        <customSerialIO>
          <keys dataType="Array" type="System.Type[]" id="4" length="3">
            <object dataType="Type" id="5" value="Duality.Components.Transform" />
            <object dataType="Type" id="6" value="Duality.Components.Renderers.SpriteRenderer" />
            <object dataType="Type" id="7" value="PhysicsTestbed.FadeoutEffect" />
          </keys>
          <values dataType="Array" type="Duality.Component[]" id="8" length="3">
            <object dataType="Class" type="Duality.Components.Transform" id="9">
              <pos dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">0</Y>
                <Z dataType="Float">0</Z>
              </pos>
              <angle dataType="Float">0</angle>
              <scale dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">1</X>
                <Y dataType="Float">1</Y>
                <Z dataType="Float">1</Z>
              </scale>
              <deriveAngle dataType="Bool">true</deriveAngle>
              <ignoreParent dataType="Bool">false</ignoreParent>
              <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
              <parentTransform />
              <posAbs dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">0</Y>
                <Z dataType="Float">0</Z>
              </posAbs>
              <angleAbs dataType="Float">0</angleAbs>
              <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">1</X>
                <Y dataType="Float">1</Y>
                <Z dataType="Float">1</Z>
              </scaleAbs>
              <vel dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">0</Y>
                <Z dataType="Float">0</Z>
              </vel>
              <velAbs dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">0</Y>
                <Z dataType="Float">0</Z>
              </velAbs>
              <angleVel dataType="Float">0</angleVel>
              <angleVelAbs dataType="Float">0</angleVelAbs>
              <lastPos dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">0</Y>
                <Z dataType="Float">0</Z>
              </lastPos>
              <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">0</Y>
                <Z dataType="Float">0</Z>
              </lastPosAbs>
              <lastAngle dataType="Float">0</lastAngle>
              <lastAngleAbs dataType="Float">0</lastAngleAbs>
              <OnTransformChanged />
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
            <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="10">
              <rect dataType="Struct" type="Duality.Rect">
                <X dataType="Float">-1</X>
                <Y dataType="Float">-1</Y>
                <W dataType="Float">2</W>
                <H dataType="Float">2</H>
              </rect>
              <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                <contentPath />
              </sharedMat>
              <customMat dataType="Class" type="Duality.Resources.BatchInfo" id="11">
                <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
                  <contentPath dataType="String">Default:DrawTechnique:Add</contentPath>
                </technique>
                <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                  <R dataType="Byte">255</R>
                  <G dataType="Byte">255</G>
                  <B dataType="Byte">255</B>
                  <A dataType="Byte">255</A>
                </mainColor>
                <textures dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.String],[Duality.ContentRef`1[[Duality.Resources.Texture]]]]" id="12" surrogate="true">
                  <customSerialIO />
                  <customSerialIO>
                    <mainTex dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Texture]]">
                      <contentPath dataType="String">Default:Texture:White</contentPath>
                    </mainTex>
                  </customSerialIO>
                </textures>
                <uniforms />
                <dirtyFlag dataType="Enum" type="Duality.Resources.BatchInfo+DirtyFlag" name="All" value="3" />
              </customMat>
              <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                <R dataType="Byte">255</R>
                <G dataType="Byte">255</G>
                <B dataType="Byte">255</B>
                <A dataType="Byte">255</A>
              </colorTint>
              <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
              <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
              <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
            <object dataType="Class" type="PhysicsTestbed.FadeoutEffect" id="13">
              <fade dataType="Float">1</fade>
              <fadeSpeed dataType="Float">0.1</fadeSpeed>
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
          </values>
        </customSerialIO>
      </compMap>
      <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="14">
        <_items dataType="Array" type="Duality.Component[]" id="15" length="4">
          <object dataType="ObjectRef">9</object>
          <object dataType="ObjectRef">10</object>
          <object dataType="ObjectRef">13</object>
          <object />
        </_items>
        <_size dataType="Int">3</_size>
        <_version dataType="Int">3</_version>
      </compList>
      <active dataType="Bool">true</active>
      <disposed dataType="Bool">false</disposed>
      <compTransform dataType="ObjectRef">9</compTransform>
      <EventComponentAdded />
      <EventComponentRemoving />
      <EventCollisionBegin />
      <EventCollisionEnd />
      <EventCollisionSolve />
    </objTree>
    <sourcePath dataType="String">HitParticle</sourcePath>
  </object>
</root>