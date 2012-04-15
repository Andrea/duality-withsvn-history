<?xml version="1.0" encoding="utf-8"?>
<root>
  <object dataType="Class" type="Duality.Resources.Scene" id="1">
    <globalGravity dataType="Struct" type="OpenTK.Vector2">
      <X dataType="Float">0</X>
      <Y dataType="Float">33</Y>
    </globalGravity>
    <objectManager dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="2">
      <RegisteredObjectComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="3" multi="true">
        <object dataType="MethodInfo" id="4" value="M:Duality.Resources.Scene:objectManager_RegisteredObjectComponentAdded(System.Object,Duality.ComponentEventArgs)" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="5" length="1">
          <object dataType="ObjectRef">3</object>
        </object>
      </RegisteredObjectComponentAdded>
      <RegisteredObjectComponentRemoved dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="6" multi="true">
        <object dataType="MethodInfo" id="7" value="M:Duality.Resources.Scene:objectManager_RegisteredObjectComponentRemoved(System.Object,Duality.ComponentEventArgs)" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="8" length="1">
          <object dataType="ObjectRef">6</object>
        </object>
      </RegisteredObjectComponentRemoved>
      <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="9">
        <_items dataType="Array" type="Duality.GameObject[]" id="10" length="16">
          <object dataType="Class" type="Duality.GameObject" id="11">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="12" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="13" length="3">
                  <object dataType="Type" id="14" value="Duality.Components.Transform" />
                  <object dataType="Type" id="15" value="Duality.Components.Renderers.SpriteRenderer" />
                  <object dataType="Type" id="16" value="Duality.Components.Collider" />
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="17" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="18">
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
                    <extUpdater dataType="Class" type="Duality.Components.Collider" id="19">
                      <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                      <linearDamp dataType="Float">0</linearDamp>
                      <angularDamp dataType="Float">0</angularDamp>
                      <fixedAngle dataType="Bool">false</fixedAngle>
                      <ignoreGravity dataType="Bool">false</ignoreGravity>
                      <continous dataType="Bool">false</continous>
                      <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                      <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="20">
                        <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="21" length="4">
                          <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="22">
                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="23" length="4">
                              <object dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">-192</X>
                                <Y dataType="Float">-16</Y>
                              </object>
                              <object dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">192</X>
                                <Y dataType="Float">-16</Y>
                              </object>
                              <object dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">192</X>
                                <Y dataType="Float">16</Y>
                              </object>
                              <object dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">-192</X>
                                <Y dataType="Float">16</Y>
                              </object>
                            </vertices>
                            <parent dataType="ObjectRef">19</parent>
                            <density dataType="Float">1</density>
                            <friction dataType="Float">0.35</friction>
                            <restitution dataType="Float">0.05</restitution>
                            <sensor dataType="Bool">false</sensor>
                          </object>
                          <object />
                          <object />
                          <object />
                        </_items>
                        <_size dataType="Int">1</_size>
                        <_version dataType="Int">3</_version>
                      </shapes>
                      <joints dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+JointInfo]]" id="24">
                        <_items dataType="Array" type="Duality.Components.Collider+JointInfo[]" id="25" length="0" />
                        <_size dataType="Int">0</_size>
                        <_version dataType="Int">0</_version>
                      </joints>
                      <gameobj dataType="ObjectRef">11</gameobj>
                      <disposed dataType="Bool">false</disposed>
                      <active dataType="Bool">true</active>
                    </extUpdater>
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
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="26">
                    <rect dataType="Struct" type="Duality.Rect">
                      <x dataType="Float">-192</x>
                      <y dataType="Float">-16</y>
                      <w dataType="Float">384</w>
                      <h dataType="Float">32</h>
                    </rect>
                    <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                      <contentPath dataType="String">Data\Wall.Material.res</contentPath>
                    </sharedMat>
                    <customMat />
                    <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                      <r dataType="Byte">255</r>
                      <g dataType="Byte">255</g>
                      <b dataType="Byte">255</b>
                      <a dataType="Byte">255</a>
                    </colorTint>
                    <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="WrapBoth" value="3" />
                    <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                    <visibilityGroup dataType="UInt">1</visibilityGroup>
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">19</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="27">
              <_items dataType="Array" type="Duality.Component[]" id="28" length="4">
                <object dataType="ObjectRef">18</object>
                <object dataType="ObjectRef">26</object>
                <object dataType="ObjectRef">19</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">BottomWall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">18</compTransform>
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="29" multi="true">
              <object dataType="MethodInfo" id="30" value="M:Duality.ObjectManagers.GameObjectManager:OnRegisteredObjectComponentAdded(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">2</object>
              <object dataType="Array" type="System.Delegate[]" id="31" length="1">
                <object dataType="ObjectRef">29</object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="32" multi="true">
              <object dataType="MethodInfo" id="33" value="M:Duality.ObjectManagers.GameObjectManager:OnRegisteredObjectComponentRemoved(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">2</object>
              <object dataType="Array" type="System.Delegate[]" id="34" length="1">
                <object dataType="ObjectRef">32</object>
              </object>
            </EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="35">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="36" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="37" length="3">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">15</object>
                  <object dataType="ObjectRef">16</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="38" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="39">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">207.236908</X>
                      <Y dataType="Float">-679.0526</Y>
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
                      <X dataType="Float">0.2709147</X>
                      <Y dataType="Float">0.2709147</Y>
                      <Z dataType="Float">0.2709147</Z>
                    </scale>
                    <deriveAngle dataType="Bool">true</deriveAngle>
                    <extUpdater dataType="Class" type="Duality.Components.Collider" id="40">
                      <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                      <linearDamp dataType="Float">0</linearDamp>
                      <angularDamp dataType="Float">0</angularDamp>
                      <fixedAngle dataType="Bool">false</fixedAngle>
                      <ignoreGravity dataType="Bool">false</ignoreGravity>
                      <continous dataType="Bool">true</continous>
                      <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                      <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="41">
                        <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="42" length="4">
                          <object dataType="Class" type="Duality.Components.Collider+CircleShapeInfo" id="43">
                            <radius dataType="Float">128</radius>
                            <position dataType="Struct" type="OpenTK.Vector2">
                              <X dataType="Float">0</X>
                              <Y dataType="Float">0</Y>
                            </position>
                            <parent dataType="ObjectRef">40</parent>
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
                      <joints dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+JointInfo]]" id="44">
                        <_items dataType="ObjectRef">25</_items>
                        <_size dataType="Int">0</_size>
                        <_version dataType="Int">0</_version>
                      </joints>
                      <gameobj dataType="ObjectRef">35</gameobj>
                      <disposed dataType="Bool">false</disposed>
                      <active dataType="Bool">true</active>
                    </extUpdater>
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">207.236908</X>
                      <Y dataType="Float">-679.0526</Y>
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
                      <X dataType="Float">0.2709147</X>
                      <Y dataType="Float">0.2709147</Y>
                      <Z dataType="Float">0.2709147</Z>
                    </scaleAbs>
                    <gameobj dataType="ObjectRef">35</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="45">
                    <rect dataType="Struct" type="Duality.Rect">
                      <x dataType="Float">-128</x>
                      <y dataType="Float">-128</y>
                      <w dataType="Float">256</w>
                      <h dataType="Float">256</h>
                    </rect>
                    <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                      <contentPath dataType="String">Default:Material:DualityLogo256</contentPath>
                    </sharedMat>
                    <customMat />
                    <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                      <r dataType="Byte">255</r>
                      <g dataType="Byte">255</g>
                      <b dataType="Byte">255</b>
                      <a dataType="Byte">255</a>
                    </colorTint>
                    <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                    <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                    <visibilityGroup dataType="UInt">1</visibilityGroup>
                    <gameobj dataType="ObjectRef">35</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">40</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="46">
              <_items dataType="Array" type="Duality.Component[]" id="47" length="4">
                <object dataType="ObjectRef">39</object>
                <object dataType="ObjectRef">45</object>
                <object dataType="ObjectRef">40</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">DualityLogo256</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">39</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="48">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="49" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="50" length="3">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">15</object>
                  <object dataType="ObjectRef">16</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="51" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="52">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">-176</X>
                      <Y dataType="Float">-288</Y>
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
                    <extUpdater dataType="Class" type="Duality.Components.Collider" id="53">
                      <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                      <linearDamp dataType="Float">0</linearDamp>
                      <angularDamp dataType="Float">0</angularDamp>
                      <fixedAngle dataType="Bool">false</fixedAngle>
                      <ignoreGravity dataType="Bool">false</ignoreGravity>
                      <continous dataType="Bool">false</continous>
                      <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                      <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="54">
                        <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="55" length="4">
                          <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="56">
                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="57" length="4">
                              <object dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">-16</X>
                                <Y dataType="Float">-272</Y>
                              </object>
                              <object dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">16</X>
                                <Y dataType="Float">-272</Y>
                              </object>
                              <object dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">16</X>
                                <Y dataType="Float">272</Y>
                              </object>
                              <object dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">-16</X>
                                <Y dataType="Float">272</Y>
                              </object>
                            </vertices>
                            <parent dataType="ObjectRef">53</parent>
                            <density dataType="Float">1</density>
                            <friction dataType="Float">0.35</friction>
                            <restitution dataType="Float">0.05</restitution>
                            <sensor dataType="Bool">false</sensor>
                          </object>
                          <object />
                          <object />
                          <object />
                        </_items>
                        <_size dataType="Int">1</_size>
                        <_version dataType="Int">4</_version>
                      </shapes>
                      <joints dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+JointInfo]]" id="58">
                        <_items dataType="Array" type="Duality.Components.Collider+JointInfo[]" id="59" length="0" />
                        <_size dataType="Int">0</_size>
                        <_version dataType="Int">0</_version>
                      </joints>
                      <gameobj dataType="ObjectRef">48</gameobj>
                      <disposed dataType="Bool">false</disposed>
                      <active dataType="Bool">true</active>
                    </extUpdater>
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">-176</X>
                      <Y dataType="Float">-288</Y>
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
                    <gameobj dataType="ObjectRef">48</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="60">
                    <rect dataType="Struct" type="Duality.Rect">
                      <x dataType="Float">-16</x>
                      <y dataType="Float">-272</y>
                      <w dataType="Float">32</w>
                      <h dataType="Float">544</h>
                    </rect>
                    <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                      <contentPath dataType="String">Data\Wall.Material.res</contentPath>
                    </sharedMat>
                    <customMat />
                    <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                      <r dataType="Byte">255</r>
                      <g dataType="Byte">255</g>
                      <b dataType="Byte">255</b>
                      <a dataType="Byte">255</a>
                    </colorTint>
                    <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="WrapBoth" value="3" />
                    <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                    <visibilityGroup dataType="UInt">1</visibilityGroup>
                    <gameobj dataType="ObjectRef">48</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">53</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="61">
              <_items dataType="Array" type="Duality.Component[]" id="62" length="4">
                <object dataType="ObjectRef">52</object>
                <object dataType="ObjectRef">60</object>
                <object dataType="ObjectRef">53</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">LeftWall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">52</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="63">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="64" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="65" length="3">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">15</object>
                  <object dataType="ObjectRef">16</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="66" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="67">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">176</X>
                      <Y dataType="Float">-288</Y>
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
                    <extUpdater dataType="Class" type="Duality.Components.Collider" id="68">
                      <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                      <linearDamp dataType="Float">0</linearDamp>
                      <angularDamp dataType="Float">0</angularDamp>
                      <fixedAngle dataType="Bool">false</fixedAngle>
                      <ignoreGravity dataType="Bool">false</ignoreGravity>
                      <continous dataType="Bool">false</continous>
                      <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                      <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="69">
                        <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="70" length="4">
                          <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="71">
                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="72" length="4">
                              <object dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">-16</X>
                                <Y dataType="Float">-272</Y>
                              </object>
                              <object dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">16</X>
                                <Y dataType="Float">-272</Y>
                              </object>
                              <object dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">16</X>
                                <Y dataType="Float">272</Y>
                              </object>
                              <object dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">-16</X>
                                <Y dataType="Float">272</Y>
                              </object>
                            </vertices>
                            <parent dataType="ObjectRef">68</parent>
                            <density dataType="Float">1</density>
                            <friction dataType="Float">0.35</friction>
                            <restitution dataType="Float">0.05</restitution>
                            <sensor dataType="Bool">false</sensor>
                          </object>
                          <object />
                          <object />
                          <object />
                        </_items>
                        <_size dataType="Int">1</_size>
                        <_version dataType="Int">4</_version>
                      </shapes>
                      <joints dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+JointInfo]]" id="73">
                        <_items dataType="Array" type="Duality.Components.Collider+JointInfo[]" id="74" length="0" />
                        <_size dataType="Int">0</_size>
                        <_version dataType="Int">0</_version>
                      </joints>
                      <gameobj dataType="ObjectRef">63</gameobj>
                      <disposed dataType="Bool">false</disposed>
                      <active dataType="Bool">true</active>
                    </extUpdater>
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">176</X>
                      <Y dataType="Float">-288</Y>
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
                    <gameobj dataType="ObjectRef">63</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="75">
                    <rect dataType="Struct" type="Duality.Rect">
                      <x dataType="Float">-16</x>
                      <y dataType="Float">-272</y>
                      <w dataType="Float">32</w>
                      <h dataType="Float">544</h>
                    </rect>
                    <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                      <contentPath dataType="String">Data\Wall.Material.res</contentPath>
                    </sharedMat>
                    <customMat />
                    <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                      <r dataType="Byte">255</r>
                      <g dataType="Byte">255</g>
                      <b dataType="Byte">255</b>
                      <a dataType="Byte">255</a>
                    </colorTint>
                    <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="WrapBoth" value="3" />
                    <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                    <visibilityGroup dataType="UInt">1</visibilityGroup>
                    <gameobj dataType="ObjectRef">63</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">68</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="76">
              <_items dataType="Array" type="Duality.Component[]" id="77" length="4">
                <object dataType="ObjectRef">67</object>
                <object dataType="ObjectRef">75</object>
                <object dataType="ObjectRef">68</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">RightWall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">67</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="78">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="79" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="80" length="3">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="Type" id="81" value="Duality.Components.SoundListener" />
                  <object dataType="Type" id="82" value="Duality.Components.Camera" />
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="83" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="84">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">-272</Y>
                      <Z dataType="Float">-500</Z>
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
                      <Y dataType="Float">-272</Y>
                      <Z dataType="Float">-500</Z>
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
                    <gameobj dataType="ObjectRef">78</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.SoundListener" id="85">
                    <gameobj dataType="ObjectRef">78</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Camera" id="86">
                    <nearZ dataType="Float">0</nearZ>
                    <farZ dataType="Float">10000</farZ>
                    <zSortAccuracy dataType="Float">1000</zSortAccuracy>
                    <parallaxRefDist dataType="Float">500</parallaxRefDist>
                    <visibilityMask dataType="UInt">4294967295</visibilityMask>
                    <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                      <r dataType="Byte">0</r>
                      <g dataType="Byte">0</g>
                      <b dataType="Byte">0</b>
                      <a dataType="Byte">0</a>
                    </clearColor>
                    <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                    <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="87" length="1">
                      <object dataType="Class" type="Duality.Components.Camera+Pass" id="88">
                        <input />
                        <output dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.RenderTarget]]">
                          <contentPath />
                        </output>
                        <fitOutput dataType="Bool">false</fitOutput>
                        <keepOutput dataType="Bool">false</keepOutput>
                        <visibilityMask dataType="UInt">4294967295</visibilityMask>
                      </object>
                    </passes>
                    <CollectRendererDrawcalls />
                    <CollectOverlayDrawcalls />
                    <gameobj dataType="ObjectRef">78</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="89">
              <_items dataType="Array" type="Duality.Component[]" id="90" length="4">
                <object dataType="ObjectRef">84</object>
                <object dataType="ObjectRef">85</object>
                <object dataType="ObjectRef">86</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">MainCamera</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">84</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="91">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="92" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="93" length="1">
                  <object dataType="Type" id="94" value="Tetris.GameController" />
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="95" length="1">
                  <object dataType="Class" type="Tetris.GameController" id="96">
                    <gameobj dataType="ObjectRef">91</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="97">
              <_items dataType="Array" type="Duality.Component[]" id="98" length="4">
                <object dataType="ObjectRef">96</object>
                <object />
                <object />
                <object />
              </_items>
              <_size dataType="Int">1</_size>
              <_version dataType="Int">1</_version>
            </compList>
            <name dataType="String">GameController</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
        </_items>
        <_size dataType="Int">6</_size>
        <_version dataType="Int">92</_version>
      </allObj>
      <Registered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="99" multi="true">
        <object dataType="MethodInfo" id="100" value="M:Duality.Resources.Scene:objectManager_Registered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="101" length="1">
          <object dataType="ObjectRef">99</object>
        </object>
      </Registered>
      <Unregistered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="102" multi="true">
        <object dataType="MethodInfo" id="103" value="M:Duality.Resources.Scene:objectManager_Unregistered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="104" length="1">
          <object dataType="ObjectRef">102</object>
        </object>
      </Unregistered>
    </objectManager>
    <sourcePath />
  </object>
</root>