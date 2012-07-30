<?xml version="1.0" encoding="utf-8"?>
<root>
  <object dataType="Class" type="Duality.Resources.Prefab" id="1">
    <objTree dataType="Class" type="Duality.GameObject" id="2">
      <name dataType="String">Square</name>
      <prefabLink />
      <parent />
      <children />
      <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="3" surrogate="true">
        <customSerialIO />
        <customSerialIO>
          <keys dataType="Array" type="System.Type[]" id="4" length="3">
            <object dataType="Type" id="5" value="Duality.Components.Transform" />
            <object dataType="Type" id="6" value="Duality.Components.Renderers.SpriteRenderer" />
            <object dataType="Type" id="7" value="Duality.Components.Physics.RigidBody" />
          </keys>
          <values dataType="Array" type="Duality.Component[]" id="8" length="3">
            <object dataType="Class" type="Duality.Components.Transform" id="9">
              <pos dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">-0.5</Y>
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
                <Y dataType="Float">-0.5</Y>
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
                <Y dataType="Float">-0.5</Y>
                <Z dataType="Float">0</Z>
              </lastPos>
              <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                <X dataType="Float">0</X>
                <Y dataType="Float">-0.5</Y>
                <Z dataType="Float">0</Z>
              </lastPosAbs>
              <lastAngle dataType="Float">0</lastAngle>
              <lastAngleAbs dataType="Float">0</lastAngleAbs>
              <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="10" multi="true">
                <object dataType="MethodInfo" id="11" value="M:Duality.Components.Physics.RigidBody:OnTransformChanged(System.Object,Duality.TransformChangedEventArgs)" />
                <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="12">
                  <bodyType dataType="Enum" type="Duality.Components.Physics.BodyType" name="Dynamic" value="1" />
                  <linearDamp dataType="Float">0.3</linearDamp>
                  <angularDamp dataType="Float">0.3</angularDamp>
                  <fixedAngle dataType="Bool">false</fixedAngle>
                  <ignoreGravity dataType="Bool">false</ignoreGravity>
                  <continous dataType="Bool">false</continous>
                  <linearVel dataType="Struct" type="OpenTK.Vector2">
                    <X dataType="Float">0</X>
                    <Y dataType="Float">0</Y>
                  </linearVel>
                  <angularVel dataType="Float">0</angularVel>
                  <revolutions dataType="Float">0</revolutions>
                  <explicitMass dataType="Float">0</explicitMass>
                  <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                  <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                  <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="13">
                    <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="14" length="4">
                      <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="15">
                        <vertices dataType="Array" type="OpenTK.Vector2[]" id="16" length="4">
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-125.5</X>
                            <Y dataType="Float">-125.5</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">125</X>
                            <Y dataType="Float">-125.5</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">125</X>
                            <Y dataType="Float">125</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-125.5</X>
                            <Y dataType="Float">125</Y>
                          </object>
                        </vertices>
                        <parent dataType="ObjectRef">12</parent>
                        <density dataType="Float">1</density>
                        <friction dataType="Float">0.3</friction>
                        <restitution dataType="Float">0.3</restitution>
                        <sensor dataType="Bool">false</sensor>
                      </object>
                      <object />
                      <object />
                      <object />
                    </_items>
                    <_size dataType="Int">1</_size>
                    <_version dataType="Int">1</_version>
                  </shapes>
                  <joints />
                  <gameobj dataType="ObjectRef">2</gameobj>
                  <disposed dataType="Bool">false</disposed>
                  <active dataType="Bool">true</active>
                </object>
                <object dataType="Array" type="System.Delegate[]" id="17" length="1">
                  <object dataType="ObjectRef">10</object>
                </object>
              </OnTransformChanged>
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
            <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="18">
              <rect dataType="Struct" type="Duality.Rect">
                <X dataType="Float">-128</X>
                <Y dataType="Float">-128</Y>
                <W dataType="Float">256</W>
                <H dataType="Float">256</H>
              </rect>
              <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                <contentPath dataType="String">Data\Sprites\Square.Material.res</contentPath>
              </sharedMat>
              <customMat />
              <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                <R dataType="Byte">255</R>
                <G dataType="Byte">100</G>
                <B dataType="Byte">84</B>
                <A dataType="Byte">255</A>
              </colorTint>
              <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
              <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
              <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
            <object dataType="ObjectRef">12</object>
          </values>
        </customSerialIO>
      </compMap>
      <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="19">
        <_items dataType="Array" type="Duality.Component[]" id="20" length="4">
          <object dataType="ObjectRef">9</object>
          <object dataType="ObjectRef">18</object>
          <object dataType="ObjectRef">12</object>
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
    <sourcePath dataType="String">Square</sourcePath>
  </object>
</root>