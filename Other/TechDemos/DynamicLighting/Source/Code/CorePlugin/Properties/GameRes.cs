/*
 * A set of static helper classes that provide easy runtime access to the games resources.
 * This file is auto-generated. Any changes made to it are lost as soon as Duality decides
 * to regenerate it.
 */
namespace GameRes
{
	public static class Data {
		public static class PerPixelLighting {
			public static class SmoothAnim {
				private static Duality.ContentRef<Duality.Resources.FragmentShader> _Light_FragmentShader;
				public static Duality.ContentRef<Duality.Resources.FragmentShader> Light_FragmentShader { get { if (_Light_FragmentShader.IsExplicitNull) _Light_FragmentShader = Duality.ContentProvider.RequestContent<Duality.Resources.FragmentShader>(@"Data\PerPixelLighting\SmoothAnim\Light.FragmentShader.res"); return _Light_FragmentShader; }}
				private static Duality.ContentRef<Duality.Resources.ShaderProgram> _Light_ShaderProgram;
				public static Duality.ContentRef<Duality.Resources.ShaderProgram> Light_ShaderProgram { get { if (_Light_ShaderProgram.IsExplicitNull) _Light_ShaderProgram = Duality.ContentProvider.RequestContent<Duality.Resources.ShaderProgram>(@"Data\PerPixelLighting\SmoothAnim\Light.ShaderProgram.res"); return _Light_ShaderProgram; }}
				private static Duality.ContentRef<Duality.Resources.VertexShader> _Light_VertexShader;
				public static Duality.ContentRef<Duality.Resources.VertexShader> Light_VertexShader { get { if (_Light_VertexShader.IsExplicitNull) _Light_VertexShader = Duality.ContentProvider.RequestContent<Duality.Resources.VertexShader>(@"Data\PerPixelLighting\SmoothAnim\Light.VertexShader.res"); return _Light_VertexShader; }}
				private static Duality.ContentRef<DynamicLighting.LightingTechnique> _Mask_LightingTechnique;
				public static Duality.ContentRef<DynamicLighting.LightingTechnique> Mask_LightingTechnique { get { if (_Mask_LightingTechnique.IsExplicitNull) _Mask_LightingTechnique = Duality.ContentProvider.RequestContent<DynamicLighting.LightingTechnique>(@"Data\PerPixelLighting\SmoothAnim\Mask.LightingTechnique.res"); return _Mask_LightingTechnique; }}
				public static void LoadAll() {
					Light_FragmentShader.MakeAvailable();
					Light_ShaderProgram.MakeAvailable();
					Light_VertexShader.MakeAvailable();
					Mask_LightingTechnique.MakeAvailable();
				}
			}
			private static Duality.ContentRef<Duality.Resources.FragmentShader> _Light_FragmentShader;
			public static Duality.ContentRef<Duality.Resources.FragmentShader> Light_FragmentShader { get { if (_Light_FragmentShader.IsExplicitNull) _Light_FragmentShader = Duality.ContentProvider.RequestContent<Duality.Resources.FragmentShader>(@"Data\PerPixelLighting\Light.FragmentShader.res"); return _Light_FragmentShader; }}
			private static Duality.ContentRef<Duality.Resources.ShaderProgram> _Light_ShaderProgram;
			public static Duality.ContentRef<Duality.Resources.ShaderProgram> Light_ShaderProgram { get { if (_Light_ShaderProgram.IsExplicitNull) _Light_ShaderProgram = Duality.ContentProvider.RequestContent<Duality.Resources.ShaderProgram>(@"Data\PerPixelLighting\Light.ShaderProgram.res"); return _Light_ShaderProgram; }}
			private static Duality.ContentRef<Duality.Resources.VertexShader> _Light_VertexShader;
			public static Duality.ContentRef<Duality.Resources.VertexShader> Light_VertexShader { get { if (_Light_VertexShader.IsExplicitNull) _Light_VertexShader = Duality.ContentProvider.RequestContent<Duality.Resources.VertexShader>(@"Data\PerPixelLighting\Light.VertexShader.res"); return _Light_VertexShader; }}
			private static Duality.ContentRef<DynamicLighting.LightingTechnique> _Mask_LightingTechnique;
			public static Duality.ContentRef<DynamicLighting.LightingTechnique> Mask_LightingTechnique { get { if (_Mask_LightingTechnique.IsExplicitNull) _Mask_LightingTechnique = Duality.ContentProvider.RequestContent<DynamicLighting.LightingTechnique>(@"Data\PerPixelLighting\Mask.LightingTechnique.res"); return _Mask_LightingTechnique; }}
			public static void LoadAll() {
				SmoothAnim.LoadAll();
				Light_FragmentShader.MakeAvailable();
				Light_ShaderProgram.MakeAvailable();
				Light_VertexShader.MakeAvailable();
				Mask_LightingTechnique.MakeAvailable();
			}
		}
		public static class PerVertexLighting {
			public static class SmoothAnim {
				private static Duality.ContentRef<Duality.Resources.FragmentShader> _VertexLight_FragmentShader;
				public static Duality.ContentRef<Duality.Resources.FragmentShader> VertexLight_FragmentShader { get { if (_VertexLight_FragmentShader.IsExplicitNull) _VertexLight_FragmentShader = Duality.ContentProvider.RequestContent<Duality.Resources.FragmentShader>(@"Data\PerVertexLighting\SmoothAnim\VertexLight.FragmentShader.res"); return _VertexLight_FragmentShader; }}
				private static Duality.ContentRef<Duality.Resources.ShaderProgram> _VertexLight_ShaderProgram;
				public static Duality.ContentRef<Duality.Resources.ShaderProgram> VertexLight_ShaderProgram { get { if (_VertexLight_ShaderProgram.IsExplicitNull) _VertexLight_ShaderProgram = Duality.ContentProvider.RequestContent<Duality.Resources.ShaderProgram>(@"Data\PerVertexLighting\SmoothAnim\VertexLight.ShaderProgram.res"); return _VertexLight_ShaderProgram; }}
				private static Duality.ContentRef<Duality.Resources.VertexShader> _VertexLight_VertexShader;
				public static Duality.ContentRef<Duality.Resources.VertexShader> VertexLight_VertexShader { get { if (_VertexLight_VertexShader.IsExplicitNull) _VertexLight_VertexShader = Duality.ContentProvider.RequestContent<Duality.Resources.VertexShader>(@"Data\PerVertexLighting\SmoothAnim\VertexLight.VertexShader.res"); return _VertexLight_VertexShader; }}
				private static Duality.ContentRef<Duality.Resources.DrawTechnique> _VertexLightMask_DrawTechnique;
				public static Duality.ContentRef<Duality.Resources.DrawTechnique> VertexLightMask_DrawTechnique { get { if (_VertexLightMask_DrawTechnique.IsExplicitNull) _VertexLightMask_DrawTechnique = Duality.ContentProvider.RequestContent<Duality.Resources.DrawTechnique>(@"Data\PerVertexLighting\SmoothAnim\VertexLightMask.DrawTechnique.res"); return _VertexLightMask_DrawTechnique; }}
				public static void LoadAll() {
					VertexLight_FragmentShader.MakeAvailable();
					VertexLight_ShaderProgram.MakeAvailable();
					VertexLight_VertexShader.MakeAvailable();
					VertexLightMask_DrawTechnique.MakeAvailable();
				}
			}
			private static Duality.ContentRef<Duality.Resources.FragmentShader> _VertexLight_FragmentShader;
			public static Duality.ContentRef<Duality.Resources.FragmentShader> VertexLight_FragmentShader { get { if (_VertexLight_FragmentShader.IsExplicitNull) _VertexLight_FragmentShader = Duality.ContentProvider.RequestContent<Duality.Resources.FragmentShader>(@"Data\PerVertexLighting\VertexLight.FragmentShader.res"); return _VertexLight_FragmentShader; }}
			private static Duality.ContentRef<Duality.Resources.ShaderProgram> _VertexLight_ShaderProgram;
			public static Duality.ContentRef<Duality.Resources.ShaderProgram> VertexLight_ShaderProgram { get { if (_VertexLight_ShaderProgram.IsExplicitNull) _VertexLight_ShaderProgram = Duality.ContentProvider.RequestContent<Duality.Resources.ShaderProgram>(@"Data\PerVertexLighting\VertexLight.ShaderProgram.res"); return _VertexLight_ShaderProgram; }}
			private static Duality.ContentRef<Duality.Resources.VertexShader> _VertexLight_VertexShader;
			public static Duality.ContentRef<Duality.Resources.VertexShader> VertexLight_VertexShader { get { if (_VertexLight_VertexShader.IsExplicitNull) _VertexLight_VertexShader = Duality.ContentProvider.RequestContent<Duality.Resources.VertexShader>(@"Data\PerVertexLighting\VertexLight.VertexShader.res"); return _VertexLight_VertexShader; }}
			private static Duality.ContentRef<Duality.Resources.DrawTechnique> _VertexLightMask_DrawTechnique;
			public static Duality.ContentRef<Duality.Resources.DrawTechnique> VertexLightMask_DrawTechnique { get { if (_VertexLightMask_DrawTechnique.IsExplicitNull) _VertexLightMask_DrawTechnique = Duality.ContentProvider.RequestContent<Duality.Resources.DrawTechnique>(@"Data\PerVertexLighting\VertexLightMask.DrawTechnique.res"); return _VertexLightMask_DrawTechnique; }}
			public static void LoadAll() {
				SmoothAnim.LoadAll();
				VertexLight_FragmentShader.MakeAvailable();
				VertexLight_ShaderProgram.MakeAvailable();
				VertexLight_VertexShader.MakeAvailable();
				VertexLightMask_DrawTechnique.MakeAvailable();
			}
		}
		public static class PseudoHdr {
			private static Duality.ContentRef<Duality.Resources.RenderTarget> _ScreenRT_RenderTarget;
			public static Duality.ContentRef<Duality.Resources.RenderTarget> ScreenRT_RenderTarget { get { if (_ScreenRT_RenderTarget.IsExplicitNull) _ScreenRT_RenderTarget = Duality.ContentProvider.RequestContent<Duality.Resources.RenderTarget>(@"Data\PseudoHdr\ScreenRT.RenderTarget.res"); return _ScreenRT_RenderTarget; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _ScreenTex_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> ScreenTex_Texture { get { if (_ScreenTex_Texture.IsExplicitNull) _ScreenTex_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\PseudoHdr\ScreenTex.Texture.res"); return _ScreenTex_Texture; }}
			private static Duality.ContentRef<Duality.Resources.DrawTechnique> _Tonemapping_DrawTechnique;
			public static Duality.ContentRef<Duality.Resources.DrawTechnique> Tonemapping_DrawTechnique { get { if (_Tonemapping_DrawTechnique.IsExplicitNull) _Tonemapping_DrawTechnique = Duality.ContentProvider.RequestContent<Duality.Resources.DrawTechnique>(@"Data\PseudoHdr\Tonemapping.DrawTechnique.res"); return _Tonemapping_DrawTechnique; }}
			private static Duality.ContentRef<Duality.Resources.FragmentShader> _Tonemapping_FragmentShader;
			public static Duality.ContentRef<Duality.Resources.FragmentShader> Tonemapping_FragmentShader { get { if (_Tonemapping_FragmentShader.IsExplicitNull) _Tonemapping_FragmentShader = Duality.ContentProvider.RequestContent<Duality.Resources.FragmentShader>(@"Data\PseudoHdr\Tonemapping.FragmentShader.res"); return _Tonemapping_FragmentShader; }}
			private static Duality.ContentRef<Duality.Resources.ShaderProgram> _Tonemapping_ShaderProgram;
			public static Duality.ContentRef<Duality.Resources.ShaderProgram> Tonemapping_ShaderProgram { get { if (_Tonemapping_ShaderProgram.IsExplicitNull) _Tonemapping_ShaderProgram = Duality.ContentProvider.RequestContent<Duality.Resources.ShaderProgram>(@"Data\PseudoHdr\Tonemapping.ShaderProgram.res"); return _Tonemapping_ShaderProgram; }}
			public static void LoadAll() {
				ScreenRT_RenderTarget.MakeAvailable();
				ScreenTex_Texture.MakeAvailable();
				Tonemapping_DrawTechnique.MakeAvailable();
				Tonemapping_FragmentShader.MakeAvailable();
				Tonemapping_ShaderProgram.MakeAvailable();
			}
		}
		private static Duality.ContentRef<Duality.Resources.Material> _LightBall_Material;
		public static Duality.ContentRef<Duality.Resources.Material> LightBall_Material { get { if (_LightBall_Material.IsExplicitNull) _LightBall_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\LightBall.Material.res"); return _LightBall_Material; }}
		private static Duality.ContentRef<Duality.Resources.Material> _LightBallVertex_Material;
		public static Duality.ContentRef<Duality.Resources.Material> LightBallVertex_Material { get { if (_LightBallVertex_Material.IsExplicitNull) _LightBallVertex_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\LightBallVertex.Material.res"); return _LightBallVertex_Material; }}
		private static Duality.ContentRef<Duality.Resources.Material> _LightShip_Material;
		public static Duality.ContentRef<Duality.Resources.Material> LightShip_Material { get { if (_LightShip_Material.IsExplicitNull) _LightShip_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\LightShip.Material.res"); return _LightShip_Material; }}
		private static Duality.ContentRef<Duality.Resources.Material> _LightShip3_Material;
		public static Duality.ContentRef<Duality.Resources.Material> LightShip3_Material { get { if (_LightShip3_Material.IsExplicitNull) _LightShip3_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\LightShip3.Material.res"); return _LightShip3_Material; }}
		private static Duality.ContentRef<Duality.Resources.Material> _LightShipSmoothAnim_Material;
		public static Duality.ContentRef<Duality.Resources.Material> LightShipSmoothAnim_Material { get { if (_LightShipSmoothAnim_Material.IsExplicitNull) _LightShipSmoothAnim_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\LightShipSmoothAnim.Material.res"); return _LightShipSmoothAnim_Material; }}
		private static Duality.ContentRef<Duality.Resources.Material> _LightShipVertexSmoothAnim_Material;
		public static Duality.ContentRef<Duality.Resources.Material> LightShipVertexSmoothAnim_Material { get { if (_LightShipVertexSmoothAnim_Material.IsExplicitNull) _LightShipVertexSmoothAnim_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\LightShipVertexSmoothAnim.Material.res"); return _LightShipVertexSmoothAnim_Material; }}
		private static Duality.ContentRef<Duality.Resources.Pixmap> _normalBall_Pixmap;
		public static Duality.ContentRef<Duality.Resources.Pixmap> normalBall_Pixmap { get { if (_normalBall_Pixmap.IsExplicitNull) _normalBall_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\normalBall.Pixmap.res"); return _normalBall_Pixmap; }}
		private static Duality.ContentRef<Duality.Resources.Texture> _normalBall_Texture;
		public static Duality.ContentRef<Duality.Resources.Texture> normalBall_Texture { get { if (_normalBall_Texture.IsExplicitNull) _normalBall_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\normalBall.Texture.res"); return _normalBall_Texture; }}
		private static Duality.ContentRef<Duality.Resources.Pixmap> _ship3_c_Pixmap;
		public static Duality.ContentRef<Duality.Resources.Pixmap> ship3_c_Pixmap { get { if (_ship3_c_Pixmap.IsExplicitNull) _ship3_c_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\ship3_c.Pixmap.res"); return _ship3_c_Pixmap; }}
		private static Duality.ContentRef<Duality.Resources.Texture> _ship3_c_Texture;
		public static Duality.ContentRef<Duality.Resources.Texture> ship3_c_Texture { get { if (_ship3_c_Texture.IsExplicitNull) _ship3_c_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\ship3_c.Texture.res"); return _ship3_c_Texture; }}
		private static Duality.ContentRef<Duality.Resources.Pixmap> _ship3_n_Pixmap;
		public static Duality.ContentRef<Duality.Resources.Pixmap> ship3_n_Pixmap { get { if (_ship3_n_Pixmap.IsExplicitNull) _ship3_n_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\ship3_n.Pixmap.res"); return _ship3_n_Pixmap; }}
		private static Duality.ContentRef<Duality.Resources.Texture> _ship3_n_Texture;
		public static Duality.ContentRef<Duality.Resources.Texture> ship3_n_Texture { get { if (_ship3_n_Texture.IsExplicitNull) _ship3_n_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\ship3_n.Texture.res"); return _ship3_n_Texture; }}
		private static Duality.ContentRef<Duality.Resources.Pixmap> _ship3_s_Pixmap;
		public static Duality.ContentRef<Duality.Resources.Pixmap> ship3_s_Pixmap { get { if (_ship3_s_Pixmap.IsExplicitNull) _ship3_s_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\ship3_s.Pixmap.res"); return _ship3_s_Pixmap; }}
		private static Duality.ContentRef<Duality.Resources.Texture> _ship3_s_Texture;
		public static Duality.ContentRef<Duality.Resources.Texture> ship3_s_Texture { get { if (_ship3_s_Texture.IsExplicitNull) _ship3_s_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\ship3_s.Texture.res"); return _ship3_s_Texture; }}
		private static Duality.ContentRef<Duality.Resources.Pixmap> _ship_c_Pixmap;
		public static Duality.ContentRef<Duality.Resources.Pixmap> ship_c_Pixmap { get { if (_ship_c_Pixmap.IsExplicitNull) _ship_c_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\ship_c.Pixmap.res"); return _ship_c_Pixmap; }}
		private static Duality.ContentRef<Duality.Resources.Texture> _ship_c_Texture;
		public static Duality.ContentRef<Duality.Resources.Texture> ship_c_Texture { get { if (_ship_c_Texture.IsExplicitNull) _ship_c_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\ship_c.Texture.res"); return _ship_c_Texture; }}
		private static Duality.ContentRef<Duality.Resources.Texture> _ship_c_anim_Texture;
		public static Duality.ContentRef<Duality.Resources.Texture> ship_c_anim_Texture { get { if (_ship_c_anim_Texture.IsExplicitNull) _ship_c_anim_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\ship_c_anim.Texture.res"); return _ship_c_anim_Texture; }}
		private static Duality.ContentRef<Duality.Resources.Pixmap> _ship_n_Pixmap;
		public static Duality.ContentRef<Duality.Resources.Pixmap> ship_n_Pixmap { get { if (_ship_n_Pixmap.IsExplicitNull) _ship_n_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\ship_n.Pixmap.res"); return _ship_n_Pixmap; }}
		private static Duality.ContentRef<Duality.Resources.Texture> _ship_n_Texture;
		public static Duality.ContentRef<Duality.Resources.Texture> ship_n_Texture { get { if (_ship_n_Texture.IsExplicitNull) _ship_n_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\ship_n.Texture.res"); return _ship_n_Texture; }}
		private static Duality.ContentRef<Duality.Resources.Pixmap> _ship_s_Pixmap;
		public static Duality.ContentRef<Duality.Resources.Pixmap> ship_s_Pixmap { get { if (_ship_s_Pixmap.IsExplicitNull) _ship_s_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\ship_s.Pixmap.res"); return _ship_s_Pixmap; }}
		private static Duality.ContentRef<Duality.Resources.Texture> _ship_s_Texture;
		public static Duality.ContentRef<Duality.Resources.Texture> ship_s_Texture { get { if (_ship_s_Texture.IsExplicitNull) _ship_s_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\ship_s.Texture.res"); return _ship_s_Texture; }}
		private static Duality.ContentRef<Duality.Resources.Scene> _TestScene_Scene;
		public static Duality.ContentRef<Duality.Resources.Scene> TestScene_Scene { get { if (_TestScene_Scene.IsExplicitNull) _TestScene_Scene = Duality.ContentProvider.RequestContent<Duality.Resources.Scene>(@"Data\TestScene.Scene.res"); return _TestScene_Scene; }}
		public static void LoadAll() {
			PerPixelLighting.LoadAll();
			PerVertexLighting.LoadAll();
			PseudoHdr.LoadAll();
			LightBall_Material.MakeAvailable();
			LightBallVertex_Material.MakeAvailable();
			LightShip_Material.MakeAvailable();
			LightShip3_Material.MakeAvailable();
			LightShipSmoothAnim_Material.MakeAvailable();
			LightShipVertexSmoothAnim_Material.MakeAvailable();
			normalBall_Pixmap.MakeAvailable();
			normalBall_Texture.MakeAvailable();
			ship3_c_Pixmap.MakeAvailable();
			ship3_c_Texture.MakeAvailable();
			ship3_n_Pixmap.MakeAvailable();
			ship3_n_Texture.MakeAvailable();
			ship3_s_Pixmap.MakeAvailable();
			ship3_s_Texture.MakeAvailable();
			ship_c_Pixmap.MakeAvailable();
			ship_c_Texture.MakeAvailable();
			ship_c_anim_Texture.MakeAvailable();
			ship_n_Pixmap.MakeAvailable();
			ship_n_Texture.MakeAvailable();
			ship_s_Pixmap.MakeAvailable();
			ship_s_Texture.MakeAvailable();
			TestScene_Scene.MakeAvailable();
		}
	}

}
