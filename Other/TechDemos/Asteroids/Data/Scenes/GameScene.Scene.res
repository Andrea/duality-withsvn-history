<?xml version="1.0" encoding="utf-8"?>
<root>
  <object dataType="Class" type="Duality.Resources.Scene" id="1">
    <globalGravity dataType="Struct" type="OpenTK.Vector2">
      <X dataType="Float">0</X>
      <Y dataType="Float">0</Y>
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
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="12">
              <_items dataType="Array" type="Duality.GameObject[]" id="13" length="4">
                <object dataType="Class" type="Duality.GameObject" id="14">
                  <prefabLink />
                  <parent dataType="ObjectRef">11</parent>
                  <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="15">
                    <_items dataType="Array" type="Duality.GameObject[]" id="16" length="4">
                      <object />
                      <object />
                      <object />
                      <object />
                    </_items>
                    <_size dataType="Int">0</_size>
                    <_version dataType="Int">2</_version>
                  </children>
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="17" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="18" length="5">
                        <object dataType="Type" id="19" value="Duality.Components.Transform" />
                        <object dataType="Type" id="20" value="Duality.Components.Camera" />
                        <object dataType="Type" id="21" value="Duality.Components.SoundListener" />
                        <object dataType="Type" id="22" value="GamePlugin.HUD" />
                        <object dataType="Type" id="23" value="GamePlugin.CamStarfield" />
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="24" length="5">
                        <object dataType="Class" type="Duality.Components.Transform" id="25">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0</X>
                            <Y dataType="Float">0</Y>
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
                          <deriveAngle dataType="Bool">false</deriveAngle>
                          <extUpdater />
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform dataType="Class" type="Duality.Components.Transform" id="26">
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
                            <extUpdater dataType="Class" type="Duality.Components.Collider" id="27">
                              <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                              <linearDamp dataType="Float">0.75</linearDamp>
                              <angularDamp dataType="Float">0</angularDamp>
                              <fixedAngle dataType="Bool">false</fixedAngle>
                              <ignoreGravity dataType="Bool">false</ignoreGravity>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="28">
                                <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="29" length="4">
                                  <object dataType="Class" type="Duality.Components.Collider+CircleShapeInfo" id="30">
                                    <radius dataType="Float">18</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">27</parent>
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
                                <_version dataType="Int">5</_version>
                              </shapes>
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
                          </parentTransform>
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0</X>
                            <Y dataType="Float">0</Y>
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
                          <gameobj dataType="ObjectRef">14</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Camera" id="31">
                          <nearZ dataType="Float">0</nearZ>
                          <farZ dataType="Float">20000</farZ>
                          <zSortAccuracy dataType="Float">500</zSortAccuracy>
                          <parallaxRefDist dataType="Float">500</parallaxRefDist>
                          <visibilityMask dataType="UInt">4294967295</visibilityMask>
                          <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <r dataType="Byte">0</r>
                            <g dataType="Byte">0</g>
                            <b dataType="Byte">0</b>
                            <a dataType="Byte">0</a>
                          </clearColor>
                          <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                          <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="32" length="1">
                            <object dataType="Class" type="Duality.Components.Camera+Pass" id="33">
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
                          <gameobj dataType="ObjectRef">14</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.SoundListener" id="34">
                          <gameobj dataType="ObjectRef">14</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.HUD" id="35">
                          <gameobj dataType="ObjectRef">14</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.CamStarfield" id="36">
                          <layerCount dataType="Int">10</layerCount>
                          <starsPerLayer dataType="Int">15</starsPerLayer>
                          <layerDepth dataType="Float">1000</layerDepth>
                          <brightness dataType="Float">1</brightness>
                          <trailLength dataType="Float">2</trailLength>
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="UInt">1</visibilityGroup>
                          <gameobj dataType="ObjectRef">14</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="37">
                    <_items dataType="Array" type="Duality.Component[]" id="38" length="8">
                      <object dataType="ObjectRef">25</object>
                      <object dataType="ObjectRef">31</object>
                      <object dataType="ObjectRef">34</object>
                      <object dataType="ObjectRef">35</object>
                      <object dataType="ObjectRef">36</object>
                      <object />
                      <object />
                      <object />
                    </_items>
                    <_size dataType="Int">5</_size>
                    <_version dataType="Int">5</_version>
                  </compList>
                  <name dataType="String">MainCamera</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">25</compTransform>
                  <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="39" multi="true">
                    <object dataType="MethodInfo" id="40" value="M:Duality.ObjectManagers.GameObjectManager:OnRegisteredObjectComponentAdded(System.Object,Duality.ComponentEventArgs)" />
                    <object dataType="ObjectRef">2</object>
                    <object dataType="Array" type="System.Delegate[]" id="41" length="1">
                      <object dataType="ObjectRef">39</object>
                    </object>
                  </EventComponentAdded>
                  <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="42" multi="true">
                    <object dataType="MethodInfo" id="43" value="M:Duality.ObjectManagers.GameObjectManager:OnRegisteredObjectComponentRemoved(System.Object,Duality.ComponentEventArgs)" />
                    <object dataType="ObjectRef">2</object>
                    <object dataType="Array" type="System.Delegate[]" id="44" length="1">
                      <object dataType="ObjectRef">42</object>
                    </object>
                  </EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object />
                <object />
                <object />
              </_items>
              <_size dataType="Int">1</_size>
              <_version dataType="Int">1</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="45" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="46" length="5">
                  <object dataType="ObjectRef">19</object>
                  <object dataType="Type" id="47" value="Duality.Components.Renderers.SpriteRenderer" />
                  <object dataType="Type" id="48" value="GamePlugin.Player" />
                  <object dataType="Type" id="49" value="Duality.Components.SoundEmitter" />
                  <object dataType="Type" id="50" value="Duality.Components.Collider" />
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="51" length="5">
                  <object dataType="ObjectRef">26</object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="52">
                    <rect dataType="Struct" type="Duality.Rect">
                      <x dataType="Float">-21</x>
                      <y dataType="Float">-24</y>
                      <w dataType="Float">43</w>
                      <h dataType="Float">38</h>
                    </rect>
                    <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                      <contentPath dataType="String">Data\Materials\PlayerShip.Material.res</contentPath>
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
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="GamePlugin.Player" id="53">
                    <weaponTimer dataType="Float">0</weaponTimer>
                    <lastCtrlDir dataType="Int">0</lastCtrlDir>
                    <powerFront dataType="Int">0</powerFront>
                    <powerDiagonal dataType="Int">0</powerDiagonal>
                    <powerSide dataType="Int">0</powerSide>
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.SoundEmitter" id="54">
                    <sources dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.SoundEmitter+Source]]" id="55">
                      <_items dataType="Array" type="Duality.Components.SoundEmitter+Source[]" id="56" length="4">
                        <object dataType="Class" type="Duality.Components.SoundEmitter+Source" id="57">
                          <disposed dataType="Bool">false</disposed>
                          <sound dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Sound]]">
                            <contentPath dataType="String">Data\Sound\Engine.Sound.res</contentPath>
                          </sound>
                          <looped dataType="Bool">true</looped>
                          <paused dataType="Bool">true</paused>
                          <volume dataType="Float">1</volume>
                          <pitch dataType="Float">1</pitch>
                          <offset dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0</X>
                            <Y dataType="Float">0</Y>
                            <Z dataType="Float">0</Z>
                          </offset>
                          <hasBeenPlayed dataType="Bool">false</hasBeenPlayed>
                        </object>
                        <object />
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">1</_size>
                      <_version dataType="Int">1</_version>
                    </sources>
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">27</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="58">
              <_items dataType="Array" type="Duality.Component[]" id="59" length="8">
                <object dataType="ObjectRef">26</object>
                <object dataType="ObjectRef">52</object>
                <object dataType="ObjectRef">53</object>
                <object dataType="ObjectRef">54</object>
                <object dataType="ObjectRef">27</object>
                <object />
                <object />
                <object />
              </_items>
              <_size dataType="Int">5</_size>
              <_version dataType="Int">5</_version>
            </compList>
            <name dataType="String">Player</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">26</compTransform>
            <EventComponentAdded dataType="ObjectRef">39</EventComponentAdded>
            <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="60" multi="true">
              <object dataType="ObjectRef">43</object>
              <object dataType="ObjectRef">2</object>
              <object dataType="Array" type="System.Delegate[]" id="61" length="2">
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="62" multi="true">
                  <object dataType="MethodInfo" id="63" value="M:Duality.Components.Transform:Parent_EventComponentRemoving(System.Object,Duality.ComponentEventArgs)" />
                  <object dataType="ObjectRef">25</object>
                  <object dataType="Array" type="System.Delegate[]" id="64" length="1">
                    <object dataType="ObjectRef">62</object>
                  </object>
                </object>
                <object dataType="ObjectRef">42</object>
              </object>
            </EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="ObjectRef">14</object>
          <object dataType="Class" type="Duality.GameObject" id="65">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="66" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="67" length="2">
                  <object dataType="ObjectRef">19</object>
                  <object dataType="ObjectRef">47</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="68" length="2">
                  <object dataType="Class" type="Duality.Components.Transform" id="69">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">10000</Z>
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
                      <Z dataType="Float">10000</Z>
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
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="70">
                    <rect dataType="Struct" type="Duality.Rect">
                      <x dataType="Float">-1024</x>
                      <y dataType="Float">-1024</y>
                      <w dataType="Float">2048</w>
                      <h dataType="Float">2048</h>
                    </rect>
                    <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                      <contentPath dataType="String">Data\Materials\SpaceBg.Material.res</contentPath>
                    </sharedMat>
                    <customMat />
                    <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                      <r dataType="Byte">255</r>
                      <g dataType="Byte">255</g>
                      <b dataType="Byte">255</b>
                      <a dataType="Byte">255</a>
                    </colorTint>
                    <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="WrapBoth" value="3" />
                    <renderFlags dataType="Enum" type="Duality.RendererFlags" name="ParallaxPos" value="1" />
                    <visibilityGroup dataType="UInt">1</visibilityGroup>
                    <gameobj dataType="ObjectRef">65</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="71">
              <_items dataType="Array" type="Duality.Component[]" id="72" length="4">
                <object dataType="ObjectRef">69</object>
                <object dataType="ObjectRef">70</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">2</_version>
            </compList>
            <name dataType="String">SpaceBg</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">69</compTransform>
            <EventComponentAdded dataType="ObjectRef">39</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">42</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="73">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="74" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="75" length="1">
                  <object dataType="Type" id="76" value="GamePlugin.GameSceneController" />
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="77" length="1">
                  <object dataType="Class" type="GamePlugin.GameSceneController" id="78">
                    <currentLevel dataType="Int">0</currentLevel>
                    <currentLevelBeginTime dataType="Float">0</currentLevelBeginTime>
                    <asteroidTimer dataType="Float">0</asteroidTimer>
                    <gameobj dataType="ObjectRef">73</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="79">
              <_items dataType="Array" type="Duality.Component[]" id="80" length="4">
                <object dataType="ObjectRef">78</object>
                <object />
                <object />
                <object />
              </_items>
              <_size dataType="Int">1</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">LevelManager</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="ObjectRef">39</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">42</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="81">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="82" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="83" length="3">
                  <object dataType="ObjectRef">19</object>
                  <object dataType="ObjectRef">47</object>
                  <object dataType="ObjectRef">50</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="84" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="85">
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
                    <extUpdater dataType="Class" type="Duality.Components.Collider" id="86">
                      <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                      <linearDamp dataType="Float">0</linearDamp>
                      <angularDamp dataType="Float">0</angularDamp>
                      <fixedAngle dataType="Bool">false</fixedAngle>
                      <ignoreGravity dataType="Bool">false</ignoreGravity>
                      <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                      <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="87">
                        <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="88" length="4">
                          <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="89">
                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="90" length="4">
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
                            <parent dataType="ObjectRef">86</parent>
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
                      <gameobj dataType="ObjectRef">81</gameobj>
                      <disposed dataType="Bool">false</disposed>
                      <active dataType="Bool">true</active>
                    </extUpdater>
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
                    <gameobj dataType="ObjectRef">81</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="91">
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
                    <gameobj dataType="ObjectRef">81</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">86</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="92">
              <_items dataType="Array" type="Duality.Component[]" id="93" length="4">
                <object dataType="ObjectRef">85</object>
                <object dataType="ObjectRef">91</object>
                <object dataType="ObjectRef">86</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">7</_version>
            </compList>
            <name dataType="String">Wall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">85</compTransform>
            <EventComponentAdded dataType="ObjectRef">39</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">42</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="94">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="95" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="96" length="3">
                  <object dataType="ObjectRef">19</object>
                  <object dataType="ObjectRef">47</object>
                  <object dataType="ObjectRef">50</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="97" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="98">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">-528</Y>
                      <Z dataType="Float">0</Z>
                    </pos>
                    <vel dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </vel>
                    <angle dataType="Float">3.14159274</angle>
                    <angleVel dataType="Float">0</angleVel>
                    <scale dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">1</X>
                      <Y dataType="Float">1</Y>
                      <Z dataType="Float">1</Z>
                    </scale>
                    <deriveAngle dataType="Bool">true</deriveAngle>
                    <extUpdater dataType="Class" type="Duality.Components.Collider" id="99">
                      <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                      <linearDamp dataType="Float">0</linearDamp>
                      <angularDamp dataType="Float">0</angularDamp>
                      <fixedAngle dataType="Bool">false</fixedAngle>
                      <ignoreGravity dataType="Bool">false</ignoreGravity>
                      <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                      <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="100">
                        <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="101" length="4">
                          <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="102">
                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="103" length="4">
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
                            <parent dataType="ObjectRef">99</parent>
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
                      <gameobj dataType="ObjectRef">94</gameobj>
                      <disposed dataType="Bool">false</disposed>
                      <active dataType="Bool">true</active>
                    </extUpdater>
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">-528</Y>
                      <Z dataType="Float">0</Z>
                    </posAbs>
                    <velAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </velAbs>
                    <angleAbs dataType="Float">3.14159274</angleAbs>
                    <angleVelAbs dataType="Float">0</angleVelAbs>
                    <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">1</X>
                      <Y dataType="Float">1</Y>
                      <Z dataType="Float">1</Z>
                    </scaleAbs>
                    <gameobj dataType="ObjectRef">94</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="104">
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
                    <gameobj dataType="ObjectRef">94</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">99</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="105">
              <_items dataType="Array" type="Duality.Component[]" id="106" length="4">
                <object dataType="ObjectRef">98</object>
                <object dataType="ObjectRef">104</object>
                <object dataType="ObjectRef">99</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">5</_version>
            </compList>
            <name dataType="String">Wall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">98</compTransform>
            <EventComponentAdded dataType="ObjectRef">39</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">42</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="107">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="108" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="109" length="3">
                  <object dataType="ObjectRef">19</object>
                  <object dataType="ObjectRef">47</object>
                  <object dataType="ObjectRef">50</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="110" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="111">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">-528</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </pos>
                    <vel dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </vel>
                    <angle dataType="Float">1.57079637</angle>
                    <angleVel dataType="Float">0</angleVel>
                    <scale dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">1</X>
                      <Y dataType="Float">1</Y>
                      <Z dataType="Float">1</Z>
                    </scale>
                    <deriveAngle dataType="Bool">true</deriveAngle>
                    <extUpdater dataType="Class" type="Duality.Components.Collider" id="112">
                      <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                      <linearDamp dataType="Float">0</linearDamp>
                      <angularDamp dataType="Float">0</angularDamp>
                      <fixedAngle dataType="Bool">false</fixedAngle>
                      <ignoreGravity dataType="Bool">false</ignoreGravity>
                      <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                      <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="113">
                        <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="114" length="4">
                          <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="115">
                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="116" length="4">
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
                            <parent dataType="ObjectRef">112</parent>
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
                      <gameobj dataType="ObjectRef">107</gameobj>
                      <disposed dataType="Bool">false</disposed>
                      <active dataType="Bool">true</active>
                    </extUpdater>
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">-528</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </posAbs>
                    <velAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </velAbs>
                    <angleAbs dataType="Float">1.57079637</angleAbs>
                    <angleVelAbs dataType="Float">0</angleVelAbs>
                    <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">1</X>
                      <Y dataType="Float">1</Y>
                      <Z dataType="Float">1</Z>
                    </scaleAbs>
                    <gameobj dataType="ObjectRef">107</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="117">
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
                    <gameobj dataType="ObjectRef">107</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">112</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="118">
              <_items dataType="Array" type="Duality.Component[]" id="119" length="4">
                <object dataType="ObjectRef">111</object>
                <object dataType="ObjectRef">117</object>
                <object dataType="ObjectRef">112</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">5</_version>
            </compList>
            <name dataType="String">Wall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">111</compTransform>
            <EventComponentAdded dataType="ObjectRef">39</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">42</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="120">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="121" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="122" length="3">
                  <object dataType="ObjectRef">19</object>
                  <object dataType="ObjectRef">47</object>
                  <object dataType="ObjectRef">50</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="123" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="124">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">528</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </pos>
                    <vel dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </vel>
                    <angle dataType="Float">4.712389</angle>
                    <angleVel dataType="Float">0</angleVel>
                    <scale dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">1</X>
                      <Y dataType="Float">1</Y>
                      <Z dataType="Float">1</Z>
                    </scale>
                    <deriveAngle dataType="Bool">true</deriveAngle>
                    <extUpdater dataType="Class" type="Duality.Components.Collider" id="125">
                      <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                      <linearDamp dataType="Float">0</linearDamp>
                      <angularDamp dataType="Float">0</angularDamp>
                      <fixedAngle dataType="Bool">false</fixedAngle>
                      <ignoreGravity dataType="Bool">false</ignoreGravity>
                      <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                      <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="126">
                        <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="127" length="4">
                          <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="128">
                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="129" length="4">
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
                            <parent dataType="ObjectRef">125</parent>
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
                      <gameobj dataType="ObjectRef">120</gameobj>
                      <disposed dataType="Bool">false</disposed>
                      <active dataType="Bool">true</active>
                    </extUpdater>
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">528</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </posAbs>
                    <velAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </velAbs>
                    <angleAbs dataType="Float">4.712389</angleAbs>
                    <angleVelAbs dataType="Float">0</angleVelAbs>
                    <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">1</X>
                      <Y dataType="Float">1</Y>
                      <Z dataType="Float">1</Z>
                    </scaleAbs>
                    <gameobj dataType="ObjectRef">120</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="130">
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
                    <gameobj dataType="ObjectRef">120</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">125</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="131">
              <_items dataType="Array" type="Duality.Component[]" id="132" length="4">
                <object dataType="ObjectRef">124</object>
                <object dataType="ObjectRef">130</object>
                <object dataType="ObjectRef">125</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">5</_version>
            </compList>
            <name dataType="String">Wall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">124</compTransform>
            <EventComponentAdded dataType="ObjectRef">39</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">42</EventComponentRemoving>
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
        </_items>
        <_size dataType="Int">8</_size>
        <_version dataType="Int">50</_version>
      </allObj>
      <Registered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="133" multi="true">
        <object dataType="MethodInfo" id="134" value="M:Duality.Resources.Scene:objectManager_Registered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="135" length="1">
          <object dataType="ObjectRef">133</object>
        </object>
      </Registered>
      <Unregistered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="136" multi="true">
        <object dataType="MethodInfo" id="137" value="M:Duality.Resources.Scene:objectManager_Unregistered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="138" length="1">
          <object dataType="ObjectRef">136</object>
        </object>
      </Unregistered>
    </objectManager>
  </object>
</root>