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
                      <x dataType="Float">-194</x>
                      <y dataType="Float">-16</y>
                      <w dataType="Float">388</w>
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
                      <X dataType="Float">-178</X>
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
                    <extUpdater dataType="Class" type="Duality.Components.Collider" id="40">
                      <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                      <linearDamp dataType="Float">0</linearDamp>
                      <angularDamp dataType="Float">0</angularDamp>
                      <fixedAngle dataType="Bool">false</fixedAngle>
                      <ignoreGravity dataType="Bool">false</ignoreGravity>
                      <continous dataType="Bool">false</continous>
                      <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                      <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="41">
                        <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="42" length="4">
                          <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="43">
                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="44" length="4">
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
                            <parent dataType="ObjectRef">40</parent>
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
                      <joints dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+JointInfo]]" id="45">
                        <_items dataType="Array" type="Duality.Components.Collider+JointInfo[]" id="46" length="0" />
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
                      <X dataType="Float">-178</X>
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
                    <gameobj dataType="ObjectRef">35</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="47">
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
                    <gameobj dataType="ObjectRef">35</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">40</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="48">
              <_items dataType="Array" type="Duality.Component[]" id="49" length="4">
                <object dataType="ObjectRef">39</object>
                <object dataType="ObjectRef">47</object>
                <object dataType="ObjectRef">40</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">LeftWall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">39</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="50">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="51" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="52" length="3">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">15</object>
                  <object dataType="ObjectRef">16</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="53" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="54">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">178</X>
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
                    <extUpdater dataType="Class" type="Duality.Components.Collider" id="55">
                      <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                      <linearDamp dataType="Float">0</linearDamp>
                      <angularDamp dataType="Float">0</angularDamp>
                      <fixedAngle dataType="Bool">false</fixedAngle>
                      <ignoreGravity dataType="Bool">false</ignoreGravity>
                      <continous dataType="Bool">false</continous>
                      <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                      <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="56">
                        <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="57" length="4">
                          <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="58">
                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="59" length="4">
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
                            <parent dataType="ObjectRef">55</parent>
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
                      <joints dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+JointInfo]]" id="60">
                        <_items dataType="Array" type="Duality.Components.Collider+JointInfo[]" id="61" length="0" />
                        <_size dataType="Int">0</_size>
                        <_version dataType="Int">0</_version>
                      </joints>
                      <gameobj dataType="ObjectRef">50</gameobj>
                      <disposed dataType="Bool">false</disposed>
                      <active dataType="Bool">true</active>
                    </extUpdater>
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">178</X>
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
                    <gameobj dataType="ObjectRef">50</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="62">
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
                    <gameobj dataType="ObjectRef">50</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">55</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="63">
              <_items dataType="Array" type="Duality.Component[]" id="64" length="4">
                <object dataType="ObjectRef">54</object>
                <object dataType="ObjectRef">62</object>
                <object dataType="ObjectRef">55</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">RightWall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">54</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="65">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="66" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="67" length="3">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="Type" id="68" value="Duality.Components.SoundListener" />
                  <object dataType="Type" id="69" value="Duality.Components.Camera" />
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="70" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="71">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">128</X>
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
                      <X dataType="Float">128</X>
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
                    <gameobj dataType="ObjectRef">65</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.SoundListener" id="72">
                    <gameobj dataType="ObjectRef">65</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Camera" id="73">
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
                    <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="74" length="1">
                      <object dataType="Class" type="Duality.Components.Camera+Pass" id="75">
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
                    <gameobj dataType="ObjectRef">65</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="76">
              <_items dataType="Array" type="Duality.Component[]" id="77" length="4">
                <object dataType="ObjectRef">71</object>
                <object dataType="ObjectRef">72</object>
                <object dataType="ObjectRef">73</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">MainCamera</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">71</compTransform>
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
                <keys dataType="Array" type="System.Type[]" id="80" length="1">
                  <object dataType="Type" id="81" value="Tetris.GameController" />
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="82" length="1">
                  <object dataType="Class" type="Tetris.GameController" id="83">
                    <beginTime dataType="Float">1837.3335</beginTime>
                    <gameOver dataType="Bool">false</gameOver>
                    <gameStage dataType="Int">0</gameStage>
                    <gameobj dataType="ObjectRef">78</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="84">
              <_items dataType="Array" type="Duality.Component[]" id="85" length="4">
                <object dataType="ObjectRef">83</object>
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
          <object dataType="Class" type="Duality.GameObject" id="86">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="87" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="88" length="2">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="Type" id="89" value="Duality.Components.Renderers.TextRenderer" />
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="90" length="2">
                  <object dataType="Class" type="Duality.Components.Transform" id="91">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">128</X>
                      <Y dataType="Float">-272</Y>
                      <Z dataType="Float">-10</Z>
                    </pos>
                    <vel dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">-0.5</Z>
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
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel" value="3" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">128</X>
                      <Y dataType="Float">-272</Y>
                      <Z dataType="Float">-10</Z>
                    </posAbs>
                    <velAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">-0.5</Z>
                    </velAbs>
                    <angleAbs dataType="Float">0</angleAbs>
                    <angleVelAbs dataType="Float">0</angleVelAbs>
                    <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">1</X>
                      <Y dataType="Float">1</Y>
                      <Z dataType="Float">1</Z>
                    </scaleAbs>
                    <gameobj dataType="ObjectRef">86</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.TextRenderer" id="92">
                    <align dataType="Enum" type="Duality.Alignment" name="Center" value="0" />
                    <text dataType="Class" type="Duality.FormattedText" id="93">
                      <sourceText dataType="String">IT HAS BEGUN</sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="94" length="1">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\BigFont.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">0</maxWidth>
                      <maxHeight dataType="Int">0</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String">IT HAS BEGUN</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="95" length="1">
                        <object dataType="Int">12</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="96" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="97">
                          <text dataType="String">IT HAS BEGUN</text>
                        </object>
                      </elements>
                    </text>
                    <customMat />
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
                    <gameobj dataType="ObjectRef">86</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="98">
              <_items dataType="Array" type="Duality.Component[]" id="99" length="4">
                <object dataType="ObjectRef">91</object>
                <object dataType="ObjectRef">92</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">2</_version>
            </compList>
            <name dataType="String">Intro1</name>
            <active dataType="Bool">false</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">91</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="100">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="101" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="102" length="2">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">89</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="103" length="2">
                  <object dataType="Class" type="Duality.Components.Transform" id="104">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">128</X>
                      <Y dataType="Float">-272</Y>
                      <Z dataType="Float">-10</Z>
                    </pos>
                    <vel dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">-0.5</Z>
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
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel" value="3" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">128</X>
                      <Y dataType="Float">-272</Y>
                      <Z dataType="Float">-10</Z>
                    </posAbs>
                    <velAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">-0.5</Z>
                    </velAbs>
                    <angleAbs dataType="Float">0</angleAbs>
                    <angleVelAbs dataType="Float">0</angleVelAbs>
                    <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">1</X>
                      <Y dataType="Float">1</Y>
                      <Z dataType="Float">1</Z>
                    </scaleAbs>
                    <gameobj dataType="ObjectRef">100</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.TextRenderer" id="105">
                    <align dataType="Enum" type="Duality.Alignment" name="Center" value="0" />
                    <text dataType="Class" type="Duality.FormattedText" id="106">
                      <sourceText dataType="String">/acimagine some/nimpressive opening/nhere</sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="107" length="1">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\BigFont.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">280</maxWidth>
                      <maxHeight dataType="Int">113</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String">imagine someimpressive openinghere</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="108" length="1">
                        <object dataType="Int">34</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="109" length="6">
                        <object dataType="Class" type="Duality.FormattedText+AlignChangeElement" id="110">
                          <align dataType="Enum" type="Duality.Alignment" name="Center" value="0" />
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="111">
                          <text dataType="String">imagine some</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+NewLineElement" id="112" />
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="113">
                          <text dataType="String">impressive opening</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+NewLineElement" id="114" />
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="115">
                          <text dataType="String">here</text>
                        </object>
                      </elements>
                    </text>
                    <customMat />
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
                    <gameobj dataType="ObjectRef">100</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="116">
              <_items dataType="Array" type="Duality.Component[]" id="117" length="4">
                <object dataType="ObjectRef">104</object>
                <object dataType="ObjectRef">105</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">2</_version>
            </compList>
            <name dataType="String">Intro2</name>
            <active dataType="Bool">false</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">104</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="118">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="119" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="120" length="2">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">89</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="121" length="2">
                  <object dataType="Class" type="Duality.Components.Transform" id="122">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">128</X>
                      <Y dataType="Float">-272</Y>
                      <Z dataType="Float">-10</Z>
                    </pos>
                    <vel dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">-0.5</Z>
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
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel" value="3" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">128</X>
                      <Y dataType="Float">-272</Y>
                      <Z dataType="Float">-10</Z>
                    </posAbs>
                    <velAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">-0.5</Z>
                    </velAbs>
                    <angleAbs dataType="Float">0</angleAbs>
                    <angleVelAbs dataType="Float">0</angleVelAbs>
                    <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">1</X>
                      <Y dataType="Float">1</Y>
                      <Z dataType="Float">1</Z>
                    </scaleAbs>
                    <gameobj dataType="ObjectRef">118</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.TextRenderer" id="123">
                    <align dataType="Enum" type="Duality.Alignment" name="Center" value="0" />
                    <text dataType="Class" type="Duality.FormattedText" id="124">
                      <sourceText dataType="String">and play...</sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="125" length="1">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\BigFont.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">0</maxWidth>
                      <maxHeight dataType="Int">0</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String">and play...</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="126" length="1">
                        <object dataType="Int">11</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="127" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="128">
                          <text dataType="String">and play...</text>
                        </object>
                      </elements>
                    </text>
                    <customMat />
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
                    <gameobj dataType="ObjectRef">118</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="129">
              <_items dataType="Array" type="Duality.Component[]" id="130" length="4">
                <object dataType="ObjectRef">122</object>
                <object dataType="ObjectRef">123</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">2</_version>
            </compList>
            <name dataType="String">Intro3</name>
            <active dataType="Bool">false</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">122</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="131">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="132" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="133" length="2">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">15</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="134" length="2">
                  <object dataType="Class" type="Duality.Components.Transform" id="135">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">128</X>
                      <Y dataType="Float">-272</Y>
                      <Z dataType="Float">-10</Z>
                    </pos>
                    <vel dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">-0.5</Z>
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
                      <X dataType="Float">128</X>
                      <Y dataType="Float">-272</Y>
                      <Z dataType="Float">-10</Z>
                    </posAbs>
                    <velAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">-0.5</Z>
                    </velAbs>
                    <angleAbs dataType="Float">0</angleAbs>
                    <angleVelAbs dataType="Float">0</angleVelAbs>
                    <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">1</X>
                      <Y dataType="Float">1</Y>
                      <Z dataType="Float">1</Z>
                    </scaleAbs>
                    <gameobj dataType="ObjectRef">131</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="136">
                    <rect dataType="Struct" type="Duality.Rect">
                      <x dataType="Float">-115</x>
                      <y dataType="Float">-28</y>
                      <w dataType="Float">230</w>
                      <h dataType="Float">56</h>
                    </rect>
                    <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                      <contentPath dataType="String">Data\TetrisLogo.Material.res</contentPath>
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
                    <gameobj dataType="ObjectRef">131</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="137">
              <_items dataType="Array" type="Duality.Component[]" id="138" length="4">
                <object dataType="ObjectRef">135</object>
                <object dataType="ObjectRef">136</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">4</_version>
            </compList>
            <name dataType="String">Intro4</name>
            <active dataType="Bool">false</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">135</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="139">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="140" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="141" length="2">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">15</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="142" length="2">
                  <object dataType="Class" type="Duality.Components.Transform" id="143">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">42.6649323</X>
                      <Y dataType="Float">-312.417358</Y>
                      <Z dataType="Float">-1</Z>
                    </pos>
                    <vel dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </vel>
                    <angle dataType="Float">0</angle>
                    <angleVel dataType="Float">0</angleVel>
                    <scale dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">4.07332134</X>
                      <Y dataType="Float">4.07332134</Y>
                      <Z dataType="Float">4.07332134</Z>
                    </scale>
                    <deriveAngle dataType="Bool">true</deriveAngle>
                    <extUpdater />
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">42.6649323</X>
                      <Y dataType="Float">-312.417358</Y>
                      <Z dataType="Float">-1</Z>
                    </posAbs>
                    <velAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </velAbs>
                    <angleAbs dataType="Float">0</angleAbs>
                    <angleVelAbs dataType="Float">0</angleVelAbs>
                    <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">4.07332134</X>
                      <Y dataType="Float">4.07332134</Y>
                      <Z dataType="Float">4.07332134</Z>
                    </scaleAbs>
                    <gameobj dataType="ObjectRef">139</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="144">
                    <rect dataType="Struct" type="Duality.Rect">
                      <x dataType="Float">-128</x>
                      <y dataType="Float">-128</y>
                      <w dataType="Float">256</w>
                      <h dataType="Float">256</h>
                    </rect>
                    <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                      <contentPath />
                    </sharedMat>
                    <customMat dataType="Class" type="Duality.Resources.BatchInfo" id="145">
                      <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
                        <contentPath dataType="String">Default:DrawTechnique:Alpha</contentPath>
                      </technique>
                      <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                        <r dataType="Byte">255</r>
                        <g dataType="Byte">255</g>
                        <b dataType="Byte">255</b>
                        <a dataType="Byte">255</a>
                      </mainColor>
                      <textures dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.String],[Duality.ContentRef`1[[Duality.Resources.Texture]]]]" id="146" surrogate="true">
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
                      <r dataType="Byte">0</r>
                      <g dataType="Byte">0</g>
                      <b dataType="Byte">0</b>
                      <a dataType="Byte">255</a>
                    </colorTint>
                    <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                    <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                    <visibilityGroup dataType="UInt">1</visibilityGroup>
                    <gameobj dataType="ObjectRef">139</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="147">
              <_items dataType="Array" type="Duality.Component[]" id="148" length="4">
                <object dataType="ObjectRef">143</object>
                <object dataType="ObjectRef">144</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">2</_version>
            </compList>
            <name dataType="String">BlackPlate</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">143</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="149">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="150" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="151" length="2">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">15</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="152" length="2">
                  <object dataType="Class" type="Duality.Components.Transform" id="153">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">128</X>
                      <Y dataType="Float">-272</Y>
                      <Z dataType="Float">-10</Z>
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
                      <X dataType="Float">128</X>
                      <Y dataType="Float">-272</Y>
                      <Z dataType="Float">-10</Z>
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
                    <gameobj dataType="ObjectRef">149</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="154">
                    <rect dataType="Struct" type="Duality.Rect">
                      <x dataType="Float">-220</x>
                      <y dataType="Float">-53</y>
                      <w dataType="Float">441</w>
                      <h dataType="Float">106</h>
                    </rect>
                    <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                      <contentPath dataType="String">Data\GameOver.Material.res</contentPath>
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
                    <gameobj dataType="ObjectRef">149</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="155">
              <_items dataType="Array" type="Duality.Component[]" id="156" length="4">
                <object dataType="ObjectRef">153</object>
                <object dataType="ObjectRef">154</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">2</_version>
            </compList>
            <name dataType="String">GameOver</name>
            <active dataType="Bool">false</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">153</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="157">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="158" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="159" length="2">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">89</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="160" length="2">
                  <object dataType="Class" type="Duality.Components.Transform" id="161">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">207.1029</X>
                      <Y dataType="Float">-537.939148</Y>
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
                      <X dataType="Float">207.1029</X>
                      <Y dataType="Float">-537.939148</Y>
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
                    <gameobj dataType="ObjectRef">157</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.TextRenderer" id="162">
                    <align dataType="Enum" type="Duality.Alignment" name="Left" value="1" />
                    <text dataType="Class" type="Duality.FormattedText" id="163">
                      <sourceText dataType="String">Score: 0</sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="164" length="1">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\BigFont.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">0</maxWidth>
                      <maxHeight dataType="Int">0</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String">Score: 0</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="165" length="1">
                        <object dataType="Int">8</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="166" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="167">
                          <text dataType="String">Score: 0</text>
                        </object>
                      </elements>
                    </text>
                    <customMat />
                    <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                      <r dataType="Byte">103</r>
                      <g dataType="Byte">131</g>
                      <b dataType="Byte">82</b>
                      <a dataType="Byte">255</a>
                    </colorTint>
                    <iconMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                      <contentPath />
                    </iconMat>
                    <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                    <visibilityGroup dataType="UInt">1</visibilityGroup>
                    <gameobj dataType="ObjectRef">157</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="168">
              <_items dataType="Array" type="Duality.Component[]" id="169" length="4">
                <object dataType="ObjectRef">161</object>
                <object dataType="ObjectRef">162</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">2</_version>
            </compList>
            <name dataType="String">PointDisplay</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">161</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="170">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="171" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="172" length="2">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">89</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="173" length="2">
                  <object dataType="Class" type="Duality.Components.Transform" id="174">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">208.102921</X>
                      <Y dataType="Float">-445.9392</Y>
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
                      <X dataType="Float">208.102921</X>
                      <Y dataType="Float">-445.9392</Y>
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
                    <gameobj dataType="ObjectRef">170</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.TextRenderer" id="175">
                    <align dataType="Enum" type="Duality.Alignment" name="Left" value="1" />
                    <text dataType="Class" type="Duality.FormattedText" id="176">
                      <sourceText dataType="String">Commentary</sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="177" length="1">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\CommentaryFont.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">310</maxWidth>
                      <maxHeight dataType="Int">484</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String">Commentary</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="178" length="1">
                        <object dataType="Int">10</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="179" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="180">
                          <text dataType="String">Commentary</text>
                        </object>
                      </elements>
                    </text>
                    <customMat />
                    <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                      <r dataType="Byte">144</r>
                      <g dataType="Byte">144</g>
                      <b dataType="Byte">144</b>
                      <a dataType="Byte">255</a>
                    </colorTint>
                    <iconMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                      <contentPath />
                    </iconMat>
                    <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                    <visibilityGroup dataType="UInt">1</visibilityGroup>
                    <gameobj dataType="ObjectRef">170</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="181">
              <_items dataType="Array" type="Duality.Component[]" id="182" length="4">
                <object dataType="ObjectRef">174</object>
                <object dataType="ObjectRef">175</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">2</_version>
            </compList>
            <name dataType="String">Commentary</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">174</compTransform>
            <EventComponentAdded dataType="ObjectRef">29</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">32</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object />
          <object />
          <object />
        </_items>
        <_size dataType="Int">13</_size>
        <_version dataType="Int">107</_version>
      </allObj>
      <Registered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="183" multi="true">
        <object dataType="MethodInfo" id="184" value="M:Duality.Resources.Scene:objectManager_Registered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="185" length="1">
          <object dataType="ObjectRef">183</object>
        </object>
      </Registered>
      <Unregistered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="186" multi="true">
        <object dataType="MethodInfo" id="187" value="M:Duality.Resources.Scene:objectManager_Unregistered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="188" length="1">
          <object dataType="ObjectRef">186</object>
        </object>
      </Unregistered>
    </objectManager>
    <sourcePath />
  </object>
</root>