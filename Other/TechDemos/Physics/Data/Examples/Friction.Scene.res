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
            <name dataType="String">MainCam</name>
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
                      <sourceText dataType="String">Example: Friction</sourceText>
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
                      <displayedText dataType="String">Example: Friction</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="29" length="1">
                        <object dataType="Int">17</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="30" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="31">
                          <text dataType="String">Example: Friction</text>
                        </object>
                      </elements>
                    </name>
                    <desc dataType="Class" type="Duality.FormattedText" id="32">
                      <sourceText dataType="String">Each box has a different friction value.</sourceText>
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
                      <displayedText dataType="String">Each box has a different friction value.</displayedText>
                      <fontGlyphCount dataType="Array" type="System.Int32[]" id="34" length="1">
                        <object dataType="Int">40</object>
                      </fontGlyphCount>
                      <iconCount dataType="Int">0</iconCount>
                      <elements dataType="Array" type="Duality.FormattedText+Element[]" id="35" length="1">
                        <object dataType="Class" type="Duality.FormattedText+TextElement" id="36">
                          <text dataType="String">Each box has a different friction value.</text>
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
            <name dataType="String">StaticWorld</name>
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="68">
              <_items dataType="Array" type="Duality.GameObject[]" id="69" length="32">
                <object dataType="Class" type="Duality.GameObject" id="70">
                  <name dataType="String">Wall</name>
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
                            <X dataType="Float">168.803085</X>
                            <Y dataType="Float">116.638214</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.97585058</angle>
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
                            <X dataType="Float">168.803085</X>
                            <Y dataType="Float">116.638214</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.97585058</angleAbs>
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
                            <X dataType="Float">168.803085</X>
                            <Y dataType="Float">116.638214</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">168.803085</X>
                            <Y dataType="Float">116.638214</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">-0.3073349</lastAngle>
                          <lastAngleAbs dataType="Float">-0.3073349</lastAngleAbs>
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
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
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
                                <_version dataType="Int">1</_version>
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
                            <X dataType="Float">-512</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">1024</W>
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
                    <_version dataType="Int">3</_version>
                  </compList>
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
                  <name dataType="String">Wall</name>
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
                            <X dataType="Float">-132.477585</X>
                            <Y dataType="Float">-56.0604744</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0.3543018</angle>
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
                            <X dataType="Float">-132.477585</X>
                            <Y dataType="Float">-56.0604744</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0.3543018</angleAbs>
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
                            <X dataType="Float">-132.477585</X>
                            <Y dataType="Float">-56.0604744</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-132.477585</X>
                            <Y dataType="Float">-56.0604744</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0.3543018</lastAngle>
                          <lastAngleAbs dataType="Float">0.3543018</lastAngleAbs>
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
                              <revolutions dataType="Float">0</revolutions>
                              <explicitMass dataType="Float">0</explicitMass>
                              <colCat dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="Cat1" value="1" />
                              <colWith dataType="Enum" type="FarseerPhysics.Dynamics.Category" name="All" value="2147483647" />
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="97">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="98" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="99">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="100" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">96</parent>
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
                            <X dataType="Float">-512</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">1024</W>
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
                          <gameobj dataType="ObjectRef">90</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">96</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="103">
                    <_items dataType="Array" type="Duality.Component[]" id="104" length="4">
                      <object dataType="ObjectRef">94</object>
                      <object dataType="ObjectRef">102</object>
                      <object dataType="ObjectRef">96</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">94</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="105">
                  <name dataType="String">Wall</name>
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="106" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="107" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="108" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="109">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">11.7875519</X>
                            <Y dataType="Float">305.5487</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1.30155718</X>
                            <Y dataType="Float">1.30155718</Y>
                            <Z dataType="Float">1.30155718</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">11.7875519</X>
                            <Y dataType="Float">305.5487</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1.30155718</X>
                            <Y dataType="Float">1.30155718</Y>
                            <Z dataType="Float">1.30155718</Z>
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
                            <X dataType="Float">11.7875519</X>
                            <Y dataType="Float">305.5487</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">11.7875519</X>
                            <Y dataType="Float">305.5487</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">-6.28318548</lastAngle>
                          <lastAngleAbs dataType="Float">-6.28318548</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="110" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="111">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="112">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="113" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="114">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="115" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">-16</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">-16</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">16</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">16</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">111</parent>
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
                              <gameobj dataType="ObjectRef">105</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="116" length="1">
                              <object dataType="ObjectRef">110</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">105</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="117">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-512</X>
                            <Y dataType="Float">-16</Y>
                            <W dataType="Float">1024</W>
                            <H dataType="Float">32</H>
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
                          <gameobj dataType="ObjectRef">105</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">111</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="118">
                    <_items dataType="Array" type="Duality.Component[]" id="119" length="4">
                      <object dataType="ObjectRef">109</object>
                      <object dataType="ObjectRef">117</object>
                      <object dataType="ObjectRef">111</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">109</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="120">
                  <name dataType="String">Wall</name>
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="121" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="122" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="123" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="124">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">767.4298</X>
                            <Y dataType="Float">148.532211</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.31018829</angle>
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
                            <X dataType="Float">767.4298</X>
                            <Y dataType="Float">148.532211</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.31018829</angleAbs>
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
                            <X dataType="Float">767.4298</X>
                            <Y dataType="Float">148.532211</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">767.4298</X>
                            <Y dataType="Float">148.532211</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">-0.9729972</lastAngle>
                          <lastAngleAbs dataType="Float">-0.9729972</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="125" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="126">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="127">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="128" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="129">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="130" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">126</parent>
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
                              <gameobj dataType="ObjectRef">120</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="131" length="1">
                              <object dataType="ObjectRef">125</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">120</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="132">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-512</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">1024</W>
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
                          <gameobj dataType="ObjectRef">120</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">126</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="133">
                    <_items dataType="Array" type="Duality.Component[]" id="134" length="4">
                      <object dataType="ObjectRef">124</object>
                      <object dataType="ObjectRef">132</object>
                      <object dataType="ObjectRef">126</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">124</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="135">
                  <name dataType="String">Wall</name>
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="136" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="137" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="138" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="139">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-711.959656</X>
                            <Y dataType="Float">128.905136</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">4.266198</angle>
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
                            <X dataType="Float">-711.959656</X>
                            <Y dataType="Float">128.905136</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">4.266198</angleAbs>
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
                            <X dataType="Float">-711.959656</X>
                            <Y dataType="Float">128.905136</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-711.959656</X>
                            <Y dataType="Float">128.905136</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">-2.01698732</lastAngle>
                          <lastAngleAbs dataType="Float">-2.01698732</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="140" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="141">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="142">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="143" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="144">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="145" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">141</parent>
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
                              <gameobj dataType="ObjectRef">135</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="146" length="1">
                              <object dataType="ObjectRef">140</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">135</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="147">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-512</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">1024</W>
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
                          <gameobj dataType="ObjectRef">135</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">141</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="148">
                    <_items dataType="Array" type="Duality.Component[]" id="149" length="4">
                      <object dataType="ObjectRef">139</object>
                      <object dataType="ObjectRef">147</object>
                      <object dataType="ObjectRef">141</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">139</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="150">
                  <name dataType="String">Wall</name>
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="151" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="152" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="153" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="154">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-514.303162</X>
                            <Y dataType="Float">240.564758</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0.36995554</angle>
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
                            <X dataType="Float">-514.303162</X>
                            <Y dataType="Float">240.564758</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0.36995554</angleAbs>
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
                            <X dataType="Float">-514.303162</X>
                            <Y dataType="Float">240.564758</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-514.303162</X>
                            <Y dataType="Float">240.564758</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0.36995554</lastAngle>
                          <lastAngleAbs dataType="Float">0.36995554</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="155" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="156">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="157">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="158" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="159">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="160" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">156</parent>
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
                              <gameobj dataType="ObjectRef">150</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="161" length="1">
                              <object dataType="ObjectRef">155</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">150</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="162">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-512</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">1024</W>
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
                          <gameobj dataType="ObjectRef">150</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">156</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="163">
                    <_items dataType="Array" type="Duality.Component[]" id="164" length="4">
                      <object dataType="ObjectRef">154</object>
                      <object dataType="ObjectRef">162</object>
                      <object dataType="ObjectRef">156</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">154</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="165">
                  <name dataType="String">Wall</name>
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="166" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="167" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="168" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="169">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">594.9905</X>
                            <Y dataType="Float">234.519531</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.88250065</angle>
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
                            <X dataType="Float">594.9905</X>
                            <Y dataType="Float">234.519531</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.88250065</angleAbs>
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
                            <X dataType="Float">594.9905</X>
                            <Y dataType="Float">234.519531</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">594.9905</X>
                            <Y dataType="Float">234.519531</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">-0.400684834</lastAngle>
                          <lastAngleAbs dataType="Float">-0.400684834</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="170" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="171">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="172">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="173" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="174">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="175" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">171</parent>
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
                              <gameobj dataType="ObjectRef">165</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="176" length="1">
                              <object dataType="ObjectRef">170</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">165</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="177">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-512</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">1024</W>
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
                          <gameobj dataType="ObjectRef">165</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">171</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="178">
                    <_items dataType="Array" type="Duality.Component[]" id="179" length="4">
                      <object dataType="ObjectRef">169</object>
                      <object dataType="ObjectRef">177</object>
                      <object dataType="ObjectRef">171</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">169</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="180">
                  <name dataType="String">Wall</name>
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="181" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="182" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="183" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="184">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">462.975281</X>
                            <Y dataType="Float">-50.0152245</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.58268452</angle>
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
                            <X dataType="Float">462.975281</X>
                            <Y dataType="Float">-50.0152245</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.58268452</angleAbs>
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
                            <X dataType="Float">462.975281</X>
                            <Y dataType="Float">-50.0152245</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">462.975281</X>
                            <Y dataType="Float">-50.0152245</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">-0.7005011</lastAngle>
                          <lastAngleAbs dataType="Float">-0.7005011</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="185" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="186">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="187">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="188" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="189">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="190" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">186</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.5</friction>
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
                              <gameobj dataType="ObjectRef">180</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="191" length="1">
                              <object dataType="ObjectRef">185</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">180</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="192">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-512</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">1024</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="Class" type="Duality.Resources.BatchInfo" id="193">
                            <technique dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.DrawTechnique]]">
                              <contentPath dataType="String">Default:DrawTechnique:Mask</contentPath>
                            </technique>
                            <mainColor dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                              <R dataType="Byte">37</R>
                              <G dataType="Byte">47</G>
                              <B dataType="Byte">83</B>
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
                          <gameobj dataType="ObjectRef">180</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">186</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="194">
                    <_items dataType="Array" type="Duality.Component[]" id="195" length="4">
                      <object dataType="ObjectRef">184</object>
                      <object dataType="ObjectRef">192</object>
                      <object dataType="ObjectRef">186</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">184</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="196">
                  <name dataType="String">Wall</name>
                  <prefabLink />
                  <parent dataType="ObjectRef">67</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="197" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="198" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="199" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="200">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-404.509338</X>
                            <Y dataType="Float">-257.063171</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0.913560152</angle>
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
                            <X dataType="Float">-404.509338</X>
                            <Y dataType="Float">-257.063171</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0.913560152</angleAbs>
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
                            <X dataType="Float">-404.509338</X>
                            <Y dataType="Float">-257.063171</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-404.509338</X>
                            <Y dataType="Float">-257.063171</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0.913560033</lastAngle>
                          <lastAngleAbs dataType="Float">0.913560033</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="201" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="202">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="203">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="204" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="205">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="206" length="4">
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">-64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                      <object dataType="Struct" type="OpenTK.Vector2">
                                        <X dataType="Float">-512</X>
                                        <Y dataType="Float">64</Y>
                                      </object>
                                    </vertices>
                                    <parent dataType="ObjectRef">202</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.5</friction>
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
                              <gameobj dataType="ObjectRef">196</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="207" length="1">
                              <object dataType="ObjectRef">201</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">196</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="208">
                          <rect dataType="Struct" type="Duality.Rect">
                            <X dataType="Float">-512</X>
                            <Y dataType="Float">-64</Y>
                            <W dataType="Float">1024</W>
                            <H dataType="Float">128</H>
                          </rect>
                          <sharedMat dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Material]]">
                            <contentPath />
                          </sharedMat>
                          <customMat dataType="ObjectRef">193</customMat>
                          <colorTint dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">196</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">202</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="209">
                    <_items dataType="Array" type="Duality.Component[]" id="210" length="4">
                      <object dataType="ObjectRef">200</object>
                      <object dataType="ObjectRef">208</object>
                      <object dataType="ObjectRef">202</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">200</compTransform>
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
              </_items>
              <_size dataType="Int">9</_size>
              <_version dataType="Int">77</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="211" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="212" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="213" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="214">
              <_items dataType="Array" type="Duality.Component[]" id="215" length="0" />
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="216" multi="true">
              <object dataType="MethodInfo" id="217" value="M:Duality.Components.Transform:Parent_EventComponentAdded(System.Object,Duality.ComponentEventArgs)" />
              <object dataType="ObjectRef">200</object>
              <object dataType="Array" type="System.Delegate[]" id="218" length="10">
                <object dataType="ObjectRef">61</object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="219" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">76</object>
                  <object dataType="Array" type="System.Delegate[]" id="220" length="1">
                    <object dataType="ObjectRef">219</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="221" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">94</object>
                  <object dataType="Array" type="System.Delegate[]" id="222" length="1">
                    <object dataType="ObjectRef">221</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="223" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">109</object>
                  <object dataType="Array" type="System.Delegate[]" id="224" length="1">
                    <object dataType="ObjectRef">223</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="225" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">124</object>
                  <object dataType="Array" type="System.Delegate[]" id="226" length="1">
                    <object dataType="ObjectRef">225</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="227" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">139</object>
                  <object dataType="Array" type="System.Delegate[]" id="228" length="1">
                    <object dataType="ObjectRef">227</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="229" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">154</object>
                  <object dataType="Array" type="System.Delegate[]" id="230" length="1">
                    <object dataType="ObjectRef">229</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="231" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">169</object>
                  <object dataType="Array" type="System.Delegate[]" id="232" length="1">
                    <object dataType="ObjectRef">231</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="233" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">184</object>
                  <object dataType="Array" type="System.Delegate[]" id="234" length="1">
                    <object dataType="ObjectRef">233</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="235" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">200</object>
                  <object dataType="Array" type="System.Delegate[]" id="236" length="1">
                    <object dataType="ObjectRef">235</object>
                  </object>
                </object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="237">
            <name dataType="String">Dynamics</name>
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="238">
              <_items dataType="Array" type="Duality.GameObject[]" id="239" length="256">
                <object dataType="Class" type="Duality.GameObject" id="240">
                  <name dataType="String">Square</name>
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="241">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">240</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="242">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="243" length="8">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="244" value="P:Duality.Components.Transform:RelativeScale" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="245">
                            <_items dataType="Array" type="System.Int32[]" id="246" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="247" value="P:Duality.Components.Physics.RigidBody:Friction" />
                          <componentType dataType="ObjectRef">74</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="248">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">1</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="249" value="P:Duality.Components.Renderers.SpriteRenderer:ColorTint" />
                          <componentType dataType="ObjectRef">73</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="250">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">0</G>
                            <B dataType="Byte">0</B>
                            <A dataType="Byte">255</A>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="251" value="P:Duality.Components.Transform:RelativePos" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="252">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-200.522873</X>
                            <Y dataType="Float">-172.822464</Y>
                            <Z dataType="Float">0</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="PropertyInfo" id="253" value="P:Duality.Components.Transform:RelativeAngle" />
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="254">
                            <_items dataType="Array" type="System.Int32[]" id="255" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.3543018</val>
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
                      <_version dataType="Int">265</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">237</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="256" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="257" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="258" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="259">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-200.522873</X>
                            <Y dataType="Float">-172.822464</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0.3543018</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-200.522873</X>
                            <Y dataType="Float">-172.822464</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0.3543018</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
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
                            <X dataType="Float">-200.522873</X>
                            <Y dataType="Float">-172.822464</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-200.522873</X>
                            <Y dataType="Float">-172.822464</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0.3543018</lastAngle>
                          <lastAngleAbs dataType="Float">0.3543018</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="260" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="261">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="262">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="263" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="264">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="265" length="4">
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
                                    <parent dataType="ObjectRef">261</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">1</friction>
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
                              <gameobj dataType="ObjectRef">240</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="266" length="1">
                              <object dataType="ObjectRef">260</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">240</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="267">
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
                            <G dataType="Byte">0</G>
                            <B dataType="Byte">0</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">240</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">261</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="268">
                    <_items dataType="Array" type="Duality.Component[]" id="269" length="4">
                      <object dataType="ObjectRef">259</object>
                      <object dataType="ObjectRef">267</object>
                      <object dataType="ObjectRef">261</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">259</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="270">
                  <name dataType="String">Square</name>
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="271">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">270</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="272">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="273" length="8">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">244</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="274">
                            <_items dataType="Array" type="System.Int32[]" id="275" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">247</prop>
                          <componentType dataType="ObjectRef">74</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="276">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.8</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">249</prop>
                          <componentType dataType="ObjectRef">73</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="277">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">255</R>
                            <G dataType="Byte">182</G>
                            <B dataType="Byte">0</B>
                            <A dataType="Byte">255</A>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">253</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="278">
                            <_items dataType="ObjectRef">255</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.3543018</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">251</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="279">
                            <_items dataType="Array" type="System.Int32[]" id="280" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-135.536026</X>
                            <Y dataType="Float">-148.641678</Y>
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
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">5</_size>
                      <_version dataType="Int">319</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">237</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="281" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="282" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="283" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="284">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-135.536026</X>
                            <Y dataType="Float">-148.641678</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0.3543018</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-135.536026</X>
                            <Y dataType="Float">-148.641678</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0.3543018</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
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
                            <X dataType="Float">-135.536026</X>
                            <Y dataType="Float">-148.641678</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-135.536026</X>
                            <Y dataType="Float">-148.641678</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0.3543018</lastAngle>
                          <lastAngleAbs dataType="Float">0.3543018</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="285" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="286">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="287">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="288" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="289">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="290" length="4">
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
                                    <parent dataType="ObjectRef">286</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.8</friction>
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
                              <gameobj dataType="ObjectRef">270</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="291" length="1">
                              <object dataType="ObjectRef">285</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">270</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="292">
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
                            <G dataType="Byte">182</G>
                            <B dataType="Byte">0</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">270</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">286</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="293">
                    <_items dataType="Array" type="Duality.Component[]" id="294" length="4">
                      <object dataType="ObjectRef">284</object>
                      <object dataType="ObjectRef">292</object>
                      <object dataType="ObjectRef">286</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">284</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="295">
                  <name dataType="String">Square</name>
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="296">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">295</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="297">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="298" length="8">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">244</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="299">
                            <_items dataType="Array" type="System.Int32[]" id="300" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">247</prop>
                          <componentType dataType="ObjectRef">74</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="301">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.6</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">249</prop>
                          <componentType dataType="ObjectRef">73</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="302">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">182</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">0</B>
                            <A dataType="Byte">255</A>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">253</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="303">
                            <_items dataType="ObjectRef">255</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.3543018</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">251</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="304">
                            <_items dataType="ObjectRef">280</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-72.06045</X>
                            <Y dataType="Float">-124.460884</Y>
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
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">5</_size>
                      <_version dataType="Int">191</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">237</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="305" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="306" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="307" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="308">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-72.06045</X>
                            <Y dataType="Float">-124.460884</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0.3543018</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-72.06045</X>
                            <Y dataType="Float">-124.460884</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0.3543018</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
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
                            <X dataType="Float">-72.06045</X>
                            <Y dataType="Float">-124.460884</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">-72.06045</X>
                            <Y dataType="Float">-124.460884</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0.3543018</lastAngle>
                          <lastAngleAbs dataType="Float">0.3543018</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="309" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="310">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="311">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="312" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="313">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="314" length="4">
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
                                    <parent dataType="ObjectRef">310</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.6</friction>
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
                              <gameobj dataType="ObjectRef">295</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="315" length="1">
                              <object dataType="ObjectRef">309</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">295</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="316">
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
                            <R dataType="Byte">182</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">0</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">295</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">310</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="317">
                    <_items dataType="Array" type="Duality.Component[]" id="318" length="4">
                      <object dataType="ObjectRef">308</object>
                      <object dataType="ObjectRef">316</object>
                      <object dataType="ObjectRef">310</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">308</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="319">
                  <name dataType="String">Square</name>
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="320">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">319</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="321">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="322" length="8">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">244</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="323">
                            <_items dataType="Array" type="System.Int32[]" id="324" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">249</prop>
                          <componentType dataType="ObjectRef">73</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="325">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">12</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">0</B>
                            <A dataType="Byte">255</A>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">247</prop>
                          <componentType dataType="ObjectRef">74</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="326">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.4</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">253</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="327">
                            <_items dataType="ObjectRef">255</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.3543018</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">251</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="328">
                            <_items dataType="ObjectRef">280</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1.99420166</X>
                            <Y dataType="Float">-95.74621</Y>
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
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">5</_size>
                      <_version dataType="Int">107</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">237</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="329" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="330" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="331" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="332">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1.99420166</X>
                            <Y dataType="Float">-95.74621</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">0.3543018</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1.99420166</X>
                            <Y dataType="Float">-95.74621</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">0.3543018</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
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
                            <X dataType="Float">1.99420166</X>
                            <Y dataType="Float">-95.74621</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">1.99420166</X>
                            <Y dataType="Float">-95.74621</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">0.3543018</lastAngle>
                          <lastAngleAbs dataType="Float">0.3543018</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="333" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="334">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="335">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="336" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="337">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="338" length="4">
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
                                    <parent dataType="ObjectRef">334</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.4</friction>
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
                              <gameobj dataType="ObjectRef">319</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="339" length="1">
                              <object dataType="ObjectRef">333</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">319</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="340">
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
                            <R dataType="Byte">12</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">0</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">319</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="ObjectRef">334</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="341">
                    <_items dataType="Array" type="Duality.Component[]" id="342" length="4">
                      <object dataType="ObjectRef">332</object>
                      <object dataType="ObjectRef">340</object>
                      <object dataType="ObjectRef">334</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">332</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="343">
                  <name dataType="String">Square</name>
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="344">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">343</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="345">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="346" length="8">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">244</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="347">
                            <_items dataType="Array" type="System.Int32[]" id="348" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">249</prop>
                          <componentType dataType="ObjectRef">73</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="349">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">0</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">231</B>
                            <A dataType="Byte">255</A>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">247</prop>
                          <componentType dataType="ObjectRef">74</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="350">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0.2</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">253</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="351">
                            <_items dataType="ObjectRef">255</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">5.976007</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">251</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="352">
                            <_items dataType="ObjectRef">280</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">213.577057</X>
                            <Y dataType="Float">32.71421</Y>
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
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">5</_size>
                      <_version dataType="Int">131</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">237</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="353" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="354" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="355" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="356">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">213.577057</X>
                            <Y dataType="Float">32.71421</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.976007</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">213.577057</X>
                            <Y dataType="Float">32.71421</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.976007</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
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
                            <X dataType="Float">213.577057</X>
                            <Y dataType="Float">32.71421</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">213.577057</X>
                            <Y dataType="Float">32.71421</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">5.976007</lastAngle>
                          <lastAngleAbs dataType="Float">5.976007</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="357" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="358">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="359">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="360" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="361">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="362" length="4">
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
                                    <parent dataType="ObjectRef">358</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0.2</friction>
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
                              <gameobj dataType="ObjectRef">343</gameobj>
                              <disposed dataType="Bool">false</disposed>
                              <active dataType="Bool">true</active>
                            </object>
                            <object dataType="Array" type="System.Delegate[]" id="363" length="1">
                              <object dataType="ObjectRef">357</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">343</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="364">
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
                            <R dataType="Byte">0</R>
                            <G dataType="Byte">255</G>
                            <B dataType="Byte">231</B>
                            <A dataType="Byte">255</A>
                          </colorTint>
                          <rectMode dataType="Enum" type="Duality.Components.Renderers.SpriteRenderer+UVMode" name="Stretch" value="0" />
                          <renderFlags dataType="Enum" type="Duality.RendererFlags" name="Default" value="3" />
                          <visibilityGroup dataType="Enum" type="Duality.VisibilityFlag" name="Group0" value="1" />
                          <gameobj dataType="ObjectRef">343</gameobj>
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
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">356</compTransform>
                  <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
                  <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
                  <EventCollisionBegin />
                  <EventCollisionEnd />
                  <EventCollisionSolve />
                </object>
                <object dataType="Class" type="Duality.GameObject" id="367">
                  <name dataType="String">Square</name>
                  <prefabLink dataType="Class" type="Duality.Resources.PrefabLink" id="368">
                    <prefab dataType="Struct" type="Duality.ContentRef`1[[Duality.Resources.Prefab]]">
                      <contentPath dataType="String">Data\Bodies\Square.Prefab.res</contentPath>
                    </prefab>
                    <obj dataType="ObjectRef">367</obj>
                    <changes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Resources.PrefabLink+VarMod]]" id="369">
                      <_items dataType="Array" type="Duality.Resources.PrefabLink+VarMod[]" id="370" length="8">
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">244</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="371">
                            <_items dataType="Array" type="System.Int32[]" id="372" length="0" />
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">0</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">249</prop>
                          <componentType dataType="ObjectRef">73</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="373">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="Duality.ColorFormat.ColorRgba">
                            <R dataType="Byte">0</R>
                            <G dataType="Byte">140</G>
                            <B dataType="Byte">255</B>
                            <A dataType="Byte">255</A>
                          </val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">247</prop>
                          <componentType dataType="ObjectRef">74</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="374">
                            <_items dataType="ObjectRef">246</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">0</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">253</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="375">
                            <_items dataType="ObjectRef">255</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Float">5.976007</val>
                        </object>
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop dataType="ObjectRef">251</prop>
                          <componentType dataType="ObjectRef">14</componentType>
                          <childIndex dataType="Class" type="System.Collections.Generic.List`1[[System.Int32]]" id="376">
                            <_items dataType="ObjectRef">280</_items>
                            <_size dataType="Int">0</_size>
                            <_version dataType="Int">1</_version>
                          </childIndex>
                          <val dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">144.058319</X>
                            <Y dataType="Float">55.3836975</Y>
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
                        <object dataType="Struct" type="Duality.Resources.PrefabLink+VarMod">
                          <prop />
                          <componentType />
                          <childIndex />
                          <val />
                        </object>
                      </_items>
                      <_size dataType="Int">5</_size>
                      <_version dataType="Int">525</_version>
                    </changes>
                  </prefabLink>
                  <parent dataType="ObjectRef">237</parent>
                  <children />
                  <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="377" surrogate="true">
                    <customSerialIO />
                    <customSerialIO>
                      <keys dataType="Array" type="System.Type[]" id="378" length="3">
                        <object dataType="ObjectRef">14</object>
                        <object dataType="ObjectRef">73</object>
                        <object dataType="ObjectRef">74</object>
                      </keys>
                      <values dataType="Array" type="Duality.Component[]" id="379" length="3">
                        <object dataType="Class" type="Duality.Components.Transform" id="380">
                          <pos dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">144.058319</X>
                            <Y dataType="Float">55.3836975</Y>
                            <Z dataType="Float">0</Z>
                          </pos>
                          <angle dataType="Float">5.976007</angle>
                          <scale dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
                          </scale>
                          <deriveAngle dataType="Bool">true</deriveAngle>
                          <ignoreParent dataType="Bool">false</ignoreParent>
                          <changes dataType="Enum" type="Duality.Components.Transform+DirtyFlags" name="None" value="0" />
                          <parentTransform />
                          <posAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">144.058319</X>
                            <Y dataType="Float">55.3836975</Y>
                            <Z dataType="Float">0</Z>
                          </posAbs>
                          <angleAbs dataType="Float">5.976007</angleAbs>
                          <scaleAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">0.201502681</X>
                            <Y dataType="Float">0.201502681</Y>
                            <Z dataType="Float">0.201502681</Z>
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
                            <X dataType="Float">144.058319</X>
                            <Y dataType="Float">55.3836975</Y>
                            <Z dataType="Float">0</Z>
                          </lastPos>
                          <lastPosAbs dataType="Struct" type="OpenTK.Vector3">
                            <X dataType="Float">144.058319</X>
                            <Y dataType="Float">55.3836975</Y>
                            <Z dataType="Float">0</Z>
                          </lastPosAbs>
                          <lastAngle dataType="Float">5.976007</lastAngle>
                          <lastAngleAbs dataType="Float">5.976007</lastAngleAbs>
                          <OnTransformChanged dataType="Delegate" type="System.EventHandler`1[[Duality.TransformChangedEventArgs]]" id="381" multi="true">
                            <object dataType="ObjectRef">78</object>
                            <object dataType="Class" type="Duality.Components.Physics.RigidBody" id="382">
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
                              <shapes dataType="Class" type="System.Collections.Generic.List`1[[Duality.Components.Physics.ShapeInfo]]" id="383">
                                <_items dataType="Array" type="Duality.Components.Physics.ShapeInfo[]" id="384" length="4">
                                  <object dataType="Class" type="Duality.Components.Physics.PolyShapeInfo" id="385">
                                    <vertices dataType="Array" type="OpenTK.Vector2[]" id="386" length="4">
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
                                    <parent dataType="ObjectRef">382</parent>
                                    <density dataType="Float">1</density>
                                    <friction dataType="Float">0</friction>
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
                            <object dataType="Array" type="System.Delegate[]" id="387" length="1">
                              <object dataType="ObjectRef">381</object>
                            </object>
                          </OnTransformChanged>
                          <gameobj dataType="ObjectRef">367</gameobj>
                          <disposed dataType="Bool">false</disposed>
                          <active dataType="Bool">true</active>
                        </object>
                        <object dataType="Class" type="Duality.Components.Renderers.SpriteRenderer" id="388">
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
                            <R dataType="Byte">0</R>
                            <G dataType="Byte">140</G>
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
                        <object dataType="ObjectRef">382</object>
                      </values>
                    </customSerialIO>
                  </compMap>
                  <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="389">
                    <_items dataType="Array" type="Duality.Component[]" id="390" length="4">
                      <object dataType="ObjectRef">380</object>
                      <object dataType="ObjectRef">388</object>
                      <object dataType="ObjectRef">382</object>
                      <object />
                    </_items>
                    <_size dataType="Int">3</_size>
                    <_version dataType="Int">3</_version>
                  </compList>
                  <active dataType="Bool">true</active>
                  <disposed dataType="Bool">false</disposed>
                  <compTransform dataType="ObjectRef">380</compTransform>
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
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
                <object />
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
              <_version dataType="Int">524</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="391" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="392" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="393" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="394">
              <_items dataType="ObjectRef">215</_items>
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="395" multi="true">
              <object dataType="ObjectRef">217</object>
              <object dataType="ObjectRef">380</object>
              <object dataType="Array" type="System.Delegate[]" id="396" length="7">
                <object dataType="ObjectRef">61</object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="397" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">259</object>
                  <object dataType="Array" type="System.Delegate[]" id="398" length="1">
                    <object dataType="ObjectRef">397</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="399" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">284</object>
                  <object dataType="Array" type="System.Delegate[]" id="400" length="1">
                    <object dataType="ObjectRef">399</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="401" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">308</object>
                  <object dataType="Array" type="System.Delegate[]" id="402" length="1">
                    <object dataType="ObjectRef">401</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="403" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">332</object>
                  <object dataType="Array" type="System.Delegate[]" id="404" length="1">
                    <object dataType="ObjectRef">403</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="405" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">356</object>
                  <object dataType="Array" type="System.Delegate[]" id="406" length="1">
                    <object dataType="ObjectRef">405</object>
                  </object>
                </object>
                <object dataType="Delegate" type="System.EventHandler`1[[Duality.ComponentEventArgs]]" id="407" multi="true">
                  <object dataType="ObjectRef">217</object>
                  <object dataType="ObjectRef">380</object>
                  <object dataType="Array" type="System.Delegate[]" id="408" length="1">
                    <object dataType="ObjectRef">407</object>
                  </object>
                </object>
              </object>
            </EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="Class" type="Duality.GameObject" id="409">
            <name dataType="String">Text</name>
            <prefabLink />
            <parent />
            <children dataType="Class" type="System.Collections.Generic.List`1[[Duality.GameObject]]" id="410">
              <_items dataType="Array" type="Duality.GameObject[]" id="411" length="4">
                <object />
                <object />
                <object />
                <object />
              </_items>
              <_size dataType="Int">0</_size>
              <_version dataType="Int">6</_version>
            </children>
            <compMap dataType="Class" type="System.Collections.Generic.Dictionary`2[[System.Type],[Duality.Component]]" id="412" surrogate="true">
              <customSerialIO />
              <customSerialIO>
                <keys dataType="Array" type="System.Type[]" id="413" length="0" />
                <values dataType="Array" type="Duality.Component[]" id="414" length="0" />
              </customSerialIO>
            </compMap>
            <compList dataType="Class" type="System.Collections.Generic.List`1[[Duality.Component]]" id="415">
              <_items dataType="Array" type="Duality.Component[]" id="416" length="0" />
              <_size dataType="Int">0</_size>
              <_version dataType="Int">0</_version>
            </compList>
            <active dataType="Bool">true</active>
            <disposed dataType="Bool">false</disposed>
            <compTransform />
            <EventComponentAdded dataType="ObjectRef">61</EventComponentAdded>
            <EventComponentRemoving dataType="ObjectRef">64</EventComponentRemoving>
            <EventCollisionBegin />
            <EventCollisionEnd />
            <EventCollisionSolve />
          </object>
          <object dataType="ObjectRef">70</object>
          <object dataType="ObjectRef">90</object>
          <object dataType="ObjectRef">105</object>
          <object dataType="ObjectRef">120</object>
          <object dataType="ObjectRef">135</object>
          <object dataType="ObjectRef">150</object>
          <object dataType="ObjectRef">165</object>
          <object dataType="ObjectRef">180</object>
          <object dataType="ObjectRef">196</object>
          <object dataType="ObjectRef">240</object>
          <object dataType="ObjectRef">270</object>
          <object dataType="ObjectRef">295</object>
          <object dataType="ObjectRef">319</object>
          <object dataType="ObjectRef">343</object>
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
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
          <object />
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
        <_size dataType="Int">19</_size>
        <_version dataType="Int">615</_version>
      </allObj>
      <Registered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="417" multi="true">
        <object dataType="MethodInfo" id="418" value="M:Duality.Resources.Scene:objectManager_Registered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="419" length="1">
          <object dataType="ObjectRef">417</object>
        </object>
      </Registered>
      <Unregistered dataType="Delegate" type="System.EventHandler`1[[Duality.ObjectManagerEventArgs`1[[Duality.GameObject]]]]" id="420" multi="true">
        <object dataType="MethodInfo" id="421" value="M:Duality.Resources.Scene:objectManager_Unregistered(System.Object,Duality.ObjectManagerEventArgs`1[[Duality.GameObject]])" />
        <object dataType="ObjectRef">1</object>
        <object dataType="Array" type="System.Delegate[]" id="422" length="1">
          <object dataType="ObjectRef">420</object>
        </object>
      </Unregistered>
    </objectManager>
    <sourcePath />
  </object>
</root>