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
                            <R dataType="Byte">0</R>
                            <G dataType="Byte">0</G>
                            <B dataType="Byte">0</B>
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
                      <sourceText dataType="String">Example: Landscape</sourceText>
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
                      <displayedText dataType="String">Example: Landscape</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="29" length="1">
                        <object dataType="Int">18</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="30" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="31">
                          <text dataType="String">Example: Landscape</text>
                        </object>
                      </elements>
                    </name>
                    <desc dataType="Class" type="Duality.FormattedText" id="32">
                      <sourceText dataType="String">This example shows how a 2d landscape could be/nrepresented physically.</sourceText>
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
                      <displayedText dataType="String">This example shows how a 2d landscape could berepresented physically.</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="34" length="1">
                        <object dataType="Int">69</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="35" length="3">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="36">
                          <text dataType="String">This example shows how a 2d landscape could be</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+NewLineElement" id="37" />
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="38">
                          <text dataType="String">represented physically.</text>
                        </object>
                      </elements>
                    </desc>
                    <physicsTimeVal dataType="Float">0</physicsTimeVal>
                    <physicsTimeAcc dataType="Float">0</physicsTimeAcc>
                    <physicsTimeCounter dataType="Int">100</physicsTimeCounter>
                    <mouseJoint />
                    <controls dataType="Class" type="Duality.FormattedText" id="39">
                      <sourceText dataType="String">/cFFAAAAFFLeft Mouse/cFFFFFFFF: Drag object/n/cFFAAAAFFRight Mouse/cFFFFFFFF: Create object/n/cFFAAAAFFNumber keys/cFFFFFFFF: Select testbed scene/n</sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="40" length="1">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\SmallFont.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">500</maxWidth>
                      <maxHeight dataType="Int">500</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String">Left Mouse: Drag objectRight Mouse: Create objectNumber keys: Select testbed scene</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="41" length="1">
                        <object dataType="Int">82</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="42" length="15">
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="43">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">170</G>
                            <B dataType="Byte">170</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="44">
                          <text dataType="String">Left Mouse</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="45">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="46">
                          <text dataType="String">: Drag object</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+NewLineElement" id="47" />
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="48">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">170</G>
                            <B dataType="Byte">170</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="49">
                          <text dataType="String">Right Mouse</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="50">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="51">
                          <text dataType="String">: Create object</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+NewLineElement" id="52" />
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="53">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">170</G>
                            <B dataType="Byte">170</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="54">
                          <text dataType="String">Number keys</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+ColorChangeElement" id="55">
                          <color dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </color>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="56">
                          <text dataType="String">: Select testbed scene</text>
                        </object>
                        <object dataType="Class" type="Duality.FormattedText+NewLineElement" id="57" />
                      </elements>
                    </controls>
                    <stats dataType="Class" type="Duality.FormattedText" id="58">
                      <sourceText />
                      <icons />
                      <flowAreas />
                      <fonts />
                      <maxWidth dataType="Int">0</maxWidth>
                      <maxHeight dataType="Int">0</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String"></displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="59" length="0" />
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="60" length="0" />
                    </stats>
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="61">
              <_items dataType="Array" type="Duality.Component[]" id="62" length="4">
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
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="63" multi="true">
              <object dataType="MethodInfo" id="64" value="M:Duality.ObjectManagers.GameObjectManager:OnRegisteredObjectComponentAdded(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">2</object>
              <object dataType="Array" type="System.Delegate[]" id="65" length="1">
                <object dataType="ObjectRef">63</object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="66" multi="true">
              <object dataType="MethodInfo" id="67" value="M:Duality.ObjectManagers.GameObjectManager:OnRegisteredObjectComponentRemoved(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">2</object>
              <object dataType="Array" type="System.Delegate[]" id="68" length="1">
                <object dataType="ObjectRef">66</object>
              </object>
            </EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="69">
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="70">
              <_items dataType="Array" type="Duality.GameObject[]" id="71" length="32">
                <object dataType="Class" type="Duality.GameObject" id="72">
                  <prefabLink />
                  <parent dataType="ObjectRef">69</parent>
                  <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="73">
                    <_items dataType="Array" type="Duality.GameObject[]" id="74" length="4">
                      <object dataType="Class" type="Duality.GameObject" id="75">
                        <prefabLink />
                        <parent dataType="ObjectRef">72</parent>
                        <children />
                        <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="76" surrogate="true">
                          <customSerialIO />
                          <customSerialIO>
                            <keys dataType="Array" type="System.Type[]" id="77" length="2">
                              <object dataType="ObjectRef">14</object>
                              <object dataType="Type" id="78" value="Duality.Components.Renderers.SpriteRenderer" />
                            </keys>
                            <values dataType="Array" type="Duality.Component[]" id="79" length="2">
                              <object dataType="Class" type="Duality.Components.Transform" id="80">
                                <pos dataType="Struct" type="OpenTK.Vector3">
                                  <X dataType="Float">0</X>
                                  <Y dataType="Float">0</Y>
                                  <Z dataType="Float">2</Z>
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
                                <parentTransform dataType="Class" type="Duality.Components.Transform" id="81">
                                  <pos dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">0</X>
                                    <Y dataType="Float">0</Y>
                                    <Z dataType="Float">-1</Z>
                                  </pos>
                                  <angle dataType="Float">0</angle>
                                  <scale dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">0.7985333</X>
                                    <Y dataType="Float">0.7985333</Y>
                                    <Z dataType="Float">0.7985333</Z>
                                  </scale>
                                  <deriveAngle dataType="Bool">true</deriveAngle>
                                  <ignoreParent dataType="Bool">false</ignoreParent>
                                  <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                                  <parentTransform />
                                  <posAbs dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">0</X>
                                    <Y dataType="Float">0</Y>
                                    <Z dataType="Float">-1</Z>
                                  </posAbs>
                                  <angleAbs dataType="Float">0</angleAbs>
                                  <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">0.7985333</X>
                                    <Y dataType="Float">0.7985333</Y>
                                    <Z dataType="Float">0.7985333</Z>
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
                                    <Z dataType="Float">-1</Z>
                                  </lastPos>
                                  <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                                    <X dataType="Float">0</X>
                                    <Y dataType="Float">0</Y>
                                    <Z dataType="Float">-1</Z>
                                  </lastPosAbs>
                                  <lastAngle dataType="Float">0</lastAngle>
                                  <lastAngleAbs dataType="Float">0</lastAngleAbs>
                                  <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="82" multi="true">
                                    <object dataType="MethodInfo" id="83" value="M:Duality.Components.Physics.RigidBody:OnTransformChanged(System.Object,Duality.TransformChangedEventArgs)" />
                                    <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="84">
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
                                      <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="85">
                                        <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="86" length="4">
                                          <object dataType="Class" type="Duality.Components.Physics.LoopShapeInfo" id="87">
                                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="88" length="99">
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-602.0434</X>
                                                <Y dataType="Float">110.550476</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-521.817139</X>
                                                <Y dataType="Float">123.738358</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-467.9666</X>
                                                <Y dataType="Float">141.322189</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-395.4333</X>
                                                <Y dataType="Float">168.796936</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-321.800964</X>
                                                <Y dataType="Float">228.1424</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-239.376724</X>
                                                <Y dataType="Float">284.1909</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-158.051468</X>
                                                <Y dataType="Float">327.051483</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-60.24138</X>
                                                <Y dataType="Float">346.833282</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">54.889328</X>
                                                <Y dataType="Float">357.71582</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">172.481247</X>
                                                <Y dataType="Float">358.814819</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">228.529724</X>
                                                <Y dataType="Float">350.0229</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">286.776184</X>
                                                <Y dataType="Float">336.835022</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">325.240845</X>
                                                <Y dataType="Float">311.558228</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">341.7257</X>
                                                <Y dataType="Float">259.9057</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">335.131744</X>
                                                <Y dataType="Float">189.570358</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">297.7661</X>
                                                <Y dataType="Float">122.531982</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">264.7964</X>
                                                <Y dataType="Float">103.849152</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">214.242859</X>
                                                <Y dataType="Float">90.66127</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">169.18428</X>
                                                <Y dataType="Float">97.25522</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">150.50145</X>
                                                <Y dataType="Float">132.422882</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">161.491348</X>
                                                <Y dataType="Float">166.491577</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">179.07518</X>
                                                <Y dataType="Float">181.877441</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">181.273163</X>
                                                <Y dataType="Float">186.273392</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">174.67923</X>
                                                <Y dataType="Float">186.273392</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">137.313568</X>
                                                <Y dataType="Float">177.481476</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">119.729729</X>
                                                <Y dataType="Float">148.907745</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">107.640839</X>
                                                <Y dataType="Float">84.06734</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">119.3047</X>
                                                <Y dataType="Float">45.31189</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">125.898643</X>
                                                <Y dataType="Float">27.7280579</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">144.581482</X>
                                                <Y dataType="Float">2.45128632</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">174.2542</X>
                                                <Y dataType="Float">-17.3305283</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">181.947128</X>
                                                <Y dataType="Float">-18.4295273</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">237.9956</X>
                                                <Y dataType="Float">-22.8254776</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">294.0441</X>
                                                <Y dataType="Float">-15.1325455</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">366.577423</X>
                                                <Y dataType="Float">23.3320923</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">388.55722</X>
                                                <Y dataType="Float">62.89573</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">441.308746</X>
                                                <Y dataType="Float">94.76643</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">498.456238</X>
                                                <Y dataType="Float">84.87553</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">530.3269</X>
                                                <Y dataType="Float">47.5098724</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">530.3269</X>
                                                <Y dataType="Float">-3.043663</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">506.14917</X>
                                                <Y dataType="Float">-36.01336</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">468.7835</X>
                                                <Y dataType="Float">-53.59719</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">441.308746</X>
                                                <Y dataType="Float">-89.86387</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">429.219849</X>
                                                <Y dataType="Float">-126.130531</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">426.380676</X>
                                                <Y dataType="Float">-180.802872</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">437.370575</X>
                                                <Y dataType="Float">-197.28772</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">460.449341</X>
                                                <Y dataType="Float">-204.980652</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">454.9544</X>
                                                <Y dataType="Float">-189.594788</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">465.9443</X>
                                                <Y dataType="Float">-169.812973</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">487.9241</X>
                                                <Y dataType="Float">-153.328125</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">527.487732</X>
                                                <Y dataType="Float">-156.625092</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">539.57666</X>
                                                <Y dataType="Float">-165.417</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">549.4675</X>
                                                <Y dataType="Float">-195.089737</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">551.6655</X>
                                                <Y dataType="Float">-231.356415</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">537.378662</X>
                                                <Y dataType="Float">-290.701843</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">516.497864</X>
                                                <Y dataType="Float">-318.1766</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">454.9544</X>
                                                <Y dataType="Float">-354.443268</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">408.7968</X>
                                                <Y dataType="Float">-367.631134</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">349.4514</X>
                                                <Y dataType="Float">-378.621033</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">259.3342</X>
                                                <Y dataType="Float">-384.116</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">125.257446</X>
                                                <Y dataType="Float">-383.017</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">69.2089539</X>
                                                <Y dataType="Float">-379.720032</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">82.3968353</X>
                                                <Y dataType="Float">-333.562439</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">83.49582</X>
                                                <Y dataType="Float">-213.772568</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">117.564514</X>
                                                <Y dataType="Float">-204.980652</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">117.564514</X>
                                                <Y dataType="Float">-188.4958</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">84.59482</X>
                                                <Y dataType="Float">-178.604889</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">30.7443085</X>
                                                <Y dataType="Float">-187.3968</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-27.5021667</X>
                                                <Y dataType="Float">-186.297821</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-26.4031677</X>
                                                <Y dataType="Float">-209.3766</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">8.764496</X>
                                                <Y dataType="Float">-210.4756</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">12.0614929</X>
                                                <Y dataType="Float">-239.049332</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">10.9624939</X>
                                                <Y dataType="Float">-362.1362</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">2.92990112</X>
                                                <Y dataType="Float">-417.141174</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-33.3367653</X>
                                                <Y dataType="Float">-453.407837</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-60.8115158</X>
                                                <Y dataType="Float">-451.209869</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-79.49434</X>
                                                <Y dataType="Float">-436.923</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-83.8903046</X>
                                                <Y dataType="Float">-386.369446</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-144.334747</X>
                                                <Y dataType="Float">-399.557343</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-266.322632</X>
                                                <Y dataType="Float">-399.557343</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-270.7186</X>
                                                <Y dataType="Float">-453.407837</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-277.312531</X>
                                                <Y dataType="Float">-483.080566</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-458.21344</X>
                                                <Y dataType="Float">-479.602173</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-515.5252</X>
                                                <Y dataType="Float">-485.05484</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-568.0158</X>
                                                <Y dataType="Float">-472.951</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-603.235657</X>
                                                <Y dataType="Float">-409.208649</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-613.1265</X>
                                                <Y dataType="Float">-339.9723</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-575.760864</X>
                                                <Y dataType="Float">-290.517731</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-516.4154</X>
                                                <Y dataType="Float">-266.339966</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-390.0316</X>
                                                <Y dataType="Float">-243.261169</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-376.843719</X>
                                                <Y dataType="Float">-220.182388</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-376.843719</X>
                                                <Y dataType="Float">-186.1137</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-546.088135</X>
                                                <Y dataType="Float">-172.925812</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-588.9488</X>
                                                <Y dataType="Float">-156.440979</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-593.3447</X>
                                                <Y dataType="Float">-122.372284</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-562.573</X>
                                                <Y dataType="Float">-101.491478</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-558.177063</X>
                                                <Y dataType="Float">-4.780365</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-561.474</X>
                                                <Y dataType="Float">5.11055</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-603.235657</X>
                                                <Y dataType="Float">29.2883148</Y>
                                              </object>
                                            </vertices>
                                            <parent dataType="ObjectRef">84</parent>
                                            <density dataType="Float">1</density>
                                            <friction dataType="Float">0.3</friction>
                                            <restitution dataType="Float">0.3</restitution>
                                            <sensor dataType="Bool">false</sensor>
                                          </object>
                                          <object dataType="Class" type="Duality.Components.Physics.LoopShapeInfo" id="89">
                                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="90" length="5">
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">445.9133</X>
                                                <Y dataType="Float">411.885773</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">463.150024</X>
                                                <Y dataType="Float">308.4654</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">483.259552</X>
                                                <Y dataType="Float">289.313477</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">569.443237</X>
                                                <Y dataType="Float">290.271057</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">604.874268</X>
                                                <Y dataType="Float">401.352234</Y>
                                              </object>
                                            </vertices>
                                            <parent dataType="ObjectRef">84</parent>
                                            <density dataType="Float">1</density>
                                            <friction dataType="Float">0.3</friction>
                                            <restitution dataType="Float">0.3</restitution>
                                            <sensor dataType="Bool">false</sensor>
                                          </object>
                                          <object dataType="Class" type="Duality.Components.Physics.LoopShapeInfo" id="91">
                                            <vertices dataType="Array" type="OpenTK.Vector2[]" id="92" length="14">
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-569.4121</X>
                                                <Y dataType="Float">439.1353</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-574.2001</X>
                                                <Y dataType="Float">376.891541</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-544.5146</X>
                                                <Y dataType="Float">310.8174</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-489.931641</X>
                                                <Y dataType="Float">282.089539</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-436.306244</X>
                                                <Y dataType="Float">309.859833</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-437.263855</X>
                                                <Y dataType="Float">316.563</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-434.391052</X>
                                                <Y dataType="Float">321.350983</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-413.3239</X>
                                                <Y dataType="Float">306.029449</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-365.444122</X>
                                                <Y dataType="Float">300.283875</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-315.6491</X>
                                                <Y dataType="Float">334.757324</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-298.412384</X>
                                                <Y dataType="Float">399.873871</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-321.3947</X>
                                                <Y dataType="Float">467.86322</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-392.256836</X>
                                                <Y dataType="Float">485.0999</Y>
                                              </object>
                                              <object dataType="Struct" type="OpenTK.Vector2">
                                                <X dataType="Float">-512.91394</X>
                                                <Y dataType="Float">477.439148</Y>
                                              </object>
                                            </vertices>
                                            <parent dataType="ObjectRef">84</parent>
                                            <density dataType="Float">1</density>
                                            <friction dataType="Float">0.3</friction>
                                            <restitution dataType="Float">0.3</restitution>
                                            <sensor dataType="Bool">false</sensor>
                                          </object>
                                          <object />
                                        </_items>
                                        <_size dataType="Int">3</_size>
                                        <_version dataType="Int">15</_version>
                                      </shapes>
                                      <joints />
                                      <gameobj dataType="ObjectRef">72</gameobj>
                                      <disposed dataType="Bool">false</disposed>
                                      <active dataType="Bool">true</active>
                                    </object>
                                    <object dataType="Array" type="System.Delegate[]" id="93" length="1">
                                      <object dataType="ObjectRef">82</object>
                                    </object>
                                  </OnTransformChanged>
                                  <gameobj dataType="ObjectRef">72</gameobj>
                                  <disposed dataType="Bool">false</disposed>
                                  <active dataType="Bool">true</active>
                                </parentTransform>
                                <posAbs dataType="Struct" type="OpenTK.Vector3">
                                  <X dataType="Float">0</X>
                                  <Y dataType="Float">0</Y>
                                  <Z dataType="Float">0.597066641</Z>
                                </posAbs>
                                <angleAbs dataType="Float">0</angleAbs>
                                <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                                  <X dataType="Float">0.7985333</X>
                                  <Y dataType="Float">0.7985333</Y>
                                  <Z dataType="Float">0.7985333</Z>
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
                                  <Z dataType="Float">2</Z>
                                </lastPos>
                                <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                                  <X dataType="Float">0</X>
                                  <Y dataType="Float">0</Y>
                                  <Z dataType="Float">0.597066641</Z>
                                </lastPosAbs>
                                <lastAngle dataType="Float">0</lastAngle>
                                <lastAngleAbs dataType="Float">0</lastAngleAbs>
                                <OnTransformChanged />
                                <gameobj dataType="ObjectRef">75</gameobj>
                                <disposed dataType="Bool">false</disposed>
                                <active dataType="Bool">true</active>
                              </object>
                              <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="94">
                                <rect dataType="Struct" type="Duality.Rect">
                                  <X dataType="Float">-640</X>
                                  <Y dataType="Float">-512</Y>
                                  <W dataType="Float">1280</W>
                                  <H dataType="Float">1024</H>
                                </rect>
                                <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                                  <contentPath dataType="String">Data\LevelGraphics\ShadowLevelBg.Material.res</contentPath>
                                </sharedMat>
                                <customMat />
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
                            </values>
                          </customSerialIO>
                        </compMap>
                        <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="95">
                          <_items dataType="Array" type="Duality.Component[]" id="96" length="4">
                            <object dataType="ObjectRef">80</object>
                            <object dataType="ObjectRef">94</object>
                            <object />
                            <object />
                          </_items>
                          <_size dataType="Int">2</_size>
                          <_version dataType="Int">4</_version>
                        </compList>
                        <name dataType="String">ShadowLevelBg</name>
                        <active dataType="Bool">true</active>
                        <disposed dataType="Bool">false</disposed>
                        <compTransform dataType="ObjectRef">80</compTransform>
                        <EventComponentAdded dataType="ObjectRef">63</EventComponentAdded>
                        <EventComponentRemoving dataType="ObjectRef">66</EventComponentRemoving>
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
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="97" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="98" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="Type" id="99" value="Duality.Components.Physics.RigidBody" />
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="100" length="3">
                        <object dataType="ObjectRef">81</object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="101">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-640</X>
                            <Y dataType="Float">-512</Y>
                            <W dataType="Float">1280</W>
                            <H dataType="Float">1024</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath dataType="String">Data\LevelGraphics\ShadowLevel.Material.res</contentPath>
                          </sharedMat>
                          <customMat />
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">72</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">84</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="102">
                    <_items dataType="Array" type="Duality.Component[]" id="103" length="4">
                      <object dataType="ObjectRef">81</object>
                      <object dataType="ObjectRef">101</object>
                      <object dataType="ObjectRef">84</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">5</_version>
                  </compList>
                  <name dataType="String">ShadowLevel</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">81</compTransform>
                  <EventComponentAdded dataType="ObjectRef">63</EventComponentAdded>
                  <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="104" multi="true">
                    <object dataType="MethodInfo" id="105" value="M:Duality.Components.Transform:Parent_EventComponentRemoving(System.Object,Duality.ComponentEventArgs)" />
                    <object dataType="ObjectRef">80</object>
                    <object dataType="Array" type="System.Delegate[]" id="106" length="2">
                      <object dataType="ObjectRef">66</object>
                      <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="107" multi="true">
                        <object dataType="ObjectRef">105</object>
                        <object dataType="ObjectRef">80</object>
                        <object dataType="Array" type="System.Delegate[]" id="108" length="1">
                          <object dataType="ObjectRef">107</object>
                        </object>
                      </object>
                    </object>
                  </EventComponentRemoving>
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
              </_items>
              <_size dataType="Int">1</_size>
              <_version dataType="Int">47</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="109" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="110" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="111" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="112">
              <_items dataType="Array" type="Duality.Component[]" id="113" length="0" />
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <name dataType="String">StaticWorld</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="114" multi="true">
              <object dataType="MethodInfo" id="115" value="M:Duality.Components.Transform:Parent_EventComponentAdded(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">81</object>
              <object dataType="Array" type="System.Delegate[]" id="116" length="2">
                <object dataType="ObjectRef">63</object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="117" multi="true">
                  <object dataType="ObjectRef">115</object>
                  <object dataType="ObjectRef">81</object>
                  <object dataType="Array" type="System.Delegate[]" id="118" length="1">
                    <object dataType="ObjectRef">117</object>
                  </object>
                </object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">66</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="119">
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="120">
              <_items dataType="Array" type="Duality.GameObject[]" id="121" length="64">
                <object dataType="Class" type="Duality.GameObject" id="122">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="123">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">122</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="124">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="125" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="126" value="P:Duality.Components.Transform:RelativeScale" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="127">
                            <_items dataType="Array" type="System.Int32[]" id="128" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.2</X>
                            <Y dataType="Float">0.2</Y>
                            <Z dataType="Float">0.2</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="129" value="P:Duality.Components.Renderers.SpriteRenderer:ColorTint" />
                          <componentType dataType="ObjectRef">78</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="130">
                            <_items dataType="Array" type="System.Int32[]" id="131" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">63</R>
                            <G dataType="Byte">40</G>
                            <B dataType="Byte">20</B>
                            <A dataType="Byte">255</A>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="132" value="P:Duality.Components.Transform:RelativePos" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="133">
                            <_items dataType="Array" type="System.Int32[]" id="134" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-442.280975</X>
                            <Y dataType="Float">-318.045441</Y>
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
                      <_version dataType="Int">581</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">119</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="135" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="136" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">99</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="137" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="138">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-442.280975</X>
                            <Y dataType="Float">-318.045441</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.2</X>
                            <Y dataType="Float">0.2</Y>
                            <Z dataType="Float">0.2</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-442.280975</X>
                            <Y dataType="Float">-318.045441</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.2</X>
                            <Y dataType="Float">0.2</Y>
                            <Z dataType="Float">0.2</Z>
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
                            <X dataType="Float">-442.280975</X>
                            <Y dataType="Float">-318.045441</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-442.280975</X>
                            <Y dataType="Float">-318.045441</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="139" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="140">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="141">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="142" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="143">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">140</parent>
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
                              <gameobj dataType="ObjectRef">122</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="144" length="1">
                              <object dataType="ObjectRef">139</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">122</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="145">
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
                            <R dataType="Byte">63</R>
                            <G dataType="Byte">40</G>
                            <B dataType="Byte">20</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">122</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">140</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="146">
                    <_items dataType="Array" type="Duality.Component[]" id="147" length="4">
                      <object dataType="ObjectRef">138</object>
                      <object dataType="ObjectRef">145</object>
                      <object dataType="ObjectRef">140</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">138</compTransform>
                  <EventComponentAdded dataType="ObjectRef">63</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">66</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="148">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="149">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">148</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="150">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="151" length="3">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">129</prop>
                          <componentType dataType="ObjectRef">78</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="152">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">63</R>
                            <G dataType="Byte">40</G>
                            <B dataType="Byte">20</B>
                            <A dataType="Byte">255</A>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">126</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="153">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.14287588</X>
                            <Y dataType="Float">0.14287588</Y>
                            <Z dataType="Float">0.14287588</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">132</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="154">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-405.7499</X>
                            <Y dataType="Float">41.7660522</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">220</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">119</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="155" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="156" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">99</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="157" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="158">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-405.7499</X>
                            <Y dataType="Float">41.7660522</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.14287588</X>
                            <Y dataType="Float">0.14287588</Y>
                            <Z dataType="Float">0.14287588</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-405.7499</X>
                            <Y dataType="Float">41.7660522</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.14287588</X>
                            <Y dataType="Float">0.14287588</Y>
                            <Z dataType="Float">0.14287588</Z>
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
                            <X dataType="Float">-405.7499</X>
                            <Y dataType="Float">41.7660522</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-405.7499</X>
                            <Y dataType="Float">41.7660522</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="159" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="160">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="161">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="162" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="163">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">160</parent>
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
                              <gameobj dataType="ObjectRef">148</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="164" length="1">
                              <object dataType="ObjectRef">159</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">148</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="165">
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
                            <R dataType="Byte">63</R>
                            <G dataType="Byte">40</G>
                            <B dataType="Byte">20</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">148</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">160</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="166">
                    <_items dataType="Array" type="Duality.Component[]" id="167" length="4">
                      <object dataType="ObjectRef">158</object>
                      <object dataType="ObjectRef">165</object>
                      <object dataType="ObjectRef">160</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">158</compTransform>
                  <EventComponentAdded dataType="ObjectRef">63</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">66</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="168">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="169">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\RoundSquare.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">168</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="170">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="171" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">129</prop>
                          <componentType dataType="ObjectRef">78</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="172">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">64</R>
                            <G dataType="Byte">59</G>
                            <B dataType="Byte">19</B>
                            <A dataType="Byte">255</A>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">126</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="173">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.294843346</X>
                            <Y dataType="Float">0.294843346</Y>
                            <Z dataType="Float">0.294843346</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">132</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="174">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">178.6254</X>
                            <Y dataType="Float">-57.6664734</Y>
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
                      <_version dataType="Int">419</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">119</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="175" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="176" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">99</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="177" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="178">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">178.6254</X>
                            <Y dataType="Float">-57.6664734</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.294843346</X>
                            <Y dataType="Float">0.294843346</Y>
                            <Z dataType="Float">0.294843346</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">178.6254</X>
                            <Y dataType="Float">-57.6664734</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.294843346</X>
                            <Y dataType="Float">0.294843346</Y>
                            <Z dataType="Float">0.294843346</Z>
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
                            <X dataType="Float">178.6254</X>
                            <Y dataType="Float">-57.6664734</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">178.6254</X>
                            <Y dataType="Float">-57.6664734</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="179" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="180">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="181">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="182" length="8">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="183">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">180</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="184">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">180</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="185">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">180</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="186">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">180</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="187">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="188" length="8">
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
                                    <parent dataType="ObjectRef">180</parent>
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
                              <gameobj dataType="ObjectRef">168</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="189" length="1">
                              <object dataType="ObjectRef">179</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">168</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="190">
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
                            <R dataType="Byte">64</R>
                            <G dataType="Byte">59</G>
                            <B dataType="Byte">19</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">168</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">180</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="191">
                    <_items dataType="Array" type="Duality.Component[]" id="192" length="4">
                      <object dataType="ObjectRef">178</object>
                      <object dataType="ObjectRef">190</object>
                      <object dataType="ObjectRef">180</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">RoundSquare</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">178</compTransform>
                  <EventComponentAdded dataType="ObjectRef">63</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">66</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="193">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="194">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Complex.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">193</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="195">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="196" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">132</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="197">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-395.226227</X>
                            <Y dataType="Float">312.242828</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">126</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="198">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.37218675</X>
                            <Y dataType="Float">0.37218675</Y>
                            <Z dataType="Float">0.37218675</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">129</prop>
                          <componentType dataType="ObjectRef">78</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="199">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">47</R>
                            <G dataType="Byte">65</G>
                            <B dataType="Byte">25</B>
                            <A dataType="Byte">255</A>
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
                      <_version dataType="Int">345</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">119</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="200" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="201" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">99</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="202" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="203">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-395.226227</X>
                            <Y dataType="Float">312.242828</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.37218675</X>
                            <Y dataType="Float">0.37218675</Y>
                            <Z dataType="Float">0.37218675</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-395.226227</X>
                            <Y dataType="Float">312.242828</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.37218675</X>
                            <Y dataType="Float">0.37218675</Y>
                            <Z dataType="Float">0.37218675</Z>
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
                            <X dataType="Float">-395.226227</X>
                            <Y dataType="Float">312.242828</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-395.226227</X>
                            <Y dataType="Float">312.242828</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="204" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="205">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="206">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="207" length="16">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="208">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="209" length="5">
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
                                    <parent dataType="ObjectRef">205</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="210">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="211" length="5">
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
                                    <parent dataType="ObjectRef">205</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="212">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="213" length="5">
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
                                    <parent dataType="ObjectRef">205</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="214">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="215" length="5">
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
                                    <parent dataType="ObjectRef">205</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="216">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="217" length="5">
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
                                    <parent dataType="ObjectRef">205</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="218">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="219" length="4">
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
                                    <parent dataType="ObjectRef">205</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="220">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="221" length="4">
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
                                    <parent dataType="ObjectRef">205</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="222">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="223" length="4">
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
                                    <parent dataType="ObjectRef">205</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="224">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="225" length="4">
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
                                    <parent dataType="ObjectRef">205</parent>
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
                              <gameobj dataType="ObjectRef">193</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="226" length="1">
                              <object dataType="ObjectRef">204</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">193</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="227">
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
                            <R dataType="Byte">47</R>
                            <G dataType="Byte">65</G>
                            <B dataType="Byte">25</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">193</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">205</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="228">
                    <_items dataType="Array" type="Duality.Component[]" id="229" length="4">
                      <object dataType="ObjectRef">203</object>
                      <object dataType="ObjectRef">227</object>
                      <object dataType="ObjectRef">205</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Complex</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">203</compTransform>
                  <EventComponentAdded dataType="ObjectRef">63</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">66</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="230">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="231">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">230</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="232">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="233" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">129</prop>
                          <componentType dataType="ObjectRef">78</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="234">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">63</R>
                            <G dataType="Byte">25</G>
                            <B dataType="Byte">21</B>
                            <A dataType="Byte">255</A>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">126</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="235">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.295132369</X>
                            <Y dataType="Float">0.295132369</Y>
                            <Z dataType="Float">0.295132369</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="236" value="P:Duality.Components.Transform:RelativeAngle" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="237">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.154480875</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">132</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="238">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-125.178413</X>
                            <Y dataType="Float">151.4019</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">4</_size>
                      <_version dataType="Int">280</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">119</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="239" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="240" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">99</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="241" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="242">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-125.178413</X>
                            <Y dataType="Float">151.4019</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0.154480875</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.295132369</X>
                            <Y dataType="Float">0.295132369</Y>
                            <Z dataType="Float">0.295132369</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-125.178413</X>
                            <Y dataType="Float">151.4019</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0.154480875</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.295132369</X>
                            <Y dataType="Float">0.295132369</Y>
                            <Z dataType="Float">0.295132369</Z>
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
                            <X dataType="Float">-125.178413</X>
                            <Y dataType="Float">151.4019</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-125.178413</X>
                            <Y dataType="Float">151.4019</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0.154480875</lastAngle>
                          <lastAngleAbs dataType="Float">0.154480875</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="243" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="244">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="245">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="246" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="247">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="248" length="4">
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
                                    <parent dataType="ObjectRef">244</parent>
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
                              <gameobj dataType="ObjectRef">230</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="249" length="1">
                              <object dataType="ObjectRef">243</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">230</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="250">
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
                            <R dataType="Byte">63</R>
                            <G dataType="Byte">25</G>
                            <B dataType="Byte">21</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">230</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">244</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="251">
                    <_items dataType="Array" type="Duality.Component[]" id="252" length="4">
                      <object dataType="ObjectRef">242</object>
                      <object dataType="ObjectRef">250</object>
                      <object dataType="ObjectRef">244</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">242</compTransform>
                  <EventComponentAdded dataType="ObjectRef">63</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">66</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="253">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="254">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">253</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="255">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="256" length="3">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">129</prop>
                          <componentType dataType="ObjectRef">78</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="257">
                            <_items dataType="Array" type="System.Int32[]" id="258" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">63</R>
                            <G dataType="Byte">40</G>
                            <B dataType="Byte">20</B>
                            <A dataType="Byte">255</A>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">132</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="259">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">404.394562</X>
                            <Y dataType="Float">276.6514</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">126</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="260">
                            <_items dataType="ObjectRef">134</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.166457728</X>
                            <Y dataType="Float">0.166457728</Y>
                            <Z dataType="Float">0.166457728</Z>
                          </val>
                        </object>
                      </_items>
                      <_size dataType="Int">3</_size>
                      <_version dataType="Int">128</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">119</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="261" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="262" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">78</object>
                        <object dataType="ObjectRef">99</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="263" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="264">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">404.394562</X>
                            <Y dataType="Float">276.6514</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.166457728</X>
                            <Y dataType="Float">0.166457728</Y>
                            <Z dataType="Float">0.166457728</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">404.394562</X>
                            <Y dataType="Float">276.6514</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.166457728</X>
                            <Y dataType="Float">0.166457728</Y>
                            <Z dataType="Float">0.166457728</Z>
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
                            <X dataType="Float">404.394562</X>
                            <Y dataType="Float">276.6514</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">404.394562</X>
                            <Y dataType="Float">276.6514</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0</lastAngle>
                          <lastAngleAbs dataType="Float">0</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="265" multi="true">
                            <object dataType="ObjectRef">83</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="266">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="267">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="268" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="269">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">266</parent>
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
                              <gameobj dataType="ObjectRef">253</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="270" length="1">
                              <object dataType="ObjectRef">265</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">253</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="271">
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
                            <R dataType="Byte">63</R>
                            <G dataType="Byte">40</G>
                            <B dataType="Byte">20</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">253</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">266</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="272">
                    <_items dataType="Array" type="Duality.Component[]" id="273" length="4">
                      <object dataType="ObjectRef">264</object>
                      <object dataType="ObjectRef">271</object>
                      <object dataType="ObjectRef">266</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">264</compTransform>
                  <EventComponentAdded dataType="ObjectRef">63</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">66</EventComponentRemoving>
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
              </_items>
              <_size dataType="Int">6</_size>
              <_version dataType="Int">162</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="274" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="275" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="276" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="277">
              <_items dataType="ObjectRef">113</_items>
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <name dataType="String">Dynamics</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="278" multi="true">
              <object dataType="ObjectRef">115</object>
              <object dataType="ObjectRef">264</object>
              <object dataType="Array" type="System.Delegate[]" id="279" length="7">
                <object dataType="ObjectRef">63</object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="280" multi="true">
                  <object dataType="ObjectRef">115</object>
                  <object dataType="ObjectRef">138</object>
                  <object dataType="Array" type="System.Delegate[]" id="281" length="1">
                    <object dataType="ObjectRef">280</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="282" multi="true">
                  <object dataType="ObjectRef">115</object>
                  <object dataType="ObjectRef">158</object>
                  <object dataType="Array" type="System.Delegate[]" id="283" length="1">
                    <object dataType="ObjectRef">282</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="284" multi="true">
                  <object dataType="ObjectRef">115</object>
                  <object dataType="ObjectRef">178</object>
                  <object dataType="Array" type="System.Delegate[]" id="285" length="1">
                    <object dataType="ObjectRef">284</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="286" multi="true">
                  <object dataType="ObjectRef">115</object>
                  <object dataType="ObjectRef">203</object>
                  <object dataType="Array" type="System.Delegate[]" id="287" length="1">
                    <object dataType="ObjectRef">286</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="288" multi="true">
                  <object dataType="ObjectRef">115</object>
                  <object dataType="ObjectRef">242</object>
                  <object dataType="Array" type="System.Delegate[]" id="289" length="1">
                    <object dataType="ObjectRef">288</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="290" multi="true">
                  <object dataType="ObjectRef">115</object>
                  <object dataType="ObjectRef">264</object>
                  <object dataType="Array" type="System.Delegate[]" id="291" length="1">
                    <object dataType="ObjectRef">290</object>
                  </object>
                </object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">66</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="292">
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="293">
              <_items dataType="Array" type="Duality.GameObject[]" id="294" length="4">
                <object />
                <object />
                <object />
                <object />
              </_items>
              <_size dataType="Int">0</_size>
              <_version dataType="Int">6</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="295" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="296" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="297" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="298">
              <_items dataType="Array" type="Duality.Component[]" id="299" length="0" />
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <name dataType="String">Text</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="ObjectRef">63</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">66</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="ObjectRef">72</object>
          <object dataType="ObjectRef">75</object>
          <object dataType="ObjectRef">122</object>
          <object dataType="ObjectRef">148</object>
          <object dataType="ObjectRef">168</object>
          <object dataType="ObjectRef">193</object>
          <object dataType="ObjectRef">230</object>
          <object dataType="ObjectRef">253</object>
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
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
        <_size dataType="Int">12</_size>
        <_version dataType="Int">226</_version>
      </allObj>
      <Registered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="300" multi="true">
        <object dataType="MethodInfo" id="301" value="M:Duality.Resources.Scene:objectManager_Registered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="302" length="1">
          <object dataType="ObjectRef">300</object>
        </object>
      </Registered>
      <Unregistered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="303" multi="true">
        <object dataType="MethodInfo" id="304" value="M:Duality.Resources.Scene:objectManager_Unregistered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="305" length="1">
          <object dataType="ObjectRef">303</object>
        </object>
      </Unregistered>
    </objectManager>
    <sourcePath />
  </object>
</root>