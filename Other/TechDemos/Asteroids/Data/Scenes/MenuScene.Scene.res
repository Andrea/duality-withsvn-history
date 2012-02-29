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
        <_items dataType="Array" type="Duality.GameObject[]" id="10" length="512">
          <object dataType="Class" type="Duality.GameObject" id="11">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="12" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="13" length="4">
                  <object dataType="Type" id="14" value="Duality.Components.Transform" />
                  <object dataType="Type" id="15" value="Duality.Components.Camera" />
                  <object dataType="Type" id="16" value="Duality.Components.SoundListener" />
                  <object dataType="Type" id="17" value="GamePlugin.CamStarfield" />
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="18" length="4">
                  <object dataType="Class" type="Duality.Components.Transform" id="19">
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
                    <deriveAngle dataType="Bool">true</deriveAngle>
                    <extUpdater />
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                    <parentTransform />
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
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Camera" id="20">
                    <nearZ dataType="Float">0</nearZ>
                    <farZ dataType="Float">10000</farZ>
                    <zSortAccuracy dataType="Float">1000</zSortAccuracy>
                    <parallaxRefDist dataType="Float">500</parallaxRefDist>
                    <visibilityMask dataType="UInt">4294967295</visibilityMask>
                    <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                      <r dataType="Byte">11</r>
                      <g dataType="Byte">15</g>
                      <b dataType="Byte">26</b>
                      <a dataType="Byte">255</a>
                    </clearColor>
                    <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                    <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="21" length="1">
                      <object dataType="Class" type="Duality.Components.Camera+Pass" id="22">
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
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.SoundListener" id="23">
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="GamePlugin.CamStarfield" id="24">
                    <layerCount dataType="Int">10</layerCount>
                    <starsPerLayer dataType="Int">50</starsPerLayer>
                    <layerDepth dataType="Float">500</layerDepth>
                    <brightness dataType="Float">1</brightness>
                    <trailLength dataType="Float">5</trailLength>
                    <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                    <visibilityGroup dataType="UInt">1</visibilityGroup>
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="25">
              <_items dataType="Array" type="Duality.Component[]" id="26" length="4">
                <object dataType="ObjectRef">19</object>
                <object dataType="ObjectRef">20</object>
                <object dataType="ObjectRef">23</object>
                <object dataType="ObjectRef">24</object>
              </_items>
              <_size dataType="Int">4</_size>
              <_version dataType="Int">4</_version>
            </compList>
            <name dataType="String">MainCamera</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">19</compTransform>
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="27" multi="true">
              <object dataType="MethodInfo" id="28" value="M:Duality.ObjectManagers.GameObjectManager:OnRegisteredObjectComponentAdded(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="29">
                <RegisteredObjectComponentAdded />
                <RegisteredObjectComponentRemoved />
                <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="30">
                  <_items dataType="Array" type="Duality.GameObject[]" id="31" length="4">
                    <object dataType="Class" type="Duality.GameObject" id="32">
                      <prefabLink />
                      <parent />
                      <children />
                      <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="33" surrogate="true">
                        <customSerialIO />
                        <customSerialIO>
                          <keys dataType="Array" type="System.Type[]" id="34" length="3">
                            <object dataType="ObjectRef">14</object>
                            <object dataType="ObjectRef">16</object>
                            <object dataType="ObjectRef">15</object>
                          </keys>
                          <values dataType="Array" type="Duality.Component[]" id="35" length="3">
                            <object dataType="Class" type="Duality.Components.Transform" id="36">
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
                              <deriveAngle dataType="Bool">true</deriveAngle>
                              <extUpdater />
                              <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                              <parentTransform />
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
                              <gameobj dataType="ObjectRef">32</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Class" type="Duality.Components.SoundListener" id="37">
                              <gameobj dataType="ObjectRef">32</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Class" type="Duality.Components.Camera" id="38">
                              <nearZ dataType="Float">0</nearZ>
                              <farZ dataType="Float">100000</farZ>
                              <zSortAccuracy dataType="Float">100</zSortAccuracy>
                              <parallaxRefDist dataType="Float">500</parallaxRefDist>
                              <visibilityMask dataType="UInt">4294967295</visibilityMask>
                              <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                <r dataType="Byte">38</r>
                                <g dataType="Byte">24</g>
                                <b dataType="Byte">35</b>
                                <a dataType="Byte">0</a>
                              </clearColor>
                              <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                              <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="39" length="1">
                                <object dataType="Class" type="Duality.Components.Camera+Pass" id="40">
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
                              <gameobj dataType="ObjectRef">32</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                          </values>
                        </customSerialIO>
                      </compMap>
                      <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="41">
                        <_items dataType="Array" type="Duality.Component[]" id="42" length="4">
                          <object dataType="ObjectRef">36</object>
                          <object dataType="ObjectRef">37</object>
                          <object dataType="ObjectRef">38</object>
                          <object />
                        </_items>
                        <_size dataType="Int">3</_size>
                        <_version dataType="Int">3</_version>
                      </compList>
                      <name dataType="String">CamView Camera 0</name>
                      <active dataType="Bool">true</active>
                      <disposed dataType="Bool">false</disposed>
                      <compTransform dataType="ObjectRef">36</compTransform>
                      <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="43" multi="true">
                        <object dataType="ObjectRef">28</object>
                        <object dataType="ObjectRef">29</object>
                        <object dataType="Array" type="System.Delegate[]" id="44" length="1">
                          <object dataType="ObjectRef">43</object>
                        </object>
                      </EventComponentAdded>
                      <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="45" multi="true">
                        <object dataType="MethodInfo" id="46" value="M:Duality.ObjectManagers.GameObjectManager:OnRegisteredObjectComponentRemoved(System.Object,Duality.ComponentEventArgs)" />
                        <object dataType="ObjectRef">29</object>
                        <object dataType="Array" type="System.Delegate[]" id="47" length="1">
                          <object dataType="ObjectRef">45</object>
                        </object>
                      </EventComponentRemoving>
                      <EventCollisionBegin />
                      <EventCollisionEnd />
                      <EventCollisionSolve />
                    </object>
                    <object dataType="ObjectRef">11</object>
                    <object />
                    <object />
                  </_items>
                  <_size dataType="Int">2</_size>
                  <_version dataType="Int">4</_version>
                </allObj>
                <Registered />
                <Unregistered />
              </object>
              <object dataType="Array" type="System.Delegate[]" id="48" length="25">
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="49" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="ObjectRef">2</object>
                  <object dataType="Array" type="System.Delegate[]" id="50" length="1">
                    <object dataType="ObjectRef">49</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="51" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="52">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="53">
                      <_items dataType="Array" type="Duality.GameObject[]" id="54" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="55">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="56" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="57" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="58" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="59">
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
                                  <deriveAngle dataType="Bool">true</deriveAngle>
                                  <extUpdater />
                                  <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                                  <parentTransform />
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
                                  <gameobj dataType="ObjectRef">55</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="60">
                                  <gameobj dataType="ObjectRef">55</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="61">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">0</r>
                                    <g dataType="Byte">0</g>
                                    <b dataType="Byte">0</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="62" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="63">
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
                                  <gameobj dataType="ObjectRef">55</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="64">
                            <_items dataType="Array" type="Duality.Component[]" id="65" length="4">
                              <object dataType="ObjectRef">59</object>
                              <object dataType="ObjectRef">60</object>
                              <object dataType="ObjectRef">61</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">59</compTransform>
                          <EventComponentAdded dataType="ObjectRef">51</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="66" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">52</object>
                            <object dataType="Array" type="System.Delegate[]" id="67" length="1">
                              <object dataType="ObjectRef">66</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">2</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="68" length="1">
                    <object dataType="ObjectRef">51</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="69" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="70">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="71">
                      <_items dataType="Array" type="Duality.GameObject[]" id="72" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="73">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="74" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="75" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="76" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="77">
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
                                  <deriveAngle dataType="Bool">true</deriveAngle>
                                  <extUpdater />
                                  <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                                  <parentTransform />
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
                                  <gameobj dataType="ObjectRef">73</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="78">
                                  <gameobj dataType="ObjectRef">73</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="79">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">0</r>
                                    <g dataType="Byte">0</g>
                                    <b dataType="Byte">0</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="80" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="81">
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
                                  <gameobj dataType="ObjectRef">73</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="82">
                            <_items dataType="Array" type="Duality.Component[]" id="83" length="4">
                              <object dataType="ObjectRef">77</object>
                              <object dataType="ObjectRef">78</object>
                              <object dataType="ObjectRef">79</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">77</compTransform>
                          <EventComponentAdded dataType="ObjectRef">69</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="84" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">70</object>
                            <object dataType="Array" type="System.Delegate[]" id="85" length="1">
                              <object dataType="ObjectRef">84</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">4</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="86" length="1">
                    <object dataType="ObjectRef">69</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="87" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="88">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="89">
                      <_items dataType="Array" type="Duality.GameObject[]" id="90" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="91">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="92" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="93" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="94" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="95">
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
                                  <deriveAngle dataType="Bool">true</deriveAngle>
                                  <extUpdater />
                                  <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                                  <parentTransform />
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
                                  <gameobj dataType="ObjectRef">91</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="96">
                                  <gameobj dataType="ObjectRef">91</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="97">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">0</r>
                                    <g dataType="Byte">0</g>
                                    <b dataType="Byte">0</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="98" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="99">
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
                                  <gameobj dataType="ObjectRef">91</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="100">
                            <_items dataType="Array" type="Duality.Component[]" id="101" length="4">
                              <object dataType="ObjectRef">95</object>
                              <object dataType="ObjectRef">96</object>
                              <object dataType="ObjectRef">97</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">95</compTransform>
                          <EventComponentAdded dataType="ObjectRef">87</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="102" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">88</object>
                            <object dataType="Array" type="System.Delegate[]" id="103" length="1">
                              <object dataType="ObjectRef">102</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">6</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="104" length="1">
                    <object dataType="ObjectRef">87</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="105" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="106">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="107">
                      <_items dataType="Array" type="Duality.GameObject[]" id="108" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="109">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="110" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="111" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="112" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="113">
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
                                  <deriveAngle dataType="Bool">true</deriveAngle>
                                  <extUpdater />
                                  <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                                  <parentTransform />
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
                                  <gameobj dataType="ObjectRef">109</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="114">
                                  <gameobj dataType="ObjectRef">109</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="115">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">0</r>
                                    <g dataType="Byte">0</g>
                                    <b dataType="Byte">0</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="116" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="117">
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
                                  <gameobj dataType="ObjectRef">109</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="118">
                            <_items dataType="Array" type="Duality.Component[]" id="119" length="4">
                              <object dataType="ObjectRef">113</object>
                              <object dataType="ObjectRef">114</object>
                              <object dataType="ObjectRef">115</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">113</compTransform>
                          <EventComponentAdded dataType="ObjectRef">105</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="120" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">106</object>
                            <object dataType="Array" type="System.Delegate[]" id="121" length="1">
                              <object dataType="ObjectRef">120</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">8</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="122" length="1">
                    <object dataType="ObjectRef">105</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="123" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="124">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="125">
                      <_items dataType="Array" type="Duality.GameObject[]" id="126" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="127">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="128" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="129" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="130" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="131">
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
                                  <deriveAngle dataType="Bool">true</deriveAngle>
                                  <extUpdater />
                                  <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                                  <parentTransform />
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
                                  <gameobj dataType="ObjectRef">127</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="132">
                                  <gameobj dataType="ObjectRef">127</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="133">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">0</r>
                                    <g dataType="Byte">0</g>
                                    <b dataType="Byte">0</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="134" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="135">
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
                                  <gameobj dataType="ObjectRef">127</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="136">
                            <_items dataType="Array" type="Duality.Component[]" id="137" length="4">
                              <object dataType="ObjectRef">131</object>
                              <object dataType="ObjectRef">132</object>
                              <object dataType="ObjectRef">133</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">131</compTransform>
                          <EventComponentAdded dataType="ObjectRef">123</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="138" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">124</object>
                            <object dataType="Array" type="System.Delegate[]" id="139" length="1">
                              <object dataType="ObjectRef">138</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">10</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="140" length="1">
                    <object dataType="ObjectRef">123</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="141" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="142">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="143">
                      <_items dataType="Array" type="Duality.GameObject[]" id="144" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="145">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="146" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="147" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="148" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="149">
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
                                  <deriveAngle dataType="Bool">true</deriveAngle>
                                  <extUpdater />
                                  <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                                  <parentTransform />
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
                                  <gameobj dataType="ObjectRef">145</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="150">
                                  <gameobj dataType="ObjectRef">145</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="151">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">0</r>
                                    <g dataType="Byte">0</g>
                                    <b dataType="Byte">0</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="152" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="153">
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
                                  <gameobj dataType="ObjectRef">145</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="154">
                            <_items dataType="Array" type="Duality.Component[]" id="155" length="4">
                              <object dataType="ObjectRef">149</object>
                              <object dataType="ObjectRef">150</object>
                              <object dataType="ObjectRef">151</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">149</compTransform>
                          <EventComponentAdded dataType="ObjectRef">141</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="156" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">142</object>
                            <object dataType="Array" type="System.Delegate[]" id="157" length="1">
                              <object dataType="ObjectRef">156</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">12</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="158" length="1">
                    <object dataType="ObjectRef">141</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="159" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="160">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="161">
                      <_items dataType="Array" type="Duality.GameObject[]" id="162" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="163">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="164" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="165" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="166" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="167">
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
                                  <deriveAngle dataType="Bool">true</deriveAngle>
                                  <extUpdater />
                                  <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                                  <parentTransform />
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
                                  <gameobj dataType="ObjectRef">163</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="168">
                                  <gameobj dataType="ObjectRef">163</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="169">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">0</r>
                                    <g dataType="Byte">0</g>
                                    <b dataType="Byte">0</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="170" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="171">
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
                                  <gameobj dataType="ObjectRef">163</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="172">
                            <_items dataType="Array" type="Duality.Component[]" id="173" length="4">
                              <object dataType="ObjectRef">167</object>
                              <object dataType="ObjectRef">168</object>
                              <object dataType="ObjectRef">169</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">167</compTransform>
                          <EventComponentAdded dataType="ObjectRef">159</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="174" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">160</object>
                            <object dataType="Array" type="System.Delegate[]" id="175" length="1">
                              <object dataType="ObjectRef">174</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">14</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="176" length="1">
                    <object dataType="ObjectRef">159</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="177" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="178">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="179">
                      <_items dataType="Array" type="Duality.GameObject[]" id="180" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="181">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="182" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="183" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="184" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="185">
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
                                  <deriveAngle dataType="Bool">true</deriveAngle>
                                  <extUpdater />
                                  <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                                  <parentTransform />
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
                                  <gameobj dataType="ObjectRef">181</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="186">
                                  <gameobj dataType="ObjectRef">181</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="187">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">0</r>
                                    <g dataType="Byte">0</g>
                                    <b dataType="Byte">0</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="188" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="189">
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
                                  <gameobj dataType="ObjectRef">181</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="190">
                            <_items dataType="Array" type="Duality.Component[]" id="191" length="4">
                              <object dataType="ObjectRef">185</object>
                              <object dataType="ObjectRef">186</object>
                              <object dataType="ObjectRef">187</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">185</compTransform>
                          <EventComponentAdded dataType="ObjectRef">177</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="192" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">178</object>
                            <object dataType="Array" type="System.Delegate[]" id="193" length="1">
                              <object dataType="ObjectRef">192</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">16</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="194" length="1">
                    <object dataType="ObjectRef">177</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="195" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="196">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="197">
                      <_items dataType="Array" type="Duality.GameObject[]" id="198" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="199">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="200" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="201" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="202" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="203">
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
                                  <deriveAngle dataType="Bool">true</deriveAngle>
                                  <extUpdater />
                                  <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                                  <parentTransform />
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
                                  <gameobj dataType="ObjectRef">199</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="204">
                                  <gameobj dataType="ObjectRef">199</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="205">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">0</r>
                                    <g dataType="Byte">0</g>
                                    <b dataType="Byte">0</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="206" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="207">
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
                                  <gameobj dataType="ObjectRef">199</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="208">
                            <_items dataType="Array" type="Duality.Component[]" id="209" length="4">
                              <object dataType="ObjectRef">203</object>
                              <object dataType="ObjectRef">204</object>
                              <object dataType="ObjectRef">205</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">203</compTransform>
                          <EventComponentAdded dataType="ObjectRef">195</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="210" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">196</object>
                            <object dataType="Array" type="System.Delegate[]" id="211" length="1">
                              <object dataType="ObjectRef">210</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">18</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="212" length="1">
                    <object dataType="ObjectRef">195</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="213" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="214">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="215">
                      <_items dataType="Array" type="Duality.GameObject[]" id="216" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="217">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="218" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="219" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="220" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="221">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">-57.5866623</X>
                                    <Y dataType="Float">-20.4692745</Y>
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
                                    <X dataType="Float">-57.5866623</X>
                                    <Y dataType="Float">-20.4692745</Y>
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
                                  <gameobj dataType="ObjectRef">217</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="222">
                                  <gameobj dataType="ObjectRef">217</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="223">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">0</r>
                                    <g dataType="Byte">0</g>
                                    <b dataType="Byte">0</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="224" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="225">
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
                                  <gameobj dataType="ObjectRef">217</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="226">
                            <_items dataType="Array" type="Duality.Component[]" id="227" length="4">
                              <object dataType="ObjectRef">221</object>
                              <object dataType="ObjectRef">222</object>
                              <object dataType="ObjectRef">223</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">221</compTransform>
                          <EventComponentAdded dataType="ObjectRef">213</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="228" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">214</object>
                            <object dataType="Array" type="System.Delegate[]" id="229" length="1">
                              <object dataType="ObjectRef">228</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">20</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="230" length="1">
                    <object dataType="ObjectRef">213</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="231" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="232">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="233">
                      <_items dataType="Array" type="Duality.GameObject[]" id="234" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="235">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="236" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="237" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="238" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="239">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">-57.5866623</X>
                                    <Y dataType="Float">-20.4692745</Y>
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
                                    <X dataType="Float">-57.5866623</X>
                                    <Y dataType="Float">-20.4692745</Y>
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
                                  <gameobj dataType="ObjectRef">235</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="240">
                                  <gameobj dataType="ObjectRef">235</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="241">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">0</r>
                                    <g dataType="Byte">0</g>
                                    <b dataType="Byte">0</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="242" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="243">
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
                                  <gameobj dataType="ObjectRef">235</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="244">
                            <_items dataType="Array" type="Duality.Component[]" id="245" length="4">
                              <object dataType="ObjectRef">239</object>
                              <object dataType="ObjectRef">240</object>
                              <object dataType="ObjectRef">241</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">239</compTransform>
                          <EventComponentAdded dataType="ObjectRef">231</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="246" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">232</object>
                            <object dataType="Array" type="System.Delegate[]" id="247" length="1">
                              <object dataType="ObjectRef">246</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">22</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="248" length="1">
                    <object dataType="ObjectRef">231</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="249" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="250">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="251">
                      <_items dataType="Array" type="Duality.GameObject[]" id="252" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="253">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="254" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="255" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="256" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="257">
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
                                  <deriveAngle dataType="Bool">true</deriveAngle>
                                  <extUpdater />
                                  <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                                  <parentTransform />
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
                                  <gameobj dataType="ObjectRef">253</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="258">
                                  <gameobj dataType="ObjectRef">253</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="259">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">0</r>
                                    <g dataType="Byte">0</g>
                                    <b dataType="Byte">0</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="260" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="261">
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
                                  <gameobj dataType="ObjectRef">253</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="262">
                            <_items dataType="Array" type="Duality.Component[]" id="263" length="4">
                              <object dataType="ObjectRef">257</object>
                              <object dataType="ObjectRef">258</object>
                              <object dataType="ObjectRef">259</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">257</compTransform>
                          <EventComponentAdded dataType="ObjectRef">249</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="264" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">250</object>
                            <object dataType="Array" type="System.Delegate[]" id="265" length="1">
                              <object dataType="ObjectRef">264</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">2</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="266" length="1">
                    <object dataType="ObjectRef">249</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="267" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="268">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="269">
                      <_items dataType="Array" type="Duality.GameObject[]" id="270" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="271">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="272" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="273" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="274" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="275">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                  <gameobj dataType="ObjectRef">271</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="276">
                                  <gameobj dataType="ObjectRef">271</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="277">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">38</r>
                                    <g dataType="Byte">24</g>
                                    <b dataType="Byte">35</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="278" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="279">
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
                                  <gameobj dataType="ObjectRef">271</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="280">
                            <_items dataType="Array" type="Duality.Component[]" id="281" length="4">
                              <object dataType="ObjectRef">275</object>
                              <object dataType="ObjectRef">276</object>
                              <object dataType="ObjectRef">277</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">275</compTransform>
                          <EventComponentAdded dataType="ObjectRef">267</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="282" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">268</object>
                            <object dataType="Array" type="System.Delegate[]" id="283" length="1">
                              <object dataType="ObjectRef">282</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">4</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="284" length="1">
                    <object dataType="ObjectRef">267</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="285" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="286">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="287">
                      <_items dataType="Array" type="Duality.GameObject[]" id="288" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="289">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="290" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="291" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="292" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="293">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                  <gameobj dataType="ObjectRef">289</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="294">
                                  <gameobj dataType="ObjectRef">289</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="295">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">38</r>
                                    <g dataType="Byte">24</g>
                                    <b dataType="Byte">35</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="296" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="297">
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
                                  <gameobj dataType="ObjectRef">289</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="298">
                            <_items dataType="Array" type="Duality.Component[]" id="299" length="4">
                              <object dataType="ObjectRef">293</object>
                              <object dataType="ObjectRef">294</object>
                              <object dataType="ObjectRef">295</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">293</compTransform>
                          <EventComponentAdded dataType="ObjectRef">285</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="300" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">286</object>
                            <object dataType="Array" type="System.Delegate[]" id="301" length="1">
                              <object dataType="ObjectRef">300</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">6</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="302" length="1">
                    <object dataType="ObjectRef">285</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="303" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="304">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="305">
                      <_items dataType="Array" type="Duality.GameObject[]" id="306" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="307">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="308" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="309" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="310" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="311">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                  <gameobj dataType="ObjectRef">307</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="312">
                                  <gameobj dataType="ObjectRef">307</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="313">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">38</r>
                                    <g dataType="Byte">24</g>
                                    <b dataType="Byte">35</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="314" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="315">
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
                                  <gameobj dataType="ObjectRef">307</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="316">
                            <_items dataType="Array" type="Duality.Component[]" id="317" length="4">
                              <object dataType="ObjectRef">311</object>
                              <object dataType="ObjectRef">312</object>
                              <object dataType="ObjectRef">313</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">311</compTransform>
                          <EventComponentAdded dataType="ObjectRef">303</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="318" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">304</object>
                            <object dataType="Array" type="System.Delegate[]" id="319" length="1">
                              <object dataType="ObjectRef">318</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">8</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="320" length="1">
                    <object dataType="ObjectRef">303</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="321" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="322">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="323">
                      <_items dataType="Array" type="Duality.GameObject[]" id="324" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="325">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="326" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="327" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="328" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="329">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                  <gameobj dataType="ObjectRef">325</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="330">
                                  <gameobj dataType="ObjectRef">325</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="331">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">38</r>
                                    <g dataType="Byte">24</g>
                                    <b dataType="Byte">35</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="332" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="333">
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
                                  <gameobj dataType="ObjectRef">325</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="334">
                            <_items dataType="Array" type="Duality.Component[]" id="335" length="4">
                              <object dataType="ObjectRef">329</object>
                              <object dataType="ObjectRef">330</object>
                              <object dataType="ObjectRef">331</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">329</compTransform>
                          <EventComponentAdded dataType="ObjectRef">321</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="336" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">322</object>
                            <object dataType="Array" type="System.Delegate[]" id="337" length="1">
                              <object dataType="ObjectRef">336</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">10</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="338" length="1">
                    <object dataType="ObjectRef">321</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="339" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="340">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="341">
                      <_items dataType="Array" type="Duality.GameObject[]" id="342" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="343">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="344" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="345" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="346" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="347">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                  <gameobj dataType="ObjectRef">343</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="348">
                                  <gameobj dataType="ObjectRef">343</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="349">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">38</r>
                                    <g dataType="Byte">24</g>
                                    <b dataType="Byte">35</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="350" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="351">
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
                                  <gameobj dataType="ObjectRef">343</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="352">
                            <_items dataType="Array" type="Duality.Component[]" id="353" length="4">
                              <object dataType="ObjectRef">347</object>
                              <object dataType="ObjectRef">348</object>
                              <object dataType="ObjectRef">349</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">347</compTransform>
                          <EventComponentAdded dataType="ObjectRef">339</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="354" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">340</object>
                            <object dataType="Array" type="System.Delegate[]" id="355" length="1">
                              <object dataType="ObjectRef">354</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">12</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="356" length="1">
                    <object dataType="ObjectRef">339</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="357" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="358">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="359">
                      <_items dataType="Array" type="Duality.GameObject[]" id="360" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="361">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="362" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="363" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="364" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="365">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                  <gameobj dataType="ObjectRef">361</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="366">
                                  <gameobj dataType="ObjectRef">361</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="367">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">38</r>
                                    <g dataType="Byte">24</g>
                                    <b dataType="Byte">35</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="368" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="369">
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
                                  <gameobj dataType="ObjectRef">361</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="370">
                            <_items dataType="Array" type="Duality.Component[]" id="371" length="4">
                              <object dataType="ObjectRef">365</object>
                              <object dataType="ObjectRef">366</object>
                              <object dataType="ObjectRef">367</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">365</compTransform>
                          <EventComponentAdded dataType="ObjectRef">357</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="372" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">358</object>
                            <object dataType="Array" type="System.Delegate[]" id="373" length="1">
                              <object dataType="ObjectRef">372</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">14</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="374" length="1">
                    <object dataType="ObjectRef">357</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="375" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="376">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="377">
                      <_items dataType="Array" type="Duality.GameObject[]" id="378" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="379">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="380" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="381" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="382" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="383">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                  <gameobj dataType="ObjectRef">379</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="384">
                                  <gameobj dataType="ObjectRef">379</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="385">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">38</r>
                                    <g dataType="Byte">24</g>
                                    <b dataType="Byte">35</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="386" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="387">
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
                                  <gameobj dataType="ObjectRef">379</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="388">
                            <_items dataType="Array" type="Duality.Component[]" id="389" length="4">
                              <object dataType="ObjectRef">383</object>
                              <object dataType="ObjectRef">384</object>
                              <object dataType="ObjectRef">385</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">383</compTransform>
                          <EventComponentAdded dataType="ObjectRef">375</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="390" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">376</object>
                            <object dataType="Array" type="System.Delegate[]" id="391" length="1">
                              <object dataType="ObjectRef">390</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">16</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="392" length="1">
                    <object dataType="ObjectRef">375</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="393" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="394">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="395">
                      <_items dataType="Array" type="Duality.GameObject[]" id="396" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="397">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="398" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="399" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="400" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="401">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                    <X dataType="Float">33.7096176</X>
                                    <Y dataType="Float">5.88362</Y>
                                    <Z dataType="Float">-683.3435</Z>
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
                                  <gameobj dataType="ObjectRef">397</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="402">
                                  <gameobj dataType="ObjectRef">397</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="403">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">38</r>
                                    <g dataType="Byte">24</g>
                                    <b dataType="Byte">35</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="404" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="405">
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
                                  <gameobj dataType="ObjectRef">397</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="406">
                            <_items dataType="Array" type="Duality.Component[]" id="407" length="4">
                              <object dataType="ObjectRef">401</object>
                              <object dataType="ObjectRef">402</object>
                              <object dataType="ObjectRef">403</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">401</compTransform>
                          <EventComponentAdded dataType="ObjectRef">393</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="408" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">394</object>
                            <object dataType="Array" type="System.Delegate[]" id="409" length="1">
                              <object dataType="ObjectRef">408</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">18</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="410" length="1">
                    <object dataType="ObjectRef">393</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="411" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="412">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="413">
                      <_items dataType="Array" type="Duality.GameObject[]" id="414" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="415">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="416" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="417" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="418" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="419">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">-71.35755</X>
                                    <Y dataType="Float">35.2602768</Y>
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
                                    <X dataType="Float">-71.35755</X>
                                    <Y dataType="Float">35.2602768</Y>
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
                                  <gameobj dataType="ObjectRef">415</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="420">
                                  <gameobj dataType="ObjectRef">415</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="421">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">38</r>
                                    <g dataType="Byte">24</g>
                                    <b dataType="Byte">35</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="422" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="423">
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
                                  <gameobj dataType="ObjectRef">415</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="424">
                            <_items dataType="Array" type="Duality.Component[]" id="425" length="4">
                              <object dataType="ObjectRef">419</object>
                              <object dataType="ObjectRef">420</object>
                              <object dataType="ObjectRef">421</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">419</compTransform>
                          <EventComponentAdded dataType="ObjectRef">411</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="426" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">412</object>
                            <object dataType="Array" type="System.Delegate[]" id="427" length="1">
                              <object dataType="ObjectRef">426</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">6</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="428" length="1">
                    <object dataType="ObjectRef">411</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="429" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="430">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="431">
                      <_items dataType="Array" type="Duality.GameObject[]" id="432" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="433">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="434" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="435" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="436" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="437">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">-52.99836</X>
                                    <Y dataType="Float">37.7360764</Y>
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
                                    <X dataType="Float">-52.99836</X>
                                    <Y dataType="Float">37.7360764</Y>
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
                                  <gameobj dataType="ObjectRef">433</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="438">
                                  <gameobj dataType="ObjectRef">433</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="439">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">38</r>
                                    <g dataType="Byte">24</g>
                                    <b dataType="Byte">35</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="440" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="441">
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
                                  <gameobj dataType="ObjectRef">433</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="442">
                            <_items dataType="Array" type="Duality.Component[]" id="443" length="4">
                              <object dataType="ObjectRef">437</object>
                              <object dataType="ObjectRef">438</object>
                              <object dataType="ObjectRef">439</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">437</compTransform>
                          <EventComponentAdded dataType="ObjectRef">429</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="444" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">430</object>
                            <object dataType="Array" type="System.Delegate[]" id="445" length="1">
                              <object dataType="ObjectRef">444</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">8</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="446" length="1">
                    <object dataType="ObjectRef">429</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="447" multi="true">
                  <object dataType="ObjectRef">28</object>
                  <object dataType="Class" type="Duality.ObjectManagers.GameObjectManager" id="448">
                    <RegisteredObjectComponentAdded />
                    <RegisteredObjectComponentRemoved />
                    <allObj dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="449">
                      <_items dataType="Array" type="Duality.GameObject[]" id="450" length="4">
                        <object dataType="Class" type="Duality.GameObject" id="451">
                          <prefabLink />
                          <parent />
                          <children />
                          <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="452" surrogate="true">
                            <customSerialIO />
                            <customSerialIO>
                              <keys dataType="Array" type="System.Type[]" id="453" length="3">
                                <object dataType="ObjectRef">14</object>
                                <object dataType="ObjectRef">16</object>
                                <object dataType="ObjectRef">15</object>
                              </keys>
                              <values dataType="Array" type="Duality.Component[]" id="454" length="3">
                                <object dataType="Class" type="Duality.Components.Transform" id="455">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">-52.99836</X>
                                    <Y dataType="Float">37.7360764</Y>
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
                                    <X dataType="Float">-52.99836</X>
                                    <Y dataType="Float">37.7360764</Y>
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
                                  <gameobj dataType="ObjectRef">451</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.SoundListener" id="456">
                                  <gameobj dataType="ObjectRef">451</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                                <object dataType="Class" type="Duality.Components.Camera" id="457">
                                  <nearZ dataType="Float">0</nearZ>
                                  <farZ dataType="Float">100000</farZ>
                                  <zSortAccuracy dataType="Float">100</zSortAccuracy>
                                  <parallaxRefDist dataType="Float">500</parallaxRefDist>
                                  <visibilityMask dataType="UInt">4294967295</visibilityMask>
                                  <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                                    <r dataType="Byte">38</r>
                                    <g dataType="Byte">24</g>
                                    <b dataType="Byte">35</b>
                                    <a dataType="Byte">0</a>
                                  </clearColor>
                                  <clearMask dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                                  <passes dataType="Array" type="Duality.Components.Camera+Pass[]" id="458" length="1">
                                    <object dataType="Class" type="Duality.Components.Camera+Pass" id="459">
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
                                  <gameobj dataType="ObjectRef">451</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </object>
                              </values>
                            </customSerialIO>
                          </compMap>
                          <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="460">
                            <_items dataType="Array" type="Duality.Component[]" id="461" length="4">
                              <object dataType="ObjectRef">455</object>
                              <object dataType="ObjectRef">456</object>
                              <object dataType="ObjectRef">457</object>
                              <object />
                            </_items>
                            <_size dataType="Int">3</_size>
                            <_version dataType="Int">3</_version>
                          </compList>
                          <name dataType="String">CamView Camera 0</name>
                          <active dataType="Bool">true</active>
                          <disposed dataType="Bool">false</disposed>
                          <compTransform dataType="ObjectRef">455</compTransform>
                          <EventComponentAdded dataType="ObjectRef">447</EventComponentAdded>
                          <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="462" multi="true">
                            <object dataType="ObjectRef">46</object>
                            <object dataType="ObjectRef">448</object>
                            <object dataType="Array" type="System.Delegate[]" id="463" length="1">
                              <object dataType="ObjectRef">462</object>
                            </object>
                          </EventComponentRemoving>
                          <EventCollisionBegin />
                          <EventCollisionEnd />
                          <EventCollisionSolve />
                        </object>
                        <object dataType="ObjectRef">11</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">10</_version>
                    </allObj>
                    <Registered />
                    <Unregistered />
                  </object>
                  <object dataType="Array" type="System.Delegate[]" id="464" length="1">
                    <object dataType="ObjectRef">447</object>
                  </object>
                </object>
                <object dataType="ObjectRef">43</object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="465" multi="true">
              <object dataType="ObjectRef">46</object>
              <object dataType="ObjectRef">29</object>
              <object dataType="Array" type="System.Delegate[]" id="466" length="25">
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="467" multi="true">
                  <object dataType="ObjectRef">46</object>
                  <object dataType="ObjectRef">2</object>
                  <object dataType="Array" type="System.Delegate[]" id="468" length="1">
                    <object dataType="ObjectRef">467</object>
                  </object>
                </object>
                <object dataType="ObjectRef">66</object>
                <object dataType="ObjectRef">84</object>
                <object dataType="ObjectRef">102</object>
                <object dataType="ObjectRef">120</object>
                <object dataType="ObjectRef">138</object>
                <object dataType="ObjectRef">156</object>
                <object dataType="ObjectRef">174</object>
                <object dataType="ObjectRef">192</object>
                <object dataType="ObjectRef">210</object>
                <object dataType="ObjectRef">228</object>
                <object dataType="ObjectRef">246</object>
                <object dataType="ObjectRef">264</object>
                <object dataType="ObjectRef">282</object>
                <object dataType="ObjectRef">300</object>
                <object dataType="ObjectRef">318</object>
                <object dataType="ObjectRef">336</object>
                <object dataType="ObjectRef">354</object>
                <object dataType="ObjectRef">372</object>
                <object dataType="ObjectRef">390</object>
                <object dataType="ObjectRef">408</object>
                <object dataType="ObjectRef">426</object>
                <object dataType="ObjectRef">444</object>
                <object dataType="ObjectRef">462</object>
                <object dataType="ObjectRef">45</object>
              </object>
            </EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="469">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="470" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="471" length="2">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="Type" id="472" value="Duality.Components.Renderers.TextRenderer" />
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="473" length="2">
                  <object dataType="Class" type="Duality.Components.Transform" id="474">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">-200</Y>
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
                      <Y dataType="Float">-200</Y>
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
                    <gameobj dataType="ObjectRef">469</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.TextRenderer" id="475">
                    <align dataType="Enum" type="Duality.Alignment" name="Center" value="0" />
                    <text dataType="Class" type="Duality.FormattedText" id="476">
                      <sourceText dataType="String">/ac/cAAFFAAFFAsteroids/n/f[1][Press Return to play]</sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="477" length="2">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\Fonts\HUD.Font.res</contentPath>
                        </object>
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\Fonts\HUD_Small.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">200</maxWidth>
                      <maxHeight dataType="Int">100</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String">Asteroids[Press Return to play]</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="478" length="2">
                        <object dataType="Int">9</object>
                        <object dataType="Int">22</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="479" length="6">
                        <object dataType="Class" type="Duality.FormattedText+AlignChangeElement" id="480">
                          <align dataType="Enum" type="Duality.Alignment" name="Center" value="0" />
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="481">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <r dataType="Byte">170</r>
                            <g dataType="Byte">255</g>
                            <b dataType="Byte">170</b>
                            <a dataType="Byte">255</a>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="482">
                          <text dataType="String">Asteroids</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+NewLineElement" id="483" />
                        <object dataType="Class" type="Duality.FormattedText+FontChangeElement" id="484">
                          <fontIndex dataType="Int">1</fontIndex>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="485">
                          <text dataType="String">[Press Return to play]</text>
                        </object>
                      </elements>
                    </text>
                    <customMat dataType="Class" type="Duality.Resources.BatchInfo" id="486">
                      <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
                        <contentPath dataType="String">Default:DrawTechnique:Add</contentPath>
                      </technique>
                      <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                        <r dataType="Byte">255</r>
                        <g dataType="Byte">255</g>
                        <b dataType="Byte">255</b>
                        <a dataType="Byte">255</a>
                      </mainColor>
                      <textures dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.String],[Duality.ContentRef`1[[Duality.Resources.Texture]]]]" id="487" surrogate="true">
                        <customSerialIO />
                        <customSerialIO>
                          <count dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Texture]]">
                            <contentPath />
                          </count>
                          <keys dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Texture]]">
                            <contentPath />
                          </keys>
                          <values dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Texture]]">
                            <contentPath />
                          </values>
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
                    <gameobj dataType="ObjectRef">469</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="488">
              <_items dataType="Array" type="Duality.Component[]" id="489" length="4">
                <object dataType="ObjectRef">474</object>
                <object dataType="ObjectRef">475</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">2</_version>
            </compList>
            <name dataType="String">TitleText</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">474</compTransform>
            <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="490">
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="491">
              <_items dataType="Array" type="Duality.GameObject[]" id="492" length="64">
                <object dataType="Class" type="Duality.GameObject" id="493">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="494">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidSmall.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">493</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="495">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="496" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="497" value="P:Duality.Components.Transform:RelativePos" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="498">
                            <_items dataType="Array" type="System.Int32[]" id="499" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-188.99614</X>
                            <Y dataType="Float">-384.992126</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="500" value="P:Duality.Components.Transform:RelativeVel" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="501">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.450777173</X>
                            <Y dataType="Float">0.379604667</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="502" value="P:Duality.Components.Transform:RelativeAngleVel" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="503">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.0009144091</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">153</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="504" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="505" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="Type" id="506" value="Duality.Components.Renderers.AnimSpriteRenderer" />
                        <object dataType="Type" id="507" value="GamePlugin.Asteroid" />
                        <object dataType="Type" id="508" value="Duality.Components.Collider" />
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="509" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="510">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-188.99614</X>
                            <Y dataType="Float">-384.992126</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.450777173</X>
                            <Y dataType="Float">0.379604667</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">3.07849479</angle>
                          <angleVel dataType="Float">0.0009144091</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="511">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="512">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="513" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+CircleShapeInfo" id="514">
                                  <radius dataType="Float">17.0660477</radius>
                                  <position dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-0</X>
                                    <Y dataType="Float">0</Y>
                                  </position>
                                  <parent dataType="ObjectRef">511</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">493</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-188.99614</X>
                            <Y dataType="Float">-384.992126</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.450777173</X>
                            <Y dataType="Float">0.379604667</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">3.07849479</angleAbs>
                          <angleVelAbs dataType="Float">0.0009144091</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">493</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="515">
                          <animFirstFrame dataType="Int">0</animFirstFrame>
                          <animFrameCount dataType="Int">2</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.07746739</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-18</x>
                            <y dataType="Float">-20</y>
                            <w dataType="Float">36</w>
                            <h dataType="Float">39</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\SmallAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">493</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="516">
                          <hp dataType="Float">50</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Small" value="0" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="None" value="0" />
                          <gameobj dataType="ObjectRef">493</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">511</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="517">
                    <_items dataType="Array" type="Duality.Component[]" id="518" length="4">
                      <object dataType="ObjectRef">510</object>
                      <object dataType="ObjectRef">515</object>
                      <object dataType="ObjectRef">516</object>
                      <object dataType="ObjectRef">511</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidSmall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">510</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="519">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="520">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidSmallBlue.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">519</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="521">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="522" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="523">
                            <_items dataType="Array" type="System.Int32[]" id="524" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">95.64077</X>
                            <Y dataType="Float">-438.990936</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="525">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.788392663</X>
                            <Y dataType="Float">-0.9308083</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="526">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">-0.00279364525</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">195</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="527" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="528" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">506</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="529" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="530">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">95.64077</X>
                            <Y dataType="Float">-438.990936</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.788392663</X>
                            <Y dataType="Float">-0.9308083</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">1.80525088</angle>
                          <angleVel dataType="Float">-0.00279364525</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="531">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="532">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="533" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+CircleShapeInfo" id="534">
                                  <radius dataType="Float">17.0660477</radius>
                                  <position dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-0</X>
                                    <Y dataType="Float">0</Y>
                                  </position>
                                  <parent dataType="ObjectRef">531</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">519</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">95.64077</X>
                            <Y dataType="Float">-438.990936</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.788392663</X>
                            <Y dataType="Float">-0.9308083</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">1.80525088</angleAbs>
                          <angleVelAbs dataType="Float">-0.00279364525</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">519</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="535">
                          <animFirstFrame dataType="Int">4</animFirstFrame>
                          <animFrameCount dataType="Int">2</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.210971117</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-18</x>
                            <y dataType="Float">-20</y>
                            <w dataType="Float">36</w>
                            <h dataType="Float">39</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\SmallAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">519</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="536">
                          <hp dataType="Float">50</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Small" value="0" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="Blue" value="1" />
                          <gameobj dataType="ObjectRef">519</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">531</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="537">
                    <_items dataType="Array" type="Duality.Component[]" id="538" length="4">
                      <object dataType="ObjectRef">530</object>
                      <object dataType="ObjectRef">535</object>
                      <object dataType="ObjectRef">536</object>
                      <object dataType="ObjectRef">531</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidSmallBlue</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">530</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="539">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="540">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidSmallGreen.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">539</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="541">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="542" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="543">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-313.993622</X>
                            <Y dataType="Float">84.99827</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="544">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.749245644</X>
                            <Y dataType="Float">-0.420557171</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="545">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.00401712628</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">111</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="546" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="547" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">506</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="548" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="549">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-313.993622</X>
                            <Y dataType="Float">84.99827</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.749245644</X>
                            <Y dataType="Float">-0.420557171</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">5.898911</angle>
                          <angleVel dataType="Float">0.00401712628</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="550">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="551">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="552" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+CircleShapeInfo" id="553">
                                  <radius dataType="Float">17.0660477</radius>
                                  <position dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-0</X>
                                    <Y dataType="Float">0</Y>
                                  </position>
                                  <parent dataType="ObjectRef">550</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">539</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-313.993622</X>
                            <Y dataType="Float">84.99827</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.749245644</X>
                            <Y dataType="Float">-0.420557171</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">5.898911</angleAbs>
                          <angleVelAbs dataType="Float">0.00401712628</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">539</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="554">
                          <animFirstFrame dataType="Int">2</animFirstFrame>
                          <animFrameCount dataType="Int">2</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.207566455</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-18</x>
                            <y dataType="Float">-20</y>
                            <w dataType="Float">36</w>
                            <h dataType="Float">39</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\SmallAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">539</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="555">
                          <hp dataType="Float">50</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Small" value="0" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="Green" value="2" />
                          <gameobj dataType="ObjectRef">539</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">550</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="556">
                    <_items dataType="Array" type="Duality.Component[]" id="557" length="4">
                      <object dataType="ObjectRef">549</object>
                      <object dataType="ObjectRef">554</object>
                      <object dataType="ObjectRef">555</object>
                      <object dataType="ObjectRef">550</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidSmallGreen</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">549</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="558">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="559">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidBig1.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">558</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="560">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="561" length="8">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="562">
                            <_items dataType="Array" type="System.Int32[]" id="563" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-310.4717</X>
                            <Y dataType="Float">-239.506714</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="564">
                            <_items dataType="ObjectRef">563</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.713664055</X>
                            <Y dataType="Float">-0.27898863</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="565">
                            <_items dataType="ObjectRef">563</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.006855141</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="566" value="P:Duality.Components.Collider:FixedAngle" />
                          <componentType dataType="ObjectRef">508</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="567">
                            <_items dataType="Array" type="System.Int32[]" id="568" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Bool">false</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="569" value="P:Duality.Components.Collider:Shapes" />
                          <componentType dataType="ObjectRef">508</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="570">
                            <_items dataType="Array" type="System.Int32[]" id="571" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="572">
                            <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="573" length="4">
                              <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="574">
                                <vertices dataType="Array" type="OpenTK.Vector2[]" id="575" length="9">
                                  <object dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-38</X>
                                    <Y dataType="Float">29.5</Y>
                                  </object>
                                  <object dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-35</X>
                                    <Y dataType="Float">-9.5</Y>
                                  </object>
                                  <object dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-26</X>
                                    <Y dataType="Float">-35.5</Y>
                                  </object>
                                  <object dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-1</X>
                                    <Y dataType="Float">-47.5</Y>
                                  </object>
                                  <object dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">23</X>
                                    <Y dataType="Float">-40.5</Y>
                                  </object>
                                  <object dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">35</X>
                                    <Y dataType="Float">-20.5</Y>
                                  </object>
                                  <object dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">30</X>
                                    <Y dataType="Float">28.5</Y>
                                  </object>
                                  <object dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">1</X>
                                    <Y dataType="Float">44.5</Y>
                                  </object>
                                  <object dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-26</X>
                                    <Y dataType="Float">47.5</Y>
                                  </object>
                                </vertices>
                                <parent dataType="Class" type="Duality.Components.Collider" id="576">
                                  <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                                  <linearDamp dataType="Float">0</linearDamp>
                                  <angularDamp dataType="Float">0</angularDamp>
                                  <fixedAngle dataType="Bool">false</fixedAngle>
                                  <ignoreGravity dataType="Bool">false</ignoreGravity>
                                  <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                                  <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                                  <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="577">
                                    <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="578" length="4">
                                      <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="579">
                                        <vertices dataType="Array" type="OpenTK.Vector2[]" id="580" length="9">
                                          <object dataType="Struct" type="OpenTK.Vector2">
                                            <X dataType="Float">-38</X>
                                            <Y dataType="Float">29.5</Y>
                                          </object>
                                          <object dataType="Struct" type="OpenTK.Vector2">
                                            <X dataType="Float">-35</X>
                                            <Y dataType="Float">-9.5</Y>
                                          </object>
                                          <object dataType="Struct" type="OpenTK.Vector2">
                                            <X dataType="Float">-26</X>
                                            <Y dataType="Float">-35.5</Y>
                                          </object>
                                          <object dataType="Struct" type="OpenTK.Vector2">
                                            <X dataType="Float">-1</X>
                                            <Y dataType="Float">-47.5</Y>
                                          </object>
                                          <object dataType="Struct" type="OpenTK.Vector2">
                                            <X dataType="Float">23</X>
                                            <Y dataType="Float">-40.5</Y>
                                          </object>
                                          <object dataType="Struct" type="OpenTK.Vector2">
                                            <X dataType="Float">35</X>
                                            <Y dataType="Float">-20.5</Y>
                                          </object>
                                          <object dataType="Struct" type="OpenTK.Vector2">
                                            <X dataType="Float">30</X>
                                            <Y dataType="Float">28.5</Y>
                                          </object>
                                          <object dataType="Struct" type="OpenTK.Vector2">
                                            <X dataType="Float">1</X>
                                            <Y dataType="Float">44.5</Y>
                                          </object>
                                          <object dataType="Struct" type="OpenTK.Vector2">
                                            <X dataType="Float">-26</X>
                                            <Y dataType="Float">47.5</Y>
                                          </object>
                                        </vertices>
                                        <parent dataType="ObjectRef">576</parent>
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
                                    <_version dataType="Int">3</_version>
                                  </shapes>
                                  <gameobj dataType="ObjectRef">558</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </parent>
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
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">5</_size>
                      <_version dataType="Int">2684</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="581" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="582" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="Type" id="583" value="Duality.Components.Renderers.SpriteRenderer" />
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="584" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="585">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-310.4717</X>
                            <Y dataType="Float">-239.506714</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.713664055</X>
                            <Y dataType="Float">-0.27898863</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">5.38783264</angle>
                          <angleVel dataType="Float">0.006855141</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="ObjectRef">576</extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-310.4717</X>
                            <Y dataType="Float">-239.506714</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.713664055</X>
                            <Y dataType="Float">-0.27898863</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">5.38783264</angleAbs>
                          <angleVelAbs dataType="Float">0.006855141</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">558</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="586">
                          <hp dataType="Float">200</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Big" value="2" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="None" value="0" />
                          <gameobj dataType="ObjectRef">558</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="587">
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-40</x>
                            <y dataType="Float">-50</y>
                            <w dataType="Float">80</w>
                            <h dataType="Float">100</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\BigAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">558</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">576</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="588">
                    <_items dataType="Array" type="Duality.Component[]" id="589" length="4">
                      <object dataType="ObjectRef">585</object>
                      <object dataType="ObjectRef">586</object>
                      <object dataType="ObjectRef">587</object>
                      <object dataType="ObjectRef">576</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidBig1</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">585</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="590">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="591">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidBig2.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">590</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="592">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="593" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="594">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">187.834457</X>
                            <Y dataType="Float">154.995178</Y>
                            <Z dataType="Float">0.0101318359</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="595">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.9712594</X>
                            <Y dataType="Float">-0.817263961</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="596">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.00522822</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">555</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="597" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="598" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">583</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="599" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="600">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">187.834457</X>
                            <Y dataType="Float">154.995178</Y>
                            <Z dataType="Float">0.0101318359</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.9712594</X>
                            <Y dataType="Float">-0.817263961</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">2.93130469</angle>
                          <angleVel dataType="Float">0.00522822</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="601">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="602">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="603" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="604">
                                  <vertices dataType="Array" type="OpenTK.Vector2[]" id="605" length="7">
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">51.9766235</X>
                                      <Y dataType="Float">25.0934372</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0.921569467</X>
                                      <Y dataType="Float">64.77191</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-46.3838577</X>
                                      <Y dataType="Float">45.7141953</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-50.37493</X>
                                      <Y dataType="Float">-10.1791954</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-28.9750443</X>
                                      <Y dataType="Float">-63.7706566</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">38.6764221</X>
                                      <Y dataType="Float">-51.92672</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">54.27158</X>
                                      <Y dataType="Float">-28.4577827</Y>
                                    </object>
                                  </vertices>
                                  <parent dataType="ObjectRef">601</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">590</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">187.834457</X>
                            <Y dataType="Float">154.995178</Y>
                            <Z dataType="Float">0.0101318359</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.9712594</X>
                            <Y dataType="Float">-0.817263961</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">2.93130469</angleAbs>
                          <angleVelAbs dataType="Float">0.00522822</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">590</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="606">
                          <hp dataType="Float">200</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Big" value="2" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="None" value="0" />
                          <gameobj dataType="ObjectRef">590</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="607">
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-56</x>
                            <y dataType="Float">-66</y>
                            <w dataType="Float">112</w>
                            <h dataType="Float">132</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\BigAsteroid2.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">590</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">601</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="608">
                    <_items dataType="Array" type="Duality.Component[]" id="609" length="4">
                      <object dataType="ObjectRef">600</object>
                      <object dataType="ObjectRef">606</object>
                      <object dataType="ObjectRef">607</object>
                      <object dataType="ObjectRef">601</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidBig2</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">600</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="610">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="611">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidBig3.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">610</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="612">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="613" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="614">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">239.995056</X>
                            <Y dataType="Float">-369.992432</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="615">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.800337553</X>
                            <Y dataType="Float">0.6783792</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="616">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">-0.00121740694</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">177</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="617" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="618" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">583</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="619" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="620">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">239.995056</X>
                            <Y dataType="Float">-369.992432</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.800337553</X>
                            <Y dataType="Float">0.6783792</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">1.99499071</angle>
                          <angleVel dataType="Float">-0.00121740694</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="621">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="622">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="623" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="624">
                                  <vertices dataType="Array" type="OpenTK.Vector2[]" id="625" length="10">
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">16.835041</X>
                                      <Y dataType="Float">-52.16159</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-15.4025917</X>
                                      <Y dataType="Float">-53.0189629</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-38.72013</X>
                                      <Y dataType="Float">-39.37006</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-57.155838</X>
                                      <Y dataType="Float">19.3251247</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-50.1272621</X>
                                      <Y dataType="Float">30.30689</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-22.6593151</X>
                                      <Y dataType="Float">51.8826141</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-2.5431118</X>
                                      <Y dataType="Float">51.3009033</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">35.75358</X>
                                      <Y dataType="Float">29.6130333</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">56.1691971</X>
                                      <Y dataType="Float">8.442241</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">52.21269</X>
                                      <Y dataType="Float">-20.0769825</Y>
                                    </object>
                                  </vertices>
                                  <parent dataType="ObjectRef">621</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">610</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">239.995056</X>
                            <Y dataType="Float">-369.992432</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.800337553</X>
                            <Y dataType="Float">0.6783792</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">1.99499071</angleAbs>
                          <angleVelAbs dataType="Float">-0.00121740694</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">610</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="626">
                          <hp dataType="Float">200</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Big" value="2" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="None" value="0" />
                          <gameobj dataType="ObjectRef">610</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="627">
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-60</x>
                            <y dataType="Float">-55</y>
                            <w dataType="Float">119</w>
                            <h dataType="Float">110</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\BigAsteroid3.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">610</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">621</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="628">
                    <_items dataType="Array" type="Duality.Component[]" id="629" length="4">
                      <object dataType="ObjectRef">620</object>
                      <object dataType="ObjectRef">626</object>
                      <object dataType="ObjectRef">627</object>
                      <object dataType="ObjectRef">621</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidBig3</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">620</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="630">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="631">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidMedium.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">630</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="632">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="633" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="634">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-126.997391</X>
                            <Y dataType="Float">47.9989738</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="635">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.9095549</X>
                            <Y dataType="Float">0.470527172</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="636">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.00251022936</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">213</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="637" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="638" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">506</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="639" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="640">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-126.997391</X>
                            <Y dataType="Float">47.9989738</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.9095549</X>
                            <Y dataType="Float">0.470527172</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">1.19711423</angle>
                          <angleVel dataType="Float">0.00251022936</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="641">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="642">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="643" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="644">
                                  <vertices dataType="Array" type="OpenTK.Vector2[]" id="645" length="6">
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-28.2318077</X>
                                      <Y dataType="Float">-8.61481</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-9.830182</X>
                                      <Y dataType="Float">-29.74252</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">25.43189</X>
                                      <Y dataType="Float">-11.8096962</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">25.4518166</X>
                                      <Y dataType="Float">10.1220074</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-2.25434685</X>
                                      <Y dataType="Float">28.0208473</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-18.7667065</X>
                                      <Y dataType="Float">20.78607</Y>
                                    </object>
                                  </vertices>
                                  <parent dataType="ObjectRef">641</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">630</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-126.997391</X>
                            <Y dataType="Float">47.9989738</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.9095549</X>
                            <Y dataType="Float">0.470527172</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">1.19711423</angleAbs>
                          <angleVelAbs dataType="Float">0.00251022936</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">630</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="646">
                          <animFirstFrame dataType="Int">0</animFirstFrame>
                          <animFrameCount dataType="Int">1</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.434377044</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-31</x>
                            <y dataType="Float">-31</y>
                            <w dataType="Float">62</w>
                            <h dataType="Float">62</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\MediumAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">630</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="647">
                          <hp dataType="Float">100</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Medium" value="1" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="None" value="0" />
                          <gameobj dataType="ObjectRef">630</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">641</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="648">
                    <_items dataType="Array" type="Duality.Component[]" id="649" length="4">
                      <object dataType="ObjectRef">640</object>
                      <object dataType="ObjectRef">646</object>
                      <object dataType="ObjectRef">647</object>
                      <object dataType="ObjectRef">641</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidMedium</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">640</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="650">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="651">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidMediumBlue.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">650</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="652">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="653" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="654">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-426.991241</X>
                            <Y dataType="Float">344.99295</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="655">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.156644747</X>
                            <Y dataType="Float">0.6570483</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="656">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.00061554194</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">129</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="657" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="658" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">506</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="659" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="660">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-426.991241</X>
                            <Y dataType="Float">344.99295</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.156644747</X>
                            <Y dataType="Float">0.6570483</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">5.061864</angle>
                          <angleVel dataType="Float">0.00061554194</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="661">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="662">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="663" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="664">
                                  <vertices dataType="Array" type="OpenTK.Vector2[]" id="665" length="6">
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-1.40597022</X>
                                      <Y dataType="Float">28.4652977</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">24.648901</X>
                                      <Y dataType="Float">7.725393</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">25.6355457</X>
                                      <Y dataType="Float">-12.2502556</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-8.680518</X>
                                      <Y dataType="Float">-26.9610558</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-27.5959148</X>
                                      <Y dataType="Float">-8.872172</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-17.0904942</X>
                                      <Y dataType="Float">21.6832867</Y>
                                    </object>
                                  </vertices>
                                  <parent dataType="ObjectRef">661</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">650</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-426.991241</X>
                            <Y dataType="Float">344.99295</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.156644747</X>
                            <Y dataType="Float">0.6570483</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">5.061864</angleAbs>
                          <angleVelAbs dataType="Float">0.00061554194</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">650</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="666">
                          <animFirstFrame dataType="Int">2</animFirstFrame>
                          <animFrameCount dataType="Int">1</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.09873016</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-31</x>
                            <y dataType="Float">-31</y>
                            <w dataType="Float">62</w>
                            <h dataType="Float">62</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\MediumAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">650</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="667">
                          <hp dataType="Float">100</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Medium" value="1" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="Blue" value="1" />
                          <gameobj dataType="ObjectRef">650</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">661</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="668">
                    <_items dataType="Array" type="Duality.Component[]" id="669" length="4">
                      <object dataType="ObjectRef">660</object>
                      <object dataType="ObjectRef">666</object>
                      <object dataType="ObjectRef">667</object>
                      <object dataType="ObjectRef">661</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidMediumBlue</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">660</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="670">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="671">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidMediumGreen.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">670</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="672">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="673" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="674">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">386.992035</X>
                            <Y dataType="Float">-122.99749</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="675">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.103702359</X>
                            <Y dataType="Float">-0.9879268</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="676">
                            <_items dataType="ObjectRef">499</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.00123327854</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">129</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="677" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="678" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">506</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="679" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="680">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">386.992035</X>
                            <Y dataType="Float">-122.99749</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.103702359</X>
                            <Y dataType="Float">-0.9879268</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">2.68273163</angle>
                          <angleVel dataType="Float">0.00123327854</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="681">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="682">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="683" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="684">
                                  <vertices dataType="Array" type="OpenTK.Vector2[]" id="685" length="7">
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-10.7899847</X>
                                      <Y dataType="Float">-28.89336</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-27.4086838</X>
                                      <Y dataType="Float">-5.832161</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-14.4693279</X>
                                      <Y dataType="Float">25.4340038</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0.625009656</X>
                                      <Y dataType="Float">26.5114956</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">22.6630726</X>
                                      <Y dataType="Float">10.1309023</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">31.3580418</X>
                                      <Y dataType="Float">-11.4421721</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">9.601216</X>
                                      <Y dataType="Float">-23.1315079</Y>
                                    </object>
                                  </vertices>
                                  <parent dataType="ObjectRef">681</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">670</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">386.992035</X>
                            <Y dataType="Float">-122.99749</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.103702359</X>
                            <Y dataType="Float">-0.9879268</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">2.68273163</angleAbs>
                          <angleVelAbs dataType="Float">0.00123327854</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">670</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="686">
                          <animFirstFrame dataType="Int">1</animFirstFrame>
                          <animFrameCount dataType="Int">1</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.5574974</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-31</x>
                            <y dataType="Float">-31</y>
                            <w dataType="Float">62</w>
                            <h dataType="Float">62</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\MediumAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">670</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="687">
                          <hp dataType="Float">100</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Medium" value="1" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="Green" value="2" />
                          <gameobj dataType="ObjectRef">670</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">681</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="688">
                    <_items dataType="Array" type="Duality.Component[]" id="689" length="4">
                      <object dataType="ObjectRef">680</object>
                      <object dataType="ObjectRef">686</object>
                      <object dataType="ObjectRef">687</object>
                      <object dataType="ObjectRef">681</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidMediumGreen</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">680</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="690">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="691">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidSmallGreen.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">690</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="692">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="693" length="3">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="694">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">395.832062</X>
                            <Y dataType="Float">394.44397</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="695">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.9093718</X>
                            <Y dataType="Float">-0.95112896</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="696">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">-0.009747339</val>
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">210</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="697" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="698" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">506</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="699" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="700">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">395.832062</X>
                            <Y dataType="Float">394.44397</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.9093718</X>
                            <Y dataType="Float">-0.95112896</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">5.898911</angle>
                          <angleVel dataType="Float">-0.009747339</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="701">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="702">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="703" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+CircleShapeInfo" id="704">
                                  <radius dataType="Float">17.0660477</radius>
                                  <position dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-0</X>
                                    <Y dataType="Float">0</Y>
                                  </position>
                                  <parent dataType="ObjectRef">701</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">690</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">395.832062</X>
                            <Y dataType="Float">394.44397</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.9093718</X>
                            <Y dataType="Float">-0.95112896</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">5.898911</angleAbs>
                          <angleVelAbs dataType="Float">-0.009747339</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">690</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="705">
                          <animFirstFrame dataType="Int">2</animFirstFrame>
                          <animFrameCount dataType="Int">2</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.499253929</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-18</x>
                            <y dataType="Float">-20</y>
                            <w dataType="Float">36</w>
                            <h dataType="Float">39</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\SmallAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">690</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="706">
                          <hp dataType="Float">50</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Small" value="0" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="Green" value="2" />
                          <gameobj dataType="ObjectRef">690</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">701</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="707">
                    <_items dataType="Array" type="Duality.Component[]" id="708" length="4">
                      <object dataType="ObjectRef">700</object>
                      <object dataType="ObjectRef">705</object>
                      <object dataType="ObjectRef">706</object>
                      <object dataType="ObjectRef">701</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidSmallGreen</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">700</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="709">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="710">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidSmall.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">709</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="711">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="712" length="3">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="713">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-134.707321</X>
                            <Y dataType="Float">402.1951</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="714">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.233764157</X>
                            <Y dataType="Float">-0.521661162</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="715">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">-0.00885804</val>
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">228</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="716" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="717" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">506</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="718" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="719">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-134.707321</X>
                            <Y dataType="Float">402.1951</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.233764157</X>
                            <Y dataType="Float">-0.521661162</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">3.07849479</angle>
                          <angleVel dataType="Float">-0.00885804</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="720">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="721">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="722" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+CircleShapeInfo" id="723">
                                  <radius dataType="Float">17.0660477</radius>
                                  <position dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-0</X>
                                    <Y dataType="Float">0</Y>
                                  </position>
                                  <parent dataType="ObjectRef">720</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">709</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-134.707321</X>
                            <Y dataType="Float">402.1951</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.233764157</X>
                            <Y dataType="Float">-0.521661162</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">3.07849479</angleAbs>
                          <angleVelAbs dataType="Float">-0.00885804</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">709</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="724">
                          <animFirstFrame dataType="Int">0</animFirstFrame>
                          <animFrameCount dataType="Int">2</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.8148088</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-18</x>
                            <y dataType="Float">-20</y>
                            <w dataType="Float">36</w>
                            <h dataType="Float">39</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\SmallAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">709</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="725">
                          <hp dataType="Float">50</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Small" value="0" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="None" value="0" />
                          <gameobj dataType="ObjectRef">709</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">720</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="726">
                    <_items dataType="Array" type="Duality.Component[]" id="727" length="4">
                      <object dataType="ObjectRef">719</object>
                      <object dataType="ObjectRef">724</object>
                      <object dataType="ObjectRef">725</object>
                      <object dataType="ObjectRef">720</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidSmall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">719</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="728">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="729">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidSmall.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">728</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="730">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="731" length="3">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="732">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">119.092438</X>
                            <Y dataType="Float">-6.327839</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="733">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.709398568</X>
                            <Y dataType="Float">-0.0178335886</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="734">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">-0.00419290969</val>
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">432</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="735" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="736" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">506</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="737" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="738">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">119.092438</X>
                            <Y dataType="Float">-6.327839</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.709398568</X>
                            <Y dataType="Float">-0.0178335886</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">3.07849479</angle>
                          <angleVel dataType="Float">-0.00419290969</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="739">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="740">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="741" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+CircleShapeInfo" id="742">
                                  <radius dataType="Float">17.0660477</radius>
                                  <position dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-0</X>
                                    <Y dataType="Float">0</Y>
                                  </position>
                                  <parent dataType="ObjectRef">739</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">728</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">119.092438</X>
                            <Y dataType="Float">-6.327839</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.709398568</X>
                            <Y dataType="Float">-0.0178335886</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">3.07849479</angleAbs>
                          <angleVelAbs dataType="Float">-0.00419290969</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">728</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="743">
                          <animFirstFrame dataType="Int">0</animFirstFrame>
                          <animFrameCount dataType="Int">2</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.636465251</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-18</x>
                            <y dataType="Float">-20</y>
                            <w dataType="Float">36</w>
                            <h dataType="Float">39</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\SmallAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">728</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="744">
                          <hp dataType="Float">50</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Small" value="0" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="None" value="0" />
                          <gameobj dataType="ObjectRef">728</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">739</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="745">
                    <_items dataType="Array" type="Duality.Component[]" id="746" length="4">
                      <object dataType="ObjectRef">738</object>
                      <object dataType="ObjectRef">743</object>
                      <object dataType="ObjectRef">744</object>
                      <object dataType="ObjectRef">739</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidSmall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">738</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="747">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="748">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidSmall.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">747</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="749">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="750" length="3">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="751">
                            <_items dataType="Array" type="System.Int32[]" id="752" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">401.394165</X>
                            <Y dataType="Float">-219.4113</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="753">
                            <_items dataType="Array" type="System.Int32[]" id="754" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.709398568</X>
                            <Y dataType="Float">-0.0178335886</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="755">
                            <_items dataType="Array" type="System.Int32[]" id="756" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Float">-0.00419290969</val>
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">6</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="757" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="758" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">506</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="759" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="760">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">401.394165</X>
                            <Y dataType="Float">-219.4113</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.709398568</X>
                            <Y dataType="Float">-0.0178335886</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">3.07849479</angle>
                          <angleVel dataType="Float">-0.00419290969</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="761">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="762">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="763" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+CircleShapeInfo" id="764">
                                  <radius dataType="Float">17.0660477</radius>
                                  <position dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-0</X>
                                    <Y dataType="Float">0</Y>
                                  </position>
                                  <parent dataType="ObjectRef">761</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">747</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">401.394165</X>
                            <Y dataType="Float">-219.4113</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.709398568</X>
                            <Y dataType="Float">-0.0178335886</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">3.07849479</angleAbs>
                          <angleVelAbs dataType="Float">-0.00419290969</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">747</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="765">
                          <animFirstFrame dataType="Int">0</animFirstFrame>
                          <animFrameCount dataType="Int">2</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.557412565</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-18</x>
                            <y dataType="Float">-20</y>
                            <w dataType="Float">36</w>
                            <h dataType="Float">39</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\SmallAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">747</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="766">
                          <hp dataType="Float">50</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Small" value="0" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="None" value="0" />
                          <gameobj dataType="ObjectRef">747</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">761</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="767">
                    <_items dataType="Array" type="Duality.Component[]" id="768" length="4">
                      <object dataType="ObjectRef">760</object>
                      <object dataType="ObjectRef">765</object>
                      <object dataType="ObjectRef">766</object>
                      <object dataType="ObjectRef">761</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidSmall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">760</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="769">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="770">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidMedium.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">769</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="771">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="772" length="3">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="773">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">388.74588</X>
                            <Y dataType="Float">141.6471</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="774">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.9313286</X>
                            <Y dataType="Float">-0.9845435</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="775">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">-0.000136201968</val>
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">186</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="776" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="777" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">506</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="778" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="779">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">388.74588</X>
                            <Y dataType="Float">141.6471</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.9313286</X>
                            <Y dataType="Float">-0.9845435</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">1.19711423</angle>
                          <angleVel dataType="Float">-0.000136201968</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="780">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="781">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="782" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="783">
                                  <vertices dataType="Array" type="OpenTK.Vector2[]" id="784" length="6">
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-28.2318077</X>
                                      <Y dataType="Float">-8.61481</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-9.830182</X>
                                      <Y dataType="Float">-29.74252</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">25.43189</X>
                                      <Y dataType="Float">-11.8096962</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">25.4518166</X>
                                      <Y dataType="Float">10.1220074</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-2.25434685</X>
                                      <Y dataType="Float">28.0208473</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-18.7667065</X>
                                      <Y dataType="Float">20.78607</Y>
                                    </object>
                                  </vertices>
                                  <parent dataType="ObjectRef">780</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">769</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">388.74588</X>
                            <Y dataType="Float">141.6471</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-0.9313286</X>
                            <Y dataType="Float">-0.9845435</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">1.19711423</angleAbs>
                          <angleVelAbs dataType="Float">-0.000136201968</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">769</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="785">
                          <animFirstFrame dataType="Int">0</animFirstFrame>
                          <animFrameCount dataType="Int">1</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.7007865</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-31</x>
                            <y dataType="Float">-31</y>
                            <w dataType="Float">62</w>
                            <h dataType="Float">62</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\MediumAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">769</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="786">
                          <hp dataType="Float">100</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Medium" value="1" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="None" value="0" />
                          <gameobj dataType="ObjectRef">769</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">780</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="787">
                    <_items dataType="Array" type="Duality.Component[]" id="788" length="4">
                      <object dataType="ObjectRef">779</object>
                      <object dataType="ObjectRef">785</object>
                      <object dataType="ObjectRef">786</object>
                      <object dataType="ObjectRef">780</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidMedium</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">779</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="789">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="790">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidMedium.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">789</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="791">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="792" length="3">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="793">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-365.86795</X>
                            <Y dataType="Float">-389.0255</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="794">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.447277546</X>
                            <Y dataType="Float">0.4126062</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="795">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">-0.00038075386</val>
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">258</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="796" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="797" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">506</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="798" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="799">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-365.86795</X>
                            <Y dataType="Float">-389.0255</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.447277546</X>
                            <Y dataType="Float">0.4126062</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">1.19711423</angle>
                          <angleVel dataType="Float">-0.00038075386</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="800">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="801">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="802" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="803">
                                  <vertices dataType="Array" type="OpenTK.Vector2[]" id="804" length="6">
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-28.2318077</X>
                                      <Y dataType="Float">-8.61481</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-9.830182</X>
                                      <Y dataType="Float">-29.74252</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">25.43189</X>
                                      <Y dataType="Float">-11.8096962</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">25.4518166</X>
                                      <Y dataType="Float">10.1220074</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-2.25434685</X>
                                      <Y dataType="Float">28.0208473</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-18.7667065</X>
                                      <Y dataType="Float">20.78607</Y>
                                    </object>
                                  </vertices>
                                  <parent dataType="ObjectRef">800</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">789</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-365.86795</X>
                            <Y dataType="Float">-389.0255</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.447277546</X>
                            <Y dataType="Float">0.4126062</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">1.19711423</angleAbs>
                          <angleVelAbs dataType="Float">-0.00038075386</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">789</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="805">
                          <animFirstFrame dataType="Int">0</animFirstFrame>
                          <animFrameCount dataType="Int">1</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.5465443</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-31</x>
                            <y dataType="Float">-31</y>
                            <w dataType="Float">62</w>
                            <h dataType="Float">62</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\MediumAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">789</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="806">
                          <hp dataType="Float">100</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Medium" value="1" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="None" value="0" />
                          <gameobj dataType="ObjectRef">789</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">800</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="807">
                    <_items dataType="Array" type="Duality.Component[]" id="808" length="4">
                      <object dataType="ObjectRef">799</object>
                      <object dataType="ObjectRef">805</object>
                      <object dataType="ObjectRef">806</object>
                      <object dataType="ObjectRef">800</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidMedium</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">799</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="809">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="810">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Prefabs\AsteroidSmallBlue.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">809</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="811">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="812" length="3">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">497</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="813">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">274.7937</X>
                            <Y dataType="Float">398.41333</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">500</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="814">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.5993732</X>
                            <Y dataType="Float">-0.0218397956</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">502</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="815">
                            <_items dataType="ObjectRef">524</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">-0.008289159</val>
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">348</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">490</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="816" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="817" length="4">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">506</object>
                        <object dataType="ObjectRef">507</object>
                        <object dataType="ObjectRef">508</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="818" length="4">
                        <object dataType="Class" type="Duality.Components.Transform" id="819">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">274.7937</X>
                            <Y dataType="Float">398.41333</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <vel dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.5993732</X>
                            <Y dataType="Float">-0.0218397956</Y>
                            <Z dataType="Float">0</Z>
                          </vel>
                          <angle dataType="Float">1.80525088</angle>
                          <angleVel dataType="Float">-0.008289159</angleVel>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <extUpdater dataType="Class" type="Duality.Components.Collider" id="820">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Dynamic" value="1" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="821">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="822" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+CircleShapeInfo" id="823">
                                  <radius dataType="Float">17.0660477</radius>
                                  <position dataType="Struct" type="OpenTK.Vector2">
                                    <X dataType="Float">-0</X>
                                    <Y dataType="Float">0</Y>
                                  </position>
                                  <parent dataType="ObjectRef">820</parent>
                                  <density dataType="Float">1</density>
                                  <friction dataType="Float">0.3</friction>
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
                            <gameobj dataType="ObjectRef">809</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </extUpdater>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, AngleVel" value="11" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">274.7937</X>
                            <Y dataType="Float">398.41333</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <velAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.5993732</X>
                            <Y dataType="Float">-0.0218397956</Y>
                            <Z dataType="Float">0</Z>
                          </velAbs>
                          <angleAbs dataType="Float">1.80525088</angleAbs>
                          <angleVelAbs dataType="Float">-0.008289159</angleVelAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1</X>
                            <Y dataType="Float">1</Y>
                            <Z dataType="Float">1</Z>
                          </scaleAbs>
                          <gameobj dataType="ObjectRef">809</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.AnimSpriteRenderer" id="824">
                          <animFirstFrame dataType="Int">4</animFirstFrame>
                          <animFrameCount dataType="Int">2</animFrameCount>
                          <animDuration dataType="Float">1</animDuration>
                          <animLoopMode dataType="Enum" type="Duality.Components.Renderers.AnimSpriteRenderer+LoopMode" name="RandomSingle" value="3" />
                          <animTime dataType="Float">0.342655033</animTime>
                          <animCycle dataType="Int">0</animCycle>
                          <verticesSmooth />
                          <rect dataType="Struct" type="Duality.Rect">
                            <x dataType="Float">-18</x>
                            <y dataType="Float">-20</y>
                            <w dataType="Float">36</w>
                            <h dataType="Float">39</h>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Materials\SmallAsteroid.Material.res</contentPath>
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
                          <gameobj dataType="ObjectRef">809</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="GamePlugin.Asteroid" id="825">
                          <hp dataType="Float">50</hp>
                          <type dataType="Enum" type="GamePlugin.AsteroidType" name="Small" value="0" />
                          <powerup dataType="Enum" type="GamePlugin.PowerupType" name="Blue" value="1" />
                          <gameobj dataType="ObjectRef">809</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">820</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="826">
                    <_items dataType="Array" type="Duality.Component[]" id="827" length="4">
                      <object dataType="ObjectRef">819</object>
                      <object dataType="ObjectRef">824</object>
                      <object dataType="ObjectRef">825</object>
                      <object dataType="ObjectRef">820</object>
                    </_items>
                    <_size dataType="Int">4</_size>
                    <_version dataType="Int">4</_version>
                  </compList>
                  <name dataType="String">AsteroidSmallBlue</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">819</compTransform>
                  <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
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
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
              </_items>
              <_size dataType="Int">16</_size>
              <_version dataType="Int">104</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="828" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="829" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="830" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="831">
              <_items dataType="Array" type="Duality.Component[]" id="832" length="0" />
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <name dataType="String">Asteroids</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="833" multi="true">
              <object dataType="MethodInfo" id="834" value="M:Duality.Components.Transform:Parent_EventComponentAdded(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">819</object>
              <object dataType="Array" type="System.Delegate[]" id="835" length="17">
                <object dataType="ObjectRef">49</object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="836" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">510</object>
                  <object dataType="Array" type="System.Delegate[]" id="837" length="1">
                    <object dataType="ObjectRef">836</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="838" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">530</object>
                  <object dataType="Array" type="System.Delegate[]" id="839" length="1">
                    <object dataType="ObjectRef">838</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="840" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">549</object>
                  <object dataType="Array" type="System.Delegate[]" id="841" length="1">
                    <object dataType="ObjectRef">840</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="842" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">585</object>
                  <object dataType="Array" type="System.Delegate[]" id="843" length="1">
                    <object dataType="ObjectRef">842</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="844" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">600</object>
                  <object dataType="Array" type="System.Delegate[]" id="845" length="1">
                    <object dataType="ObjectRef">844</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="846" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">620</object>
                  <object dataType="Array" type="System.Delegate[]" id="847" length="1">
                    <object dataType="ObjectRef">846</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="848" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">640</object>
                  <object dataType="Array" type="System.Delegate[]" id="849" length="1">
                    <object dataType="ObjectRef">848</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="850" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">660</object>
                  <object dataType="Array" type="System.Delegate[]" id="851" length="1">
                    <object dataType="ObjectRef">850</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="852" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">680</object>
                  <object dataType="Array" type="System.Delegate[]" id="853" length="1">
                    <object dataType="ObjectRef">852</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="854" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">700</object>
                  <object dataType="Array" type="System.Delegate[]" id="855" length="1">
                    <object dataType="ObjectRef">854</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="856" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">719</object>
                  <object dataType="Array" type="System.Delegate[]" id="857" length="1">
                    <object dataType="ObjectRef">856</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="858" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">738</object>
                  <object dataType="Array" type="System.Delegate[]" id="859" length="1">
                    <object dataType="ObjectRef">858</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="860" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">760</object>
                  <object dataType="Array" type="System.Delegate[]" id="861" length="1">
                    <object dataType="ObjectRef">860</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="862" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">779</object>
                  <object dataType="Array" type="System.Delegate[]" id="863" length="1">
                    <object dataType="ObjectRef">862</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="864" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">799</object>
                  <object dataType="Array" type="System.Delegate[]" id="865" length="1">
                    <object dataType="ObjectRef">864</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="866" multi="true">
                  <object dataType="ObjectRef">834</object>
                  <object dataType="ObjectRef">819</object>
                  <object dataType="Array" type="System.Delegate[]" id="867" length="1">
                    <object dataType="ObjectRef">866</object>
                  </object>
                </object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="868">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="869" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="870" length="1">
                  <object dataType="Type" id="871" value="GamePlugin.MenuSceneController" />
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="872" length="1">
                  <object dataType="Class" type="GamePlugin.MenuSceneController" id="873">
                    <mainCamObj />
                    <gameobj dataType="ObjectRef">868</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="874">
              <_items dataType="Array" type="Duality.Component[]" id="875" length="4">
                <object dataType="ObjectRef">873</object>
                <object />
                <object />
                <object />
              </_items>
              <_size dataType="Int">1</_size>
              <_version dataType="Int">1</_version>
            </compList>
            <name dataType="String">MenuSceneController</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="876">
            <prefabLink />
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="877" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="878" length="2">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">472</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="879" length="2">
                  <object dataType="Class" type="Duality.Components.Transform" id="880">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">-100</Y>
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
                      <Y dataType="Float">-100</Y>
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
                    <gameobj dataType="ObjectRef">876</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.TextRenderer" id="881">
                    <align dataType="Enum" type="Duality.Alignment" name="Top" value="4" />
                    <text dataType="Class" type="Duality.FormattedText" id="882">
                      <sourceText dataType="String"></sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="883" length="2">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\Fonts\HUD.Font.res</contentPath>
                        </object>
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\Fonts\Highscore.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">400</maxWidth>
                      <maxHeight dataType="Int">400</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String"></displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="884" length="0" />
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="885" length="0" />
                    </text>
                    <customMat dataType="Class" type="Duality.Resources.BatchInfo" id="886">
                      <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
                        <contentPath dataType="String">Default:DrawTechnique:Add</contentPath>
                      </technique>
                      <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                        <r dataType="Byte">255</r>
                        <g dataType="Byte">255</g>
                        <b dataType="Byte">255</b>
                        <a dataType="Byte">255</a>
                      </mainColor>
                      <textures dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.String],[Duality.ContentRef`1[[Duality.Resources.Texture]]]]" id="887" surrogate="true">
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
                    <gameobj dataType="ObjectRef">876</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="888">
              <_items dataType="Array" type="Duality.Component[]" id="889" length="4">
                <object dataType="ObjectRef">880</object>
                <object dataType="ObjectRef">881</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">2</_version>
            </compList>
            <name dataType="String">HighscoreText</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">880</compTransform>
            <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="890">
            <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="891">
              <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                <contentPath dataType="String">Data\Prefabs\Wall.Prefab.res</contentPath>
              </prefab>
              <obj dataType="ObjectRef">890</obj>
              <changes />
            </prefabLink>
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="892" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="893" length="3">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">583</object>
                  <object dataType="ObjectRef">508</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="894" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="895">
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
                    <extUpdater dataType="Class" type="Duality.Components.Collider" id="896">
                      <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                      <linearDamp dataType="Float">0</linearDamp>
                      <angularDamp dataType="Float">0</angularDamp>
                      <fixedAngle dataType="Bool">false</fixedAngle>
                      <ignoreGravity dataType="Bool">false</ignoreGravity>
                      <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                      <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="897">
                        <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="898" length="4">
                          <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="899">
                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="900" length="4">
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
                            <parent dataType="ObjectRef">896</parent>
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
                      <gameobj dataType="ObjectRef">890</gameobj>
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
                    <gameobj dataType="ObjectRef">890</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="901">
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
                    <gameobj dataType="ObjectRef">890</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">896</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="902">
              <_items dataType="Array" type="Duality.Component[]" id="903" length="4">
                <object dataType="ObjectRef">895</object>
                <object dataType="ObjectRef">901</object>
                <object dataType="ObjectRef">896</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">Wall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">895</compTransform>
            <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="904">
            <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="905">
              <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                <contentPath dataType="String">Data\Prefabs\Wall.Prefab.res</contentPath>
              </prefab>
              <obj dataType="ObjectRef">904</obj>
              <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="906">
                <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="907" length="8">
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop dataType="PropertyInfo" id="908" value="P:Duality.Components.Transform:RelativeAngle" />
                    <componentType dataType="ObjectRef">14</componentType>
                    <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="909">
                      <_items dataType="ObjectRef">499</_items>
                      <_size dataType="Int">0</_size>
                      <_version dataType="Int">1</_version>
                    </childIndex>
                    <val dataType="Float">1.57079637</val>
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop dataType="ObjectRef">497</prop>
                    <componentType dataType="ObjectRef">14</componentType>
                    <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="910">
                      <_items dataType="ObjectRef">499</_items>
                      <_size dataType="Int">0</_size>
                      <_version dataType="Int">1</_version>
                    </childIndex>
                    <val dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">-528.2135</X>
                      <Y dataType="Float">0.205034062</Y>
                      <Z dataType="Float">0</Z>
                    </val>
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop dataType="ObjectRef">500</prop>
                    <componentType dataType="ObjectRef">14</componentType>
                    <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="911">
                      <_items dataType="ObjectRef">499</_items>
                      <_size dataType="Int">0</_size>
                      <_version dataType="Int">1</_version>
                    </childIndex>
                    <val dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </val>
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop dataType="ObjectRef">502</prop>
                    <componentType dataType="ObjectRef">14</componentType>
                    <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="912">
                      <_items dataType="ObjectRef">499</_items>
                      <_size dataType="Int">0</_size>
                      <_version dataType="Int">1</_version>
                    </childIndex>
                    <val dataType="Float">0</val>
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop dataType="ObjectRef">569</prop>
                    <componentType dataType="ObjectRef">508</componentType>
                    <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="913">
                      <_items dataType="Array" type="System.Int32[]" id="914" length="0" />
                      <_size dataType="Int">0</_size>
                      <_version dataType="Int">1</_version>
                    </childIndex>
                    <val dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="915">
                      <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="916" length="4">
                        <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="917">
                          <vertices dataType="Array" type="OpenTK.Vector2[]" id="918" length="4">
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
                          <parent dataType="Class" type="Duality.Components.Collider" id="919">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="920">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="921" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="922">
                                  <vertices dataType="Array" type="OpenTK.Vector2[]" id="923" length="4">
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
                                  <parent dataType="ObjectRef">919</parent>
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
                              <_version dataType="Int">3</_version>
                            </shapes>
                            <gameobj dataType="ObjectRef">904</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </parent>
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
                      <_version dataType="Int">3</_version>
                    </val>
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop />
                    <componentType />
                    <childIndex />
                    <val />
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop />
                    <componentType />
                    <childIndex />
                    <val />
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop />
                    <componentType />
                    <childIndex />
                    <val />
                  </object>
                </_items>
                <_size dataType="Int">5</_size>
                <_version dataType="Int">667</_version>
              </changes>
            </prefabLink>
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="924" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="925" length="3">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">583</object>
                  <object dataType="ObjectRef">508</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="926" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="927">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">-528.2135</X>
                      <Y dataType="Float">0.205034062</Y>
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
                    <extUpdater dataType="ObjectRef">919</extUpdater>
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, Angle, AngleVel" value="15" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">-528.2135</X>
                      <Y dataType="Float">0.205034062</Y>
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
                    <gameobj dataType="ObjectRef">904</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="928">
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
                    <gameobj dataType="ObjectRef">904</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">919</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="929">
              <_items dataType="Array" type="Duality.Component[]" id="930" length="4">
                <object dataType="ObjectRef">927</object>
                <object dataType="ObjectRef">928</object>
                <object dataType="ObjectRef">919</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">Wall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">927</compTransform>
            <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="931">
            <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="932">
              <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                <contentPath dataType="String">Data\Prefabs\Wall.Prefab.res</contentPath>
              </prefab>
              <obj dataType="ObjectRef">931</obj>
              <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="933">
                <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="934" length="4">
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop dataType="ObjectRef">908</prop>
                    <componentType dataType="ObjectRef">14</componentType>
                    <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="935">
                      <_items dataType="ObjectRef">499</_items>
                      <_size dataType="Int">0</_size>
                      <_version dataType="Int">1</_version>
                    </childIndex>
                    <val dataType="Float">4.712389</val>
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop dataType="ObjectRef">497</prop>
                    <componentType dataType="ObjectRef">14</componentType>
                    <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="936">
                      <_items dataType="ObjectRef">499</_items>
                      <_size dataType="Int">0</_size>
                      <_version dataType="Int">1</_version>
                    </childIndex>
                    <val dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">528.066</X>
                      <Y dataType="Float">0.194900036</Y>
                      <Z dataType="Float">0</Z>
                    </val>
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop dataType="ObjectRef">500</prop>
                    <componentType dataType="ObjectRef">14</componentType>
                    <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="937">
                      <_items dataType="ObjectRef">499</_items>
                      <_size dataType="Int">0</_size>
                      <_version dataType="Int">1</_version>
                    </childIndex>
                    <val dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">0</Z>
                    </val>
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop dataType="ObjectRef">502</prop>
                    <componentType dataType="ObjectRef">14</componentType>
                    <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="938">
                      <_items dataType="ObjectRef">499</_items>
                      <_size dataType="Int">0</_size>
                      <_version dataType="Int">1</_version>
                    </childIndex>
                    <val dataType="Float">0</val>
                  </object>
                </_items>
                <_size dataType="Int">4</_size>
                <_version dataType="Int">670</_version>
              </changes>
            </prefabLink>
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="939" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="940" length="3">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">583</object>
                  <object dataType="ObjectRef">508</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="941" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="942">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">528.066</X>
                      <Y dataType="Float">0.194900036</Y>
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
                    <extUpdater dataType="Class" type="Duality.Components.Collider" id="943">
                      <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                      <linearDamp dataType="Float">0</linearDamp>
                      <angularDamp dataType="Float">0</angularDamp>
                      <fixedAngle dataType="Bool">false</fixedAngle>
                      <ignoreGravity dataType="Bool">false</ignoreGravity>
                      <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                      <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="944">
                        <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="945" length="4">
                          <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="946">
                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="947" length="4">
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
                            <parent dataType="ObjectRef">943</parent>
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
                      <gameobj dataType="ObjectRef">931</gameobj>
                      <disposed dataType="Bool">false</disposed>
                      <active dataType="Bool">true</active>
                    </extUpdater>
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Vel, Angle, AngleVel" value="15" />
                    <parentTransform />
                    <posAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">528.066</X>
                      <Y dataType="Float">0.194900036</Y>
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
                    <gameobj dataType="ObjectRef">931</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="948">
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
                    <gameobj dataType="ObjectRef">931</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">943</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="949">
              <_items dataType="Array" type="Duality.Component[]" id="950" length="4">
                <object dataType="ObjectRef">942</object>
                <object dataType="ObjectRef">948</object>
                <object dataType="ObjectRef">943</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">Wall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">942</compTransform>
            <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="951">
            <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="952">
              <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                <contentPath dataType="String">Data\Prefabs\Wall.Prefab.res</contentPath>
              </prefab>
              <obj dataType="ObjectRef">951</obj>
              <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="953">
                <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="954" length="8">
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop dataType="ObjectRef">908</prop>
                    <componentType dataType="ObjectRef">14</componentType>
                    <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="955">
                      <_items dataType="Array" type="System.Int32[]" id="956" length="0" />
                      <_size dataType="Int">0</_size>
                      <_version dataType="Int">1</_version>
                    </childIndex>
                    <val dataType="Float">3.14159274</val>
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop dataType="ObjectRef">497</prop>
                    <componentType dataType="ObjectRef">14</componentType>
                    <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="957">
                      <_items dataType="ObjectRef">956</_items>
                      <_size dataType="Int">0</_size>
                      <_version dataType="Int">1</_version>
                    </childIndex>
                    <val dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">-528</Y>
                      <Z dataType="Float">0</Z>
                    </val>
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop dataType="ObjectRef">569</prop>
                    <componentType dataType="ObjectRef">508</componentType>
                    <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="958">
                      <_items dataType="ObjectRef">914</_items>
                      <_size dataType="Int">0</_size>
                      <_version dataType="Int">1</_version>
                    </childIndex>
                    <val dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="959">
                      <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="960" length="4">
                        <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="961">
                          <vertices dataType="Array" type="OpenTK.Vector2[]" id="962" length="4">
                            <object dataType="Struct" type="OpenTK.Vector2">
                              <X dataType="Float">-510.5488</X>
                              <Y dataType="Float">-16.2566223</Y>
                            </object>
                            <object dataType="Struct" type="OpenTK.Vector2">
                              <X dataType="Float">513.4512</X>
                              <Y dataType="Float">-16.8368111</Y>
                            </object>
                            <object dataType="Struct" type="OpenTK.Vector2">
                              <X dataType="Float">513.4512</X>
                              <Y dataType="Float">-0.5915315</Y>
                            </object>
                            <object dataType="Struct" type="OpenTK.Vector2">
                              <X dataType="Float">-510.5488</X>
                              <Y dataType="Float">-1.17172039</Y>
                            </object>
                          </vertices>
                          <parent dataType="Class" type="Duality.Components.Collider" id="963">
                            <bodyType dataType="Enum" type="Duality.Components.Collider+BodyType" name="Static" value="0" />
                            <linearDamp dataType="Float">0</linearDamp>
                            <angularDamp dataType="Float">0</angularDamp>
                            <fixedAngle dataType="Bool">false</fixedAngle>
                            <ignoreGravity dataType="Bool">false</ignoreGravity>
                            <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                            <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                            <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Collider+ShapeInfo]]" id="964">
                              <_items dataType="Array" type="Duality.Components.Collider+ShapeInfo[]" id="965" length="4">
                                <object dataType="Class" type="Duality.Components.Collider+PolyShapeInfo" id="966">
                                  <vertices dataType="Array" type="OpenTK.Vector2[]" id="967" length="4">
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-510.5488</X>
                                      <Y dataType="Float">-16.2566223</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">513.4512</X>
                                      <Y dataType="Float">-16.8368111</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">513.4512</X>
                                      <Y dataType="Float">-0.5915315</Y>
                                    </object>
                                    <object dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-510.5488</X>
                                      <Y dataType="Float">-1.17172039</Y>
                                    </object>
                                  </vertices>
                                  <parent dataType="ObjectRef">963</parent>
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
                              <_version dataType="Int">3</_version>
                            </shapes>
                            <gameobj dataType="ObjectRef">951</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </parent>
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
                      <_version dataType="Int">3</_version>
                    </val>
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop />
                    <componentType />
                    <childIndex />
                    <val />
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop />
                    <componentType />
                    <childIndex />
                    <val />
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop />
                    <componentType />
                    <childIndex />
                    <val />
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop />
                    <componentType />
                    <childIndex />
                    <val />
                  </object>
                  <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                    <prop />
                    <componentType />
                    <childIndex />
                    <val />
                  </object>
                </_items>
                <_size dataType="Int">3</_size>
                <_version dataType="Int">613</_version>
              </changes>
            </prefabLink>
            <parent />
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="968" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="969" length="3">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">583</object>
                  <object dataType="ObjectRef">508</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="970" length="3">
                  <object dataType="Class" type="Duality.Components.Transform" id="971">
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
                    <extUpdater dataType="ObjectRef">963</extUpdater>
                    <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="Pos, Angle" value="5" />
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
                    <gameobj dataType="ObjectRef">951</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="972">
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
                    <gameobj dataType="ObjectRef">951</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="ObjectRef">963</object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="973">
              <_items dataType="Array" type="Duality.Component[]" id="974" length="4">
                <object dataType="ObjectRef">971</object>
                <object dataType="ObjectRef">972</object>
                <object dataType="ObjectRef">963</object>
                <object />
              </_items>
              <_size dataType="Int">3</_size>
              <_version dataType="Int">3</_version>
            </compList>
            <name dataType="String">Wall</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">971</compTransform>
            <EventComponentAdded dataType="ObjectRef">49</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">467</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="ObjectRef">558</object>
          <object dataType="ObjectRef">590</object>
          <object dataType="ObjectRef">610</object>
          <object dataType="ObjectRef">630</object>
          <object dataType="ObjectRef">650</object>
          <object dataType="ObjectRef">670</object>
          <object dataType="ObjectRef">493</object>
          <object dataType="ObjectRef">519</object>
          <object dataType="ObjectRef">539</object>
          <object dataType="ObjectRef">690</object>
          <object dataType="ObjectRef">709</object>
          <object dataType="ObjectRef">728</object>
          <object dataType="ObjectRef">747</object>
          <object dataType="ObjectRef">769</object>
          <object dataType="ObjectRef">789</object>
          <object dataType="ObjectRef">809</object>
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
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
        </_items>
        <_size dataType="Int">25</_size>
        <_version dataType="Int">795</_version>
      </allObj>
      <Registered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="975" multi="true">
        <object dataType="MethodInfo" id="976" value="M:Duality.Resources.Scene:objectManager_Registered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="977" length="1">
          <object dataType="ObjectRef">975</object>
        </object>
      </Registered>
      <Unregistered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="978" multi="true">
        <object dataType="MethodInfo" id="979" value="M:Duality.Resources.Scene:objectManager_Unregistered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="980" length="1">
          <object dataType="ObjectRef">978</object>
        </object>
      </Unregistered>
    </objectManager>
  </object>
</root>