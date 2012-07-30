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
        <_items dataType="Array" type="Duality.GameObject[]" id="10" length="128">
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
                    <disposed dataType="Bool">false</disposed>
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
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="Duality.Components.SoundListener" id="25">
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                  <object dataType="Class" type="PhysicsTestbed.Testbed" id="26">
                    <name dataType="Class" type="Duality.FormattedText" id="27">
                      <sourceText dataType="String">Example: Basic Physics</sourceText>
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
                      <displayedText dataType="String">Example: Basic Physics</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="29" length="1">
                        <object dataType="Int">22</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="30" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="31">
                          <text dataType="String">Example: Basic Physics</text>
                        </object>
                      </elements>
                    </name>
                    <desc dataType="Class" type="Duality.FormattedText" id="32">
                      <sourceText dataType="String">This example shows some basic physical behavior.</sourceText>
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
                      <displayedText dataType="String">This example shows some basic physical behavior.</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="34" length="1">
                        <object dataType="Int">48</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="35" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="36">
                          <text dataType="String">This example shows some basic physical behavior.</text>
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
                    <disposed dataType="Bool">false</disposed>
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
            <disposed dataType="Bool">false</disposed>
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
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="89" length="1">
                              <object dataType="ObjectRef">82</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">75</gameobj>
                          <disposed dataType="Bool">false</disposed>
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
                          <disposed dataType="Bool">false</disposed>
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
                  <disposed dataType="Bool">false</disposed>
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
                            <X dataType="Float">-169.088623</X>
                            <Y dataType="Float">245.850922</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0.605511546</angle>
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
                            <X dataType="Float">-169.088623</X>
                            <Y dataType="Float">245.850922</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0.605511546</angleAbs>
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
                            <X dataType="Float">-169.088623</X>
                            <Y dataType="Float">245.850922</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-169.088623</X>
                            <Y dataType="Float">245.850922</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0.605511546</lastAngle>
                          <lastAngleAbs dataType="Float">0.605511546</lastAngleAbs>
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
                              <revolutions dataType="Float">0.1927403</revolutions>
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
                              <joints />
                              <gameobj dataType="ObjectRef">95</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="106" length="1">
                              <object dataType="ObjectRef">100</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">95</gameobj>
                          <disposed dataType="Bool">false</disposed>
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
                          <customMat dataType="Class" type="Duality.Resources.BatchInfo" id="108">
                            <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
                              <contentPath dataType="String">Default:DrawTechnique:Mask</contentPath>
                            </technique>
                            <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                              <R dataType="Byte">97</R>
                              <G dataType="Byte">68</G>
                              <B dataType="Byte">119</B>
                              <A dataType="Byte">255</A>
                            </mainColor>
                            <textures dataType="ObjectRef">92</textures>
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
                          <gameobj dataType="ObjectRef">95</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">101</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="109">
                    <_items dataType="Array" type="Duality.Component[]" id="110" length="4">
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
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">99</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="111">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="112" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="113" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="114" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="115">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">170.873444</X>
                            <Y dataType="Float">248.886261</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.74945354</angle>
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
                            <X dataType="Float">170.873444</X>
                            <Y dataType="Float">248.886261</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.74945354</angleAbs>
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
                            <X dataType="Float">170.873444</X>
                            <Y dataType="Float">248.886261</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">170.873444</X>
                            <Y dataType="Float">248.886261</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">5.74945354</lastAngle>
                          <lastAngleAbs dataType="Float">5.74945354</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="116" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="117">
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
                              <revolutions dataType="Float">1.83010781</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="118">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="119" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="120">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="121" length="4">
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
                                    <parent dataType="ObjectRef">117</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0</friction>
                                    <restitution dataType="Float">0</restitution>
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
                              <gameobj dataType="ObjectRef">111</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="122" length="1">
                              <object dataType="ObjectRef">116</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">111</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="123">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="Class" type="Duality.Resources.BatchInfo" id="124">
                            <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
                              <contentPath dataType="String">Default:DrawTechnique:Mask</contentPath>
                            </technique>
                            <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                              <R dataType="Byte">68</R>
                              <G dataType="Byte">117</G>
                              <B dataType="Byte">119</B>
                              <A dataType="Byte">255</A>
                            </mainColor>
                            <textures dataType="ObjectRef">92</textures>
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
                          <gameobj dataType="ObjectRef">111</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">117</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="125">
                    <_items dataType="Array" type="Duality.Component[]" id="126" length="4">
                      <object dataType="ObjectRef">115</object>
                      <object dataType="ObjectRef">123</object>
                      <object dataType="ObjectRef">117</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">115</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="127">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="128" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="129" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="130" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="131">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-37.9395142</X>
                            <Y dataType="Float">111.675308</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.780141</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-37.9395142</X>
                            <Y dataType="Float">111.675308</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.780141</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
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
                            <X dataType="Float">-37.9395142</X>
                            <Y dataType="Float">111.675308</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-37.9395142</X>
                            <Y dataType="Float">111.675308</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">5.780141</lastAngle>
                          <lastAngleAbs dataType="Float">5.780141</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="132" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="133">
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
                              <revolutions dataType="Float">1.83987594</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="134">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="135" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="136">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="137" length="4">
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
                                    <parent dataType="ObjectRef">133</parent>
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
                              <gameobj dataType="ObjectRef">127</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="138" length="1">
                              <object dataType="ObjectRef">132</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">127</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="139">
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
                          <gameobj dataType="ObjectRef">127</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">133</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="140">
                    <_items dataType="Array" type="Duality.Component[]" id="141" length="4">
                      <object dataType="ObjectRef">131</object>
                      <object dataType="ObjectRef">139</object>
                      <object dataType="ObjectRef">133</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">131</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="142">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="143" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="144" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="145" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="146">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">55.8835144</X>
                            <Y dataType="Float">179.267517</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0.404443264</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">55.8835144</X>
                            <Y dataType="Float">179.267517</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0.404443264</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
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
                            <X dataType="Float">55.8835144</X>
                            <Y dataType="Float">179.267517</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">55.8835144</X>
                            <Y dataType="Float">179.267517</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0.404443264</lastAngle>
                          <lastAngleAbs dataType="Float">0.404443264</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="147" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="148">
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
                              <revolutions dataType="Float">0.128738284</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="149">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="150" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="151">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="152" length="4">
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
                                    <parent dataType="ObjectRef">148</parent>
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
                              <gameobj dataType="ObjectRef">142</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="153" length="1">
                              <object dataType="ObjectRef">147</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">142</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="154">
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
                          <gameobj dataType="ObjectRef">142</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">148</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="155">
                    <_items dataType="Array" type="Duality.Component[]" id="156" length="4">
                      <object dataType="ObjectRef">146</object>
                      <object dataType="ObjectRef">154</object>
                      <object dataType="ObjectRef">148</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">146</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="157">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="158" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="159" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="160" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="161">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">228.139359</X>
                            <Y dataType="Float">55.9391479</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.08888531</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">228.139359</X>
                            <Y dataType="Float">55.9391479</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.08888531</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
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
                            <X dataType="Float">228.139359</X>
                            <Y dataType="Float">55.9391479</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">228.139359</X>
                            <Y dataType="Float">55.9391479</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">5.08888531</lastAngle>
                          <lastAngleAbs dataType="Float">5.08888531</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="162" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="163">
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
                              <revolutions dataType="Float">1.61984241</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="164">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="165" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="166">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="167" length="4">
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
                                    <parent dataType="ObjectRef">163</parent>
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
                              <gameobj dataType="ObjectRef">157</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="168" length="1">
                              <object dataType="ObjectRef">162</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">157</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="169">
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
                          <gameobj dataType="ObjectRef">157</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">163</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="170">
                    <_items dataType="Array" type="Duality.Component[]" id="171" length="4">
                      <object dataType="ObjectRef">161</object>
                      <object dataType="ObjectRef">169</object>
                      <object dataType="ObjectRef">163</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">161</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="172">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="173" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="174" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="175" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="176">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-234.940765</X>
                            <Y dataType="Float">37.9006538</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">4.35941935</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-234.940765</X>
                            <Y dataType="Float">37.9006538</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">4.35941935</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
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
                            <X dataType="Float">-234.940765</X>
                            <Y dataType="Float">37.9006538</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-234.940765</X>
                            <Y dataType="Float">37.9006538</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">4.35941935</lastAngle>
                          <lastAngleAbs dataType="Float">4.35941935</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="177" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="178">
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
                              <revolutions dataType="Float">1.3876462</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="179">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="180" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="181">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="182" length="4">
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
                                    <parent dataType="ObjectRef">178</parent>
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
                              <gameobj dataType="ObjectRef">172</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="183" length="1">
                              <object dataType="ObjectRef">177</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">172</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="184">
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
                          <gameobj dataType="ObjectRef">172</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">178</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="185">
                    <_items dataType="Array" type="Duality.Component[]" id="186" length="4">
                      <object dataType="ObjectRef">176</object>
                      <object dataType="ObjectRef">184</object>
                      <object dataType="ObjectRef">178</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">176</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="187">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="188" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="189" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="190" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="191">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">138.453247</X>
                            <Y dataType="Float">-77.91572</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.87791348</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">138.453247</X>
                            <Y dataType="Float">-77.91572</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.87791348</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
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
                            <X dataType="Float">138.453247</X>
                            <Y dataType="Float">-77.91572</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">138.453247</X>
                            <Y dataType="Float">-77.91572</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">5.87791348</lastAngle>
                          <lastAngleAbs dataType="Float">5.87791348</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="192" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="193">
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
                              <revolutions dataType="Float">1.87099791</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="194">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="195" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="196">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="197" length="4">
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
                                    <parent dataType="ObjectRef">193</parent>
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
                              <gameobj dataType="ObjectRef">187</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="198" length="1">
                              <object dataType="ObjectRef">192</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">187</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="199">
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
                          <gameobj dataType="ObjectRef">187</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">193</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="200">
                    <_items dataType="Array" type="Duality.Component[]" id="201" length="4">
                      <object dataType="ObjectRef">191</object>
                      <object dataType="ObjectRef">199</object>
                      <object dataType="ObjectRef">193</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">191</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="202">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="203" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="204" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="205" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="206">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-6.84147835</X>
                            <Y dataType="Float">-88.93236</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0.3752693</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-6.84147835</X>
                            <Y dataType="Float">-88.93236</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0.3752693</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
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
                            <X dataType="Float">-6.84147835</X>
                            <Y dataType="Float">-88.93236</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-6.84147835</X>
                            <Y dataType="Float">-88.93236</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0.3752693</lastAngle>
                          <lastAngleAbs dataType="Float">0.3752693</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="207" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="208">
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
                              <revolutions dataType="Float">0.119451925</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="209">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="210" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="211">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="212" length="4">
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
                                    <parent dataType="ObjectRef">208</parent>
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
                              <gameobj dataType="ObjectRef">202</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="213" length="1">
                              <object dataType="ObjectRef">207</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">202</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="214">
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
                          <gameobj dataType="ObjectRef">202</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">208</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="215">
                    <_items dataType="Array" type="Duality.Component[]" id="216" length="4">
                      <object dataType="ObjectRef">206</object>
                      <object dataType="ObjectRef">214</object>
                      <object dataType="ObjectRef">208</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">206</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="217">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="218" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="219" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="220" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="221">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">103.445923</X>
                            <Y dataType="Float">11.63135</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.780141</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.108402841</X>
                            <Y dataType="Float">0.108402841</Y>
                            <Z dataType="Float">0.108402841</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">103.445923</X>
                            <Y dataType="Float">11.63135</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.780141</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.108402841</X>
                            <Y dataType="Float">0.108402841</Y>
                            <Z dataType="Float">0.108402841</Z>
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
                            <X dataType="Float">103.445923</X>
                            <Y dataType="Float">11.63135</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">103.445923</X>
                            <Y dataType="Float">11.63135</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">5.780141</lastAngle>
                          <lastAngleAbs dataType="Float">5.780141</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="222" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="223">
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
                              <revolutions dataType="Float">1.83987594</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="224">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="225" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="226">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="227" length="4">
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
                                    <parent dataType="ObjectRef">223</parent>
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
                              <gameobj dataType="ObjectRef">217</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="228" length="1">
                              <object dataType="ObjectRef">222</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">217</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="229">
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
                          <gameobj dataType="ObjectRef">217</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">223</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="230">
                    <_items dataType="Array" type="Duality.Component[]" id="231" length="4">
                      <object dataType="ObjectRef">221</object>
                      <object dataType="ObjectRef">229</object>
                      <object dataType="ObjectRef">223</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">221</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="232">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="233" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="234" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="235" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="236">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-320.101166</X>
                            <Y dataType="Float">-124.154022</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">4.730546</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-320.101166</X>
                            <Y dataType="Float">-124.154022</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">4.730546</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
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
                            <X dataType="Float">-320.101166</X>
                            <Y dataType="Float">-124.154022</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-320.101166</X>
                            <Y dataType="Float">-124.154022</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">4.730546</lastAngle>
                          <lastAngleAbs dataType="Float">4.730546</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="237" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="238">
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
                              <revolutions dataType="Float">1.5057795</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="239">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="240" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="241">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="242" length="4">
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
                                    <parent dataType="ObjectRef">238</parent>
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
                              <gameobj dataType="ObjectRef">232</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="243" length="1">
                              <object dataType="ObjectRef">237</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">232</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="244">
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
                          <gameobj dataType="ObjectRef">232</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">238</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="245">
                    <_items dataType="Array" type="Duality.Component[]" id="246" length="4">
                      <object dataType="ObjectRef">236</object>
                      <object dataType="ObjectRef">244</object>
                      <object dataType="ObjectRef">238</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">236</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="247">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="248" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="249" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="250" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="251">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-275.452454</X>
                            <Y dataType="Float">151.173523</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">3.86887264</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-275.452454</X>
                            <Y dataType="Float">151.173523</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">3.86887264</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
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
                            <X dataType="Float">-275.452454</X>
                            <Y dataType="Float">151.173523</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-275.452454</X>
                            <Y dataType="Float">151.173523</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">3.86887264</lastAngle>
                          <lastAngleAbs dataType="Float">3.86887264</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="252" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="253">
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
                              <revolutions dataType="Float">1.23150039</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="254">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="255" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="256">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="257" length="4">
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
                                    <parent dataType="ObjectRef">253</parent>
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
                              <gameobj dataType="ObjectRef">247</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="258" length="1">
                              <object dataType="ObjectRef">252</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">247</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="259">
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
                          <gameobj dataType="ObjectRef">247</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">253</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="260">
                    <_items dataType="Array" type="Duality.Component[]" id="261" length="4">
                      <object dataType="ObjectRef">251</object>
                      <object dataType="ObjectRef">259</object>
                      <object dataType="ObjectRef">253</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">251</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="262">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="263" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="264" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="265" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="266">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-338.289</X>
                            <Y dataType="Float">79.2411</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">4.10604429</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.2101587</X>
                            <Y dataType="Float">0.2101587</Y>
                            <Z dataType="Float">0.2101587</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-338.289</X>
                            <Y dataType="Float">79.2411</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">4.10604429</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.2101587</X>
                            <Y dataType="Float">0.2101587</Y>
                            <Z dataType="Float">0.2101587</Z>
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
                            <X dataType="Float">-338.289</X>
                            <Y dataType="Float">79.2411</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-338.289</X>
                            <Y dataType="Float">79.2411</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">4.10604429</lastAngle>
                          <lastAngleAbs dataType="Float">4.10604429</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="267" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="268">
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
                              <revolutions dataType="Float">1.30699444</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="269">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="270" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="271">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="272" length="4">
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
                                    <parent dataType="ObjectRef">268</parent>
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
                              <gameobj dataType="ObjectRef">262</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="273" length="1">
                              <object dataType="ObjectRef">267</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">262</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="274">
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
                          <gameobj dataType="ObjectRef">262</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">268</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="275">
                    <_items dataType="Array" type="Duality.Component[]" id="276" length="4">
                      <object dataType="ObjectRef">266</object>
                      <object dataType="ObjectRef">274</object>
                      <object dataType="ObjectRef">268</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">266</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="277">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="278" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="279" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="280" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="281">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">278.575745</X>
                            <Y dataType="Float">149.368622</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.36169767</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">278.575745</X>
                            <Y dataType="Float">149.368622</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.36169767</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
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
                            <X dataType="Float">278.575745</X>
                            <Y dataType="Float">149.368622</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">278.575745</X>
                            <Y dataType="Float">149.368622</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">5.36169767</lastAngle>
                          <lastAngleAbs dataType="Float">5.36169767</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="282" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="283">
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
                              <revolutions dataType="Float">1.70668137</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="284">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="285" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="286">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="287" length="4">
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
                                    <parent dataType="ObjectRef">283</parent>
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
                              <gameobj dataType="ObjectRef">277</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="288" length="1">
                              <object dataType="ObjectRef">282</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">277</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="289">
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
                          <gameobj dataType="ObjectRef">277</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">283</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="290">
                    <_items dataType="Array" type="Duality.Component[]" id="291" length="4">
                      <object dataType="ObjectRef">281</object>
                      <object dataType="ObjectRef">289</object>
                      <object dataType="ObjectRef">283</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">281</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="292">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="293" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="294" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="295" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="296">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">338.933868</X>
                            <Y dataType="Float">60.9</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.21575975</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.241430372</X>
                            <Y dataType="Float">0.241430372</Y>
                            <Z dataType="Float">0.241430372</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">338.933868</X>
                            <Y dataType="Float">60.9</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.21575975</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.241430372</X>
                            <Y dataType="Float">0.241430372</Y>
                            <Z dataType="Float">0.241430372</Z>
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
                            <X dataType="Float">338.933868</X>
                            <Y dataType="Float">60.9</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">338.933868</X>
                            <Y dataType="Float">60.9</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">5.21575975</lastAngle>
                          <lastAngleAbs dataType="Float">5.21575975</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="297" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="298">
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
                              <revolutions dataType="Float">1.66022789</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="299">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="300" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="301">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="302" length="4">
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
                                    <parent dataType="ObjectRef">298</parent>
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
                              <gameobj dataType="ObjectRef">292</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="303" length="1">
                              <object dataType="ObjectRef">297</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">292</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="304">
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
                          <gameobj dataType="ObjectRef">292</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">298</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="305">
                    <_items dataType="Array" type="Duality.Component[]" id="306" length="4">
                      <object dataType="ObjectRef">296</object>
                      <object dataType="ObjectRef">304</object>
                      <object dataType="ObjectRef">298</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">296</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="307">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="308" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="309" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="310" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="311">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-118.572273</X>
                            <Y dataType="Float">-27.4348717</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0.0540567636</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-118.572273</X>
                            <Y dataType="Float">-27.4348717</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0.0540567636</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.193000838</X>
                            <Y dataType="Float">0.193000838</Y>
                            <Z dataType="Float">0.193000838</Z>
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
                            <X dataType="Float">-118.572273</X>
                            <Y dataType="Float">-27.4348717</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-118.572273</X>
                            <Y dataType="Float">-27.4348717</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0.0540567636</lastAngle>
                          <lastAngleAbs dataType="Float">0.0540567636</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="312" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="313">
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
                              <revolutions dataType="Float">0.0172068011</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="314">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="315" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="316">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="317" length="4">
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
                                    <parent dataType="ObjectRef">313</parent>
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
                              <gameobj dataType="ObjectRef">307</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="318" length="1">
                              <object dataType="ObjectRef">312</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">307</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="319">
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
                          <gameobj dataType="ObjectRef">307</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">313</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="320">
                    <_items dataType="Array" type="Duality.Component[]" id="321" length="4">
                      <object dataType="ObjectRef">311</object>
                      <object dataType="ObjectRef">319</object>
                      <object dataType="ObjectRef">313</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">311</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="322">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="323" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="324" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="325" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="326">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">397.208679</X>
                            <Y dataType="Float">-41.079155</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.21575975</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.241430372</X>
                            <Y dataType="Float">0.241430372</Y>
                            <Z dataType="Float">0.241430372</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">397.208679</X>
                            <Y dataType="Float">-41.079155</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.21575975</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.241430372</X>
                            <Y dataType="Float">0.241430372</Y>
                            <Z dataType="Float">0.241430372</Z>
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
                            <X dataType="Float">397.208679</X>
                            <Y dataType="Float">-41.079155</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">397.208679</X>
                            <Y dataType="Float">-41.079155</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">5.21575975</lastAngle>
                          <lastAngleAbs dataType="Float">5.21575975</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="327" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="328">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="329">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="330" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="331">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="332" length="4">
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
                                    <parent dataType="ObjectRef">328</parent>
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
                              <gameobj dataType="ObjectRef">322</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="333" length="1">
                              <object dataType="ObjectRef">327</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">322</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="334">
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
                          <gameobj dataType="ObjectRef">322</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">328</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="335">
                    <_items dataType="Array" type="Duality.Component[]" id="336" length="4">
                      <object dataType="ObjectRef">326</object>
                      <object dataType="ObjectRef">334</object>
                      <object dataType="ObjectRef">328</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">326</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="337">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="338" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="339" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="340" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="341">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">455.4835</X>
                            <Y dataType="Float">-145.1395</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.21575975</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.241430372</X>
                            <Y dataType="Float">0.241430372</Y>
                            <Z dataType="Float">0.241430372</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">455.4835</X>
                            <Y dataType="Float">-145.1395</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.21575975</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.241430372</X>
                            <Y dataType="Float">0.241430372</Y>
                            <Z dataType="Float">0.241430372</Z>
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
                            <X dataType="Float">455.4835</X>
                            <Y dataType="Float">-145.1395</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">455.4835</X>
                            <Y dataType="Float">-145.1395</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">5.21575975</lastAngle>
                          <lastAngleAbs dataType="Float">5.21575975</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="342" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="343">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="344">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="345" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="346">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="347" length="4">
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
                                    <parent dataType="ObjectRef">343</parent>
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
                              <gameobj dataType="ObjectRef">337</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="348" length="1">
                              <object dataType="ObjectRef">342</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">337</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="349">
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
                          <gameobj dataType="ObjectRef">337</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">343</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="350">
                    <_items dataType="Array" type="Duality.Component[]" id="351" length="4">
                      <object dataType="ObjectRef">341</object>
                      <object dataType="ObjectRef">349</object>
                      <object dataType="ObjectRef">343</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">341</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="352">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="353" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="354" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="355" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="356">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-395.7032</X>
                            <Y dataType="Float">-1.56196976</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">4.10604429</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.2101587</X>
                            <Y dataType="Float">0.2101587</Y>
                            <Z dataType="Float">0.2101587</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-395.7032</X>
                            <Y dataType="Float">-1.56196976</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">4.10604429</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.2101587</X>
                            <Y dataType="Float">0.2101587</Y>
                            <Z dataType="Float">0.2101587</Z>
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
                            <X dataType="Float">-395.7032</X>
                            <Y dataType="Float">-1.56196976</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-395.7032</X>
                            <Y dataType="Float">-1.56196976</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">4.10604429</lastAngle>
                          <lastAngleAbs dataType="Float">4.10604429</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="357" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="358">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="359">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="360" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="361">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="362" length="4">
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
                                    <parent dataType="ObjectRef">358</parent>
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
                              <gameobj dataType="ObjectRef">352</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="363" length="1">
                              <object dataType="ObjectRef">357</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">352</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="364">
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
                          <gameobj dataType="ObjectRef">352</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">358</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="365">
                    <_items dataType="Array" type="Duality.Component[]" id="366" length="4">
                      <object dataType="ObjectRef">356</object>
                      <object dataType="ObjectRef">364</object>
                      <object dataType="ObjectRef">358</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">356</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="367">
                  <prefabLink />
                  <parent dataType="ObjectRef">72</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="368" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="369" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="370" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="371">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-458.138458</X>
                            <Y dataType="Float">-86.5274658</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">4.10604429</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.2101587</X>
                            <Y dataType="Float">0.2101587</Y>
                            <Z dataType="Float">0.2101587</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-458.138458</X>
                            <Y dataType="Float">-86.5274658</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">4.10604429</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.2101587</X>
                            <Y dataType="Float">0.2101587</Y>
                            <Z dataType="Float">0.2101587</Z>
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
                            <X dataType="Float">-458.138458</X>
                            <Y dataType="Float">-86.5274658</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-458.138458</X>
                            <Y dataType="Float">-86.5274658</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">4.10604429</lastAngle>
                          <lastAngleAbs dataType="Float">4.10604429</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="372" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="373">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="374">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="375" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="376">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="377" length="4">
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
                                    <parent dataType="ObjectRef">373</parent>
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
                              <gameobj dataType="ObjectRef">367</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="378" length="1">
                              <object dataType="ObjectRef">372</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">367</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="379">
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
                          <gameobj dataType="ObjectRef">367</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">373</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="380">
                    <_items dataType="Array" type="Duality.Component[]" id="381" length="4">
                      <object dataType="ObjectRef">371</object>
                      <object dataType="ObjectRef">379</object>
                      <object dataType="ObjectRef">373</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">371</compTransform>
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
              </_items>
              <_size dataType="Int">20</_size>
              <_version dataType="Int">24</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="382" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="383" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="384" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="385">
              <_items dataType="Array" type="Duality.Component[]" id="386" length="0" />
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <name dataType="String">StaticWorld</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="387" multi="true">
              <object dataType="MethodInfo" id="388" value="M:Duality.Components.Transform:Parent_EventComponentAdded(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">371</object>
              <object dataType="Array" type="System.Delegate[]" id="389" length="21">
                <object dataType="ObjectRef">66</object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="390" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">81</object>
                  <object dataType="Array" type="System.Delegate[]" id="391" length="1">
                    <object dataType="ObjectRef">390</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="392" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">99</object>
                  <object dataType="Array" type="System.Delegate[]" id="393" length="1">
                    <object dataType="ObjectRef">392</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="394" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">115</object>
                  <object dataType="Array" type="System.Delegate[]" id="395" length="1">
                    <object dataType="ObjectRef">394</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="396" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">131</object>
                  <object dataType="Array" type="System.Delegate[]" id="397" length="1">
                    <object dataType="ObjectRef">396</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="398" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">146</object>
                  <object dataType="Array" type="System.Delegate[]" id="399" length="1">
                    <object dataType="ObjectRef">398</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="400" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">161</object>
                  <object dataType="Array" type="System.Delegate[]" id="401" length="1">
                    <object dataType="ObjectRef">400</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="402" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">176</object>
                  <object dataType="Array" type="System.Delegate[]" id="403" length="1">
                    <object dataType="ObjectRef">402</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="404" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">191</object>
                  <object dataType="Array" type="System.Delegate[]" id="405" length="1">
                    <object dataType="ObjectRef">404</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="406" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">206</object>
                  <object dataType="Array" type="System.Delegate[]" id="407" length="1">
                    <object dataType="ObjectRef">406</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="408" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">221</object>
                  <object dataType="Array" type="System.Delegate[]" id="409" length="1">
                    <object dataType="ObjectRef">408</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="410" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">236</object>
                  <object dataType="Array" type="System.Delegate[]" id="411" length="1">
                    <object dataType="ObjectRef">410</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="412" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">251</object>
                  <object dataType="Array" type="System.Delegate[]" id="413" length="1">
                    <object dataType="ObjectRef">412</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="414" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">266</object>
                  <object dataType="Array" type="System.Delegate[]" id="415" length="1">
                    <object dataType="ObjectRef">414</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="416" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">281</object>
                  <object dataType="Array" type="System.Delegate[]" id="417" length="1">
                    <object dataType="ObjectRef">416</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="418" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">296</object>
                  <object dataType="Array" type="System.Delegate[]" id="419" length="1">
                    <object dataType="ObjectRef">418</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="420" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">311</object>
                  <object dataType="Array" type="System.Delegate[]" id="421" length="1">
                    <object dataType="ObjectRef">420</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="422" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">326</object>
                  <object dataType="Array" type="System.Delegate[]" id="423" length="1">
                    <object dataType="ObjectRef">422</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="424" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">341</object>
                  <object dataType="Array" type="System.Delegate[]" id="425" length="1">
                    <object dataType="ObjectRef">424</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="426" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">356</object>
                  <object dataType="Array" type="System.Delegate[]" id="427" length="1">
                    <object dataType="ObjectRef">426</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="428" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">371</object>
                  <object dataType="Array" type="System.Delegate[]" id="429" length="1">
                    <object dataType="ObjectRef">428</object>
                  </object>
                </object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="430">
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="431">
              <_items dataType="Array" type="Duality.GameObject[]" id="432" length="64">
                <object dataType="Class" type="Duality.GameObject" id="433">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="434">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">433</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="435">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="436" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="437" value="P:Duality.Components.Transform:RelativePos" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="438">
                            <_items dataType="Array" type="System.Int32[]" id="439" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">7.52377224</X>
                            <Y dataType="Float">-204.646591</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="440" value="P:Duality.Components.Transform:RelativeScale" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="441">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.08366492</X>
                            <Y dataType="Float">0.08366492</Y>
                            <Z dataType="Float">0.08366492</Z>
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
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">322</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="442" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="443" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="444" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="445">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">7.52377224</X>
                            <Y dataType="Float">-204.646591</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.08366492</X>
                            <Y dataType="Float">0.08366492</Y>
                            <Z dataType="Float">0.08366492</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">7.52377224</X>
                            <Y dataType="Float">-204.646591</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.08366492</X>
                            <Y dataType="Float">0.08366492</Y>
                            <Z dataType="Float">0.08366492</Z>
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
                            <X dataType="Float">7.52377224</X>
                            <Y dataType="Float">-204.646591</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">7.52377224</X>
                            <Y dataType="Float">-204.646591</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="446" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="447">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="448">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="449" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="450">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">447</parent>
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
                              <gameobj dataType="ObjectRef">433</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="451" length="1">
                              <object dataType="ObjectRef">446</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">433</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="452">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">433</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">447</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="453">
                    <_items dataType="Array" type="Duality.Component[]" id="454" length="4">
                      <object dataType="ObjectRef">445</object>
                      <object dataType="ObjectRef">452</object>
                      <object dataType="ObjectRef">447</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">445</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="455">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="456">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">455</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="457">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="458" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="459">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">133.171753</X>
                            <Y dataType="Float">-173.046768</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="460">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">58</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="461" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="462" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="463" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="464">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">133.171753</X>
                            <Y dataType="Float">-173.046768</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">133.171753</X>
                            <Y dataType="Float">-173.046768</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
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
                            <X dataType="Float">133.171753</X>
                            <Y dataType="Float">-173.046768</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">133.171753</X>
                            <Y dataType="Float">-173.046768</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="465" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="466">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="467">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="468" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="469">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">466</parent>
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
                              <gameobj dataType="ObjectRef">455</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="470" length="1">
                              <object dataType="ObjectRef">465</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">455</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="471">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">455</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">466</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="472">
                    <_items dataType="Array" type="Duality.Component[]" id="473" length="4">
                      <object dataType="ObjectRef">464</object>
                      <object dataType="ObjectRef">471</object>
                      <object dataType="ObjectRef">466</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">464</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="474">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="475">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">474</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="476">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="477" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="478">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.110788427</X>
                            <Y dataType="Float">0.110788427</Y>
                            <Z dataType="Float">0.110788427</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="479">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">67.1349258</X>
                            <Y dataType="Float">-178.300415</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">254</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="480" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="481" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="482" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="483">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">67.1349258</X>
                            <Y dataType="Float">-178.300415</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.110788427</X>
                            <Y dataType="Float">0.110788427</Y>
                            <Z dataType="Float">0.110788427</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">67.1349258</X>
                            <Y dataType="Float">-178.300415</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.110788427</X>
                            <Y dataType="Float">0.110788427</Y>
                            <Z dataType="Float">0.110788427</Z>
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
                            <X dataType="Float">67.1349258</X>
                            <Y dataType="Float">-178.300415</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">67.1349258</X>
                            <Y dataType="Float">-178.300415</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="484" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="485">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="486">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="487" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="488">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">485</parent>
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
                              <gameobj dataType="ObjectRef">474</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="489" length="1">
                              <object dataType="ObjectRef">484</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">474</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="490">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">474</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">485</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="491">
                    <_items dataType="Array" type="Duality.Component[]" id="492" length="4">
                      <object dataType="ObjectRef">483</object>
                      <object dataType="ObjectRef">490</object>
                      <object dataType="ObjectRef">485</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">483</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="493">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="494">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">493</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="495">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="496" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="497">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-108.897751</X>
                            <Y dataType="Float">-192.076965</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="498">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.1699463</X>
                            <Y dataType="Float">0.1699463</Y>
                            <Z dataType="Float">0.1699463</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">98</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="499" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="500" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="501" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="502">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-108.897751</X>
                            <Y dataType="Float">-192.076965</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.1699463</X>
                            <Y dataType="Float">0.1699463</Y>
                            <Z dataType="Float">0.1699463</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-108.897751</X>
                            <Y dataType="Float">-192.076965</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.1699463</X>
                            <Y dataType="Float">0.1699463</Y>
                            <Z dataType="Float">0.1699463</Z>
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
                            <X dataType="Float">-108.897751</X>
                            <Y dataType="Float">-192.076965</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-108.897751</X>
                            <Y dataType="Float">-192.076965</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="503" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="504">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="505">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="506" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="507">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">504</parent>
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
                              <gameobj dataType="ObjectRef">493</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="508" length="1">
                              <object dataType="ObjectRef">503</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">493</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="509">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">493</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">504</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="510">
                    <_items dataType="Array" type="Duality.Component[]" id="511" length="4">
                      <object dataType="ObjectRef">502</object>
                      <object dataType="ObjectRef">509</object>
                      <object dataType="ObjectRef">504</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">502</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="512">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="513">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\RoundSquare.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">512</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="514">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="515" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="516">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.207024947</X>
                            <Y dataType="Float">0.207024947</Y>
                            <Z dataType="Float">0.207024947</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="517">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-318.626984</X>
                            <Y dataType="Float">-203.182419</Y>
                            <Z dataType="Float">0</Z>
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
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">452</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="518" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="519" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="520" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="521">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-318.626984</X>
                            <Y dataType="Float">-203.182419</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.207024947</X>
                            <Y dataType="Float">0.207024947</Y>
                            <Z dataType="Float">0.207024947</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-318.626984</X>
                            <Y dataType="Float">-203.182419</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.207024947</X>
                            <Y dataType="Float">0.207024947</Y>
                            <Z dataType="Float">0.207024947</Z>
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
                            <X dataType="Float">-318.626984</X>
                            <Y dataType="Float">-203.182419</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-318.626984</X>
                            <Y dataType="Float">-203.182419</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="522" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="523">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="524">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="525" length="8">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="526">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">523</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="527">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">523</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="528">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">523</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="529">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">523</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="530">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="531" length="8">
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
                                    <parent dataType="ObjectRef">523</parent>
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
                              <gameobj dataType="ObjectRef">512</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="532" length="1">
                              <object dataType="ObjectRef">522</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">512</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="533">
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
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">238</G>
                            <B dataType="Byte">76</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">512</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">523</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="534">
                    <_items dataType="Array" type="Duality.Component[]" id="535" length="4">
                      <object dataType="ObjectRef">521</object>
                      <object dataType="ObjectRef">533</object>
                      <object dataType="ObjectRef">523</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">RoundSquare</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">521</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="536">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="537">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\RoundSquare.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">536</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="538">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="539" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="540">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">170.858948</X>
                            <Y dataType="Float">-137.379028</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="541">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.09788746</X>
                            <Y dataType="Float">0.09788746</Y>
                            <Z dataType="Float">0.09788746</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">382</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="542" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="543" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="544" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="545">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">170.858948</X>
                            <Y dataType="Float">-137.379028</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.09788746</X>
                            <Y dataType="Float">0.09788746</Y>
                            <Z dataType="Float">0.09788746</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">170.858948</X>
                            <Y dataType="Float">-137.379028</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.09788746</X>
                            <Y dataType="Float">0.09788746</Y>
                            <Z dataType="Float">0.09788746</Z>
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
                            <X dataType="Float">170.858948</X>
                            <Y dataType="Float">-137.379028</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">170.858948</X>
                            <Y dataType="Float">-137.379028</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="546" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="547">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="548">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="549" length="8">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="550">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">547</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="551">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">547</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="552">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">547</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="553">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">547</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="554">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="555" length="8">
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
                                    <parent dataType="ObjectRef">547</parent>
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
                              <gameobj dataType="ObjectRef">536</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="556" length="1">
                              <object dataType="ObjectRef">546</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">536</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="557">
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
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">238</G>
                            <B dataType="Byte">76</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">536</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">547</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="558">
                    <_items dataType="Array" type="Duality.Component[]" id="559" length="4">
                      <object dataType="ObjectRef">545</object>
                      <object dataType="ObjectRef">557</object>
                      <object dataType="ObjectRef">547</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">RoundSquare</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">545</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="560">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="561">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">560</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="562">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="563" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="564">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.20108597</X>
                            <Y dataType="Float">0.20108597</Y>
                            <Z dataType="Float">0.20108597</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="565">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-145.460144</X>
                            <Y dataType="Float">-75.53899</Y>
                            <Z dataType="Float">0</Z>
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
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">346</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="566" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="567" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="568" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="569">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-145.460144</X>
                            <Y dataType="Float">-75.53899</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.20108597</X>
                            <Y dataType="Float">0.20108597</Y>
                            <Z dataType="Float">0.20108597</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-145.460144</X>
                            <Y dataType="Float">-75.53899</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.20108597</X>
                            <Y dataType="Float">0.20108597</Y>
                            <Z dataType="Float">0.20108597</Z>
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
                            <X dataType="Float">-145.460144</X>
                            <Y dataType="Float">-75.53899</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-145.460144</X>
                            <Y dataType="Float">-75.53899</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="570" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="571">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="572">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="573" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="574">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="575" length="4">
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
                                    <parent dataType="ObjectRef">571</parent>
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
                              <gameobj dataType="ObjectRef">560</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="576" length="1">
                              <object dataType="ObjectRef">570</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">560</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="577">
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
                          <gameobj dataType="ObjectRef">560</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">571</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="578">
                    <_items dataType="Array" type="Duality.Component[]" id="579" length="4">
                      <object dataType="ObjectRef">569</object>
                      <object dataType="ObjectRef">577</object>
                      <object dataType="ObjectRef">571</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">569</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="580">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="581">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">580</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="582">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="583" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="584">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.119707078</X>
                            <Y dataType="Float">0.119707078</Y>
                            <Z dataType="Float">0.119707078</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="585">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-38.0957031</X>
                            <Y dataType="Float">-143.65126</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">136</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="586" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="587" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="588" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="589">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-38.0957031</X>
                            <Y dataType="Float">-143.65126</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.119707078</X>
                            <Y dataType="Float">0.119707078</Y>
                            <Z dataType="Float">0.119707078</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-38.0957031</X>
                            <Y dataType="Float">-143.65126</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.119707078</X>
                            <Y dataType="Float">0.119707078</Y>
                            <Z dataType="Float">0.119707078</Z>
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
                            <X dataType="Float">-38.0957031</X>
                            <Y dataType="Float">-143.65126</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-38.0957031</X>
                            <Y dataType="Float">-143.65126</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="590" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="591">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="592">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="593" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="594">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="595" length="4">
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
                                    <parent dataType="ObjectRef">591</parent>
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
                              <gameobj dataType="ObjectRef">580</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="596" length="1">
                              <object dataType="ObjectRef">590</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">580</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="597">
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
                          <gameobj dataType="ObjectRef">580</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">591</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="598">
                    <_items dataType="Array" type="Duality.Component[]" id="599" length="4">
                      <object dataType="ObjectRef">589</object>
                      <object dataType="ObjectRef">597</object>
                      <object dataType="ObjectRef">591</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">589</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="600">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="601">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">600</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="602">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="603" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="604">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">248.2078</X>
                            <Y dataType="Float">-18.9711456</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="605">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0719603449</X>
                            <Y dataType="Float">0.0719603449</Y>
                            <Z dataType="Float">0.0719603449</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">214</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="606" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="607" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="608" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="609">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">248.2078</X>
                            <Y dataType="Float">-18.9711456</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0719603449</X>
                            <Y dataType="Float">0.0719603449</Y>
                            <Z dataType="Float">0.0719603449</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">248.2078</X>
                            <Y dataType="Float">-18.9711456</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0719603449</X>
                            <Y dataType="Float">0.0719603449</Y>
                            <Z dataType="Float">0.0719603449</Z>
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
                            <X dataType="Float">248.2078</X>
                            <Y dataType="Float">-18.9711456</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">248.2078</X>
                            <Y dataType="Float">-18.9711456</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="610" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="611">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="612">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="613" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="614">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="615" length="4">
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
                                    <parent dataType="ObjectRef">611</parent>
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
                              <gameobj dataType="ObjectRef">600</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="616" length="1">
                              <object dataType="ObjectRef">610</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">600</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="617">
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
                          <gameobj dataType="ObjectRef">600</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">611</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="618">
                    <_items dataType="Array" type="Duality.Component[]" id="619" length="4">
                      <object dataType="ObjectRef">609</object>
                      <object dataType="ObjectRef">617</object>
                      <object dataType="ObjectRef">611</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">609</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="620">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="621">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Complex.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">620</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="622">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="623" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="624">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3182426</X>
                            <Y dataType="Float">0.3182426</Y>
                            <Z dataType="Float">0.3182426</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="625">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-244.3668</X>
                            <Y dataType="Float">-55.40873</Y>
                            <Z dataType="Float">0</Z>
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
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">328</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="626" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="627" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="628" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="629">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-244.3668</X>
                            <Y dataType="Float">-55.40873</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3182426</X>
                            <Y dataType="Float">0.3182426</Y>
                            <Z dataType="Float">0.3182426</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-244.3668</X>
                            <Y dataType="Float">-55.40873</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.3182426</X>
                            <Y dataType="Float">0.3182426</Y>
                            <Z dataType="Float">0.3182426</Z>
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
                            <X dataType="Float">-244.3668</X>
                            <Y dataType="Float">-55.40873</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-244.3668</X>
                            <Y dataType="Float">-55.40873</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="630" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="631">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="632">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="633" length="16">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="634">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="635" length="5">
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
                                    <parent dataType="ObjectRef">631</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="636">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="637" length="5">
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
                                    <parent dataType="ObjectRef">631</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="638">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="639" length="5">
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
                                    <parent dataType="ObjectRef">631</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="640">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="641" length="5">
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
                                    <parent dataType="ObjectRef">631</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="642">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="643" length="5">
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
                                    <parent dataType="ObjectRef">631</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="644">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="645" length="4">
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
                                    <parent dataType="ObjectRef">631</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="646">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="647" length="4">
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
                                    <parent dataType="ObjectRef">631</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="648">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="649" length="4">
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
                                    <parent dataType="ObjectRef">631</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="650">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="651" length="4">
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
                                    <parent dataType="ObjectRef">631</parent>
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
                              <gameobj dataType="ObjectRef">620</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="652" length="1">
                              <object dataType="ObjectRef">630</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">620</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="653">
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
                          <gameobj dataType="ObjectRef">620</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">631</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="654">
                    <_items dataType="Array" type="Duality.Component[]" id="655" length="4">
                      <object dataType="ObjectRef">629</object>
                      <object dataType="ObjectRef">653</object>
                      <object dataType="ObjectRef">631</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Complex</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">629</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="656">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="657">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Complex.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">656</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="658">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="659" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="660">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.156829</X>
                            <Y dataType="Float">0.156829</Y>
                            <Z dataType="Float">0.156829</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="661">
                            <_items dataType="ObjectRef">439</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">286.9899</X>
                            <Y dataType="Float">-160.543289</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">144</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="662" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="663" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="664" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="665">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">286.9899</X>
                            <Y dataType="Float">-160.543289</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.156829</X>
                            <Y dataType="Float">0.156829</Y>
                            <Z dataType="Float">0.156829</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">286.9899</X>
                            <Y dataType="Float">-160.543289</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.156829</X>
                            <Y dataType="Float">0.156829</Y>
                            <Z dataType="Float">0.156829</Z>
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
                            <X dataType="Float">286.9899</X>
                            <Y dataType="Float">-160.543289</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">286.9899</X>
                            <Y dataType="Float">-160.543289</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="666" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="667">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="668">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="669" length="16">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="670">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="671" length="5">
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
                                    <parent dataType="ObjectRef">667</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="672">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="673" length="5">
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
                                    <parent dataType="ObjectRef">667</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="674">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="675" length="5">
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
                                    <parent dataType="ObjectRef">667</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="676">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="677" length="5">
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
                                    <parent dataType="ObjectRef">667</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="678">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="679" length="5">
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
                                    <parent dataType="ObjectRef">667</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="680">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="681" length="4">
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
                                    <parent dataType="ObjectRef">667</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="682">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="683" length="4">
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
                                    <parent dataType="ObjectRef">667</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="684">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="685" length="4">
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
                                    <parent dataType="ObjectRef">667</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="686">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="687" length="4">
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
                                    <parent dataType="ObjectRef">667</parent>
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
                              <gameobj dataType="ObjectRef">656</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="688" length="1">
                              <object dataType="ObjectRef">666</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">656</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="689">
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
                          <gameobj dataType="ObjectRef">656</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">667</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="690">
                    <_items dataType="Array" type="Duality.Component[]" id="691" length="4">
                      <object dataType="ObjectRef">665</object>
                      <object dataType="ObjectRef">689</object>
                      <object dataType="ObjectRef">667</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Complex</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">665</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="692">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="693">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">692</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="694">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="695" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="696">
                            <_items dataType="Array" type="System.Int32[]" id="697" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.110788427</X>
                            <Y dataType="Float">0.110788427</Y>
                            <Z dataType="Float">0.110788427</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="698">
                            <_items dataType="Array" type="System.Int32[]" id="699" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-214.170044</X>
                            <Y dataType="Float">-151.3064</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">76</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="700" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="701" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="702" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="703">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-214.170044</X>
                            <Y dataType="Float">-151.3064</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.110788427</X>
                            <Y dataType="Float">0.110788427</Y>
                            <Z dataType="Float">0.110788427</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-214.170044</X>
                            <Y dataType="Float">-151.3064</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.110788427</X>
                            <Y dataType="Float">0.110788427</Y>
                            <Z dataType="Float">0.110788427</Z>
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
                            <X dataType="Float">-214.170044</X>
                            <Y dataType="Float">-151.3064</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-214.170044</X>
                            <Y dataType="Float">-151.3064</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="704" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="705">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="706">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="707" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="708">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">705</parent>
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
                              <gameobj dataType="ObjectRef">692</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="709" length="1">
                              <object dataType="ObjectRef">704</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">692</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="710">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">692</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">705</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="711">
                    <_items dataType="Array" type="Duality.Component[]" id="712" length="4">
                      <object dataType="ObjectRef">703</object>
                      <object dataType="ObjectRef">710</object>
                      <object dataType="ObjectRef">705</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">703</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="713">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="714">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\RoundSquare.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">713</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="715">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="716" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="717">
                            <_items dataType="Array" type="System.Int32[]" id="718" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.09788746</X>
                            <Y dataType="Float">0.09788746</Y>
                            <Z dataType="Float">0.09788746</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="719">
                            <_items dataType="ObjectRef">699</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-13.8358927</X>
                            <Y dataType="Float">50.1582832</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">84</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="720" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="721" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="722" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="723">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-13.8358927</X>
                            <Y dataType="Float">50.1582832</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.09788746</X>
                            <Y dataType="Float">0.09788746</Y>
                            <Z dataType="Float">0.09788746</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-13.8358927</X>
                            <Y dataType="Float">50.1582832</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.09788746</X>
                            <Y dataType="Float">0.09788746</Y>
                            <Z dataType="Float">0.09788746</Z>
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
                            <X dataType="Float">-13.8358927</X>
                            <Y dataType="Float">50.1582832</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-13.8358927</X>
                            <Y dataType="Float">50.1582832</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="724" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="725">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="726">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="727" length="8">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="728">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">725</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="729">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">725</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="730">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">725</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="731">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">725</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="732">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="733" length="8">
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
                                    <parent dataType="ObjectRef">725</parent>
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
                              <gameobj dataType="ObjectRef">713</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="734" length="1">
                              <object dataType="ObjectRef">724</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">713</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="735">
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
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">238</G>
                            <B dataType="Byte">76</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">713</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">725</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="736">
                    <_items dataType="Array" type="Duality.Component[]" id="737" length="4">
                      <object dataType="ObjectRef">723</object>
                      <object dataType="ObjectRef">735</object>
                      <object dataType="ObjectRef">725</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">RoundSquare</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">723</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="738">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="739">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">738</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="740">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="741" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="742">
                            <_items dataType="Array" type="System.Int32[]" id="743" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.119707078</X>
                            <Y dataType="Float">0.119707078</Y>
                            <Z dataType="Float">0.119707078</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="744">
                            <_items dataType="ObjectRef">699</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">64.19837</X>
                            <Y dataType="Float">134.813248</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="745" value="P:Duality.Components.Physics.RigidBody:Shapes" />
                          <componentType dataType="ObjectRef">79</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="746">
                            <_items dataType="Array" type="System.Int32[]" id="747" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="748">
                            <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="749" length="4">
                              <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="750">
                                <vertices dataType="Array" type="OpenTK.Vector2[]" id="751" length="4">
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
                                <parent dataType="Class" type="Duality.Components.Physics.RigidBody" id="752">
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
                                  <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="753">
                                    <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="754" length="4">
                                      <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="755">
                                        <vertices dataType="Array" type="OpenTK.Vector2[]" id="756" length="4">
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
                                        <parent dataType="ObjectRef">752</parent>
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
                                    <_version dataType="Int">3</_version>
                                  </shapes>
                                  <joints />
                                  <gameobj dataType="ObjectRef">738</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </parent>
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
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">221</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="757" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="758" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="759" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="760">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">64.19837</X>
                            <Y dataType="Float">134.813248</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.119707078</X>
                            <Y dataType="Float">0.119707078</Y>
                            <Z dataType="Float">0.119707078</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">64.19837</X>
                            <Y dataType="Float">134.813248</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.119707078</X>
                            <Y dataType="Float">0.119707078</Y>
                            <Z dataType="Float">0.119707078</Z>
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
                            <X dataType="Float">64.19837</X>
                            <Y dataType="Float">134.813248</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">64.19837</X>
                            <Y dataType="Float">134.813248</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="761" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="ObjectRef">752</object>
                            <object dataType="Array" type="System.Delegate[]" id="762" length="1">
                              <object dataType="ObjectRef">761</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">738</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="763">
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
                          <gameobj dataType="ObjectRef">738</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">752</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="764">
                    <_items dataType="Array" type="Duality.Component[]" id="765" length="4">
                      <object dataType="ObjectRef">760</object>
                      <object dataType="ObjectRef">763</object>
                      <object dataType="ObjectRef">752</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">760</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="766">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="767">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Complex.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">766</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="768">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="769" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="770">
                            <_items dataType="Array" type="System.Int32[]" id="771" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.156829</X>
                            <Y dataType="Float">0.156829</Y>
                            <Z dataType="Float">0.156829</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="772">
                            <_items dataType="ObjectRef">699</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-107.97406</X>
                            <Y dataType="Float">-274.202271</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">78</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="773" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="774" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="775" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="776">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-107.97406</X>
                            <Y dataType="Float">-274.202271</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.156829</X>
                            <Y dataType="Float">0.156829</Y>
                            <Z dataType="Float">0.156829</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-107.97406</X>
                            <Y dataType="Float">-274.202271</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.156829</X>
                            <Y dataType="Float">0.156829</Y>
                            <Z dataType="Float">0.156829</Z>
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
                            <X dataType="Float">-107.97406</X>
                            <Y dataType="Float">-274.202271</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-107.97406</X>
                            <Y dataType="Float">-274.202271</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="777" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="778">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="779">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="780" length="16">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="781">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="782" length="5">
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
                                    <parent dataType="ObjectRef">778</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="783">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="784" length="5">
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
                                    <parent dataType="ObjectRef">778</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="785">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="786" length="5">
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
                                    <parent dataType="ObjectRef">778</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="787">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="788" length="5">
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
                                    <parent dataType="ObjectRef">778</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="789">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="790" length="5">
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
                                    <parent dataType="ObjectRef">778</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="791">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="792" length="4">
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
                                    <parent dataType="ObjectRef">778</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="793">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="794" length="4">
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
                                    <parent dataType="ObjectRef">778</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="795">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="796" length="4">
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
                                    <parent dataType="ObjectRef">778</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="797">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="798" length="4">
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
                                    <parent dataType="ObjectRef">778</parent>
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
                              <gameobj dataType="ObjectRef">766</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="799" length="1">
                              <object dataType="ObjectRef">777</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">766</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="800">
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
                          <gameobj dataType="ObjectRef">766</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">778</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="801">
                    <_items dataType="Array" type="Duality.Component[]" id="802" length="4">
                      <object dataType="ObjectRef">776</object>
                      <object dataType="ObjectRef">800</object>
                      <object dataType="ObjectRef">778</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Complex</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">776</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="803">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="804">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">803</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="805">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="806" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="807">
                            <_items dataType="Array" type="System.Int32[]" id="808" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.08366492</X>
                            <Y dataType="Float">0.08366492</Y>
                            <Z dataType="Float">0.08366492</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="809">
                            <_items dataType="ObjectRef">699</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">125.445969</X>
                            <Y dataType="Float">-29.8959</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">144</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="810" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="811" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="812" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="813">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">125.445969</X>
                            <Y dataType="Float">-29.8959</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.08366492</X>
                            <Y dataType="Float">0.08366492</Y>
                            <Z dataType="Float">0.08366492</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">125.445969</X>
                            <Y dataType="Float">-29.8959</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.08366492</X>
                            <Y dataType="Float">0.08366492</Y>
                            <Z dataType="Float">0.08366492</Z>
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
                            <X dataType="Float">125.445969</X>
                            <Y dataType="Float">-29.8959</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">125.445969</X>
                            <Y dataType="Float">-29.8959</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="814" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="815">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="816">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="817" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="818">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">815</parent>
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
                              <gameobj dataType="ObjectRef">803</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="819" length="1">
                              <object dataType="ObjectRef">814</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">803</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="820">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">803</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">815</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="821">
                    <_items dataType="Array" type="Duality.Component[]" id="822" length="4">
                      <object dataType="ObjectRef">813</object>
                      <object dataType="ObjectRef">820</object>
                      <object dataType="ObjectRef">815</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">813</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="823">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="824">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">823</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="825">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="826" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="827">
                            <_items dataType="Array" type="System.Int32[]" id="828" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="829">
                            <_items dataType="ObjectRef">699</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-47.26087</X>
                            <Y dataType="Float">-185.8334</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">66</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="830" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="831" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="832" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="833">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-47.26087</X>
                            <Y dataType="Float">-185.8334</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-47.26087</X>
                            <Y dataType="Float">-185.8334</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
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
                            <X dataType="Float">-47.26087</X>
                            <Y dataType="Float">-185.8334</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-47.26087</X>
                            <Y dataType="Float">-185.8334</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="834" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="835">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="836">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="837" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="838">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">835</parent>
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
                              <gameobj dataType="ObjectRef">823</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="839" length="1">
                              <object dataType="ObjectRef">834</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">823</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="840">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">823</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">835</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="841">
                    <_items dataType="Array" type="Duality.Component[]" id="842" length="4">
                      <object dataType="ObjectRef">833</object>
                      <object dataType="ObjectRef">840</object>
                      <object dataType="ObjectRef">835</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">833</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="843">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="844">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">843</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="845">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="846" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="847">
                            <_items dataType="Array" type="System.Int32[]" id="848" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="849">
                            <_items dataType="ObjectRef">699</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">172.9544</X>
                            <Y dataType="Float">-218.510361</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">118</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="850" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="851" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="852" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="853">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">172.9544</X>
                            <Y dataType="Float">-218.510361</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">172.9544</X>
                            <Y dataType="Float">-218.510361</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
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
                            <X dataType="Float">172.9544</X>
                            <Y dataType="Float">-218.510361</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">172.9544</X>
                            <Y dataType="Float">-218.510361</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="854" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="855">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="856">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="857" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="858">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">855</parent>
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
                              <gameobj dataType="ObjectRef">843</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="859" length="1">
                              <object dataType="ObjectRef">854</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">843</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="860">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">843</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">855</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="861">
                    <_items dataType="Array" type="Duality.Component[]" id="862" length="4">
                      <object dataType="ObjectRef">853</object>
                      <object dataType="ObjectRef">860</object>
                      <object dataType="ObjectRef">855</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">853</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="863">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="864">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">863</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="865">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="866" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="867">
                            <_items dataType="Array" type="System.Int32[]" id="868" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="869">
                            <_items dataType="ObjectRef">699</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">96.2355957</X>
                            <Y dataType="Float">-278.1813</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">22</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="870" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="871" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="872" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="873">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">96.2355957</X>
                            <Y dataType="Float">-278.1813</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">96.2355957</X>
                            <Y dataType="Float">-278.1813</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
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
                            <X dataType="Float">96.2355957</X>
                            <Y dataType="Float">-278.1813</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">96.2355957</X>
                            <Y dataType="Float">-278.1813</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="874" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="875">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="876">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="877" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="878">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">875</parent>
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
                              <gameobj dataType="ObjectRef">863</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="879" length="1">
                              <object dataType="ObjectRef">874</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">863</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="880">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">863</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">875</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="881">
                    <_items dataType="Array" type="Duality.Component[]" id="882" length="4">
                      <object dataType="ObjectRef">873</object>
                      <object dataType="ObjectRef">880</object>
                      <object dataType="ObjectRef">875</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">873</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="883">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="884">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">883</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="885">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="886" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="887">
                            <_items dataType="Array" type="System.Int32[]" id="888" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="889" value="P:Duality.Components.Transform:RelativeAngle" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="890">
                            <_items dataType="ObjectRef">699</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">2.58536029</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="891">
                            <_items dataType="ObjectRef">699</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">58.1647873</X>
                            <Y dataType="Float">-226.701523</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">159</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="892" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="893" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="894" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="895">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">58.1647873</X>
                            <Y dataType="Float">-226.701523</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">2.58536029</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">58.1647873</X>
                            <Y dataType="Float">-226.701523</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">2.58536029</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
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
                            <X dataType="Float">58.1647873</X>
                            <Y dataType="Float">-226.701523</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">58.1647873</X>
                            <Y dataType="Float">-226.701523</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">2.58536029</lastAngle>
                          <lastAngleAbs dataType="Float">2.58536029</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="896" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="897">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="898">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="899" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="900">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">897</parent>
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
                              <gameobj dataType="ObjectRef">883</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="901" length="1">
                              <object dataType="ObjectRef">896</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">883</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="902">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">883</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">897</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="903">
                    <_items dataType="Array" type="Duality.Component[]" id="904" length="4">
                      <object dataType="ObjectRef">895</object>
                      <object dataType="ObjectRef">902</object>
                      <object dataType="ObjectRef">897</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">895</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="905">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="906">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">905</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="907">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="908" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="909">
                            <_items dataType="Array" type="System.Int32[]" id="910" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">889</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="911">
                            <_items dataType="ObjectRef">699</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">2.58536029</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="912">
                            <_items dataType="ObjectRef">699</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-3.50410461</X>
                            <Y dataType="Float">-258.624268</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">159</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="913" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="914" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="915" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="916">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-3.50410461</X>
                            <Y dataType="Float">-258.624268</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">2.58536029</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-3.50410461</X>
                            <Y dataType="Float">-258.624268</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">2.58536029</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
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
                            <X dataType="Float">-3.50410461</X>
                            <Y dataType="Float">-258.624268</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-3.50410461</X>
                            <Y dataType="Float">-258.624268</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">2.58536029</lastAngle>
                          <lastAngleAbs dataType="Float">2.58536029</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="917" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="918">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="919">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="920" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="921">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">918</parent>
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
                              <gameobj dataType="ObjectRef">905</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="922" length="1">
                              <object dataType="ObjectRef">917</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">905</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="923">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">905</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">918</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="924">
                    <_items dataType="Array" type="Duality.Component[]" id="925" length="4">
                      <object dataType="ObjectRef">916</object>
                      <object dataType="ObjectRef">923</object>
                      <object dataType="ObjectRef">918</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">916</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="926">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="927">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">926</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="928">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="929" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="930">
                            <_items dataType="Array" type="System.Int32[]" id="931" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.08366492</X>
                            <Y dataType="Float">0.08366492</Y>
                            <Z dataType="Float">0.08366492</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="932">
                            <_items dataType="Array" type="System.Int32[]" id="933" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">114.080063</X>
                            <Y dataType="Float">-231.6406</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">26</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="934" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="935" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="936" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="937">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">114.080063</X>
                            <Y dataType="Float">-231.6406</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.08366492</X>
                            <Y dataType="Float">0.08366492</Y>
                            <Z dataType="Float">0.08366492</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">114.080063</X>
                            <Y dataType="Float">-231.6406</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.08366492</X>
                            <Y dataType="Float">0.08366492</Y>
                            <Z dataType="Float">0.08366492</Z>
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
                            <X dataType="Float">114.080063</X>
                            <Y dataType="Float">-231.6406</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">114.080063</X>
                            <Y dataType="Float">-231.6406</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="938" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="939">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="940">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="941" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="942">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">939</parent>
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
                              <gameobj dataType="ObjectRef">926</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="943" length="1">
                              <object dataType="ObjectRef">938</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">926</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="944">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">926</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">939</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="945">
                    <_items dataType="Array" type="Duality.Component[]" id="946" length="4">
                      <object dataType="ObjectRef">937</object>
                      <object dataType="ObjectRef">944</object>
                      <object dataType="ObjectRef">939</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">937</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="947">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="948">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">947</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="949">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="950" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="951">
                            <_items dataType="Array" type="System.Int32[]" id="952" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.110788427</X>
                            <Y dataType="Float">0.110788427</Y>
                            <Z dataType="Float">0.110788427</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="953">
                            <_items dataType="ObjectRef">933</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">64.29445</X>
                            <Y dataType="Float">-314.691162</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">48</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="954" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="955" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="956" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="957">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">64.29445</X>
                            <Y dataType="Float">-314.691162</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.110788427</X>
                            <Y dataType="Float">0.110788427</Y>
                            <Z dataType="Float">0.110788427</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">64.29445</X>
                            <Y dataType="Float">-314.691162</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.110788427</X>
                            <Y dataType="Float">0.110788427</Y>
                            <Z dataType="Float">0.110788427</Z>
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
                            <X dataType="Float">64.29445</X>
                            <Y dataType="Float">-314.691162</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">64.29445</X>
                            <Y dataType="Float">-314.691162</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="958" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="959">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="960">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="961" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="962">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">959</parent>
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
                              <gameobj dataType="ObjectRef">947</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="963" length="1">
                              <object dataType="ObjectRef">958</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">947</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="964">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">947</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">959</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="965">
                    <_items dataType="Array" type="Duality.Component[]" id="966" length="4">
                      <object dataType="ObjectRef">957</object>
                      <object dataType="ObjectRef">964</object>
                      <object dataType="ObjectRef">959</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">957</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="967">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="968">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">967</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="969">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="970" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="971">
                            <_items dataType="Array" type="System.Int32[]" id="972" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.110788427</X>
                            <Y dataType="Float">0.110788427</Y>
                            <Z dataType="Float">0.110788427</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="973">
                            <_items dataType="ObjectRef">933</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-79.19904</X>
                            <Y dataType="Float">-401.356049</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">124</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="974" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="975" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="976" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="977">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-79.19904</X>
                            <Y dataType="Float">-401.356049</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.110788427</X>
                            <Y dataType="Float">0.110788427</Y>
                            <Z dataType="Float">0.110788427</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-79.19904</X>
                            <Y dataType="Float">-401.356049</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.110788427</X>
                            <Y dataType="Float">0.110788427</Y>
                            <Z dataType="Float">0.110788427</Z>
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
                            <X dataType="Float">-79.19904</X>
                            <Y dataType="Float">-401.356049</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-79.19904</X>
                            <Y dataType="Float">-401.356049</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="978" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="979">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="980">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="981" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="982">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">979</parent>
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
                              <gameobj dataType="ObjectRef">967</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="983" length="1">
                              <object dataType="ObjectRef">978</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">967</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="984">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">967</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">979</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="985">
                    <_items dataType="Array" type="Duality.Component[]" id="986" length="4">
                      <object dataType="ObjectRef">977</object>
                      <object dataType="ObjectRef">984</object>
                      <object dataType="ObjectRef">979</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">977</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="987">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="988">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">987</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="989">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="990" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="991">
                            <_items dataType="Array" type="System.Int32[]" id="992" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="993">
                            <_items dataType="ObjectRef">933</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-47.25788</X>
                            <Y dataType="Float">-364.846222</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">124</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="994" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="995" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="996" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="997">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-47.25788</X>
                            <Y dataType="Float">-364.846222</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-47.25788</X>
                            <Y dataType="Float">-364.846222</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
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
                            <X dataType="Float">-47.25788</X>
                            <Y dataType="Float">-364.846222</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-47.25788</X>
                            <Y dataType="Float">-364.846222</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="998" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="999">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1000">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1001" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1002">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">999</parent>
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
                              <gameobj dataType="ObjectRef">987</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1003" length="1">
                              <object dataType="ObjectRef">998</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">987</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1004">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">987</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">999</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1005">
                    <_items dataType="Array" type="Duality.Component[]" id="1006" length="4">
                      <object dataType="ObjectRef">997</object>
                      <object dataType="ObjectRef">1004</object>
                      <object dataType="ObjectRef">999</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">997</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1007">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1008">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1007</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1009">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1010" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1011">
                            <_items dataType="Array" type="System.Int32[]" id="1012" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.08366492</X>
                            <Y dataType="Float">0.08366492</Y>
                            <Z dataType="Float">0.08366492</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1013">
                            <_items dataType="ObjectRef">933</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-29.4134064</X>
                            <Y dataType="Float">-318.305542</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">124</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1014" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1015" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1016" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1017">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-29.4134064</X>
                            <Y dataType="Float">-318.305542</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.08366492</X>
                            <Y dataType="Float">0.08366492</Y>
                            <Z dataType="Float">0.08366492</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-29.4134064</X>
                            <Y dataType="Float">-318.305542</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.08366492</X>
                            <Y dataType="Float">0.08366492</Y>
                            <Z dataType="Float">0.08366492</Z>
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
                            <X dataType="Float">-29.4134064</X>
                            <Y dataType="Float">-318.305542</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-29.4134064</X>
                            <Y dataType="Float">-318.305542</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1018" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1019">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1020">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1021" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1022">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1019</parent>
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
                              <gameobj dataType="ObjectRef">1007</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1023" length="1">
                              <object dataType="ObjectRef">1018</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1007</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1024">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">1007</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1019</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1025">
                    <_items dataType="Array" type="Duality.Component[]" id="1026" length="4">
                      <object dataType="ObjectRef">1017</object>
                      <object dataType="ObjectRef">1024</object>
                      <object dataType="ObjectRef">1019</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1017</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1027">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1028">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1027</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1029">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1030" length="3">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1031">
                            <_items dataType="Array" type="System.Int32[]" id="1032" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">889</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1033">
                            <_items dataType="Array" type="System.Int32[]" id="1034" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Float">2.58536029</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1035">
                            <_items dataType="ObjectRef">933</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-85.3287048</X>
                            <Y dataType="Float">-313.366455</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">126</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1036" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1037" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1038" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1039">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-85.3287048</X>
                            <Y dataType="Float">-313.366455</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">2.58536029</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-85.3287048</X>
                            <Y dataType="Float">-313.366455</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">2.58536029</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
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
                            <X dataType="Float">-85.3287048</X>
                            <Y dataType="Float">-313.366455</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-85.3287048</X>
                            <Y dataType="Float">-313.366455</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">2.58536029</lastAngle>
                          <lastAngleAbs dataType="Float">2.58536029</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1040" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1041">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1042">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1043" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1044">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1041</parent>
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
                              <gameobj dataType="ObjectRef">1027</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1045" length="1">
                              <object dataType="ObjectRef">1040</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1027</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1046">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">1027</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1041</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1047">
                    <_items dataType="Array" type="Duality.Component[]" id="1048" length="4">
                      <object dataType="ObjectRef">1039</object>
                      <object dataType="ObjectRef">1046</object>
                      <object dataType="ObjectRef">1041</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1039</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1049">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1050">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1049</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1051">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1052" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1053">
                            <_items dataType="Array" type="System.Int32[]" id="1054" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1055">
                            <_items dataType="ObjectRef">933</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">29.46093</X>
                            <Y dataType="Float">-305.175323</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">124</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1056" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1057" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1058" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1059">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">29.46093</X>
                            <Y dataType="Float">-305.175323</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">29.46093</X>
                            <Y dataType="Float">-305.175323</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0461763255</X>
                            <Y dataType="Float">0.0461763255</Y>
                            <Z dataType="Float">0.0461763255</Z>
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
                            <X dataType="Float">29.46093</X>
                            <Y dataType="Float">-305.175323</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">29.46093</X>
                            <Y dataType="Float">-305.175323</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1060" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1061">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1062">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1063" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1064">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1061</parent>
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
                              <gameobj dataType="ObjectRef">1049</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1065" length="1">
                              <object dataType="ObjectRef">1060</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1049</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1066">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-128</X>
                            <Y dataType="Float">-128</Y>
                            <W dataType="Float">256</W>
                            <H dataType="Float">256</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\Sprites\Circle.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">164</G>
                            <B dataType="Byte">82</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">1049</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1061</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1067">
                    <_items dataType="Array" type="Duality.Component[]" id="1068" length="4">
                      <object dataType="ObjectRef">1059</object>
                      <object dataType="ObjectRef">1066</object>
                      <object dataType="ObjectRef">1061</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1059</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1069">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1070">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1069</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1071">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1072" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1073">
                            <_items dataType="Array" type="System.Int32[]" id="1074" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.119707078</X>
                            <Y dataType="Float">0.119707078</Y>
                            <Z dataType="Float">0.119707078</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1075">
                            <_items dataType="ObjectRef">933</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">25.83847</X>
                            <Y dataType="Float">-370.9692</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">54</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1076" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1077" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1078" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1079">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">25.83847</X>
                            <Y dataType="Float">-370.9692</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.119707078</X>
                            <Y dataType="Float">0.119707078</Y>
                            <Z dataType="Float">0.119707078</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">25.83847</X>
                            <Y dataType="Float">-370.9692</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.119707078</X>
                            <Y dataType="Float">0.119707078</Y>
                            <Z dataType="Float">0.119707078</Z>
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
                            <X dataType="Float">25.83847</X>
                            <Y dataType="Float">-370.9692</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">25.83847</X>
                            <Y dataType="Float">-370.9692</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1080" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1081">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1082">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1083" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="1084">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="1085" length="4">
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
                                    <parent dataType="ObjectRef">1081</parent>
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
                              <gameobj dataType="ObjectRef">1069</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1086" length="1">
                              <object dataType="ObjectRef">1080</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1069</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1087">
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
                          <gameobj dataType="ObjectRef">1069</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1081</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1088">
                    <_items dataType="Array" type="Duality.Component[]" id="1089" length="4">
                      <object dataType="ObjectRef">1079</object>
                      <object dataType="ObjectRef">1087</object>
                      <object dataType="ObjectRef">1081</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1079</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1090">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1091">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1090</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1092">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1093" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1094">
                            <_items dataType="Array" type="System.Int32[]" id="1095" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0719603449</X>
                            <Y dataType="Float">0.0719603449</Y>
                            <Z dataType="Float">0.0719603449</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1096">
                            <_items dataType="ObjectRef">933</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-40.20085</X>
                            <Y dataType="Float">-253.392792</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">58</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1097" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1098" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1099" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1100">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-40.20085</X>
                            <Y dataType="Float">-253.392792</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0719603449</X>
                            <Y dataType="Float">0.0719603449</Y>
                            <Z dataType="Float">0.0719603449</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-40.20085</X>
                            <Y dataType="Float">-253.392792</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0719603449</X>
                            <Y dataType="Float">0.0719603449</Y>
                            <Z dataType="Float">0.0719603449</Z>
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
                            <X dataType="Float">-40.20085</X>
                            <Y dataType="Float">-253.392792</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-40.20085</X>
                            <Y dataType="Float">-253.392792</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1101" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1102">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1103">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1104" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="1105">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="1106" length="4">
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
                                    <parent dataType="ObjectRef">1102</parent>
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
                              <gameobj dataType="ObjectRef">1090</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1107" length="1">
                              <object dataType="ObjectRef">1101</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1090</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1108">
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
                          <gameobj dataType="ObjectRef">1090</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1102</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1109">
                    <_items dataType="Array" type="Duality.Component[]" id="1110" length="4">
                      <object dataType="ObjectRef">1100</object>
                      <object dataType="ObjectRef">1108</object>
                      <object dataType="ObjectRef">1102</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1100</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1111">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1112">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1111</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1113">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1114" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1115">
                            <_items dataType="Array" type="System.Int32[]" id="1116" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0719603449</X>
                            <Y dataType="Float">0.0719603449</Y>
                            <Z dataType="Float">0.0719603449</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1117">
                            <_items dataType="ObjectRef">933</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">69.1969147</X>
                            <Y dataType="Float">-259.0757</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">18</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1118" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1119" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1120" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1121">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">69.1969147</X>
                            <Y dataType="Float">-259.0757</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0719603449</X>
                            <Y dataType="Float">0.0719603449</Y>
                            <Z dataType="Float">0.0719603449</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">69.1969147</X>
                            <Y dataType="Float">-259.0757</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0719603449</X>
                            <Y dataType="Float">0.0719603449</Y>
                            <Z dataType="Float">0.0719603449</Z>
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
                            <X dataType="Float">69.1969147</X>
                            <Y dataType="Float">-259.0757</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">69.1969147</X>
                            <Y dataType="Float">-259.0757</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1122" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1123">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1124">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1125" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="1126">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="1127" length="4">
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
                                    <parent dataType="ObjectRef">1123</parent>
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
                              <gameobj dataType="ObjectRef">1111</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1128" length="1">
                              <object dataType="ObjectRef">1122</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1111</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1129">
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
                          <gameobj dataType="ObjectRef">1111</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1123</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1130">
                    <_items dataType="Array" type="Duality.Component[]" id="1131" length="4">
                      <object dataType="ObjectRef">1121</object>
                      <object dataType="ObjectRef">1129</object>
                      <object dataType="ObjectRef">1123</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1121</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1132">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1133">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1132</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1134">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1135" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1136">
                            <_items dataType="Array" type="System.Int32[]" id="1137" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0719603449</X>
                            <Y dataType="Float">0.0719603449</Y>
                            <Z dataType="Float">0.0719603449</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1138">
                            <_items dataType="Array" type="System.Int32[]" id="1139" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">184.015427</X>
                            <Y dataType="Float">-263.496429</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">42</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1140" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1141" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1142" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1143">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">184.015427</X>
                            <Y dataType="Float">-263.496429</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0719603449</X>
                            <Y dataType="Float">0.0719603449</Y>
                            <Z dataType="Float">0.0719603449</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">184.015427</X>
                            <Y dataType="Float">-263.496429</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.0719603449</X>
                            <Y dataType="Float">0.0719603449</Y>
                            <Z dataType="Float">0.0719603449</Z>
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
                            <X dataType="Float">184.015427</X>
                            <Y dataType="Float">-263.496429</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">184.015427</X>
                            <Y dataType="Float">-263.496429</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1144" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1145">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1146">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1147" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="1148">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="1149" length="4">
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
                                    <parent dataType="ObjectRef">1145</parent>
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
                              <gameobj dataType="ObjectRef">1132</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1150" length="1">
                              <object dataType="ObjectRef">1144</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1132</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1151">
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
                          <gameobj dataType="ObjectRef">1132</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1145</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1152">
                    <_items dataType="Array" type="Duality.Component[]" id="1153" length="4">
                      <object dataType="ObjectRef">1143</object>
                      <object dataType="ObjectRef">1151</object>
                      <object dataType="ObjectRef">1145</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">7</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1143</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1154">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1155">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\RoundSquare.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1154</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1156">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1157" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1158">
                            <_items dataType="Array" type="System.Int32[]" id="1159" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.09788746</X>
                            <Y dataType="Float">0.09788746</Y>
                            <Z dataType="Float">0.09788746</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1160">
                            <_items dataType="ObjectRef">933</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-121.81192</X>
                            <Y dataType="Float">-341.965179</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">60</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1161" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1162" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1163" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1164">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-121.81192</X>
                            <Y dataType="Float">-341.965179</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.09788746</X>
                            <Y dataType="Float">0.09788746</Y>
                            <Z dataType="Float">0.09788746</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-121.81192</X>
                            <Y dataType="Float">-341.965179</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.09788746</X>
                            <Y dataType="Float">0.09788746</Y>
                            <Z dataType="Float">0.09788746</Z>
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
                            <X dataType="Float">-121.81192</X>
                            <Y dataType="Float">-341.965179</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-121.81192</X>
                            <Y dataType="Float">-341.965179</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1165" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1166">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1167">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1168" length="8">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1169">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1166</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1170">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1166</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1171">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1166</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1172">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1166</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="1173">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="1174" length="8">
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
                                    <parent dataType="ObjectRef">1166</parent>
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
                              <gameobj dataType="ObjectRef">1154</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1175" length="1">
                              <object dataType="ObjectRef">1165</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1154</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1176">
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
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">238</G>
                            <B dataType="Byte">76</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">1154</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1166</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1177">
                    <_items dataType="Array" type="Duality.Component[]" id="1178" length="4">
                      <object dataType="ObjectRef">1164</object>
                      <object dataType="ObjectRef">1176</object>
                      <object dataType="ObjectRef">1166</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">RoundSquare</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1164</compTransform>
                  <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1179">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1180">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\RoundSquare.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1179</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1181">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1182" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">440</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1183">
                            <_items dataType="Array" type="System.Int32[]" id="1184" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.09788746</X>
                            <Y dataType="Float">0.09788746</Y>
                            <Z dataType="Float">0.09788746</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">437</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1185">
                            <_items dataType="ObjectRef">933</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">106.927773</X>
                            <Y dataType="Float">-364.69696</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">42</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">430</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1186" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1187" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">79</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1188" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1189">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">106.927773</X>
                            <Y dataType="Float">-364.69696</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.09788746</X>
                            <Y dataType="Float">0.09788746</Y>
                            <Z dataType="Float">0.09788746</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">106.927773</X>
                            <Y dataType="Float">-364.69696</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.09788746</X>
                            <Y dataType="Float">0.09788746</Y>
                            <Z dataType="Float">0.09788746</Z>
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
                            <X dataType="Float">106.927773</X>
                            <Y dataType="Float">-364.69696</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">106.927773</X>
                            <Y dataType="Float">-364.69696</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1190" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1191">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1192">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1193" length="8">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1194">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1191</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1195">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1191</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1196">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1191</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1197">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1191</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="1198">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="1199" length="8">
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
                                    <parent dataType="ObjectRef">1191</parent>
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
                              <gameobj dataType="ObjectRef">1179</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1200" length="1">
                              <object dataType="ObjectRef">1190</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1179</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1201">
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
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">238</G>
                            <B dataType="Byte">76</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">1179</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1191</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1202">
                    <_items dataType="Array" type="Duality.Component[]" id="1203" length="4">
                      <object dataType="ObjectRef">1189</object>
                      <object dataType="ObjectRef">1201</object>
                      <object dataType="ObjectRef">1191</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">RoundSquare</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1189</compTransform>
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
              </_items>
              <_size dataType="Int">34</_size>
              <_version dataType="Int">122</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1204" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="1205" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="1206" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1207">
              <_items dataType="ObjectRef">386</_items>
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <name dataType="String">Dynamics</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1208" multi="true">
              <object dataType="ObjectRef">388</object>
              <object dataType="ObjectRef">1189</object>
              <object dataType="Array" type="System.Delegate[]" id="1209" length="35">
                <object dataType="ObjectRef">66</object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1210" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">445</object>
                  <object dataType="Array" type="System.Delegate[]" id="1211" length="1">
                    <object dataType="ObjectRef">1210</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1212" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">464</object>
                  <object dataType="Array" type="System.Delegate[]" id="1213" length="1">
                    <object dataType="ObjectRef">1212</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1214" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">483</object>
                  <object dataType="Array" type="System.Delegate[]" id="1215" length="1">
                    <object dataType="ObjectRef">1214</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1216" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">502</object>
                  <object dataType="Array" type="System.Delegate[]" id="1217" length="1">
                    <object dataType="ObjectRef">1216</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1218" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">521</object>
                  <object dataType="Array" type="System.Delegate[]" id="1219" length="1">
                    <object dataType="ObjectRef">1218</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1220" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">545</object>
                  <object dataType="Array" type="System.Delegate[]" id="1221" length="1">
                    <object dataType="ObjectRef">1220</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1222" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">569</object>
                  <object dataType="Array" type="System.Delegate[]" id="1223" length="1">
                    <object dataType="ObjectRef">1222</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1224" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">589</object>
                  <object dataType="Array" type="System.Delegate[]" id="1225" length="1">
                    <object dataType="ObjectRef">1224</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1226" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">609</object>
                  <object dataType="Array" type="System.Delegate[]" id="1227" length="1">
                    <object dataType="ObjectRef">1226</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1228" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">629</object>
                  <object dataType="Array" type="System.Delegate[]" id="1229" length="1">
                    <object dataType="ObjectRef">1228</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1230" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">665</object>
                  <object dataType="Array" type="System.Delegate[]" id="1231" length="1">
                    <object dataType="ObjectRef">1230</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1232" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">703</object>
                  <object dataType="Array" type="System.Delegate[]" id="1233" length="1">
                    <object dataType="ObjectRef">1232</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1234" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">723</object>
                  <object dataType="Array" type="System.Delegate[]" id="1235" length="1">
                    <object dataType="ObjectRef">1234</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1236" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">760</object>
                  <object dataType="Array" type="System.Delegate[]" id="1237" length="1">
                    <object dataType="ObjectRef">1236</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1238" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">776</object>
                  <object dataType="Array" type="System.Delegate[]" id="1239" length="1">
                    <object dataType="ObjectRef">1238</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1240" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">813</object>
                  <object dataType="Array" type="System.Delegate[]" id="1241" length="1">
                    <object dataType="ObjectRef">1240</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1242" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">833</object>
                  <object dataType="Array" type="System.Delegate[]" id="1243" length="1">
                    <object dataType="ObjectRef">1242</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1244" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">853</object>
                  <object dataType="Array" type="System.Delegate[]" id="1245" length="1">
                    <object dataType="ObjectRef">1244</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1246" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">873</object>
                  <object dataType="Array" type="System.Delegate[]" id="1247" length="1">
                    <object dataType="ObjectRef">1246</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1248" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">895</object>
                  <object dataType="Array" type="System.Delegate[]" id="1249" length="1">
                    <object dataType="ObjectRef">1248</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1250" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">916</object>
                  <object dataType="Array" type="System.Delegate[]" id="1251" length="1">
                    <object dataType="ObjectRef">1250</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1252" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">937</object>
                  <object dataType="Array" type="System.Delegate[]" id="1253" length="1">
                    <object dataType="ObjectRef">1252</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1254" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">957</object>
                  <object dataType="Array" type="System.Delegate[]" id="1255" length="1">
                    <object dataType="ObjectRef">1254</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1256" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">977</object>
                  <object dataType="Array" type="System.Delegate[]" id="1257" length="1">
                    <object dataType="ObjectRef">1256</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1258" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">997</object>
                  <object dataType="Array" type="System.Delegate[]" id="1259" length="1">
                    <object dataType="ObjectRef">1258</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1260" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">1017</object>
                  <object dataType="Array" type="System.Delegate[]" id="1261" length="1">
                    <object dataType="ObjectRef">1260</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1262" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">1039</object>
                  <object dataType="Array" type="System.Delegate[]" id="1263" length="1">
                    <object dataType="ObjectRef">1262</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1264" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">1059</object>
                  <object dataType="Array" type="System.Delegate[]" id="1265" length="1">
                    <object dataType="ObjectRef">1264</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1266" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">1079</object>
                  <object dataType="Array" type="System.Delegate[]" id="1267" length="1">
                    <object dataType="ObjectRef">1266</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1268" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">1100</object>
                  <object dataType="Array" type="System.Delegate[]" id="1269" length="1">
                    <object dataType="ObjectRef">1268</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1270" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">1121</object>
                  <object dataType="Array" type="System.Delegate[]" id="1271" length="1">
                    <object dataType="ObjectRef">1270</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1272" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">1143</object>
                  <object dataType="Array" type="System.Delegate[]" id="1273" length="1">
                    <object dataType="ObjectRef">1272</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1274" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">1164</object>
                  <object dataType="Array" type="System.Delegate[]" id="1275" length="1">
                    <object dataType="ObjectRef">1274</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1276" multi="true">
                  <object dataType="ObjectRef">388</object>
                  <object dataType="ObjectRef">1189</object>
                  <object dataType="Array" type="System.Delegate[]" id="1277" length="1">
                    <object dataType="ObjectRef">1276</object>
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
          <object dataType="ObjectRef">95</object>
          <object dataType="ObjectRef">111</object>
          <object dataType="ObjectRef">127</object>
          <object dataType="ObjectRef">142</object>
          <object dataType="ObjectRef">157</object>
          <object dataType="ObjectRef">172</object>
          <object dataType="ObjectRef">187</object>
          <object dataType="ObjectRef">202</object>
          <object dataType="ObjectRef">217</object>
          <object dataType="ObjectRef">232</object>
          <object dataType="ObjectRef">247</object>
          <object dataType="ObjectRef">262</object>
          <object dataType="ObjectRef">277</object>
          <object dataType="ObjectRef">292</object>
          <object dataType="ObjectRef">307</object>
          <object dataType="Class" type="Duality.GameObject" id="1278">
            <prefabLink />
            <parent dataType="Class" type="Duality.GameObject" id="1279">
              <prefabLink />
              <parent />
              <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="1280">
                <_items dataType="Array" type="Duality.GameObject[]" id="1281" length="4">
                  <object dataType="ObjectRef">1278</object>
                  <object dataType="Class" type="Duality.GameObject" id="1282">
                    <prefabLink />
                    <parent dataType="ObjectRef">1279</parent>
                    <children />
                    <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1283" surrogate="true">
                      <customSerialIO />
                      <customSerialIO>
                        <keys dataType="Array" type="System.Type[]" id="1284" length="2">
                          <object dataType="ObjectRef">14</object>
                          <object dataType="Type" id="1285" value="Duality.Components.Renderers.TextRenderer" />
                        </keys>
                        <values dataType="Array" type="Duality.Component[]" id="1286" length="2">
                          <object dataType="Class" type="Duality.Components.Transform" id="1287">
                            <pos dataType="Struct" type="OpenTK.Vector3">
                              <X dataType="Float">172.45752</X>
                              <Y dataType="Float">249.660477</Y>
                              <Z dataType="Float">-1</Z>
                            </pos>
                            <angle dataType="Float">5.74911451</angle>
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
                              <X dataType="Float">172.45752</X>
                              <Y dataType="Float">249.660477</Y>
                              <Z dataType="Float">-1</Z>
                            </posAbs>
                            <angleAbs dataType="Float">5.74911451</angleAbs>
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
                              <X dataType="Float">172.45752</X>
                              <Y dataType="Float">249.660477</Y>
                              <Z dataType="Float">-1</Z>
                            </lastPos>
                            <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                              <X dataType="Float">172.45752</X>
                              <Y dataType="Float">249.660477</Y>
                              <Z dataType="Float">-1</Z>
                            </lastPosAbs>
                            <lastAngle dataType="Float">5.74911451</lastAngle>
                            <lastAngleAbs dataType="Float">5.74911451</lastAngleAbs>
                            <OnTransformChanged />
                            <gameobj dataType="ObjectRef">1282</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </object>
                          <object dataType="Class" type="Duality.Components.Renderers.TextRenderer" id="1288">
                            <align dataType="Enum" type="Duality.Alignment" name="Center" value="0" />
                            <text dataType="Class" type="Duality.FormattedText" id="1289">
                              <sourceText dataType="String">Ice</sourceText>
                              <icons />
                              <flowAreas />
                              <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="1290" length="1">
                                <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                                  <contentPath dataType="String">Data\BigFont.Font.res</contentPath>
                                </object>
                              </fonts>
                              <maxWidth dataType="Int">0</maxWidth>
                              <maxHeight dataType="Int">0</maxHeight>
                              <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                              <displayedText dataType="String">Ice</displayedText>
                              <fontGlyphCount dataType="Array" type="System.Int32[]" id="1291" length="1">
                                <object dataType="Int">3</object>
                              </fontGlyphCount>
                              <iconCount dataType="Int">0</iconCount>
                              <elements dataType="Array" type="Duality.FormattedText+Element[]" id="1292" length="1">
                                <object dataType="Class" type="Duality.FormattedText+TextElement" id="1293">
                                  <text dataType="String">Ice</text>
                                </object>
                              </elements>
                            </text>
                            <customMat />
                            <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                              <R dataType="Byte">255</R>
                              <G dataType="Byte">255</G>
                              <B dataType="Byte">255</B>
                              <A dataType="Byte">64</A>
                            </colorTint>
                            <iconMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                              <contentPath />
                            </iconMat>
                            <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                            <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                            <gameobj dataType="ObjectRef">1282</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </object>
                        </values>
                      </customSerialIO>
                    </compMap>
                    <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1294">
                      <_items dataType="Array" type="Duality.Component[]" id="1295" length="4">
                        <object dataType="ObjectRef">1287</object>
                        <object dataType="ObjectRef">1288</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">2</_version>
                    </compList>
                    <name dataType="String">Rubber</name>
                    <active dataType="Bool">true</active>
                    <disposed dataType="Bool">false</disposed>
                    <compTransform dataType="ObjectRef">1287</compTransform>
                    <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
                    <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
                    <EventCollisionBegin />
                    <EventCollisionEnd />
                    <EventCollisionSolve />
                  </object>
                  <object />
                  <object />
                </_items>
                <_size dataType="Int">2</_size>
                <_version dataType="Int">4</_version>
              </children>
              <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1296" surrogate="true">
                <customSerialIO />
                <customSerialIO>
                  <keys dataType="Array" type="System.Type[]" id="1297" length="0" />
                  <values dataType="Array" type="Duality.Component[]" id="1298" length="0" />
                </customSerialIO>
              </compMap>
              <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1299">
                <_items dataType="Array" type="Duality.Component[]" id="1300" length="0" />
                <_size dataType="Int">0</_size>
                <_version dataType="Int">0</_version>
              </compList>
              <name dataType="String">Text</name>
              <active dataType="Bool">true</active>
              <disposed dataType="Bool">false</disposed>
              <compTransform />
              <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1301" multi="true">
                <object dataType="ObjectRef">388</object>
                <object dataType="ObjectRef">1287</object>
                <object dataType="Array" type="System.Delegate[]" id="1302" length="3">
                  <object dataType="ObjectRef">66</object>
                  <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1303" multi="true">
                    <object dataType="ObjectRef">388</object>
                    <object dataType="Class" type="Duality.Components.Transform" id="1304">
                      <pos dataType="Struct" type="OpenTK.Vector3">
                        <X dataType="Float">-167.017639</X>
                        <Y dataType="Float">249.48732</Y>
                        <Z dataType="Float">-1</Z>
                      </pos>
                      <angle dataType="Float">0.605629265</angle>
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
                        <X dataType="Float">-167.017639</X>
                        <Y dataType="Float">249.48732</Y>
                        <Z dataType="Float">-1</Z>
                      </posAbs>
                      <angleAbs dataType="Float">0.605629265</angleAbs>
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
                        <X dataType="Float">-167.017639</X>
                        <Y dataType="Float">249.48732</Y>
                        <Z dataType="Float">-1</Z>
                      </lastPos>
                      <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                        <X dataType="Float">-167.017639</X>
                        <Y dataType="Float">249.48732</Y>
                        <Z dataType="Float">-1</Z>
                      </lastPosAbs>
                      <lastAngle dataType="Float">0.605629265</lastAngle>
                      <lastAngleAbs dataType="Float">0.605629265</lastAngleAbs>
                      <OnTransformChanged />
                      <gameobj dataType="ObjectRef">1278</gameobj>
                      <disposed dataType="Bool">false</disposed>
                      <active dataType="Bool">true</active>
                    </object>
                    <object dataType="Array" type="System.Delegate[]" id="1305" length="1">
                      <object dataType="ObjectRef">1303</object>
                    </object>
                  </object>
                  <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1306" multi="true">
                    <object dataType="ObjectRef">388</object>
                    <object dataType="ObjectRef">1287</object>
                    <object dataType="Array" type="System.Delegate[]" id="1307" length="1">
                      <object dataType="ObjectRef">1306</object>
                    </object>
                  </object>
                </object>
              </EventComponentAdded>
              <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
              <EventCollisionBegin />
              <EventCollisionEnd />
              <EventCollisionSolve />
            </parent>
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1308" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="1309" length="2">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">1285</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="1310" length="2">
                  <object dataType="ObjectRef">1304</object>
                  <object dataType="Class" type="Duality.Components.Renderers.TextRenderer" id="1311">
                    <align dataType="Enum" type="Duality.Alignment" name="Center" value="0" />
                    <text dataType="Class" type="Duality.FormattedText" id="1312">
                      <sourceText dataType="String">Rubber</sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="1313" length="1">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\BigFont.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">0</maxWidth>
                      <maxHeight dataType="Int">0</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String">Rubber</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="1314" length="1">
                        <object dataType="Int">6</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="1315" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="1316">
                          <text dataType="String">Rubber</text>
                        </object>
                      </elements>
                    </text>
                    <customMat />
                    <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                      <R dataType="Byte">255</R>
                      <G dataType="Byte">255</G>
                      <B dataType="Byte">255</B>
                      <A dataType="Byte">64</A>
                    </colorTint>
                    <iconMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                      <contentPath />
                    </iconMat>
                    <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                    <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                    <gameobj dataType="ObjectRef">1278</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1317">
              <_items dataType="Array" type="Duality.Component[]" id="1318" length="4">
                <object dataType="ObjectRef">1304</object>
                <object dataType="ObjectRef">1311</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">2</_version>
            </compList>
            <name dataType="String">Rubber</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">1304</compTransform>
            <EventComponentAdded dataType="ObjectRef">66</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">69</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="ObjectRef">1279</object>
          <object dataType="ObjectRef">1282</object>
          <object dataType="ObjectRef">433</object>
          <object dataType="ObjectRef">455</object>
          <object dataType="ObjectRef">474</object>
          <object dataType="ObjectRef">493</object>
          <object dataType="ObjectRef">512</object>
          <object dataType="ObjectRef">536</object>
          <object dataType="ObjectRef">560</object>
          <object dataType="ObjectRef">580</object>
          <object dataType="ObjectRef">600</object>
          <object dataType="ObjectRef">620</object>
          <object dataType="ObjectRef">656</object>
          <object dataType="ObjectRef">692</object>
          <object dataType="ObjectRef">713</object>
          <object dataType="ObjectRef">738</object>
          <object dataType="ObjectRef">766</object>
          <object dataType="ObjectRef">803</object>
          <object dataType="ObjectRef">823</object>
          <object dataType="ObjectRef">843</object>
          <object dataType="ObjectRef">863</object>
          <object dataType="ObjectRef">883</object>
          <object dataType="ObjectRef">905</object>
          <object dataType="ObjectRef">926</object>
          <object dataType="ObjectRef">947</object>
          <object dataType="ObjectRef">967</object>
          <object dataType="ObjectRef">987</object>
          <object dataType="ObjectRef">1007</object>
          <object dataType="ObjectRef">1027</object>
          <object dataType="ObjectRef">1049</object>
          <object dataType="ObjectRef">1069</object>
          <object dataType="ObjectRef">1090</object>
          <object dataType="ObjectRef">1111</object>
          <object dataType="ObjectRef">1132</object>
          <object dataType="ObjectRef">1154</object>
          <object dataType="ObjectRef">1179</object>
          <object dataType="ObjectRef">322</object>
          <object dataType="ObjectRef">337</object>
          <object dataType="ObjectRef">352</object>
          <object dataType="ObjectRef">367</object>
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
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
        <_size dataType="Int">60</_size>
        <_version dataType="Int">162</_version>
      </allObj>
      <Registered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="1319" multi="true">
        <object dataType="MethodInfo" id="1320" value="M:Duality.Resources.Scene:objectManager_Registered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="1321" length="1">
          <object dataType="ObjectRef">1319</object>
        </object>
      </Registered>
      <Unregistered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="1322" multi="true">
        <object dataType="MethodInfo" id="1323" value="M:Duality.Resources.Scene:objectManager_Unregistered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="1324" length="1">
          <object dataType="ObjectRef">1322</object>
        </object>
      </Unregistered>
    </objectManager>
    <sourcePath />
  </object>
</root>