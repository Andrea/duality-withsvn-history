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
                      <sourceText dataType="String">/cFFAAAAFFLeft Mouse/cFFFFFFFF: Drag object/n/cFFAAAAFFRight Mouse/cFFFFFFFF: Create object/n/cFFAAAAFFNumber keys/cFFFFFFFF: Select testbed scene/n</sourceText>
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
                      <displayedText dataType="String">Left Mouse: Drag objectRight Mouse: Create objectNumber keys: Select testbed scene</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="39" length="1">
                        <object dataType="Int">82</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="40" length="15">
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
                      </elements>
                    </controls>
                    <stats dataType="Class" type="Duality.FormattedText" id="56">
                      <sourceText />
                      <icons />
                      <flowAreas />
                      <fonts />
                      <maxWidth dataType="Int">0</maxWidth>
                      <maxHeight dataType="Int">0</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String"></displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="57" length="0" />
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="58" length="0" />
                    </stats>
                    <gameobj dataType="ObjectRef">11</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="59">
              <_items dataType="Array" type="Duality.Component[]" id="60" length="4">
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
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="61" multi="true">
              <object dataType="MethodInfo" id="62" value="M:Duality.ObjectManagers.GameObjectManager:OnRegisteredObjectComponentAdded(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">2</object>
              <object dataType="Array" type="System.Delegate[]" id="63" length="1">
                <object dataType="ObjectRef">61</object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="64" multi="true">
              <object dataType="MethodInfo" id="65" value="M:Duality.ObjectManagers.GameObjectManager:OnRegisteredObjectComponentRemoved(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">2</object>
              <object dataType="Array" type="System.Delegate[]" id="66" length="1">
                <object dataType="ObjectRef">64</object>
              </object>
            </EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="67">
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="68">
              <_items dataType="Array" type="Duality.GameObject[]" id="69" length="32">
                <object dataType="Class" type="Duality.GameObject" id="70">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="71" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="72" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="Type" id="73" value="Duality.Components.Renderers.SpriteRenderer" />
                        <object dataType="Type" id="74" value="Duality.Components.Physics.RigidBody" />
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="75" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="76">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="77" multi="true">
                            <object dataType="MethodInfo" id="78" value="M:Duality.Components.Physics.RigidBody:OnTransformChanged(System.Object,Duality.TransformChangedEventArgs)" />
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="79">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="80">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="81" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="82">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="83" length="4">
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
                                    <parent dataType="ObjectRef">79</parent>
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
                              <gameobj dataType="ObjectRef">70</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="84" length="1">
                              <object dataType="ObjectRef">77</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">70</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="85">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="Class" type="Duality.Resources.BatchInfo" id="86">
                            <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
                              <contentPath dataType="String">Default:DrawTechnique:Mask</contentPath>
                            </technique>
                            <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                              <R dataType="Byte">68</R>
                              <G dataType="Byte">79</G>
                              <B dataType="Byte">119</B>
                              <A dataType="Byte">255</A>
                            </mainColor>
                            <textures dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.String],[Duality.ContentRef`1[[Duality.Resources.Texture]]]]" id="87" surrogate="true">
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
                          <gameobj dataType="ObjectRef">70</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">79</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="88">
                    <_items dataType="Array" type="Duality.Component[]" id="89" length="4">
                      <object dataType="ObjectRef">76</object>
                      <object dataType="ObjectRef">85</object>
                      <object dataType="ObjectRef">79</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">7</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">76</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="90">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="91" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="92" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="93" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="94">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="95" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="96">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="97">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="98" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="99">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="100" length="4">
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
                                    <parent dataType="ObjectRef">96</parent>
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
                              <gameobj dataType="ObjectRef">90</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="101" length="1">
                              <object dataType="ObjectRef">95</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">90</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="102">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="Class" type="Duality.Resources.BatchInfo" id="103">
                            <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
                              <contentPath dataType="String">Default:DrawTechnique:Mask</contentPath>
                            </technique>
                            <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                              <R dataType="Byte">97</R>
                              <G dataType="Byte">68</G>
                              <B dataType="Byte">119</B>
                              <A dataType="Byte">255</A>
                            </mainColor>
                            <textures dataType="ObjectRef">87</textures>
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
                          <gameobj dataType="ObjectRef">90</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">96</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="104">
                    <_items dataType="Array" type="Duality.Component[]" id="105" length="4">
                      <object dataType="ObjectRef">94</object>
                      <object dataType="ObjectRef">102</object>
                      <object dataType="ObjectRef">96</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">94</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="106">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="107" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="108" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="109" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="110">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="111" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="112">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="113">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="114" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="115">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="116" length="4">
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
                                    <parent dataType="ObjectRef">112</parent>
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
                              <gameobj dataType="ObjectRef">106</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="117" length="1">
                              <object dataType="ObjectRef">111</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">106</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="118">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="Class" type="Duality.Resources.BatchInfo" id="119">
                            <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
                              <contentPath dataType="String">Default:DrawTechnique:Mask</contentPath>
                            </technique>
                            <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                              <R dataType="Byte">68</R>
                              <G dataType="Byte">117</G>
                              <B dataType="Byte">119</B>
                              <A dataType="Byte">255</A>
                            </mainColor>
                            <textures dataType="ObjectRef">87</textures>
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
                          <gameobj dataType="ObjectRef">106</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">112</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="120">
                    <_items dataType="Array" type="Duality.Component[]" id="121" length="4">
                      <object dataType="ObjectRef">110</object>
                      <object dataType="ObjectRef">118</object>
                      <object dataType="ObjectRef">112</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">110</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="122">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="123" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="124" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="125" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="126">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="127" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="128">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="129">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="130" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="131">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="132" length="4">
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
                                    <parent dataType="ObjectRef">128</parent>
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
                            <object dataType="Array" type="System.Delegate[]" id="133" length="1">
                              <object dataType="ObjectRef">127</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">122</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="134">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">122</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">128</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="135">
                    <_items dataType="Array" type="Duality.Component[]" id="136" length="4">
                      <object dataType="ObjectRef">126</object>
                      <object dataType="ObjectRef">134</object>
                      <object dataType="ObjectRef">128</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">126</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="137">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="138" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="139" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="140" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="141">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="142" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="143">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="144">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="145" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="146">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="147" length="4">
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
                                    <parent dataType="ObjectRef">143</parent>
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
                              <gameobj dataType="ObjectRef">137</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="148" length="1">
                              <object dataType="ObjectRef">142</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">137</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="149">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">137</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">143</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="150">
                    <_items dataType="Array" type="Duality.Component[]" id="151" length="4">
                      <object dataType="ObjectRef">141</object>
                      <object dataType="ObjectRef">149</object>
                      <object dataType="ObjectRef">143</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">141</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="152">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="153" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="154" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="155" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="156">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="157" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="158">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="159">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="160" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="161">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="162" length="4">
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
                                    <parent dataType="ObjectRef">158</parent>
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
                              <gameobj dataType="ObjectRef">152</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="163" length="1">
                              <object dataType="ObjectRef">157</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">152</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="164">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">152</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">158</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="165">
                    <_items dataType="Array" type="Duality.Component[]" id="166" length="4">
                      <object dataType="ObjectRef">156</object>
                      <object dataType="ObjectRef">164</object>
                      <object dataType="ObjectRef">158</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">156</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="167">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="168" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="169" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="170" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="171">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="172" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="173">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="174">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="175" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="176">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="177" length="4">
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
                                    <parent dataType="ObjectRef">173</parent>
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
                              <gameobj dataType="ObjectRef">167</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="178" length="1">
                              <object dataType="ObjectRef">172</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">167</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="179">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">167</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">173</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="180">
                    <_items dataType="Array" type="Duality.Component[]" id="181" length="4">
                      <object dataType="ObjectRef">171</object>
                      <object dataType="ObjectRef">179</object>
                      <object dataType="ObjectRef">173</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">171</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="182">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="183" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="184" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="185" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="186">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="187" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="188">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="189">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="190" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="191">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="192" length="4">
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
                                    <parent dataType="ObjectRef">188</parent>
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
                              <gameobj dataType="ObjectRef">182</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="193" length="1">
                              <object dataType="ObjectRef">187</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">182</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="194">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">182</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">188</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="195">
                    <_items dataType="Array" type="Duality.Component[]" id="196" length="4">
                      <object dataType="ObjectRef">186</object>
                      <object dataType="ObjectRef">194</object>
                      <object dataType="ObjectRef">188</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">186</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="197">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="198" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="199" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="200" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="201">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="202" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="203">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="204">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="205" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="206">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="207" length="4">
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
                                    <parent dataType="ObjectRef">203</parent>
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
                              <gameobj dataType="ObjectRef">197</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="208" length="1">
                              <object dataType="ObjectRef">202</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">197</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="209">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">197</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">203</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="210">
                    <_items dataType="Array" type="Duality.Component[]" id="211" length="4">
                      <object dataType="ObjectRef">201</object>
                      <object dataType="ObjectRef">209</object>
                      <object dataType="ObjectRef">203</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">201</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="212">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="213" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="214" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="215" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="216">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="217" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="218">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="219">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="220" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="221">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="222" length="4">
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
                                    <parent dataType="ObjectRef">218</parent>
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
                              <gameobj dataType="ObjectRef">212</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="223" length="1">
                              <object dataType="ObjectRef">217</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">212</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="224">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">212</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">218</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="225">
                    <_items dataType="Array" type="Duality.Component[]" id="226" length="4">
                      <object dataType="ObjectRef">216</object>
                      <object dataType="ObjectRef">224</object>
                      <object dataType="ObjectRef">218</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">216</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="227">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="228" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="229" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="230" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="231">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="232" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="233">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="234">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="235" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="236">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="237" length="4">
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
                                    <parent dataType="ObjectRef">233</parent>
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
                              <gameobj dataType="ObjectRef">227</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="238" length="1">
                              <object dataType="ObjectRef">232</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">227</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="239">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">227</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">233</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="240">
                    <_items dataType="Array" type="Duality.Component[]" id="241" length="4">
                      <object dataType="ObjectRef">231</object>
                      <object dataType="ObjectRef">239</object>
                      <object dataType="ObjectRef">233</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">231</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="242">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="243" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="244" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="245" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="246">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="247" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="248">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="249">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="250" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="251">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="252" length="4">
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
                                    <parent dataType="ObjectRef">248</parent>
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
                              <gameobj dataType="ObjectRef">242</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="253" length="1">
                              <object dataType="ObjectRef">247</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">242</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="254">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">242</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">248</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="255">
                    <_items dataType="Array" type="Duality.Component[]" id="256" length="4">
                      <object dataType="ObjectRef">246</object>
                      <object dataType="ObjectRef">254</object>
                      <object dataType="ObjectRef">248</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">246</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="257">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="258" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="259" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="260" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="261">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="262" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="263">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="264">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="265" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="266">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="267" length="4">
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
                                    <parent dataType="ObjectRef">263</parent>
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
                              <gameobj dataType="ObjectRef">257</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="268" length="1">
                              <object dataType="ObjectRef">262</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">257</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="269">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">257</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">263</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="270">
                    <_items dataType="Array" type="Duality.Component[]" id="271" length="4">
                      <object dataType="ObjectRef">261</object>
                      <object dataType="ObjectRef">269</object>
                      <object dataType="ObjectRef">263</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">261</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="272">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="273" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="274" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="275" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="276">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="277" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="278">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="279">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="280" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="281">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="282" length="4">
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
                                    <parent dataType="ObjectRef">278</parent>
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
                              <gameobj dataType="ObjectRef">272</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="283" length="1">
                              <object dataType="ObjectRef">277</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">272</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="284">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">272</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">278</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="285">
                    <_items dataType="Array" type="Duality.Component[]" id="286" length="4">
                      <object dataType="ObjectRef">276</object>
                      <object dataType="ObjectRef">284</object>
                      <object dataType="ObjectRef">278</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">276</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="287">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="288" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="289" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="290" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="291">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="292" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="293">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="294">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="295" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="296">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="297" length="4">
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
                                    <parent dataType="ObjectRef">293</parent>
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
                              <gameobj dataType="ObjectRef">287</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="298" length="1">
                              <object dataType="ObjectRef">292</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">287</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="299">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">287</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">293</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="300">
                    <_items dataType="Array" type="Duality.Component[]" id="301" length="4">
                      <object dataType="ObjectRef">291</object>
                      <object dataType="ObjectRef">299</object>
                      <object dataType="ObjectRef">293</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">291</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="302">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="303" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="304" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="305" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="306">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="307" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="308">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="309">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="310" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="311">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="312" length="4">
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
                                    <parent dataType="ObjectRef">308</parent>
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
                              <gameobj dataType="ObjectRef">302</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="313" length="1">
                              <object dataType="ObjectRef">307</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">302</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="314">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">302</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">308</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="315">
                    <_items dataType="Array" type="Duality.Component[]" id="316" length="4">
                      <object dataType="ObjectRef">306</object>
                      <object dataType="ObjectRef">314</object>
                      <object dataType="ObjectRef">308</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">306</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="317">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="318" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="319" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="320" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="321">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="322" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="323">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="324">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="325" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="326">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="327" length="4">
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
                                    <parent dataType="ObjectRef">323</parent>
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
                              <gameobj dataType="ObjectRef">317</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="328" length="1">
                              <object dataType="ObjectRef">322</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">317</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="329">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">317</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">323</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="330">
                    <_items dataType="Array" type="Duality.Component[]" id="331" length="4">
                      <object dataType="ObjectRef">321</object>
                      <object dataType="ObjectRef">329</object>
                      <object dataType="ObjectRef">323</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">321</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="332">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="333" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="334" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="335" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="336">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="337" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="338">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="339">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="340" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="341">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="342" length="4">
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
                                    <parent dataType="ObjectRef">338</parent>
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
                              <gameobj dataType="ObjectRef">332</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="343" length="1">
                              <object dataType="ObjectRef">337</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">332</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="344">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">332</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">338</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="345">
                    <_items dataType="Array" type="Duality.Component[]" id="346" length="4">
                      <object dataType="ObjectRef">336</object>
                      <object dataType="ObjectRef">344</object>
                      <object dataType="ObjectRef">338</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">336</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="347">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="348" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="349" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="350" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="351">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="352" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="353">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="354">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="355" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="356">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="357" length="4">
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
                                    <parent dataType="ObjectRef">353</parent>
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
                              <gameobj dataType="ObjectRef">347</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="358" length="1">
                              <object dataType="ObjectRef">352</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">347</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="359">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">347</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">353</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="360">
                    <_items dataType="Array" type="Duality.Component[]" id="361" length="4">
                      <object dataType="ObjectRef">351</object>
                      <object dataType="ObjectRef">359</object>
                      <object dataType="ObjectRef">353</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">351</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="362">
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="363" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="364" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="365" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="366">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="367" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="368">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="369">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="370" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="371">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="372" length="4">
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
                                    <parent dataType="ObjectRef">368</parent>
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
                              <gameobj dataType="ObjectRef">362</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="373" length="1">
                              <object dataType="ObjectRef">367</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">362</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="374">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-256</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">512</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">86</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">362</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">368</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="375">
                    <_items dataType="Array" type="Duality.Component[]" id="376" length="4">
                      <object dataType="ObjectRef">366</object>
                      <object dataType="ObjectRef">374</object>
                      <object dataType="ObjectRef">368</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Wall</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">366</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
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
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="377" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="378" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="379" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="380">
              <_items dataType="Array" type="Duality.Component[]" id="381" length="0" />
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <name dataType="String">StaticWorld</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="382" multi="true">
              <object dataType="MethodInfo" id="383" value="M:Duality.Components.Transform:Parent_EventComponentAdded(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">366</object>
              <object dataType="Array" type="System.Delegate[]" id="384" length="21">
                <object dataType="ObjectRef">61</object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="385" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">76</object>
                  <object dataType="Array" type="System.Delegate[]" id="386" length="1">
                    <object dataType="ObjectRef">385</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="387" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">94</object>
                  <object dataType="Array" type="System.Delegate[]" id="388" length="1">
                    <object dataType="ObjectRef">387</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="389" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">110</object>
                  <object dataType="Array" type="System.Delegate[]" id="390" length="1">
                    <object dataType="ObjectRef">389</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="391" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">126</object>
                  <object dataType="Array" type="System.Delegate[]" id="392" length="1">
                    <object dataType="ObjectRef">391</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="393" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">141</object>
                  <object dataType="Array" type="System.Delegate[]" id="394" length="1">
                    <object dataType="ObjectRef">393</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="395" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">156</object>
                  <object dataType="Array" type="System.Delegate[]" id="396" length="1">
                    <object dataType="ObjectRef">395</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="397" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">171</object>
                  <object dataType="Array" type="System.Delegate[]" id="398" length="1">
                    <object dataType="ObjectRef">397</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="399" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">186</object>
                  <object dataType="Array" type="System.Delegate[]" id="400" length="1">
                    <object dataType="ObjectRef">399</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="401" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">201</object>
                  <object dataType="Array" type="System.Delegate[]" id="402" length="1">
                    <object dataType="ObjectRef">401</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="403" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">216</object>
                  <object dataType="Array" type="System.Delegate[]" id="404" length="1">
                    <object dataType="ObjectRef">403</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="405" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">231</object>
                  <object dataType="Array" type="System.Delegate[]" id="406" length="1">
                    <object dataType="ObjectRef">405</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="407" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">246</object>
                  <object dataType="Array" type="System.Delegate[]" id="408" length="1">
                    <object dataType="ObjectRef">407</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="409" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">261</object>
                  <object dataType="Array" type="System.Delegate[]" id="410" length="1">
                    <object dataType="ObjectRef">409</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="411" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">276</object>
                  <object dataType="Array" type="System.Delegate[]" id="412" length="1">
                    <object dataType="ObjectRef">411</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="413" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">291</object>
                  <object dataType="Array" type="System.Delegate[]" id="414" length="1">
                    <object dataType="ObjectRef">413</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="415" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">306</object>
                  <object dataType="Array" type="System.Delegate[]" id="416" length="1">
                    <object dataType="ObjectRef">415</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="417" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">321</object>
                  <object dataType="Array" type="System.Delegate[]" id="418" length="1">
                    <object dataType="ObjectRef">417</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="419" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">336</object>
                  <object dataType="Array" type="System.Delegate[]" id="420" length="1">
                    <object dataType="ObjectRef">419</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="421" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">351</object>
                  <object dataType="Array" type="System.Delegate[]" id="422" length="1">
                    <object dataType="ObjectRef">421</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="423" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">366</object>
                  <object dataType="Array" type="System.Delegate[]" id="424" length="1">
                    <object dataType="ObjectRef">423</object>
                  </object>
                </object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="425">
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="426">
              <_items dataType="Array" type="Duality.GameObject[]" id="427" length="64">
                <object dataType="Class" type="Duality.GameObject" id="428">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="429">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">428</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="430">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="431" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="432" value="P:Duality.Components.Transform:RelativePos" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="433">
                            <_items dataType="Array" type="System.Int32[]" id="434" length="0" />
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
                          <prop dataType="PropertyInfo" id="435" value="P:Duality.Components.Transform:RelativeScale" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="436">
                            <_items dataType="ObjectRef">434</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="437" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="438" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="439" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="440">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="441" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="442">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="443">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="444" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="445">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">442</parent>
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
                              <gameobj dataType="ObjectRef">428</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="446" length="1">
                              <object dataType="ObjectRef">441</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">428</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="447">
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
                          <gameobj dataType="ObjectRef">428</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">442</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="448">
                    <_items dataType="Array" type="Duality.Component[]" id="449" length="4">
                      <object dataType="ObjectRef">440</object>
                      <object dataType="ObjectRef">447</object>
                      <object dataType="ObjectRef">442</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">440</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="450">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="451">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">450</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="452">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="453" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="454">
                            <_items dataType="ObjectRef">434</_items>
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
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="455">
                            <_items dataType="ObjectRef">434</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="456" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="457" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="458" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="459">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="460" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="461">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="462">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="463" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="464">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">461</parent>
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
                              <gameobj dataType="ObjectRef">450</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="465" length="1">
                              <object dataType="ObjectRef">460</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">450</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="466">
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
                          <gameobj dataType="ObjectRef">450</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">461</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="467">
                    <_items dataType="Array" type="Duality.Component[]" id="468" length="4">
                      <object dataType="ObjectRef">459</object>
                      <object dataType="ObjectRef">466</object>
                      <object dataType="ObjectRef">461</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">459</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="469">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="470">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">469</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="471">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="472" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="473">
                            <_items dataType="ObjectRef">434</_items>
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="474">
                            <_items dataType="ObjectRef">434</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="475" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="476" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="477" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="478">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="479" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="480">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="481">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="482" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="483">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">480</parent>
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
                              <gameobj dataType="ObjectRef">469</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="484" length="1">
                              <object dataType="ObjectRef">479</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">469</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="485">
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
                          <gameobj dataType="ObjectRef">469</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">480</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="486">
                    <_items dataType="Array" type="Duality.Component[]" id="487" length="4">
                      <object dataType="ObjectRef">478</object>
                      <object dataType="ObjectRef">485</object>
                      <object dataType="ObjectRef">480</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">478</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="488">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="489">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">488</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="490">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="491" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="492">
                            <_items dataType="ObjectRef">434</_items>
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
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="493">
                            <_items dataType="ObjectRef">434</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="494" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="495" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="496" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="497">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="498" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="499">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="500">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="501" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="502">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">499</parent>
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
                              <gameobj dataType="ObjectRef">488</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="503" length="1">
                              <object dataType="ObjectRef">498</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">488</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="504">
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
                          <gameobj dataType="ObjectRef">488</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">499</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="505">
                    <_items dataType="Array" type="Duality.Component[]" id="506" length="4">
                      <object dataType="ObjectRef">497</object>
                      <object dataType="ObjectRef">504</object>
                      <object dataType="ObjectRef">499</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">497</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="507">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="508">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\RoundSquare.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">507</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="509">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="510" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="511">
                            <_items dataType="ObjectRef">434</_items>
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="512">
                            <_items dataType="ObjectRef">434</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="513" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="514" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="515" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="516">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="517" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="518">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="519">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="520" length="8">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="521">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">518</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="522">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">518</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="523">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">518</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="524">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">518</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="525">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="526" length="8">
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
                                    <parent dataType="ObjectRef">518</parent>
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
                              <gameobj dataType="ObjectRef">507</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="527" length="1">
                              <object dataType="ObjectRef">517</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">507</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="528">
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
                          <gameobj dataType="ObjectRef">507</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">518</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="529">
                    <_items dataType="Array" type="Duality.Component[]" id="530" length="4">
                      <object dataType="ObjectRef">516</object>
                      <object dataType="ObjectRef">528</object>
                      <object dataType="ObjectRef">518</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">RoundSquare</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">516</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="531">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="532">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\RoundSquare.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">531</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="533">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="534" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="535">
                            <_items dataType="ObjectRef">434</_items>
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
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="536">
                            <_items dataType="ObjectRef">434</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="537" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="538" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="539" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="540">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="541" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="542">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="543">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="544" length="8">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="545">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">542</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="546">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">542</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="547">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">542</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="548">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">542</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="549">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="550" length="8">
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
                                    <parent dataType="ObjectRef">542</parent>
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
                              <gameobj dataType="ObjectRef">531</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="551" length="1">
                              <object dataType="ObjectRef">541</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">531</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="552">
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
                          <gameobj dataType="ObjectRef">531</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">542</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="553">
                    <_items dataType="Array" type="Duality.Component[]" id="554" length="4">
                      <object dataType="ObjectRef">540</object>
                      <object dataType="ObjectRef">552</object>
                      <object dataType="ObjectRef">542</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">RoundSquare</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">540</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="555">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="556">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">555</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="557">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="558" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="559">
                            <_items dataType="ObjectRef">434</_items>
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="560">
                            <_items dataType="ObjectRef">434</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="561" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="562" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="563" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="564">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="565" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="566">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="567">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="568" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="569">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="570" length="4">
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
                                    <parent dataType="ObjectRef">566</parent>
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
                              <gameobj dataType="ObjectRef">555</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="571" length="1">
                              <object dataType="ObjectRef">565</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">555</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="572">
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
                          <gameobj dataType="ObjectRef">555</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">566</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="573">
                    <_items dataType="Array" type="Duality.Component[]" id="574" length="4">
                      <object dataType="ObjectRef">564</object>
                      <object dataType="ObjectRef">572</object>
                      <object dataType="ObjectRef">566</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">564</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="575">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="576">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">575</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="577">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="578" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="579">
                            <_items dataType="ObjectRef">434</_items>
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="580">
                            <_items dataType="ObjectRef">434</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="581" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="582" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="583" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="584">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="585" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="586">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="587">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="588" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="589">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="590" length="4">
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
                                    <parent dataType="ObjectRef">586</parent>
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
                              <gameobj dataType="ObjectRef">575</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="591" length="1">
                              <object dataType="ObjectRef">585</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">575</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="592">
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
                          <gameobj dataType="ObjectRef">575</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">586</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="593">
                    <_items dataType="Array" type="Duality.Component[]" id="594" length="4">
                      <object dataType="ObjectRef">584</object>
                      <object dataType="ObjectRef">592</object>
                      <object dataType="ObjectRef">586</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">584</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="595">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="596">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">595</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="597">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="598" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="599">
                            <_items dataType="ObjectRef">434</_items>
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
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="600">
                            <_items dataType="ObjectRef">434</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="601" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="602" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="603" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="604">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="605" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="606">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="607">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="608" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="609">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="610" length="4">
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
                                    <parent dataType="ObjectRef">606</parent>
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
                              <gameobj dataType="ObjectRef">595</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="611" length="1">
                              <object dataType="ObjectRef">605</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">595</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="612">
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
                          <gameobj dataType="ObjectRef">595</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">606</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="613">
                    <_items dataType="Array" type="Duality.Component[]" id="614" length="4">
                      <object dataType="ObjectRef">604</object>
                      <object dataType="ObjectRef">612</object>
                      <object dataType="ObjectRef">606</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">604</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="615">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="616">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Complex.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">615</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="617">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="618" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="619">
                            <_items dataType="ObjectRef">434</_items>
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="620">
                            <_items dataType="ObjectRef">434</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="621" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="622" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="623" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="624">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="625" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="626">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="627">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="628" length="16">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="629">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="630" length="5">
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
                                    <parent dataType="ObjectRef">626</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="631">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="632" length="5">
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
                                    <parent dataType="ObjectRef">626</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="633">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="634" length="5">
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
                                    <parent dataType="ObjectRef">626</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="635">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="636" length="5">
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
                                    <parent dataType="ObjectRef">626</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="637">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="638" length="5">
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
                                    <parent dataType="ObjectRef">626</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="639">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="640" length="4">
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
                                    <parent dataType="ObjectRef">626</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="641">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="642" length="4">
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
                                    <parent dataType="ObjectRef">626</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="643">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="644" length="4">
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
                                    <parent dataType="ObjectRef">626</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="645">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="646" length="4">
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
                                    <parent dataType="ObjectRef">626</parent>
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
                              <gameobj dataType="ObjectRef">615</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="647" length="1">
                              <object dataType="ObjectRef">625</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">615</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="648">
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
                          <gameobj dataType="ObjectRef">615</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">626</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="649">
                    <_items dataType="Array" type="Duality.Component[]" id="650" length="4">
                      <object dataType="ObjectRef">624</object>
                      <object dataType="ObjectRef">648</object>
                      <object dataType="ObjectRef">626</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Complex</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">624</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="651">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="652">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Complex.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">651</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="653">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="654" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="655">
                            <_items dataType="ObjectRef">434</_items>
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="656">
                            <_items dataType="ObjectRef">434</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="657" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="658" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="659" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="660">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="661" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="662">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="663">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="664" length="16">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="665">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="666" length="5">
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
                                    <parent dataType="ObjectRef">662</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="667">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="668" length="5">
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
                                    <parent dataType="ObjectRef">662</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="669">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="670" length="5">
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
                                    <parent dataType="ObjectRef">662</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="671">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="672" length="5">
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
                                    <parent dataType="ObjectRef">662</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="673">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="674" length="5">
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
                                    <parent dataType="ObjectRef">662</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="675">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="676" length="4">
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
                                    <parent dataType="ObjectRef">662</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="677">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="678" length="4">
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
                                    <parent dataType="ObjectRef">662</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="679">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="680" length="4">
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
                                    <parent dataType="ObjectRef">662</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="681">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="682" length="4">
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
                                    <parent dataType="ObjectRef">662</parent>
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
                              <gameobj dataType="ObjectRef">651</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="683" length="1">
                              <object dataType="ObjectRef">661</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">651</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="684">
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
                          <gameobj dataType="ObjectRef">651</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">662</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="685">
                    <_items dataType="Array" type="Duality.Component[]" id="686" length="4">
                      <object dataType="ObjectRef">660</object>
                      <object dataType="ObjectRef">684</object>
                      <object dataType="ObjectRef">662</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Complex</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">660</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="687">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="688">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">687</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="689">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="690" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="691">
                            <_items dataType="Array" type="System.Int32[]" id="692" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="693">
                            <_items dataType="Array" type="System.Int32[]" id="694" length="0" />
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="695" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="696" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="697" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="698">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="699" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="700">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="701">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="702" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="703">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">700</parent>
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
                              <gameobj dataType="ObjectRef">687</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="704" length="1">
                              <object dataType="ObjectRef">699</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">687</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="705">
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
                          <gameobj dataType="ObjectRef">687</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">700</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="706">
                    <_items dataType="Array" type="Duality.Component[]" id="707" length="4">
                      <object dataType="ObjectRef">698</object>
                      <object dataType="ObjectRef">705</object>
                      <object dataType="ObjectRef">700</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">698</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="708">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="709">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\RoundSquare.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">708</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="710">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="711" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="712">
                            <_items dataType="Array" type="System.Int32[]" id="713" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="714">
                            <_items dataType="ObjectRef">694</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="715" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="716" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="717" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="718">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="719" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="720">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="721">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="722" length="8">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="723">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">720</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="724">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">720</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="725">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">720</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="726">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">720</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="727">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="728" length="8">
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
                                    <parent dataType="ObjectRef">720</parent>
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
                              <gameobj dataType="ObjectRef">708</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="729" length="1">
                              <object dataType="ObjectRef">719</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">708</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="730">
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
                          <gameobj dataType="ObjectRef">708</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">720</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="731">
                    <_items dataType="Array" type="Duality.Component[]" id="732" length="4">
                      <object dataType="ObjectRef">718</object>
                      <object dataType="ObjectRef">730</object>
                      <object dataType="ObjectRef">720</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">RoundSquare</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">718</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="733">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="734">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">733</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="735">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="736" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="737">
                            <_items dataType="Array" type="System.Int32[]" id="738" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="739">
                            <_items dataType="ObjectRef">694</_items>
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
                          <prop dataType="PropertyInfo" id="740" value="P:Duality.Components.Physics.RigidBody:Shapes" />
                          <componentType dataType="ObjectRef">74</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="741">
                            <_items dataType="Array" type="System.Int32[]" id="742" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="743">
                            <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="744" length="4">
                              <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="745">
                                <vertices dataType="Array" type="OpenTK.Vector2[]" id="746" length="4">
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
                                <parent dataType="Class" type="Duality.Components.Physics.RigidBody" id="747">
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
                                  <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="748">
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
                                        <parent dataType="ObjectRef">747</parent>
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
                                  <gameobj dataType="ObjectRef">733</gameobj>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="752" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="753" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="754" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="755">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="756" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="ObjectRef">747</object>
                            <object dataType="Array" type="System.Delegate[]" id="757" length="1">
                              <object dataType="ObjectRef">756</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">733</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="758">
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
                          <gameobj dataType="ObjectRef">733</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">747</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="759">
                    <_items dataType="Array" type="Duality.Component[]" id="760" length="4">
                      <object dataType="ObjectRef">755</object>
                      <object dataType="ObjectRef">758</object>
                      <object dataType="ObjectRef">747</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">755</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="761">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="762">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Complex.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">761</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="763">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="764" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="765">
                            <_items dataType="Array" type="System.Int32[]" id="766" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="767">
                            <_items dataType="ObjectRef">694</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="768" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="769" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="770" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="771">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="772" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="773">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="774">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="775" length="16">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="776">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="777" length="5">
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
                                    <parent dataType="ObjectRef">773</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="778">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="779" length="5">
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
                                    <parent dataType="ObjectRef">773</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="780">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="781" length="5">
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
                                    <parent dataType="ObjectRef">773</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="782">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="783" length="5">
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
                                    <parent dataType="ObjectRef">773</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="784">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="785" length="5">
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
                                    <parent dataType="ObjectRef">773</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="786">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="787" length="4">
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
                                    <parent dataType="ObjectRef">773</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="788">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="789" length="4">
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
                                    <parent dataType="ObjectRef">773</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="790">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="791" length="4">
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
                                    <parent dataType="ObjectRef">773</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="792">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="793" length="4">
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
                                    <parent dataType="ObjectRef">773</parent>
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
                              <gameobj dataType="ObjectRef">761</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="794" length="1">
                              <object dataType="ObjectRef">772</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">761</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="795">
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
                          <gameobj dataType="ObjectRef">761</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">773</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="796">
                    <_items dataType="Array" type="Duality.Component[]" id="797" length="4">
                      <object dataType="ObjectRef">771</object>
                      <object dataType="ObjectRef">795</object>
                      <object dataType="ObjectRef">773</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Complex</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">771</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="798">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="799">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">798</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="800">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="801" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="802">
                            <_items dataType="Array" type="System.Int32[]" id="803" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="804">
                            <_items dataType="ObjectRef">694</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="805" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="806" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="807" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="808">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="809" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="810">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="811">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="812" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="813">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">810</parent>
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
                              <gameobj dataType="ObjectRef">798</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="814" length="1">
                              <object dataType="ObjectRef">809</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">798</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="815">
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
                          <gameobj dataType="ObjectRef">798</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">810</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="816">
                    <_items dataType="Array" type="Duality.Component[]" id="817" length="4">
                      <object dataType="ObjectRef">808</object>
                      <object dataType="ObjectRef">815</object>
                      <object dataType="ObjectRef">810</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">808</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="818">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="819">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">818</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="820">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="821" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="822">
                            <_items dataType="Array" type="System.Int32[]" id="823" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="824">
                            <_items dataType="ObjectRef">694</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="825" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="826" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="827" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="828">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="829" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="830">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="831">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="832" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="833">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">830</parent>
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
                              <gameobj dataType="ObjectRef">818</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="834" length="1">
                              <object dataType="ObjectRef">829</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">818</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="835">
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
                          <gameobj dataType="ObjectRef">818</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">830</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="836">
                    <_items dataType="Array" type="Duality.Component[]" id="837" length="4">
                      <object dataType="ObjectRef">828</object>
                      <object dataType="ObjectRef">835</object>
                      <object dataType="ObjectRef">830</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">828</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="838">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="839">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">838</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="840">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="841" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="842">
                            <_items dataType="Array" type="System.Int32[]" id="843" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="844">
                            <_items dataType="ObjectRef">694</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="845" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="846" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="847" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="848">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="849" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="850">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="851">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="852" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="853">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">850</parent>
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
                              <gameobj dataType="ObjectRef">838</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="854" length="1">
                              <object dataType="ObjectRef">849</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">838</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="855">
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
                          <gameobj dataType="ObjectRef">838</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">850</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="856">
                    <_items dataType="Array" type="Duality.Component[]" id="857" length="4">
                      <object dataType="ObjectRef">848</object>
                      <object dataType="ObjectRef">855</object>
                      <object dataType="ObjectRef">850</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">848</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="858">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="859">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">858</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="860">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="861" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="862">
                            <_items dataType="Array" type="System.Int32[]" id="863" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="864">
                            <_items dataType="ObjectRef">694</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="865" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="866" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="867" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="868">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="869" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="870">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="871">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="872" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="873">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">870</parent>
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
                              <gameobj dataType="ObjectRef">858</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="874" length="1">
                              <object dataType="ObjectRef">869</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">858</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="875">
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
                          <gameobj dataType="ObjectRef">858</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">870</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="876">
                    <_items dataType="Array" type="Duality.Component[]" id="877" length="4">
                      <object dataType="ObjectRef">868</object>
                      <object dataType="ObjectRef">875</object>
                      <object dataType="ObjectRef">870</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">868</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="878">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="879">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">878</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="880">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="881" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="882">
                            <_items dataType="Array" type="System.Int32[]" id="883" length="0" />
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
                          <prop dataType="PropertyInfo" id="884" value="P:Duality.Components.Transform:RelativeAngle" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="885">
                            <_items dataType="ObjectRef">694</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">2.58536029</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="886">
                            <_items dataType="ObjectRef">694</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="887" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="888" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="889" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="890">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="891" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="892">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="893">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="894" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="895">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">892</parent>
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
                              <gameobj dataType="ObjectRef">878</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="896" length="1">
                              <object dataType="ObjectRef">891</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">878</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="897">
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
                          <gameobj dataType="ObjectRef">878</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">892</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="898">
                    <_items dataType="Array" type="Duality.Component[]" id="899" length="4">
                      <object dataType="ObjectRef">890</object>
                      <object dataType="ObjectRef">897</object>
                      <object dataType="ObjectRef">892</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">890</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="900">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="901">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">900</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="902">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="903" length="4">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="904">
                            <_items dataType="Array" type="System.Int32[]" id="905" length="0" />
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
                          <prop dataType="ObjectRef">884</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="906">
                            <_items dataType="ObjectRef">694</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">2.58536029</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="907">
                            <_items dataType="ObjectRef">694</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="908" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="909" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="910" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="911">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="912" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="913">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="914">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="915" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="916">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">913</parent>
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
                              <gameobj dataType="ObjectRef">900</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="917" length="1">
                              <object dataType="ObjectRef">912</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">900</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="918">
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
                          <gameobj dataType="ObjectRef">900</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">913</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="919">
                    <_items dataType="Array" type="Duality.Component[]" id="920" length="4">
                      <object dataType="ObjectRef">911</object>
                      <object dataType="ObjectRef">918</object>
                      <object dataType="ObjectRef">913</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">911</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="921">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="922">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">921</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="923">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="924" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="925">
                            <_items dataType="Array" type="System.Int32[]" id="926" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="927">
                            <_items dataType="Array" type="System.Int32[]" id="928" length="0" />
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="929" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="930" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="931" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="932">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="933" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="934">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="935">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="936" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="937">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">934</parent>
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
                              <gameobj dataType="ObjectRef">921</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="938" length="1">
                              <object dataType="ObjectRef">933</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">921</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="939">
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
                          <gameobj dataType="ObjectRef">921</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">934</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="940">
                    <_items dataType="Array" type="Duality.Component[]" id="941" length="4">
                      <object dataType="ObjectRef">932</object>
                      <object dataType="ObjectRef">939</object>
                      <object dataType="ObjectRef">934</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">932</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="942">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="943">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">942</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="944">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="945" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="946">
                            <_items dataType="Array" type="System.Int32[]" id="947" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="948">
                            <_items dataType="ObjectRef">928</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="949" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="950" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="951" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="952">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="953" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="954">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="955">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="956" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="957">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">954</parent>
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
                              <gameobj dataType="ObjectRef">942</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="958" length="1">
                              <object dataType="ObjectRef">953</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">942</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="959">
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
                          <gameobj dataType="ObjectRef">942</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">954</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="960">
                    <_items dataType="Array" type="Duality.Component[]" id="961" length="4">
                      <object dataType="ObjectRef">952</object>
                      <object dataType="ObjectRef">959</object>
                      <object dataType="ObjectRef">954</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">952</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="962">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="963">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">962</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="964">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="965" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="966">
                            <_items dataType="Array" type="System.Int32[]" id="967" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="968">
                            <_items dataType="ObjectRef">928</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="969" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="970" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="971" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="972">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="973" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="974">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="975">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="976" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="977">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">974</parent>
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
                              <gameobj dataType="ObjectRef">962</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="978" length="1">
                              <object dataType="ObjectRef">973</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">962</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="979">
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
                          <gameobj dataType="ObjectRef">962</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">974</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="980">
                    <_items dataType="Array" type="Duality.Component[]" id="981" length="4">
                      <object dataType="ObjectRef">972</object>
                      <object dataType="ObjectRef">979</object>
                      <object dataType="ObjectRef">974</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">972</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="982">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="983">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">982</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="984">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="985" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="986">
                            <_items dataType="Array" type="System.Int32[]" id="987" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="988">
                            <_items dataType="ObjectRef">928</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="989" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="990" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="991" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="992">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="993" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="994">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="995">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="996" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="997">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">994</parent>
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
                              <gameobj dataType="ObjectRef">982</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="998" length="1">
                              <object dataType="ObjectRef">993</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">982</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="999">
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
                          <gameobj dataType="ObjectRef">982</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">994</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1000">
                    <_items dataType="Array" type="Duality.Component[]" id="1001" length="4">
                      <object dataType="ObjectRef">992</object>
                      <object dataType="ObjectRef">999</object>
                      <object dataType="ObjectRef">994</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">992</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1002">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1003">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1002</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1004">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1005" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1006">
                            <_items dataType="Array" type="System.Int32[]" id="1007" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1008">
                            <_items dataType="ObjectRef">928</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1009" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1010" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1011" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1012">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1013" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1014">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1015">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1016" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1017">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1014</parent>
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
                              <gameobj dataType="ObjectRef">1002</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1018" length="1">
                              <object dataType="ObjectRef">1013</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1002</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1019">
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
                          <gameobj dataType="ObjectRef">1002</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1014</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1020">
                    <_items dataType="Array" type="Duality.Component[]" id="1021" length="4">
                      <object dataType="ObjectRef">1012</object>
                      <object dataType="ObjectRef">1019</object>
                      <object dataType="ObjectRef">1014</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1012</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1022">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1023">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1022</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1024">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1025" length="3">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1026">
                            <_items dataType="Array" type="System.Int32[]" id="1027" length="0" />
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
                          <prop dataType="ObjectRef">884</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1028">
                            <_items dataType="Array" type="System.Int32[]" id="1029" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Float">2.58536029</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1030">
                            <_items dataType="ObjectRef">928</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1031" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1032" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1033" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1034">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1035" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1036">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1037">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1038" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1039">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1036</parent>
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
                              <gameobj dataType="ObjectRef">1022</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1040" length="1">
                              <object dataType="ObjectRef">1035</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1022</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1041">
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
                          <gameobj dataType="ObjectRef">1022</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1036</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1042">
                    <_items dataType="Array" type="Duality.Component[]" id="1043" length="4">
                      <object dataType="ObjectRef">1034</object>
                      <object dataType="ObjectRef">1041</object>
                      <object dataType="ObjectRef">1036</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1034</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1044">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1045">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Circle.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1044</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1046">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1047" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1048">
                            <_items dataType="Array" type="System.Int32[]" id="1049" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1050">
                            <_items dataType="ObjectRef">928</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1051" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1052" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1053" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1054">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1055" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1056">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1057">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1058" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1059">
                                    <radius dataType="Float">126</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">0</X>
                                      <Y dataType="Float">0</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1056</parent>
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
                              <gameobj dataType="ObjectRef">1044</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1060" length="1">
                              <object dataType="ObjectRef">1055</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1044</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1061">
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
                          <gameobj dataType="ObjectRef">1044</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1056</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1062">
                    <_items dataType="Array" type="Duality.Component[]" id="1063" length="4">
                      <object dataType="ObjectRef">1054</object>
                      <object dataType="ObjectRef">1061</object>
                      <object dataType="ObjectRef">1056</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Circle</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1054</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1064">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1065">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1064</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1066">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1067" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1068">
                            <_items dataType="Array" type="System.Int32[]" id="1069" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1070">
                            <_items dataType="ObjectRef">928</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1071" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1072" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1073" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1074">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1075" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1076">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1077">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1078" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="1079">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="1080" length="4">
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
                                    <parent dataType="ObjectRef">1076</parent>
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
                              <gameobj dataType="ObjectRef">1064</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1081" length="1">
                              <object dataType="ObjectRef">1075</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1064</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1082">
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
                          <gameobj dataType="ObjectRef">1064</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1076</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1083">
                    <_items dataType="Array" type="Duality.Component[]" id="1084" length="4">
                      <object dataType="ObjectRef">1074</object>
                      <object dataType="ObjectRef">1082</object>
                      <object dataType="ObjectRef">1076</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1074</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1085">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1086">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1085</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1087">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1088" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1089">
                            <_items dataType="Array" type="System.Int32[]" id="1090" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1091">
                            <_items dataType="ObjectRef">928</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1092" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1093" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1094" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1095">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1096" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1097">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1098">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1099" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="1100">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="1101" length="4">
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
                                    <parent dataType="ObjectRef">1097</parent>
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
                              <gameobj dataType="ObjectRef">1085</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1102" length="1">
                              <object dataType="ObjectRef">1096</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1085</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1103">
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
                          <gameobj dataType="ObjectRef">1085</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1097</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1104">
                    <_items dataType="Array" type="Duality.Component[]" id="1105" length="4">
                      <object dataType="ObjectRef">1095</object>
                      <object dataType="ObjectRef">1103</object>
                      <object dataType="ObjectRef">1097</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1095</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1106">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1107">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1106</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1108">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1109" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1110">
                            <_items dataType="Array" type="System.Int32[]" id="1111" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1112">
                            <_items dataType="ObjectRef">928</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1113" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1114" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1115" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1116">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1117" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1118">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1119">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1120" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="1121">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="1122" length="4">
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
                                    <parent dataType="ObjectRef">1118</parent>
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
                              <gameobj dataType="ObjectRef">1106</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1123" length="1">
                              <object dataType="ObjectRef">1117</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1106</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1124">
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
                          <gameobj dataType="ObjectRef">1106</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1118</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1125">
                    <_items dataType="Array" type="Duality.Component[]" id="1126" length="4">
                      <object dataType="ObjectRef">1116</object>
                      <object dataType="ObjectRef">1124</object>
                      <object dataType="ObjectRef">1118</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1116</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1127">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1128">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1127</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1129">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1130" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1131">
                            <_items dataType="Array" type="System.Int32[]" id="1132" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1133">
                            <_items dataType="Array" type="System.Int32[]" id="1134" length="0" />
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1135" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1136" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1137" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1138">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1139" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1140">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1141">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1142" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="1143">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="1144" length="4">
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
                                    <parent dataType="ObjectRef">1140</parent>
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
                              <gameobj dataType="ObjectRef">1127</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1145" length="1">
                              <object dataType="ObjectRef">1139</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1127</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1146">
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
                          <gameobj dataType="ObjectRef">1127</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1140</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1147">
                    <_items dataType="Array" type="Duality.Component[]" id="1148" length="4">
                      <object dataType="ObjectRef">1138</object>
                      <object dataType="ObjectRef">1146</object>
                      <object dataType="ObjectRef">1140</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">Square</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1138</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1149">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1150">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\RoundSquare.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1149</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1151">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1152" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1153">
                            <_items dataType="Array" type="System.Int32[]" id="1154" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1155">
                            <_items dataType="ObjectRef">928</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1156" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1157" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1158" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1159">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1160" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1161">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1162">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1163" length="8">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1164">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1161</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1165">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1161</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1166">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1161</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1167">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1161</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="1168">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="1169" length="8">
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
                                    <parent dataType="ObjectRef">1161</parent>
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
                              <gameobj dataType="ObjectRef">1149</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1170" length="1">
                              <object dataType="ObjectRef">1160</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1149</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1171">
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
                          <gameobj dataType="ObjectRef">1149</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1161</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1172">
                    <_items dataType="Array" type="Duality.Component[]" id="1173" length="4">
                      <object dataType="ObjectRef">1159</object>
                      <object dataType="ObjectRef">1171</object>
                      <object dataType="ObjectRef">1161</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">RoundSquare</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1159</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="1174">
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="1175">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\RoundSquare.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">1174</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="1176">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="1177" length="2">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">435</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1178">
                            <_items dataType="Array" type="System.Int32[]" id="1179" length="0" />
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
                          <prop dataType="ObjectRef">432</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="1180">
                            <_items dataType="ObjectRef">928</_items>
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
                  <parent dataType="ObjectRef">425</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1181" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="1182" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="1183" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="1184">
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
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="1185" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="1186">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="1187">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="1188" length="8">
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1189">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1186</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1190">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">-75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1186</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1191">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1186</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.CircleShapeInfo" id="1192">
                                    <radius dataType="Float">50</radius>
                                    <position dataType="Struct" type="OpenTK.Vector2">
                                      <X dataType="Float">-75</X>
                                      <Y dataType="Float">75</Y>
                                    </position>
                                    <parent dataType="ObjectRef">1186</parent>
                                    <density dataType="Float">0.7</density>
                                    <friction dataType="Float">0.3</friction>
                                    <restitution dataType="Float">0.3</restitution>
                                    <sensor dataType="Bool">false</sensor>
                                  </object>
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="1193">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="1194" length="8">
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
                                    <parent dataType="ObjectRef">1186</parent>
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
                              <gameobj dataType="ObjectRef">1174</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="1195" length="1">
                              <object dataType="ObjectRef">1185</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">1174</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="1196">
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
                          <gameobj dataType="ObjectRef">1174</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">1186</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1197">
                    <_items dataType="Array" type="Duality.Component[]" id="1198" length="4">
                      <object dataType="ObjectRef">1184</object>
                      <object dataType="ObjectRef">1196</object>
                      <object dataType="ObjectRef">1186</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <name dataType="String">RoundSquare</name>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">1184</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
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
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1199" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="1200" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="1201" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1202">
              <_items dataType="ObjectRef">381</_items>
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <name dataType="String">Dynamics</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1203" multi="true">
              <object dataType="ObjectRef">383</object>
              <object dataType="ObjectRef">1184</object>
              <object dataType="Array" type="System.Delegate[]" id="1204" length="35">
                <object dataType="ObjectRef">61</object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1205" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">440</object>
                  <object dataType="Array" type="System.Delegate[]" id="1206" length="1">
                    <object dataType="ObjectRef">1205</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1207" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">459</object>
                  <object dataType="Array" type="System.Delegate[]" id="1208" length="1">
                    <object dataType="ObjectRef">1207</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1209" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">478</object>
                  <object dataType="Array" type="System.Delegate[]" id="1210" length="1">
                    <object dataType="ObjectRef">1209</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1211" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">497</object>
                  <object dataType="Array" type="System.Delegate[]" id="1212" length="1">
                    <object dataType="ObjectRef">1211</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1213" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">516</object>
                  <object dataType="Array" type="System.Delegate[]" id="1214" length="1">
                    <object dataType="ObjectRef">1213</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1215" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">540</object>
                  <object dataType="Array" type="System.Delegate[]" id="1216" length="1">
                    <object dataType="ObjectRef">1215</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1217" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">564</object>
                  <object dataType="Array" type="System.Delegate[]" id="1218" length="1">
                    <object dataType="ObjectRef">1217</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1219" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">584</object>
                  <object dataType="Array" type="System.Delegate[]" id="1220" length="1">
                    <object dataType="ObjectRef">1219</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1221" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">604</object>
                  <object dataType="Array" type="System.Delegate[]" id="1222" length="1">
                    <object dataType="ObjectRef">1221</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1223" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">624</object>
                  <object dataType="Array" type="System.Delegate[]" id="1224" length="1">
                    <object dataType="ObjectRef">1223</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1225" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">660</object>
                  <object dataType="Array" type="System.Delegate[]" id="1226" length="1">
                    <object dataType="ObjectRef">1225</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1227" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">698</object>
                  <object dataType="Array" type="System.Delegate[]" id="1228" length="1">
                    <object dataType="ObjectRef">1227</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1229" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">718</object>
                  <object dataType="Array" type="System.Delegate[]" id="1230" length="1">
                    <object dataType="ObjectRef">1229</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1231" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">755</object>
                  <object dataType="Array" type="System.Delegate[]" id="1232" length="1">
                    <object dataType="ObjectRef">1231</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1233" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">771</object>
                  <object dataType="Array" type="System.Delegate[]" id="1234" length="1">
                    <object dataType="ObjectRef">1233</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1235" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">808</object>
                  <object dataType="Array" type="System.Delegate[]" id="1236" length="1">
                    <object dataType="ObjectRef">1235</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1237" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">828</object>
                  <object dataType="Array" type="System.Delegate[]" id="1238" length="1">
                    <object dataType="ObjectRef">1237</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1239" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">848</object>
                  <object dataType="Array" type="System.Delegate[]" id="1240" length="1">
                    <object dataType="ObjectRef">1239</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1241" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">868</object>
                  <object dataType="Array" type="System.Delegate[]" id="1242" length="1">
                    <object dataType="ObjectRef">1241</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1243" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">890</object>
                  <object dataType="Array" type="System.Delegate[]" id="1244" length="1">
                    <object dataType="ObjectRef">1243</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1245" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">911</object>
                  <object dataType="Array" type="System.Delegate[]" id="1246" length="1">
                    <object dataType="ObjectRef">1245</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1247" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">932</object>
                  <object dataType="Array" type="System.Delegate[]" id="1248" length="1">
                    <object dataType="ObjectRef">1247</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1249" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">952</object>
                  <object dataType="Array" type="System.Delegate[]" id="1250" length="1">
                    <object dataType="ObjectRef">1249</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1251" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">972</object>
                  <object dataType="Array" type="System.Delegate[]" id="1252" length="1">
                    <object dataType="ObjectRef">1251</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1253" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">992</object>
                  <object dataType="Array" type="System.Delegate[]" id="1254" length="1">
                    <object dataType="ObjectRef">1253</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1255" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">1012</object>
                  <object dataType="Array" type="System.Delegate[]" id="1256" length="1">
                    <object dataType="ObjectRef">1255</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1257" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">1034</object>
                  <object dataType="Array" type="System.Delegate[]" id="1258" length="1">
                    <object dataType="ObjectRef">1257</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1259" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">1054</object>
                  <object dataType="Array" type="System.Delegate[]" id="1260" length="1">
                    <object dataType="ObjectRef">1259</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1261" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">1074</object>
                  <object dataType="Array" type="System.Delegate[]" id="1262" length="1">
                    <object dataType="ObjectRef">1261</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1263" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">1095</object>
                  <object dataType="Array" type="System.Delegate[]" id="1264" length="1">
                    <object dataType="ObjectRef">1263</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1265" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">1116</object>
                  <object dataType="Array" type="System.Delegate[]" id="1266" length="1">
                    <object dataType="ObjectRef">1265</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1267" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">1138</object>
                  <object dataType="Array" type="System.Delegate[]" id="1268" length="1">
                    <object dataType="ObjectRef">1267</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1269" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">1159</object>
                  <object dataType="Array" type="System.Delegate[]" id="1270" length="1">
                    <object dataType="ObjectRef">1269</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1271" multi="true">
                  <object dataType="ObjectRef">383</object>
                  <object dataType="ObjectRef">1184</object>
                  <object dataType="Array" type="System.Delegate[]" id="1272" length="1">
                    <object dataType="ObjectRef">1271</object>
                  </object>
                </object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="ObjectRef">70</object>
          <object dataType="ObjectRef">90</object>
          <object dataType="ObjectRef">106</object>
          <object dataType="ObjectRef">122</object>
          <object dataType="ObjectRef">137</object>
          <object dataType="ObjectRef">152</object>
          <object dataType="ObjectRef">167</object>
          <object dataType="ObjectRef">182</object>
          <object dataType="ObjectRef">197</object>
          <object dataType="ObjectRef">212</object>
          <object dataType="ObjectRef">227</object>
          <object dataType="ObjectRef">242</object>
          <object dataType="ObjectRef">257</object>
          <object dataType="ObjectRef">272</object>
          <object dataType="ObjectRef">287</object>
          <object dataType="ObjectRef">302</object>
          <object dataType="Class" type="Duality.GameObject" id="1273">
            <prefabLink />
            <parent dataType="Class" type="Duality.GameObject" id="1274">
              <prefabLink />
              <parent />
              <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="1275">
                <_items dataType="Array" type="Duality.GameObject[]" id="1276" length="4">
                  <object dataType="ObjectRef">1273</object>
                  <object dataType="Class" type="Duality.GameObject" id="1277">
                    <prefabLink />
                    <parent dataType="ObjectRef">1274</parent>
                    <children />
                    <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1278" surrogate="true">
                      <customSerialIO />
                      <customSerialIO>
                        <keys dataType="Array" type="System.Type[]" id="1279" length="2">
                          <object dataType="ObjectRef">14</object>
                          <object dataType="Type" id="1280" value="Duality.Components.Renderers.TextRenderer" />
                        </keys>
                        <values dataType="Array" type="Duality.Component[]" id="1281" length="2">
                          <object dataType="Class" type="Duality.Components.Transform" id="1282">
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
                            <gameobj dataType="ObjectRef">1277</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </object>
                          <object dataType="Class" type="Duality.Components.Renderers.TextRenderer" id="1283">
                            <align dataType="Enum" type="Duality.Alignment" name="Center" value="0" />
                            <text dataType="Class" type="Duality.FormattedText" id="1284">
                              <sourceText dataType="String">Ice</sourceText>
                              <icons />
                              <flowAreas />
                              <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="1285" length="1">
                                <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                                  <contentPath dataType="String">Data\BigFont.Font.res</contentPath>
                                </object>
                              </fonts>
                              <maxWidth dataType="Int">0</maxWidth>
                              <maxHeight dataType="Int">0</maxHeight>
                              <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                              <displayedText dataType="String">Ice</displayedText>
                              <fontGlyphCount dataType="Array" type="System.Int32[]" id="1286" length="1">
                                <object dataType="Int">3</object>
                              </fontGlyphCount>
                              <iconCount dataType="Int">0</iconCount>
                              <elements dataType="Array" type="Duality.FormattedText+Element[]" id="1287" length="1">
                                <object dataType="Class" type="Duality.FormattedText+TextElement" id="1288">
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
                            <gameobj dataType="ObjectRef">1277</gameobj>
                            <disposed dataType="Bool">false</disposed>
                            <active dataType="Bool">true</active>
                          </object>
                        </values>
                      </customSerialIO>
                    </compMap>
                    <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1289">
                      <_items dataType="Array" type="Duality.Component[]" id="1290" length="4">
                        <object dataType="ObjectRef">1282</object>
                        <object dataType="ObjectRef">1283</object>
                        <object />
                        <object />
                      </_items>
                      <_size dataType="Int">2</_size>
                      <_version dataType="Int">2</_version>
                    </compList>
                    <name dataType="String">Rubber</name>
                    <active dataType="Bool">true</active>
                    <disposed dataType="Bool">false</disposed>
                    <compTransform dataType="ObjectRef">1282</compTransform>
                    <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                    <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
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
              <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1291" surrogate="true">
                <customSerialIO />
                <customSerialIO>
                  <keys dataType="Array" type="System.Type[]" id="1292" length="0" />
                  <values dataType="Array" type="Duality.Component[]" id="1293" length="0" />
                </customSerialIO>
              </compMap>
              <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1294">
                <_items dataType="Array" type="Duality.Component[]" id="1295" length="0" />
                <_size dataType="Int">0</_size>
                <_version dataType="Int">0</_version>
              </compList>
              <name dataType="String">Text</name>
              <active dataType="Bool">true</active>
              <disposed dataType="Bool">false</disposed>
              <compTransform />
              <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1296" multi="true">
                <object dataType="ObjectRef">383</object>
                <object dataType="ObjectRef">1282</object>
                <object dataType="Array" type="System.Delegate[]" id="1297" length="3">
                  <object dataType="ObjectRef">61</object>
                  <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1298" multi="true">
                    <object dataType="ObjectRef">383</object>
                    <object dataType="Class" type="Duality.Components.Transform" id="1299">
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
                      <gameobj dataType="ObjectRef">1273</gameobj>
                      <disposed dataType="Bool">false</disposed>
                      <active dataType="Bool">true</active>
                    </object>
                    <object dataType="Array" type="System.Delegate[]" id="1300" length="1">
                      <object dataType="ObjectRef">1298</object>
                    </object>
                  </object>
                  <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="1301" multi="true">
                    <object dataType="ObjectRef">383</object>
                    <object dataType="ObjectRef">1282</object>
                    <object dataType="Array" type="System.Delegate[]" id="1302" length="1">
                      <object dataType="ObjectRef">1301</object>
                    </object>
                  </object>
                </object>
              </EventComponentAdded>
              <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
              <EventCollisionBegin />
              <EventCollisionEnd />
              <EventCollisionSolve />
            </parent>
            <children />
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="1303" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="1304" length="2">
                  <object dataType="ObjectRef">14</object>
                  <object dataType="ObjectRef">1280</object>
                </keys>
                <values dataType="Array" type="Duality.Component[]" id="1305" length="2">
                  <object dataType="ObjectRef">1299</object>
                  <object dataType="Class" type="Duality.Components.Renderers.TextRenderer" id="1306">
                    <align dataType="Enum" type="Duality.Alignment" name="Center" value="0" />
                    <text dataType="Class" type="Duality.FormattedText" id="1307">
                      <sourceText dataType="String">Rubber</sourceText>
                      <icons />
                      <flowAreas />
                      <fonts dataType="Array" type="Duality.ContentRef`1[[Duality.Resources.Font]][]" id="1308" length="1">
                        <object dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Font]]">
                          <contentPath dataType="String">Data\BigFont.Font.res</contentPath>
                        </object>
                      </fonts>
                      <maxWidth dataType="Int">0</maxWidth>
                      <maxHeight dataType="Int">0</maxHeight>
                      <wrapMode dataType="Enum" type="Duality.FormattedText+WrapMode" name="Word" value="1" />
                      <displayedText dataType="String">Rubber</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="1309" length="1">
                        <object dataType="Int">6</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="1310" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="1311">
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
                    <gameobj dataType="ObjectRef">1273</gameobj>
                    <disposed dataType="Bool">false</disposed>
                    <active dataType="Bool">true</active>
                  </object>
                </values>
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="1312">
              <_items dataType="Array" type="Duality.Component[]" id="1313" length="4">
                <object dataType="ObjectRef">1299</object>
                <object dataType="ObjectRef">1306</object>
                <object />
                <object />
              </_items>
              <_size dataType="Int">2</_size>
              <_version dataType="Int">2</_version>
            </compList>
            <name dataType="String">Rubber</name>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform dataType="ObjectRef">1299</compTransform>
            <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="ObjectRef">1274</object>
          <object dataType="ObjectRef">1277</object>
          <object dataType="ObjectRef">428</object>
          <object dataType="ObjectRef">450</object>
          <object dataType="ObjectRef">469</object>
          <object dataType="ObjectRef">488</object>
          <object dataType="ObjectRef">507</object>
          <object dataType="ObjectRef">531</object>
          <object dataType="ObjectRef">555</object>
          <object dataType="ObjectRef">575</object>
          <object dataType="ObjectRef">595</object>
          <object dataType="ObjectRef">615</object>
          <object dataType="ObjectRef">651</object>
          <object dataType="ObjectRef">687</object>
          <object dataType="ObjectRef">708</object>
          <object dataType="ObjectRef">733</object>
          <object dataType="ObjectRef">761</object>
          <object dataType="ObjectRef">798</object>
          <object dataType="ObjectRef">818</object>
          <object dataType="ObjectRef">838</object>
          <object dataType="ObjectRef">858</object>
          <object dataType="ObjectRef">878</object>
          <object dataType="ObjectRef">900</object>
          <object dataType="ObjectRef">921</object>
          <object dataType="ObjectRef">942</object>
          <object dataType="ObjectRef">962</object>
          <object dataType="ObjectRef">982</object>
          <object dataType="ObjectRef">1002</object>
          <object dataType="ObjectRef">1022</object>
          <object dataType="ObjectRef">1044</object>
          <object dataType="ObjectRef">1064</object>
          <object dataType="ObjectRef">1085</object>
          <object dataType="ObjectRef">1106</object>
          <object dataType="ObjectRef">1127</object>
          <object dataType="ObjectRef">1149</object>
          <object dataType="ObjectRef">1174</object>
          <object dataType="ObjectRef">317</object>
          <object dataType="ObjectRef">332</object>
          <object dataType="ObjectRef">347</object>
          <object dataType="ObjectRef">362</object>
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
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
        <_version dataType="Int">160</_version>
      </allObj>
      <Registered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="1314" multi="true">
        <object dataType="MethodInfo" id="1315" value="M:Duality.Resources.Scene:objectManager_Registered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="1316" length="1">
          <object dataType="ObjectRef">1314</object>
        </object>
      </Registered>
      <Unregistered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="1317" multi="true">
        <object dataType="MethodInfo" id="1318" value="M:Duality.Resources.Scene:objectManager_Unregistered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="1319" length="1">
          <object dataType="ObjectRef">1317</object>
        </object>
      </Unregistered>
    </objectManager>
    <sourcePath />
  </object>
</root>