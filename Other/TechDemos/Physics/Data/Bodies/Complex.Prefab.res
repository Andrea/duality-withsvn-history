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
            <object dataType="Type" id="7" value="Duality.Components.Physics.RigidBody" />
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
                    <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="14" length="16">
                      <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="15">
                        <vertices dataType="Array" type="OpenTK.Vector2[]" id="16" length="5">
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-39.8356</X>
                            <Y dataType="Float">-88.48786</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-96.26299</X>
                            <Y dataType="Float">10.2600765</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-20.0860157</X>
                            <Y dataType="Float">93.77262</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">83.74039</X>
                            <Y dataType="Float">47.50216</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">71.32636</X>
                            <Y dataType="Float">-64.78835</Y>
                          </object>
                        </vertices>
                        <parent dataType="ObjectRef">12</parent>
                        <density dataType="Float">1</density>
                        <friction dataType="Float">0.3</friction>
                        <restitution dataType="Float">0.3</restitution>
                        <sensor dataType="Bool">false</sensor>
                      </object>
                      <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="17">
                        <vertices dataType="Array" type="OpenTK.Vector2[]" id="18" length="5">
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">82.61184</X>
                            <Y dataType="Float">-113.31591</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">73.58346</X>
                            <Y dataType="Float">-80.58803</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">99.54006</X>
                            <Y dataType="Float">-62.5312576</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">126.625206</X>
                            <Y dataType="Float">-82.2808456</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">114.775459</X>
                            <Y dataType="Float">-113.31591</Y>
                          </object>
                        </vertices>
                        <parent dataType="ObjectRef">12</parent>
                        <density dataType="Float">1</density>
                        <friction dataType="Float">0.3</friction>
                        <restitution dataType="Float">0.3</restitution>
                        <sensor dataType="Bool">false</sensor>
                      </object>
                      <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="19">
                        <vertices dataType="Array" type="OpenTK.Vector2[]" id="20" length="5">
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-118.833954</X>
                            <Y dataType="Float">-94.1306</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-88.92744</X>
                            <Y dataType="Float">-105.416077</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-68.61357</X>
                            <Y dataType="Float">-80.58803</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-86.1060638</X>
                            <Y dataType="Float">-54.0671539</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-116.012581</X>
                            <Y dataType="Float">-61.9669876</Y>
                          </object>
                        </vertices>
                        <parent dataType="ObjectRef">12</parent>
                        <density dataType="Float">1</density>
                        <friction dataType="Float">0.3</friction>
                        <restitution dataType="Float">0.3</restitution>
                        <sensor dataType="Bool">false</sensor>
                      </object>
                      <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="21">
                        <vertices dataType="Array" type="OpenTK.Vector2[]" id="22" length="5">
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">57.78379</X>
                            <Y dataType="Float">92.0797958</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">80.91902</X>
                            <Y dataType="Float">77.4086761</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">102.925705</X>
                            <Y dataType="Float">95.46544</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">92.2045</X>
                            <Y dataType="Float">121.422043</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">64.55508</X>
                            <Y dataType="Float">119.729218</Y>
                          </object>
                        </vertices>
                        <parent dataType="ObjectRef">12</parent>
                        <density dataType="Float">1</density>
                        <friction dataType="Float">0.3</friction>
                        <restitution dataType="Float">0.3</restitution>
                        <sensor dataType="Bool">false</sensor>
                      </object>
                      <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="23">
                        <vertices dataType="Array" type="OpenTK.Vector2[]" id="24" length="5">
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-122.783867</X>
                            <Y dataType="Float">88.12988</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-96.26299</X>
                            <Y dataType="Float">72.33021</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-71.9992142</X>
                            <Y dataType="Float">92.0797958</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-83.28469</X>
                            <Y dataType="Float">120.857765</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-114.319763</X>
                            <Y dataType="Float">118.60067</Y>
                          </object>
                        </vertices>
                        <parent dataType="ObjectRef">12</parent>
                        <density dataType="Float">1</density>
                        <friction dataType="Float">0.3</friction>
                        <restitution dataType="Float">0.3</restitution>
                        <sensor dataType="Bool">false</sensor>
                      </object>
                      <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="25">
                        <vertices dataType="Array" type="OpenTK.Vector2[]" id="26" length="4">
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-75.3848648</X>
                            <Y dataType="Float">-75.50956</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-53.94245</X>
                            <Y dataType="Float">-61.9669876</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-64.09938</X>
                            <Y dataType="Float">-44.4744949</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-85.5417938</X>
                            <Y dataType="Float">-60.2741623</Y>
                          </object>
                        </vertices>
                        <parent dataType="ObjectRef">12</parent>
                        <density dataType="Float">1</density>
                        <friction dataType="Float">0.3</friction>
                        <restitution dataType="Float">0.3</restitution>
                        <sensor dataType="Bool">false</sensor>
                      </object>
                      <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="27">
                        <vertices dataType="Array" type="OpenTK.Vector2[]" id="28" length="4">
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">66.2479</X>
                            <Y dataType="Float">-65.35263</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">82.04757</X>
                            <Y dataType="Float">-78.33093</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">96.15442</X>
                            <Y dataType="Float">-65.35263</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">71.32636</X>
                            <Y dataType="Float">-45.6030426</Y>
                          </object>
                        </vertices>
                        <parent dataType="ObjectRef">12</parent>
                        <density dataType="Float">1</density>
                        <friction dataType="Float">0.3</friction>
                        <restitution dataType="Float">0.3</restitution>
                        <sensor dataType="Bool">false</sensor>
                      </object>
                      <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="29">
                        <vertices dataType="Array" type="OpenTK.Vector2[]" id="30" length="4">
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-92.87735</X>
                            <Y dataType="Float">76.8444</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-58.4566422</X>
                            <Y dataType="Float">48.06643</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-44.3497925</X>
                            <Y dataType="Float">63.8660965</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">-80.4633255</X>
                            <Y dataType="Float">88.12988</Y>
                          </object>
                        </vertices>
                        <parent dataType="ObjectRef">12</parent>
                        <density dataType="Float">1</density>
                        <friction dataType="Float">0.3</friction>
                        <restitution dataType="Float">0.3</restitution>
                        <sensor dataType="Bool">false</sensor>
                      </object>
                      <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="31">
                        <vertices dataType="Array" type="OpenTK.Vector2[]" id="32" length="4">
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">48.19113</X>
                            <Y dataType="Float">61.609</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">63.42653</X>
                            <Y dataType="Float">89.8227</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">79.2262</X>
                            <Y dataType="Float">80.23005</Y>
                          </object>
                          <object dataType="Struct" type="OpenTK.Vector2">
                            <X dataType="Float">66.81217</X>
                            <Y dataType="Float">50.8878021</Y>
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
                      <object />
                      <object />
                      <object />
                      <object />
                    </_items>
                    <_size dataType="Int">9</_size>
                    <_version dataType="Int">9</_version>
                  </shapes>
                  <joints />
                  <gameobj dataType="ObjectRef">2</gameobj>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <active dataType="Bool">true</active>
                </object>
                <object dataType="Array" type="System.Delegate[]" id="33" length="1">
                  <object dataType="ObjectRef">10</object>
                </object>
              </OnTransformChanged>
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
              <active dataType="Bool">true</active>
            </object>
            <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="34">
              <rect dataType="Struct" type="Duality.Rect">
                <X dataType="Float">-128</X>
                <Y dataType="Float">-128</Y>
                <W dataType="Float">256</W>
                <H dataType="Float">256</H>
              </rect>
              <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                <contentPath dataType="String">Data\Sprites\Complex.Material.res</contentPath>
              </sharedMat>
              <customMat />
              <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                <R dataType="Byte">187</R>
                <G dataType="Byte">255</G>
                <B dataType="Byte">97</B>
                <A dataType="Byte">255</A>
              </colorTint>
              <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
              <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
              <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
              <gameobj dataType="ObjectRef">2</gameobj>
              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
              <active dataType="Bool">true</active>
            </object>
            <object dataType="ObjectRef">12</object>
          </values>
        </customSerialIO>
      </compMap>
      <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="35">
        <_items dataType="Array" type="Duality.Component[]" id="36" length="4">
          <object dataType="ObjectRef">9</object>
          <object dataType="ObjectRef">34</object>
          <object dataType="ObjectRef">12</object>
          <object />
        </_items>
        <_size dataType="Int">3</_size>
        <_version dataType="Int">3</_version>
      </compList>
      <name dataType="String">Complex</name>
      <active dataType="Bool">true</active>
      <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
      <compTransform dataType="ObjectRef">9</compTransform>
      <EventComponentAdded />
      <EventComponentRemoving />
      <EventCollisionBegin />
      <EventCollisionEnd />
      <EventCollisionSolve />
    </objTree>
    <sourcePath dataType="String">Complex</sourcePath>
  </object>
</root>