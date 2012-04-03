/*
 * A set of static helper classes that provide easy runtime access to the games resources.
 * This file is auto-generated. Any changes made to it are lost as soon as Duality decides
 * to regenerate it.
 */
namespace GameRes
{
	public static class Data {
		private static Duality.ContentRef<Duality.Resources.Material> _BlockA_Material;
		public static Duality.ContentRef<Duality.Resources.Material> BlockA_Material { get { if (_BlockA_Material.IsExplicitNull) _BlockA_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\BlockA.Material.res"); return _BlockA_Material; }}
		private static Duality.ContentRef<Duality.Resources.Pixmap> _BlockA_Pixmap;
		public static Duality.ContentRef<Duality.Resources.Pixmap> BlockA_Pixmap { get { if (_BlockA_Pixmap.IsExplicitNull) _BlockA_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\BlockA.Pixmap.res"); return _BlockA_Pixmap; }}
		private static Duality.ContentRef<Duality.Resources.Texture> _BlockA_Texture;
		public static Duality.ContentRef<Duality.Resources.Texture> BlockA_Texture { get { if (_BlockA_Texture.IsExplicitNull) _BlockA_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\BlockA.Texture.res"); return _BlockA_Texture; }}
		private static Duality.ContentRef<Duality.Resources.Scene> _GameScene_Scene;
		public static Duality.ContentRef<Duality.Resources.Scene> GameScene_Scene { get { if (_GameScene_Scene.IsExplicitNull) _GameScene_Scene = Duality.ContentProvider.RequestContent<Duality.Resources.Scene>(@"Data\GameScene.Scene.res"); return _GameScene_Scene; }}
		private static Duality.ContentRef<Duality.Resources.Material> _Wall_Material;
		public static Duality.ContentRef<Duality.Resources.Material> Wall_Material { get { if (_Wall_Material.IsExplicitNull) _Wall_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Wall.Material.res"); return _Wall_Material; }}
		private static Duality.ContentRef<Duality.Resources.Pixmap> _Wall_Pixmap;
		public static Duality.ContentRef<Duality.Resources.Pixmap> Wall_Pixmap { get { if (_Wall_Pixmap.IsExplicitNull) _Wall_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Wall.Pixmap.res"); return _Wall_Pixmap; }}
		private static Duality.ContentRef<Duality.Resources.Texture> _Wall_Texture;
		public static Duality.ContentRef<Duality.Resources.Texture> Wall_Texture { get { if (_Wall_Texture.IsExplicitNull) _Wall_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Wall.Texture.res"); return _Wall_Texture; }}
		public static void LoadAll() {
			BlockA_Material.MakeAvailable();
			BlockA_Pixmap.MakeAvailable();
			BlockA_Texture.MakeAvailable();
			GameScene_Scene.MakeAvailable();
			Wall_Material.MakeAvailable();
			Wall_Pixmap.MakeAvailable();
			Wall_Texture.MakeAvailable();
		}
	}

}
