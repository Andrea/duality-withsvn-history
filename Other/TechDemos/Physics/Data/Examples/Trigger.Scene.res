<?xml version="1.0" encoding="utf-8"?>
<root>
  <object dataType="Class" type="Duality.Resources.Scene" id="1">
    <globalGravity dataType="Struct" type="OpenTK.Vector2">
      <X dataType="Float">0</X>
      <Y dataType="Float">5</Y>
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
        <_items dataType="Array" type="Duality.GameObject[]" id="10" length="256">
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
                  <object dataType="Type" id="17" value="PhysicsTestbed.Testbed" />
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="18" length="4">
                  <object dataType="Class" type="Duality.Components.Transform" id="19">
                    <pos dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">-500</Z>
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
                      <Z dataType="Float">-500</Z>
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
                      <Z dataType="Float">-500</Z>
                    </lastPos>
                    <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                      <X dataType="Float">0</X>
                      <Y dataType="Float">0</Y>
                      <Z dataType="Float">-500</Z>
                    </lastPosAbs>
                    <lastAngle dataType="Float">0</lastAngle>
                    <lastAngleAbs dataType="Float">0</lastAngleAbs>
                    <OnTransformChanged />
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.Camera" id="20">
                    <nearZ dataType="Float">0</nearZ>
                    <farZ dataType="Float">10000</farZ>
                    <zSortAccuracy dataType="Float">1000</zSortAccuracy>
                    <parallaxRefDist dataType="Float">500</parallaxRefDist>
                    <visibilityMask dataType="Enum" type="Duality.VisibilityFlag" name="AllOverlay" value="4294967295" />
                    <passes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Camera+Pass]]" id="21">
                      <_items dataType="Array" type="Duality.Components.Camera+Pass[]" id="22" length="4">
                        <object dataType="Class" type="Duality.Components.Camera+Pass" id="23">
                          <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">18</R>
                            <G dataType="Byte">22</G>
                            <B dataType="Byte">32</B>
                            <A dataType="Byte">0</A>
                          </clearColor>
                          <clearDepth dataType="Float">1</clearDepth>
                          <clearFlags dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="All" value="3" />
                          <matrixMode dataType="Enum" type="Duality.RenderMatrix" name="PerspectiveWorld" value="0" />
                          <fitOutput dataType="Bool">false</fitOutput>
                          <visibilityMask dataType="Enum" type="Duality.VisibilityFlag" name="AllWorld" value="2147483647" />
                          <input />
                          <output dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.RenderTarget]]">
                            <contentPath />
                          </output>
                          <CollectDrawcalls />
                        </object>
                        <object dataType="Class" type="Duality.Components.Camera+Pass" id="24">
                          <clearColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">0</R>
                            <G dataType="Byte">0</G>
                            <B dataType="Byte">0</B>
                            <A dataType="Byte">0</A>
                          </clearColor>
                          <clearDepth dataType="Float">1</clearDepth>
                          <clearFlags dataType="Enum" type="Duality.Components.Camera+ClearFlags" name="None" value="0" />
                          <matrixMode dataType="Enum" type="Duality.RenderMatrix" name="OrthoScreen" value="1" />
                          <fitOutput dataType="Bool">false</fitOutput>
                          <visibilityMask dataType="Enum" type="Duality.VisibilityFlag" name="AllOverlay" value="4294967295" />
                          <input />
                          <output dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.RenderTarget]]">
                            <contentPath />
                          </output>
                          <CollectDrawcalls />
                        </object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">2</_version>
                    </passes>
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.SoundListener" id="25">
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="PhysicsTestbed.Testbed" id="26">
                    <name dataType="Class" type="Duality.FormattedText" id="27">
                      <sourceText dataType="String">Example: Trigger</sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="28" length="1">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\BigFont.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">500</maxWidth>
                      <maxHeight dataType="Int">500</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String">Example: Trigger</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="29" length="1">
                        <object dataType="Int">16</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="30" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="31">
                          <text dataType="String">Example: Trigger</text>
                        </object>
                      </elements>
                    </name>
                    <desc dataType="Class" type="Duality.FormattedText" id="32">
                      <sourceText dataType="String">RigidBody shapes can also be defined as sensors that report collisions but do not react to them. This example shows both sensoric shapes and collision response in general.</sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="33" length="1">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\SmallFont.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">500</maxWidth>
                      <maxHeight dataType="Int">500</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String">RigidBody shapes can also be defined as sensors that report collisions but do not react to them. This example shows both sensoric shapes and collision response in general.</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="34" length="1">
                        <object dataType="Int">171</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="35" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="36">
                          <text dataType="String">RigidBody shapes can also be defined as sensors that report collisions but do not react to them. This example shows both sensoric shapes and collision response in general.</text>
                        </object>
                      </elements>
                    </desc>
                    <physicsTimeVal dataType="Float">0</physicsTimeVal>
                    <physicsTimeAcc dataType="Float">0</physicsTimeAcc>
                    <physicsTimeCounter dataType="Int">100</physicsTimeCounter>
                    <mouseJoint />
                    <controls dataType="Class" type="Duality.FormattedText" id="37">
                      <sourceText dataType="String">/cFFAAAAFFLeft Mouse/cFFFFFFFF: Drag object/n/cFFAAAAFFRight Mouse/cFFFFFFFF: Create object/n/cFFAAAAFFNumber keys/cFFFFFFFF: Select testbed scene/n/cFFAAAAFFSpace/cFFFFFFFF: Pause / Unpause</sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="38" length="1">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\SmallFont.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">500</maxWidth>
                      <maxHeight dataType="Int">500</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String">Left Mouse: Drag objectRight Mouse: Create objectNumber keys: Select testbed sceneSpace: Pause Unpause</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="39" length="1">
                        <object dataType="Int">102</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="40" length="20">
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="41">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">170</G>
                            <B dataType="Byte">170</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="42">
                          <text dataType="String">Left Mouse</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="43">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="44">
                          <text dataType="String">: Drag object</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+NewLineElement" id="45" />
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="46">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">170</G>
                            <B dataType="Byte">170</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="47">
                          <text dataType="String">Right Mouse</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="48">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="49">
                          <text dataType="String">: Create object</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+NewLineElement" id="50" />
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="51">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">170</G>
                            <B dataType="Byte">170</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="52">
                          <text dataType="String">Number keys</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="53">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="54">
                          <text dataType="String">: Select testbed scene</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+NewLineElement" id="55" />
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="56">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">170</G>
                            <B dataType="Byte">170</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="57">
                          <text dataType="String">Space</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="58">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="59">
                          <text dataType="String">: Pause </text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="60">
                          <text dataType="String">Unpause</text>
                        </object>
                      </elements>
                    </controls>
                    <stats dataType="Class" type="Duality.FormattedText" id="61">
                      <sourceText />
                      <icons />
                      <flowAreas />
                      <fonts />
                      <maxWidth dataType="Int">0</maxWidth>
                      <maxHeight dataType="Int">0</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String"></displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="62" length="0" />
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="63" length="0" />
                    </stats>
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="64">
              <_items dataType="Array" type="Duality.Component[]" id="65" length="4">
                <object dataType="ObjectRef">19</object>
                <object dataType="ObjectRef">20</object>
                <object dataType="ObjectRef">25</object>
                <object dataType="ObjectRef">26</object>
              </_items>
              <_size dataType="Int">4</_size>
              <_version dataType="Int">4</_version>
            </compList>
            <name dataType="String">MainCam</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
            <compTransform dataType="ObjectRef">19</compTransform>
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="66" multi="true">
              <object dataType="MethodInfo" id="67" value="M:Duality.ObjectManagers.GameObjectManager:OnRegisteredObjectComponentAdded(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">2</object>
              <object dataType="Array" type="System.Delegate[]" id="68" length="1">
                <object dataType="ObjectRef">66</object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="69" multi="true">
              <object dataType="MethodInfo" id="70" value="M:Duality.ObjectManagers.GameObjectManager:OnRegisteredObjectComponentRemoved(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">2</object>
              <object dataType="Array" type="System.Delegate[]" id="71" length="1">
                <object dataType="ObjectRef">69</object>
              </object>
            </EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="72">
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="73">
              <_items dataType="Array" type="Duality.GameObject[]" id="74" length="32">
                <object dataType="Class" type="Duality.GameObject" id="75">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="76" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="77" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="Type" id="78" value="Duality.Components.Renderers.SpriteRenderer" />
                        <object dataType="Type" id="79" value="Duality.Components.Physics.RigidBody" />
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="80" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="81">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">2</X>
                            <Y dataType="Float">301</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">2</X>
                            <Y dataType="Float">301</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
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
                            <X dataType="Float">2</X>
                            <Y dataType="Float">301</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">2</X>
                            <Y dataType="Float">301</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="82" multi="true">
                            <object dataType="MethodInfo" id="83" value="M:Duality.Components.Physics.RigidBody:OnTransformChanged(System.Object,Duality.TransformChangedEventArgs)" />
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="84">
                              <bodyType dataType="Enum" type="Duality.Components.Physics.BodyType" name="Static" value="0" />
                              <linearDamp dataType="Float">0</linearDamp>
                              <angularDamp dataType="Float">0</angularDamp>
                              <fixedAngle dataType="Bool">false</fixedAngle>
                              <ignoreGravity dataType="Bool">false</ignoreGravity>
                              <continous dataType="Bool">true</continous>
                              <linearVel dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">0</X>
                                <Y dataType="Float">0</Y>
                              </linearVel>
                              <angularVel dataType="Float">0</angularVel>
                              <revolutions dataType="Float">0</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="85">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="86" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="87">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="88" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">84</parent>
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
                                <_version dataType="Int">7</_version>
                              </shapes>
                              <joints />
                              <gameobj dataType="ObjectRef">75</gameobj>
                              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="89" length="1">
                              <object dataType="ObjectRef">82</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">75</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="90">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="Class" type="Duality.Resources.BatchInfo" id="91">
                            <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
                              <contentPath dataType="String">Default:DrawTechnique:Mask</contentPath>
                            </technique>
                            <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                              <R dataType="Byte">68</R>
                              <G dataType="Byte">79</G>
                              <B dataType="Byte">119</B>
                              <A dataType="Byte">255</A>
                            </mainColor>
                            <textures dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.String],[Duality.ContentRef`1[[Duality.Resources.Texture]]]]" id="92" surrogate="true">
                              <customSerialIO />
                              <customSerialIO>
                                <mainTex dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Texture]]">
                                  <contentPath dataType="String">Default:Texture:White</contentPath>
                                </mainTex>
                              </customSerialIO>
                            </textures>
                            <uniforms />
                            <dirtyFlag dataType="Enum" type="Duality.Resources.BatchInfo+DirtyFlag" name="None" value="0" />
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
                          <gameobj dataType="ObjectRef">75</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">84</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="93">
                    <_items dataType="Array" type="Duality.Component[]" id="94" length="4">
                      <object dataType="ObjectRef">81</object>
                      <object dataType="ObjectRef">90</object>
                      <object dataType="ObjectRef">84</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">7</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <compTransform dataType="ObjectRef">81</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="95">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="96" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="97" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="98" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="99">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">193.000992</X>
                            <Y dataType="Float">301</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">193.000992</X>
                            <Y dataType="Float">301</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
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
                            <X dataType="Float">193.000992</X>
                            <Y dataType="Float">301</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">193.000992</X>
                            <Y dataType="Float">301</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="100" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="101">
                              <bodyType dataType="Enum" type="Duality.Components.Physics.BodyType" name="Static" value="0" />
                              <linearDamp dataType="Float">0</linearDamp>
                              <angularDamp dataType="Float">0</angularDamp>
                              <fixedAngle dataType="Bool">false</fixedAngle>
                              <ignoreGravity dataType="Bool">false</ignoreGravity>
                              <continous dataType="Bool">true</continous>
                              <linearVel dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">0</X>
                                <Y dataType="Float">0</Y>
                              </linearVel>
                              <angularVel dataType="Float">0</angularVel>
                              <revolutions dataType="Float">0</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="102">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="103" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="104">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="105" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">101</parent>
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
                              <gameobj dataType="ObjectRef">95</gameobj>
                              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="106" length="1">
                              <object dataType="ObjectRef">100</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">95</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="107">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">91</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">95</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">101</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="108">
                    <_items dataType="Array" type="Duality.Component[]" id="109" length="4">
                      <object dataType="ObjectRef">99</object>
                      <object dataType="ObjectRef">107</object>
                      <object dataType="ObjectRef">101</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <compTransform dataType="ObjectRef">99</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="110">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="111" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="112" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="113" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="114">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-184.998</X>
                            <Y dataType="Float">301</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-184.998</X>
                            <Y dataType="Float">301</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
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
                            <X dataType="Float">-184.998</X>
                            <Y dataType="Float">301</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-184.998</X>
                            <Y dataType="Float">301</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="115" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="116">
                              <bodyType dataType="Enum" type="Duality.Components.Physics.BodyType" name="Static" value="0" />
                              <linearDamp dataType="Float">0</linearDamp>
                              <angularDamp dataType="Float">0</angularDamp>
                              <fixedAngle dataType="Bool">false</fixedAngle>
                              <ignoreGravity dataType="Bool">false</ignoreGravity>
                              <continous dataType="Bool">true</continous>
                              <linearVel dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">0</X>
                                <Y dataType="Float">0</Y>
                              </linearVel>
                              <angularVel dataType="Float">0</angularVel>
                              <revolutions dataType="Float">0</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="117">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="118" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="119">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="120" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">116</parent>
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
                              <gameobj dataType="ObjectRef">110</gameobj>
                              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="121" length="1">
                              <object dataType="ObjectRef">115</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">110</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="122">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">91</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">110</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">116</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="123">
                    <_items dataType="Array" type="Duality.Component[]" id="124" length="4">
                      <object dataType="ObjectRef">114</object>
                      <object dataType="ObjectRef">122</object>
                      <object dataType="ObjectRef">116</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <compTransform dataType="ObjectRef">114</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="125">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="126" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="127" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="128" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="129">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">276.735718</X>
                            <Y dataType="Float">224.270966</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">4.79236126</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">276.735718</X>
                            <Y dataType="Float">224.270966</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">4.79236126</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
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
                            <X dataType="Float">276.735718</X>
                            <Y dataType="Float">224.270966</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">276.735718</X>
                            <Y dataType="Float">224.270966</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">-1.490824</lastAngle>
                          <lastAngleAbs dataType="Float">-1.490824</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="130" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="131">
                              <bodyType dataType="Enum" type="Duality.Components.Physics.BodyType" name="Static" value="0" />
                              <linearDamp dataType="Float">0</linearDamp>
                              <angularDamp dataType="Float">0</angularDamp>
                              <fixedAngle dataType="Bool">false</fixedAngle>
                              <ignoreGravity dataType="Bool">false</ignoreGravity>
                              <continous dataType="Bool">true</continous>
                              <linearVel dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">0</X>
                                <Y dataType="Float">0</Y>
                              </linearVel>
                              <angularVel dataType="Float">0</angularVel>
                              <revolutions dataType="Float">0</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="132">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="133" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="134">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="135" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">131</parent>
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
                              <gameobj dataType="ObjectRef">125</gameobj>
                              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="136" length="1">
                              <object dataType="ObjectRef">130</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">125</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="137">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">91</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">125</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">131</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="138">
                    <_items dataType="Array" type="Duality.Component[]" id="139" length="4">
                      <object dataType="ObjectRef">129</object>
                      <object dataType="ObjectRef">137</object>
                      <object dataType="ObjectRef">131</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <compTransform dataType="ObjectRef">129</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="140">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="141" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="142" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="143" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="144">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">291.3051</X>
                            <Y dataType="Float">36.96232</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">4.79236126</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">291.3051</X>
                            <Y dataType="Float">36.96232</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">4.79236126</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
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
                            <X dataType="Float">291.3051</X>
                            <Y dataType="Float">36.96232</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">291.3051</X>
                            <Y dataType="Float">36.96232</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">-1.490824</lastAngle>
                          <lastAngleAbs dataType="Float">-1.490824</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="145" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="146">
                              <bodyType dataType="Enum" type="Duality.Components.Physics.BodyType" name="Static" value="0" />
                              <linearDamp dataType="Float">0</linearDamp>
                              <angularDamp dataType="Float">0</angularDamp>
                              <fixedAngle dataType="Bool">false</fixedAngle>
                              <ignoreGravity dataType="Bool">false</ignoreGravity>
                              <continous dataType="Bool">true</continous>
                              <linearVel dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">0</X>
                                <Y dataType="Float">0</Y>
                              </linearVel>
                              <angularVel dataType="Float">0</angularVel>
                              <revolutions dataType="Float">0</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="147">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="148" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="149">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="150" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">146</parent>
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
                              <gameobj dataType="ObjectRef">140</gameobj>
                              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="151" length="1">
                              <object dataType="ObjectRef">145</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">140</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="152">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">91</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">140</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">146</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="153">
                    <_items dataType="Array" type="Duality.Component[]" id="154" length="4">
                      <object dataType="ObjectRef">144</object>
                      <object dataType="ObjectRef">152</object>
                      <object dataType="ObjectRef">146</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <compTransform dataType="ObjectRef">144</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="155">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="156" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="157" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="158" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="159">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">305.8745</X>
                            <Y dataType="Float">-150.346313</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">4.79236126</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">305.8745</X>
                            <Y dataType="Float">-150.346313</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">4.79236126</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
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
                            <X dataType="Float">305.8745</X>
                            <Y dataType="Float">-150.346313</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">305.8745</X>
                            <Y dataType="Float">-150.346313</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">-1.490824</lastAngle>
                          <lastAngleAbs dataType="Float">-1.490824</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="160" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="161">
                              <bodyType dataType="Enum" type="Duality.Components.Physics.BodyType" name="Static" value="0" />
                              <linearDamp dataType="Float">0</linearDamp>
                              <angularDamp dataType="Float">0</angularDamp>
                              <fixedAngle dataType="Bool">false</fixedAngle>
                              <ignoreGravity dataType="Bool">false</ignoreGravity>
                              <continous dataType="Bool">true</continous>
                              <linearVel dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">0</X>
                                <Y dataType="Float">0</Y>
                              </linearVel>
                              <angularVel dataType="Float">0</angularVel>
                              <revolutions dataType="Float">0</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="162">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="163" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="164">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="165" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">161</parent>
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
                              <gameobj dataType="ObjectRef">155</gameobj>
                              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="166" length="1">
                              <object dataType="ObjectRef">160</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">155</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="167">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">91</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">155</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">161</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="168">
                    <_items dataType="Array" type="Duality.Component[]" id="169" length="4">
                      <object dataType="ObjectRef">159</object>
                      <object dataType="ObjectRef">167</object>
                      <object dataType="ObjectRef">161</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <compTransform dataType="ObjectRef">159</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="170">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="171" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="172" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="173" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="174">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-301.3562</X>
                            <Y dataType="Float">40.84913</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">4.588328</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-301.3562</X>
                            <Y dataType="Float">40.84913</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">4.588328</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
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
                            <X dataType="Float">-301.3562</X>
                            <Y dataType="Float">40.84913</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-301.3562</X>
                            <Y dataType="Float">40.84913</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">-1.69485736</lastAngle>
                          <lastAngleAbs dataType="Float">-1.69485736</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="175" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="176">
                              <bodyType dataType="Enum" type="Duality.Components.Physics.BodyType" name="Static" value="0" />
                              <linearDamp dataType="Float">0</linearDamp>
                              <angularDamp dataType="Float">0</angularDamp>
                              <fixedAngle dataType="Bool">false</fixedAngle>
                              <ignoreGravity dataType="Bool">false</ignoreGravity>
                              <continous dataType="Bool">true</continous>
                              <linearVel dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">0</X>
                                <Y dataType="Float">0</Y>
                              </linearVel>
                              <angularVel dataType="Float">0</angularVel>
                              <revolutions dataType="Float">0</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="177">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="178" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="179">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="180" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">176</parent>
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
                              <gameobj dataType="ObjectRef">170</gameobj>
                              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="181" length="1">
                              <object dataType="ObjectRef">175</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">170</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="182">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">91</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">170</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">176</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="183">
                    <_items dataType="Array" type="Duality.Component[]" id="184" length="4">
                      <object dataType="ObjectRef">174</object>
                      <object dataType="ObjectRef">182</object>
                      <object dataType="ObjectRef">176</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <compTransform dataType="ObjectRef">174</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="185">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="186" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="187" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="188" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="189">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-325.0417</X>
                            <Y dataType="Float">-145.52626</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">4.588328</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-325.0417</X>
                            <Y dataType="Float">-145.52626</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">4.588328</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
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
                            <X dataType="Float">-325.0417</X>
                            <Y dataType="Float">-145.52626</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-325.0417</X>
                            <Y dataType="Float">-145.52626</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">-1.69485736</lastAngle>
                          <lastAngleAbs dataType="Float">-1.69485736</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="190" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="191">
                              <bodyType dataType="Enum" type="Duality.Components.Physics.BodyType" name="Static" value="0" />
                              <linearDamp dataType="Float">0</linearDamp>
                              <angularDamp dataType="Float">0</angularDamp>
                              <fixedAngle dataType="Bool">false</fixedAngle>
                              <ignoreGravity dataType="Bool">false</ignoreGravity>
                              <continous dataType="Bool">true</continous>
                              <linearVel dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">0</X>
                                <Y dataType="Float">0</Y>
                              </linearVel>
                              <angularVel dataType="Float">0</angularVel>
                              <revolutions dataType="Float">0</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="192">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="193" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="194">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="195" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">191</parent>
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
                              <gameobj dataType="ObjectRef">185</gameobj>
                              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="196" length="1">
                              <object dataType="ObjectRef">190</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">185</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="197">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">91</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">185</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">191</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="198">
                    <_items dataType="Array" type="Duality.Component[]" id="199" length="4">
                      <object dataType="ObjectRef">189</object>
                      <object dataType="ObjectRef">197</object>
                      <object dataType="ObjectRef">191</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <compTransform dataType="ObjectRef">189</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="200">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="201" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="202" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="203" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="204">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-277.670776</X>
                            <Y dataType="Float">227.224548</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">4.588328</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-277.670776</X>
                            <Y dataType="Float">227.224548</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">4.588328</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3842099</X>
                            <Y dataType="Float">0.3842099</Y>
                            <Z dataType="Float">0.3842099</Z>
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
                            <X dataType="Float">-277.670776</X>
                            <Y dataType="Float">227.224548</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-277.670776</X>
                            <Y dataType="Float">227.224548</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">-1.69485736</lastAngle>
                          <lastAngleAbs dataType="Float">-1.69485736</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="205" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="206">
                              <bodyType dataType="Enum" type="Duality.Components.Physics.BodyType" name="Static" value="0" />
                              <linearDamp dataType="Float">0</linearDamp>
                              <angularDamp dataType="Float">0</angularDamp>
                              <fixedAngle dataType="Bool">false</fixedAngle>
                              <ignoreGravity dataType="Bool">false</ignoreGravity>
                              <continous dataType="Bool">true</continous>
                              <linearVel dataType="Struct" type="OpenTK.Vector2">
                                <X dataType="Float">0</X>
                                <Y dataType="Float">0</Y>
                              </linearVel>
                              <angularVel dataType="Float">0</angularVel>
                              <revolutions dataType="Float">0</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="207">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="208" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="209">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="210" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-256</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">206</parent>
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
                              <gameobj dataType="ObjectRef">200</gameobj>
                              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="211" length="1">
                              <object dataType="ObjectRef">205</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">200</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="212">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">91</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">200</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">206</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="213">
                    <_items dataType="Array" type="Duality.Component[]" id="214" length="4">
                      <object dataType="ObjectRef">204</object>
                      <object dataType="ObjectRef">212</object>
                      <object dataType="ObjectRef">206</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <compTransform dataType="ObjectRef">204</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
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
              </_items>
              <_size dataType="Int">9</_size>
              <_version dataType="Int">51</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="215" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="216" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="217" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="218">
              <_items dataType="Array" type="Duality.Component[]" id="219" length="0" />
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <name dataType="String">StaticWorld</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
            <compTransform />
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="220" multi="true">
              <object dataType="MethodInfo" id="221" value="M:Duality.Components.Transform:Parent_EventComponentAdded(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">204</object>
              <object dataType="Array" type="System.Delegate[]" id="222" length="10">
                <object dataType="ObjectRef">66</object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="223" multi="true">
                  <object dataType="ObjectRef">221</object>
                  <object dataType="ObjectRef">81</object>
                  <object dataType="Array" type="System.Delegate[]" id="224" length="1">
                    <object dataType="ObjectRef">223</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="225" multi="true">
                  <object dataType="ObjectRef">221</object>
                  <object dataType="ObjectRef">99</object>
                  <object dataType="Array" type="System.Delegate[]" id="226" length="1">
                    <object dataType="ObjectRef">225</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="227" multi="true">
                  <object dataType="ObjectRef">221</object>
                  <object dataType="ObjectRef">114</object>
                  <object dataType="Array" type="System.Delegate[]" id="228" length="1">
                    <object dataType="ObjectRef">227</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="229" multi="true">
                  <object dataType="ObjectRef">221</object>
                  <object dataType="ObjectRef">129</object>
                  <object dataType="Array" type="System.Delegate[]" id="230" length="1">
                    <object dataType="ObjectRef">229</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="231" multi="true">
                  <object dataType="ObjectRef">221</object>
                  <object dataType="ObjectRef">144</object>
                  <object dataType="Array" type="System.Delegate[]" id="232" length="1">
                    <object dataType="ObjectRef">231</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="233" multi="true">
                  <object dataType="ObjectRef">221</object>
                  <object dataType="ObjectRef">159</object>
                  <object dataType="Array" type="System.Delegate[]" id="234" length="1">
                    <object dataType="ObjectRef">233</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="235" multi="true">
                  <object dataType="ObjectRef">221</object>
                  <object dataType="ObjectRef">174</object>
                  <object dataType="Array" type="System.Delegate[]" id="236" length="1">
                    <object dataType="ObjectRef">235</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="237" multi="true">
                  <object dataType="ObjectRef">221</object>
                  <object dataType="ObjectRef">189</object>
                  <object dataType="Array" type="System.Delegate[]" id="238" length="1">
                    <object dataType="ObjectRef">237</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="239" multi="true">
                  <object dataType="ObjectRef">221</object>
                  <object dataType="ObjectRef">204</object>
                  <object dataType="Array" type="System.Delegate[]" id="240" length="1">
                    <object dataType="ObjectRef">239</object>
                  </object>
                </object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="241">
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="242">
              <_items dataType="Array" type="Duality.GameObject[]" id="243" length="256">
                <object dataType="Class" type="Duality.GameObject" id="244">
                  <prefabLink />
                  <parent dataType="ObjectRef">241</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="245" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="246" length="5">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                        <object dataType="Type" id="247" value="PhysicsTestbed.Trigger" />
                        <object dataType="Type" id="248" value="PhysicsTestbed.TriggerDebugRenderer" />
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="249" length="5">
                        <object dataType="Class" type="Duality.Components.Transform" id="250">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">170.407166</X>
                            <Y dataType="Float">214.419586</Y>
                            <Z dataType="Float">-1</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.466217279</X>
                            <Y dataType="Float">0.466217279</Y>
                            <Z dataType="Float">0.466217279</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">170.407166</X>
                            <Y dataType="Float">214.419586</Y>
                            <Z dataType="Float">-1</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.466217279</X>
                            <Y dataType="Float">0.466217279</Y>
                            <Z dataType="Float">0.466217279</Z>
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
                            <X dataType="Float">170.407166</X>
                            <Y dataType="Float">214.419586</Y>
                            <Z dataType="Float">-1</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">170.407166</X>
                            <Y dataType="Float">214.419586</Y>
                            <Z dataType="Float">-1</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="251" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="252">
                              <bodyType dataType="Enum" type="Duality.Components.Physics.BodyType" name="Static" value="0" />
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="253">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="254" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="255">
                                    <radius dataType="Float">75</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">28.0672436</X>
                                      <Y dataType="Float">-34.9445076</Y>
                                    </position>
                                    <parent dataType="ObjectRef">252</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">true</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="256">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="257" length="5">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-60.5330238</X>
                                        <Y dataType="Float">-111.731438</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">114.698669</X>
                                        <Y dataType="Float">45.78017</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">32.00506</X>
                                        <Y dataType="Float">115.675987</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-90.06646</X>
                                        <Y dataType="Float">98.9403839</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-116.64653</X>
                                        <Y dataType="Float">-13.2866983</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">252</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">true</sensor>
                                  </object>
                                  <object />
                                  <object />
                                </_items>
                                <_size dataType="Int">2</_size>
                                <_version dataType="Int">4</_version>
                              </shapes>
                              <joints />
                              <gameobj dataType="ObjectRef">244</gameobj>
                              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="258" length="1">
                              <object dataType="ObjectRef">251</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">244</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="259">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Trigger.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">183</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">117</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">244</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">252</object>
                        <object dataType="Class" type="PhysicsTestbed.Trigger" id="260">
                          <collisionCount dataType="Int">0</collisionCount>
                          <sensorOnly dataType="Bool">true</sensorOnly>
                          <gameobj dataType="ObjectRef">244</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="PhysicsTestbed.TriggerDebugRenderer" id="261">
                          <oldColorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </oldColorTint>
                          <wasTriggered dataType="Bool">false</wasTriggered>
                          <gameobj dataType="ObjectRef">244</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="262">
                    <_items dataType="Array" type="Duality.Component[]" id="263" length="8">
                      <object dataType="ObjectRef">250</object>
                      <object dataType="ObjectRef">259</object>
                      <object dataType="ObjectRef">252</object>
                      <object dataType="ObjectRef">260</object>
                      <object dataType="ObjectRef">261</object>
                      <object />
                      <object />
                      <object />
                    </_items>
                    <_size dataType="Int">5</_size>
                    <_version dataType="Int">5</_version>
                  </compList>
                  <name dataType="String">Trigger</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <compTransform dataType="ObjectRef">250</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="264">
                  <prefabLink />
                  <parent dataType="ObjectRef">241</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="265" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="266" length="5">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                        <object dataType="ObjectRef">247</object>
                        <object dataType="ObjectRef">248</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="267" length="5">
                        <object dataType="Class" type="Duality.Components.Transform" id="268">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-162.942078</X>
                            <Y dataType="Float">197.8333</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.4987063</X>
                            <Y dataType="Float">0.4987063</Y>
                            <Z dataType="Float">0.4987063</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-162.942078</X>
                            <Y dataType="Float">197.8333</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.4987063</X>
                            <Y dataType="Float">0.4987063</Y>
                            <Z dataType="Float">0.4987063</Z>
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
                            <X dataType="Float">-162.942078</X>
                            <Y dataType="Float">197.8333</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-162.942078</X>
                            <Y dataType="Float">197.8333</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="269" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="270">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="271">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="272" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="273">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="274" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-102</X>
                                        <Y dataType="Float">-22</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">21</X>
                                        <Y dataType="Float">-22</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">21</X>
                                        <Y dataType="Float">102</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-102</X>
                                        <Y dataType="Float">102</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">270</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="275">
                                    <radius dataType="Float">67</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">36.6269035</X>
                                      <Y dataType="Float">-40.57122</Y>
                                    </position>
                                    <parent dataType="ObjectRef">270</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">true</sensor>
                                  </object>
                                  <object />
                                  <object />
                                </_items>
                                <_size dataType="Int">2</_size>
                                <_version dataType="Int">4</_version>
                              </shapes>
                              <joints />
                              <gameobj dataType="ObjectRef">264</gameobj>
                              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="276" length="1">
                              <object dataType="ObjectRef">269</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">264</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="277">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-111.5</X>
                            <Y dataType="Float">-111.5</Y>
                            <W dataType="Float">223</W>
                            <H dataType="Float">223</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Trigger2.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">183</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">117</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">264</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">270</object>
                        <object dataType="Class" type="PhysicsTestbed.Trigger" id="278">
                          <collisionCount dataType="Int">0</collisionCount>
                          <sensorOnly dataType="Bool">true</sensorOnly>
                          <gameobj dataType="ObjectRef">264</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="PhysicsTestbed.TriggerDebugRenderer" id="279">
                          <oldColorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </oldColorTint>
                          <wasTriggered dataType="Bool">false</wasTriggered>
                          <gameobj dataType="ObjectRef">264</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="280">
                    <_items dataType="Array" type="Duality.Component[]" id="281" length="8">
                      <object dataType="ObjectRef">268</object>
                      <object dataType="ObjectRef">277</object>
                      <object dataType="ObjectRef">270</object>
                      <object dataType="ObjectRef">278</object>
                      <object dataType="ObjectRef">279</object>
                      <object />
                      <object />
                      <object />
                    </_items>
                    <_size dataType="Int">5</_size>
                    <_version dataType="Int">5</_version>
                  </compList>
                  <name dataType="String">Trigger2</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <compTransform dataType="ObjectRef">268</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="282">
                  <prefabLink />
                  <parent dataType="ObjectRef">241</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="283" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="284" length="5">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                        <object dataType="ObjectRef">247</object>
                        <object dataType="ObjectRef">248</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="285" length="5">
                        <object dataType="Class" type="Duality.Components.Transform" id="286">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-39</X>
                            <Y dataType="Float">207</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.28207615</X>
                            <Y dataType="Float">0.28207615</Y>
                            <Z dataType="Float">0.28207615</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-39</X>
                            <Y dataType="Float">207</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.28207615</X>
                            <Y dataType="Float">0.28207615</Y>
                            <Z dataType="Float">0.28207615</Z>
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
                            <X dataType="Float">-39</X>
                            <Y dataType="Float">207</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-39</X>
                            <Y dataType="Float">207</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="287" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="288">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="289">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="290" length="8">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="291">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">288</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="292">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">288</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="293">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">288</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="294">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">288</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="295">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="296" length="8">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-75</X>
                                        <Y dataType="Float">-125</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">75</X>
                                        <Y dataType="Float">-125</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">125</X>
                                        <Y dataType="Float">-75</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">125</X>
                                        <Y dataType="Float">75</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">75</X>
                                        <Y dataType="Float">125</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-75</X>
                                        <Y dataType="Float">125</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-125</X>
                                        <Y dataType="Float">75</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-125</X>
                                        <Y dataType="Float">-75</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">288</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object />
                                  <object />
                                  <object />
                                </_items>
                                <_size dataType="Int">5</_size>
                                <_version dataType="Int">5</_version>
                              </shapes>
                              <joints />
                              <gameobj dataType="ObjectRef">282</gameobj>
                              <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="297" length="1">
                              <object dataType="ObjectRef">287</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">282</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="298">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\RoundSquare.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">183</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">117</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">282</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">288</object>
                        <object dataType="Class" type="PhysicsTestbed.Trigger" id="299">
                          <collisionCount dataType="Int">0</collisionCount>
                          <sensorOnly dataType="Bool">false</sensorOnly>
                          <gameobj dataType="ObjectRef">282</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="PhysicsTestbed.TriggerDebugRenderer" id="300">
                          <oldColorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </oldColorTint>
                          <wasTriggered dataType="Bool">false</wasTriggered>
                          <gameobj dataType="ObjectRef">282</gameobj>
                          <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                          <active dataType="Bool">true</active>
                        </object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="301">
                    <_items dataType="Array" type="Duality.Component[]" id="302" length="8">
                      <object dataType="ObjectRef">286</object>
                      <object dataType="ObjectRef">298</object>
                      <object dataType="ObjectRef">288</object>
                      <object dataType="ObjectRef">299</object>
                      <object dataType="ObjectRef">300</object>
                      <object />
                      <object />
                      <object />
                    </_items>
                    <_size dataType="Int">5</_size>
                    <_version dataType="Int">5</_version>
                  </compList>
                  <name dataType="String">Trigger3</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
                  <compTransform dataType="ObjectRef">286</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
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
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
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
              <_size dataType="Int">3</_size>
              <_version dataType="Int">513</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="303" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="304" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="305" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="306">
              <_items dataType="ObjectRef">219</_items>
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <name dataType="String">Dynamics</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
            <compTransform />
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="307" multi="true">
              <object dataType="ObjectRef">221</object>
              <object dataType="ObjectRef">286</object>
              <object dataType="Array" type="System.Delegate[]" id="308" length="4">
                <object dataType="ObjectRef">66</object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="309" multi="true">
                  <object dataType="ObjectRef">221</object>
                  <object dataType="ObjectRef">250</object>
                  <object dataType="Array" type="System.Delegate[]" id="310" length="1">
                    <object dataType="ObjectRef">309</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="311" multi="true">
                  <object dataType="ObjectRef">221</object>
                  <object dataType="ObjectRef">268</object>
                  <object dataType="Array" type="System.Delegate[]" id="312" length="1">
                    <object dataType="ObjectRef">311</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="313" multi="true">
                  <object dataType="ObjectRef">221</object>
                  <object dataType="ObjectRef">286</object>
                  <object dataType="Array" type="System.Delegate[]" id="314" length="1">
                    <object dataType="ObjectRef">313</object>
                  </object>
                </object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="ObjectRef">75</object>
          <object dataType="Class" type="Duality.GameObject" id="315">
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="316">
              <_items dataType="Array" type="Duality.GameObject[]" id="317" length="4">
                <object />
                <object />
                <object />
                <object />
              </_items>
              <_size dataType="Int">0</_size>
              <_version dataType="Int">6</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="318" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="319" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="320" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="321">
              <_items dataType="Array" type="Duality.Component[]" id="322" length="0" />
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <name dataType="String">Text</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Enum" type="Duality.DisposeState" name="Active" value="0" />
            <compTransform />
            <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="ObjectRef">95</object>
          <object dataType="ObjectRef">110</object>
          <object dataType="ObjectRef">125</object>
          <object dataType="ObjectRef">140</object>
          <object dataType="ObjectRef">155</object>
          <object dataType="ObjectRef">170</object>
          <object dataType="ObjectRef">185</object>
          <object dataType="ObjectRef">200</object>
          <object dataType="ObjectRef">244</object>
          <object dataType="ObjectRef">264</object>
          <object dataType="ObjectRef">282</object>
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
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
        <_version dataType="Int">580</_version>
      </allObj>
      <Registered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="323" multi="true">
        <object dataType="MethodInfo" id="324" value="M:Duality.Resources.Scene:objectManager_Registered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="325" length="1">
          <object dataType="ObjectRef">323</object>
        </object>
      </Registered>
      <Unregistered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="326" multi="true">
        <object dataType="MethodInfo" id="327" value="M:Duality.Resources.Scene:objectManager_Unregistered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="328" length="1">
          <object dataType="ObjectRef">326</object>
        </object>
      </Unregistered>
    </objectManager>
    <sourcePath />
  </object>
</root>