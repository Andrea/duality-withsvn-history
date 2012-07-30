/*
 * A set of static helper classes that provide easy runtime access to the games resources.
 * This file is auto-generated. Any changes made to it are lost as soon as Duality decides
 * to regenerate it.
 */
namespace GameRes
{
	public static class Data {
		public static class Bodies {
			private static Duality.ContentRef<Duality.Resources.Prefab> _Circle_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> Circle_Prefab { get { if (_Circle_Prefab.IsExplicitNull) _Circle_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Bodies\Circle.Prefab.res"); return _Circle_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _Complex_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> Complex_Prefab { get { if (_Complex_Prefab.IsExplicitNull) _Complex_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Bodies\Complex.Prefab.res"); return _Complex_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _RoundSquare_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> RoundSquare_Prefab { get { if (_RoundSquare_Prefab.IsExplicitNull) _RoundSquare_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Bodies\RoundSquare.Prefab.res"); return _RoundSquare_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _Square_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> Square_Prefab { get { if (_Square_Prefab.IsExplicitNull) _Square_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Bodies\Square.Prefab.res"); return _Square_Prefab; }}
			public static void LoadAll() {
				Circle_Prefab.MakeAvailable();
				Complex_Prefab.MakeAvailable();
				RoundSquare_Prefab.MakeAvailable();
				Square_Prefab.MakeAvailable();
			}
		}
		public static class Examples {
			private static Duality.ContentRef<Duality.Resources.Scene> _Basic_Scene;
			public static Duality.ContentRef<Duality.Resources.Scene> Basic_Scene { get { if (_Basic_Scene.IsExplicitNull) _Basic_Scene = Duality.ContentProvider.RequestContent<Duality.Resources.Scene>(@"Data\Examples\Basic.Scene.res"); return _Basic_Scene; }}
			private static Duality.ContentRef<Duality.Resources.Scene> _Friction_Scene;
			public static Duality.ContentRef<Duality.Resources.Scene> Friction_Scene { get { if (_Friction_Scene.IsExplicitNull) _Friction_Scene = Duality.ContentProvider.RequestContent<Duality.Resources.Scene>(@"Data\Examples\Friction.Scene.res"); return _Friction_Scene; }}
			private static Duality.ContentRef<Duality.Resources.Scene> _Landscape_Scene;
			public static Duality.ContentRef<Duality.Resources.Scene> Landscape_Scene { get { if (_Landscape_Scene.IsExplicitNull) _Landscape_Scene = Duality.ContentProvider.RequestContent<Duality.Resources.Scene>(@"Data\Examples\Landscape.Scene.res"); return _Landscape_Scene; }}
			private static Duality.ContentRef<Duality.Resources.Scene> _Mass_Scene;
			public static Duality.ContentRef<Duality.Resources.Scene> Mass_Scene { get { if (_Mass_Scene.IsExplicitNull) _Mass_Scene = Duality.ContentProvider.RequestContent<Duality.Resources.Scene>(@"Data\Examples\Mass.Scene.res"); return _Mass_Scene; }}
			private static Duality.ContentRef<Duality.Resources.Scene> _Restitution_Scene;
			public static Duality.ContentRef<Duality.Resources.Scene> Restitution_Scene { get { if (_Restitution_Scene.IsExplicitNull) _Restitution_Scene = Duality.ContentProvider.RequestContent<Duality.Resources.Scene>(@"Data\Examples\Restitution.Scene.res"); return _Restitution_Scene; }}
			private static Duality.ContentRef<Duality.Resources.Scene> _Stacking_Scene;
			public static Duality.ContentRef<Duality.Resources.Scene> Stacking_Scene { get { if (_Stacking_Scene.IsExplicitNull) _Stacking_Scene = Duality.ContentProvider.RequestContent<Duality.Resources.Scene>(@"Data\Examples\Stacking.Scene.res"); return _Stacking_Scene; }}
			private static Duality.ContentRef<Duality.Resources.Scene> _Trigger_Scene;
			public static Duality.ContentRef<Duality.Resources.Scene> Trigger_Scene { get { if (_Trigger_Scene.IsExplicitNull) _Trigger_Scene = Duality.ContentProvider.RequestContent<Duality.Resources.Scene>(@"Data\Examples\Trigger.Scene.res"); return _Trigger_Scene; }}
			public static void LoadAll() {
				Basic_Scene.MakeAvailable();
				Friction_Scene.MakeAvailable();
				Landscape_Scene.MakeAvailable();
				Mass_Scene.MakeAvailable();
				Restitution_Scene.MakeAvailable();
				Stacking_Scene.MakeAvailable();
				Trigger_Scene.MakeAvailable();
			}
		}
		public static class LevelGraphics {
			private static Duality.ContentRef<Duality.Resources.Material> _ShadowLevel_Material;
			public static Duality.ContentRef<Duality.Resources.Material> ShadowLevel_Material { get { if (_ShadowLevel_Material.IsExplicitNull) _ShadowLevel_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\LevelGraphics\ShadowLevel.Material.res"); return _ShadowLevel_Material; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _ShadowLevel_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> ShadowLevel_Pixmap { get { if (_ShadowLevel_Pixmap.IsExplicitNull) _ShadowLevel_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\LevelGraphics\ShadowLevel.Pixmap.res"); return _ShadowLevel_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _ShadowLevel_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> ShadowLevel_Texture { get { if (_ShadowLevel_Texture.IsExplicitNull) _ShadowLevel_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\LevelGraphics\ShadowLevel.Texture.res"); return _ShadowLevel_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Material> _ShadowLevelBg_Material;
			public static Duality.ContentRef<Duality.Resources.Material> ShadowLevelBg_Material { get { if (_ShadowLevelBg_Material.IsExplicitNull) _ShadowLevelBg_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\LevelGraphics\ShadowLevelBg.Material.res"); return _ShadowLevelBg_Material; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _ShadowLevelBg_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> ShadowLevelBg_Pixmap { get { if (_ShadowLevelBg_Pixmap.IsExplicitNull) _ShadowLevelBg_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\LevelGraphics\ShadowLevelBg.Pixmap.res"); return _ShadowLevelBg_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _ShadowLevelBg_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> ShadowLevelBg_Texture { get { if (_ShadowLevelBg_Texture.IsExplicitNull) _ShadowLevelBg_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\LevelGraphics\ShadowLevelBg.Texture.res"); return _ShadowLevelBg_Texture; }}
			public static void LoadAll() {
				ShadowLevel_Material.MakeAvailable();
				ShadowLevel_Pixmap.MakeAvailable();
				ShadowLevel_Texture.MakeAvailable();
				ShadowLevelBg_Material.MakeAvailable();
				ShadowLevelBg_Pixmap.MakeAvailable();
				ShadowLevelBg_Texture.MakeAvailable();
			}
		}
		public static class Sprites {
			private static Duality.ContentRef<Duality.Resources.Material> _Circle_Material;
			public static Duality.ContentRef<Duality.Resources.Material> Circle_Material { get { if (_Circle_Material.IsExplicitNull) _Circle_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Sprites\Circle.Material.res"); return _Circle_Material; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _Circle_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> Circle_Pixmap { get { if (_Circle_Pixmap.IsExplicitNull) _Circle_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Sprites\Circle.Pixmap.res"); return _Circle_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _Circle_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> Circle_Texture { get { if (_Circle_Texture.IsExplicitNull) _Circle_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Sprites\Circle.Texture.res"); return _Circle_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Material> _Complex_Material;
			public static Duality.ContentRef<Duality.Resources.Material> Complex_Material { get { if (_Complex_Material.IsExplicitNull) _Complex_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Sprites\Complex.Material.res"); return _Complex_Material; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _Complex_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> Complex_Pixmap { get { if (_Complex_Pixmap.IsExplicitNull) _Complex_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Sprites\Complex.Pixmap.res"); return _Complex_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _Complex_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> Complex_Texture { get { if (_Complex_Texture.IsExplicitNull) _Complex_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Sprites\Complex.Texture.res"); return _Complex_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Material> _RoundSquare_Material;
			public static Duality.ContentRef<Duality.Resources.Material> RoundSquare_Material { get { if (_RoundSquare_Material.IsExplicitNull) _RoundSquare_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Sprites\RoundSquare.Material.res"); return _RoundSquare_Material; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _RoundSquare_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> RoundSquare_Pixmap { get { if (_RoundSquare_Pixmap.IsExplicitNull) _RoundSquare_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Sprites\RoundSquare.Pixmap.res"); return _RoundSquare_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _RoundSquare_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> RoundSquare_Texture { get { if (_RoundSquare_Texture.IsExplicitNull) _RoundSquare_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Sprites\RoundSquare.Texture.res"); return _RoundSquare_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Material> _Square_Material;
			public static Duality.ContentRef<Duality.Resources.Material> Square_Material { get { if (_Square_Material.IsExplicitNull) _Square_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Sprites\Square.Material.res"); return _Square_Material; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _Square_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> Square_Pixmap { get { if (_Square_Pixmap.IsExplicitNull) _Square_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Sprites\Square.Pixmap.res"); return _Square_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _Square_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> Square_Texture { get { if (_Square_Texture.IsExplicitNull) _Square_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Sprites\Square.Texture.res"); return _Square_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Material> _Trigger_Material;
			public static Duality.ContentRef<Duality.Resources.Material> Trigger_Material { get { if (_Trigger_Material.IsExplicitNull) _Trigger_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Sprites\Trigger.Material.res"); return _Trigger_Material; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _Trigger_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> Trigger_Pixmap { get { if (_Trigger_Pixmap.IsExplicitNull) _Trigger_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Sprites\Trigger.Pixmap.res"); return _Trigger_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _Trigger_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> Trigger_Texture { get { if (_Trigger_Texture.IsExplicitNull) _Trigger_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Sprites\Trigger.Texture.res"); return _Trigger_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Material> _Trigger2_Material;
			public static Duality.ContentRef<Duality.Resources.Material> Trigger2_Material { get { if (_Trigger2_Material.IsExplicitNull) _Trigger2_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Sprites\Trigger2.Material.res"); return _Trigger2_Material; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _Trigger2_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> Trigger2_Pixmap { get { if (_Trigger2_Pixmap.IsExplicitNull) _Trigger2_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Sprites\Trigger2.Pixmap.res"); return _Trigger2_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _Trigger2_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> Trigger2_Texture { get { if (_Trigger2_Texture.IsExplicitNull) _Trigger2_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Sprites\Trigger2.Texture.res"); return _Trigger2_Texture; }}
			public static void LoadAll() {
				Circle_Material.MakeAvailable();
				Circle_Pixmap.MakeAvailable();
				Circle_Texture.MakeAvailable();
				Complex_Material.MakeAvailable();
				Complex_Pixmap.MakeAvailable();
				Complex_Texture.MakeAvailable();
				RoundSquare_Material.MakeAvailable();
				RoundSquare_Pixmap.MakeAvailable();
				RoundSquare_Texture.MakeAvailable();
				Square_Material.MakeAvailable();
				Square_Pixmap.MakeAvailable();
				Square_Texture.MakeAvailable();
				Trigger_Material.MakeAvailable();
				Trigger_Pixmap.MakeAvailable();
				Trigger_Texture.MakeAvailable();
				Trigger2_Material.MakeAvailable();
				Trigger2_Pixmap.MakeAvailable();
				Trigger2_Texture.MakeAvailable();
			}
		}
		private static Duality.ContentRef<Duality.Resources.Font> _BigFont_Font;
		public static Duality.ContentRef<Duality.Resources.Font> BigFont_Font { get { if (_BigFont_Font.IsExplicitNull) _BigFont_Font = Duality.ContentProvider.RequestContent<Duality.Resources.Font>(@"Data\BigFont.Font.res"); return _BigFont_Font; }}
		private static Duality.ContentRef<Duality.Resources.Font> _SmallFont_Font;
		public static Duality.ContentRef<Duality.Resources.Font> SmallFont_Font { get { if (_SmallFont_Font.IsExplicitNull) _SmallFont_Font = Duality.ContentProvider.RequestContent<Duality.Resources.Font>(@"Data\SmallFont.Font.res"); return _SmallFont_Font; }}
		public static void LoadAll() {
			Bodies.LoadAll();
			Examples.LoadAll();
			LevelGraphics.LoadAll();
			Sprites.LoadAll();
			BigFont_Font.MakeAvailable();
			SmallFont_Font.MakeAvailable();
		}
	}

}
