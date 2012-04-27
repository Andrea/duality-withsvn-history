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
          <keys dataType="Array" type="System.Type[]" id="4" length="4">
            <object dataType="Type" id="5" value="Duality.Components.Transform" />
            <object dataType="Type" id="6" value="Duality.Components.Collider" />
            <object dataType="Type" id="7" value="Tetris.BlockRenderer" />
            <object dataType="Type" id="8" value="Tetris.Block" />
          </keys>
          <values dataType="Array" type="Duality.Component[]" id="9" length="4">
            <object dataType="Class" type="Duality.Components.Transform" id="10">
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
            <object dataType="Class" type="Duality.Components.Collider" id="11">
              <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
              <linearDamp dataType="Float">0</linearDamp>
              <angularDamp dataType="Float">0</angularDamp>
              <fixedAngle dataType="Bool">true</fixedAngle>
              <ignoreGravity dataType="Bool">true</ignoreGravity>
              <continous dataType="Bool">true</continous>
              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="12">
                <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="13" length="4">
                  <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="14">
                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="15" length="4">
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">-32</X>
                        <Y dataType="Float">-32</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">0</X>
                        <Y dataType="Float">-32</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">0</X>
                        <Y dataType="Float">0</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">-32</X>
                        <Y dataType="Float">0</Y>
                      </object>
                    </vertices>
                    <parent dataType="ObjectRef">11</parent>
                    <density dataType="Float">1</density>
                    <friction dataType="Float">0.3</friction>
                    <restitution dataType="Float">0.3</restitution>
                    <sensor dataType="Bool">false</sensor>
                  </object>
                  <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="16">
                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="17" length="4">
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">0</X>
                        <Y dataType="Float">32</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">32</X>
                        <Y dataType="Float">32</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">32</X>
                        <Y dataType="Float">0</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">0</X>
                        <Y dataType="Float">0</Y>
                      </object>
                    </vertices>
                    <parent dataType="ObjectRef">11</parent>
                    <density dataType="Float">1</density>
                    <friction dataType="Float">0.3</friction>
                    <restitution dataType="Float">0.3</restitution>
                    <sensor dataType="Bool">false</sensor>
                  </object>
                  <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="18">
                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="19" length="4">
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">-32</X>
                        <Y dataType="Float">-64</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">0</X>
                        <Y dataType="Float">-64</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">0</X>
                        <Y dataType="Float">-32</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">-32</X>
                        <Y dataType="Float">-32</Y>
                      </object>
                    </vertices>
                    <parent dataType="ObjectRef">11</parent>
                    <density dataType="Float">1</density>
                    <friction dataType="Float">0.3</friction>
                    <restitution dataType="Float">0.3</restitution>
                    <sensor dataType="Bool">false</sensor>
                  </object>
                  <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="20">
                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="21" length="4">
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">-32</X>
                        <Y dataType="Float">0</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">0</X>
                        <Y dataType="Float">0</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">0</X>
                        <Y dataType="Float">32</Y>
                      </object>
                      <object dataType="Struct" type="OpenTK.Vector2">
                        <X dataType="Float">-32</X>
                        <Y dataType="Float">32</Y>
                      </object>
                    </vertices>
                    <parent dataType="ObjectRef">11</parent>
                    <density dataType="Float">1</density>
                    <friction dataType="Float">0.3</friction>
                    <restitution dataType="Float">0.3</restitution>
                    <sensor dataType="Bool">false</sensor>
                  </object>
                </_items>
                <_size dataType="Int">4</_size>
                <_version dataType="Int">7</_version>
              </shapes>
              <joints dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+JointInfo]]" id="22">
                <_items dataType="Array" type="Duality.Components.Collider+JointInfo[]" id="23" length="0" />
                <_size dataType="Int">0</_size>
                <_version dataType="Int">0</_version>
              </joints>
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
            <object dataType="Class" type="Tetris.BlockRenderer" id="24">
              <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                <contentPath dataType="String">Data\Blocks\BlockD.Material.res</contentPath>
              </sharedMat>
              <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                <r dataType="Byte">255</r>
                <g dataType="Byte">255</g>
                <b dataType="Byte">255</b>
                <a dataType="Byte">255</a>
              </colorTint>
              <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
              <visibilityGroup dataType="UInt">1</visibilityGroup>
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
            <object dataType="Class" type="Tetris.Block" id="25">
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Bool">false</disposed>
              <active dataType="Bool">true</active>
            </object>
          </values>
        </customSerialIO>
      </compMap>
      <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="26">
        <_items dataType="Array" type="Duality.Component[]" id="27" length="4">
          <object dataType="ObjectRef">10</object>
          <object dataType="ObjectRef">11</object>
          <object dataType="ObjectRef">24</object>
          <object dataType="ObjectRef">25</object>
        </_items>
        <_size dataType="Int">4</_size>
        <_version dataType="Int">4</_version>
      </compList>
      <name dataType="String">BlockD</name>
      <active dataType="Bool">true</active>
      <disposed dataType="Bool">false</disposed>
      <compTransform dataType="ObjectRef">10</compTransform>
      <EventComponentAdded />
      <EventComponentRemoving />
      <EventCollisionBegin />
      <EventCollisionEnd />
      <EventCollisionSolve />
    </objTree>
    <sourcePath dataType="String">BlockD</sourcePath>
  </object>
</root>