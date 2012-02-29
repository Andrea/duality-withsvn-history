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
            <object dataType="Type" id="6" value="Duality.Components.Renderers.SpriteRenderer" />
            <object dataType="Type" id="7" value="Duality.Components.Collider" />
          </keys>
          <values dataType="Array" type="Duality.Component[]" id="8" length="3">
            <object dataType="Class" type="Duality.Components.Transform" id="9">
              <pos dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">528</Y>
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
                <Y dataType="Float">528</Y>
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
            <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="10">
              <rect dataType="Struct" type="Duality.Rect">
                <x dataType="Float">-512</x>
                <y dataType="Float">-16</y>
                <w dataType="Float">1024</w>
                <h dataType="Float">32</h>
              </rect>
              <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                <contentPath dataType="String">Data\Materials\Wall.Material.res</contentPath>
              </sharedMat>
              <customMat />
              <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                <r dataType="Byte">255</r>
                <g dataType="Byte">255</g>
                <b dataType="Byte">255</b>
                <a dataType="Byte">255</a>
              </colorTint>
              <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="WrapHorizontal" value="1" />
              <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
              <visibilityGroup dataType="UInt">1</visibilityGroup>
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
            <object dataType="Class" type="Duality.Components.Collider" id="11">
              <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
              <linearDamp dataType="Float">0</linearDamp>
              <angularDamp dataType="Float">0</angularDamp>
              <fixedAngle dataType="Bool">false</fixedAngle>
              <ignoreGravity dataType="Bool">false</ignoreGravity>
              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="12">
                <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="13" length="4">
                  <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="14">
                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="15" length="4">
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">-512</X>
                        <Y dataType="Float">-16.25665</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">512</X>
                        <Y dataType="Float">-16.83684</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">512</X>
                        <Y dataType="Float">-0.5915592</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">-512</X>
                        <Y dataType="Float">-1.171748</Y>
                      </object>
                    </vertices>
                    <parent dataType="ObjectRef">11</parent>
                    <density dataType="Float">1</density>
                    <friction dataType="Float">1</friction>
                    <restitution dataType="Float">1</restitution>
                    <sensor dataType="Bool">false</sensor>
                  </object>
                  <object />
                  <object />
                  <object />
                </_items>
                <_size dataType="Int">1</_size>
                <_version dataType="Int">1</_version>
              </shapes>
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
          </values>
        </customSerialIO>
      </compMap>
      <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="16">
        <_items dataType="Array" type="Duality.Component[]" id="17" length="4">
          <object dataType="ObjectRef">9</object>
          <object dataType="ObjectRef">10</object>
          <object dataType="ObjectRef">11</object>
          <object />
        </_items>
        <_size dataType="Int">3</_size>
        <_version dataType="Int">3</_version>
      </compList>
      <name dataType="String">Wall</name>
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