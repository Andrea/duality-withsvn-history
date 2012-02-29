/*
 * A set of static helper classes that provide easy runtime access to the games resources.
 * This file is auto-generated. Any changes made to it are lost as soon as Duality decides
 * to regenerate it.
 */
namespace GameRes
{
	public static class Data {
		public static class AudioData {
			private static Duality.ContentRef<Duality.Resources.AudioData> _CollectPowerup_AudioData;
			public static Duality.ContentRef<Duality.Resources.AudioData> CollectPowerup_AudioData { get { if (_CollectPowerup_AudioData.IsExplicitNull) _CollectPowerup_AudioData = Duality.ContentProvider.RequestContent<Duality.Resources.AudioData>(@"Data\AudioData\CollectPowerup.AudioData.res"); return _CollectPowerup_AudioData; }}
			private static Duality.ContentRef<Duality.Resources.AudioData> _Engine_AudioData;
			public static Duality.ContentRef<Duality.Resources.AudioData> Engine_AudioData { get { if (_Engine_AudioData.IsExplicitNull) _Engine_AudioData = Duality.ContentProvider.RequestContent<Duality.Resources.AudioData>(@"Data\AudioData\Engine.AudioData.res"); return _Engine_AudioData; }}
			private static Duality.ContentRef<Duality.Resources.AudioData> _Explo1_AudioData;
			public static Duality.ContentRef<Duality.Resources.AudioData> Explo1_AudioData { get { if (_Explo1_AudioData.IsExplicitNull) _Explo1_AudioData = Duality.ContentProvider.RequestContent<Duality.Resources.AudioData>(@"Data\AudioData\Explo1.AudioData.res"); return _Explo1_AudioData; }}
			private static Duality.ContentRef<Duality.Resources.AudioData> _Explo2_AudioData;
			public static Duality.ContentRef<Duality.Resources.AudioData> Explo2_AudioData { get { if (_Explo2_AudioData.IsExplicitNull) _Explo2_AudioData = Duality.ContentProvider.RequestContent<Duality.Resources.AudioData>(@"Data\AudioData\Explo2.AudioData.res"); return _Explo2_AudioData; }}
			private static Duality.ContentRef<Duality.Resources.AudioData> _Explo3_AudioData;
			public static Duality.ContentRef<Duality.Resources.AudioData> Explo3_AudioData { get { if (_Explo3_AudioData.IsExplicitNull) _Explo3_AudioData = Duality.ContentProvider.RequestContent<Duality.Resources.AudioData>(@"Data\AudioData\Explo3.AudioData.res"); return _Explo3_AudioData; }}
			private static Duality.ContentRef<Duality.Resources.AudioData> _Explo4_AudioData;
			public static Duality.ContentRef<Duality.Resources.AudioData> Explo4_AudioData { get { if (_Explo4_AudioData.IsExplicitNull) _Explo4_AudioData = Duality.ContentProvider.RequestContent<Duality.Resources.AudioData>(@"Data\AudioData\Explo4.AudioData.res"); return _Explo4_AudioData; }}
			private static Duality.ContentRef<Duality.Resources.AudioData> _Explo5_AudioData;
			public static Duality.ContentRef<Duality.Resources.AudioData> Explo5_AudioData { get { if (_Explo5_AudioData.IsExplicitNull) _Explo5_AudioData = Duality.ContentProvider.RequestContent<Duality.Resources.AudioData>(@"Data\AudioData\Explo5.AudioData.res"); return _Explo5_AudioData; }}
			private static Duality.ContentRef<Duality.Resources.AudioData> _FireWeapon_AudioData;
			public static Duality.ContentRef<Duality.Resources.AudioData> FireWeapon_AudioData { get { if (_FireWeapon_AudioData.IsExplicitNull) _FireWeapon_AudioData = Duality.ContentProvider.RequestContent<Duality.Resources.AudioData>(@"Data\AudioData\FireWeapon.AudioData.res"); return _FireWeapon_AudioData; }}
			private static Duality.ContentRef<Duality.Resources.AudioData> _HitAsteroid_AudioData;
			public static Duality.ContentRef<Duality.Resources.AudioData> HitAsteroid_AudioData { get { if (_HitAsteroid_AudioData.IsExplicitNull) _HitAsteroid_AudioData = Duality.ContentProvider.RequestContent<Duality.Resources.AudioData>(@"Data\AudioData\HitAsteroid.AudioData.res"); return _HitAsteroid_AudioData; }}
			private static Duality.ContentRef<Duality.Resources.AudioData> _HitWeapon_AudioData;
			public static Duality.ContentRef<Duality.Resources.AudioData> HitWeapon_AudioData { get { if (_HitWeapon_AudioData.IsExplicitNull) _HitWeapon_AudioData = Duality.ContentProvider.RequestContent<Duality.Resources.AudioData>(@"Data\AudioData\HitWeapon.AudioData.res"); return _HitWeapon_AudioData; }}
			private static Duality.ContentRef<Duality.Resources.AudioData> _Level_AudioData;
			public static Duality.ContentRef<Duality.Resources.AudioData> Level_AudioData { get { if (_Level_AudioData.IsExplicitNull) _Level_AudioData = Duality.ContentProvider.RequestContent<Duality.Resources.AudioData>(@"Data\AudioData\Level.AudioData.res"); return _Level_AudioData; }}
			public static void LoadAll() {
				CollectPowerup_AudioData.MakeAvailable();
				Engine_AudioData.MakeAvailable();
				Explo1_AudioData.MakeAvailable();
				Explo2_AudioData.MakeAvailable();
				Explo3_AudioData.MakeAvailable();
				Explo4_AudioData.MakeAvailable();
				Explo5_AudioData.MakeAvailable();
				FireWeapon_AudioData.MakeAvailable();
				HitAsteroid_AudioData.MakeAvailable();
				HitWeapon_AudioData.MakeAvailable();
				Level_AudioData.MakeAvailable();
			}
		}
		public static class Fonts {
			private static Duality.ContentRef<Duality.Resources.Font> _Highscore_Font;
			public static Duality.ContentRef<Duality.Resources.Font> Highscore_Font { get { if (_Highscore_Font.IsExplicitNull) _Highscore_Font = Duality.ContentProvider.RequestContent<Duality.Resources.Font>(@"Data\Fonts\Highscore.Font.res"); return _Highscore_Font; }}
			private static Duality.ContentRef<Duality.Resources.Font> _HUD_Font;
			public static Duality.ContentRef<Duality.Resources.Font> HUD_Font { get { if (_HUD_Font.IsExplicitNull) _HUD_Font = Duality.ContentProvider.RequestContent<Duality.Resources.Font>(@"Data\Fonts\HUD.Font.res"); return _HUD_Font; }}
			private static Duality.ContentRef<Duality.Resources.Font> _HUD_Small_Font;
			public static Duality.ContentRef<Duality.Resources.Font> HUD_Small_Font { get { if (_HUD_Small_Font.IsExplicitNull) _HUD_Small_Font = Duality.ContentProvider.RequestContent<Duality.Resources.Font>(@"Data\Fonts\HUD_Small.Font.res"); return _HUD_Small_Font; }}
			public static void LoadAll() {
				Highscore_Font.MakeAvailable();
				HUD_Font.MakeAvailable();
				HUD_Small_Font.MakeAvailable();
			}
		}
		public static class Materials {
			private static Duality.ContentRef<Duality.Resources.Material> _BigAsteroid_Material;
			public static Duality.ContentRef<Duality.Resources.Material> BigAsteroid_Material { get { if (_BigAsteroid_Material.IsExplicitNull) _BigAsteroid_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\BigAsteroid.Material.res"); return _BigAsteroid_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _BigAsteroid2_Material;
			public static Duality.ContentRef<Duality.Resources.Material> BigAsteroid2_Material { get { if (_BigAsteroid2_Material.IsExplicitNull) _BigAsteroid2_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\BigAsteroid2.Material.res"); return _BigAsteroid2_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _BigAsteroid3_Material;
			public static Duality.ContentRef<Duality.Resources.Material> BigAsteroid3_Material { get { if (_BigAsteroid3_Material.IsExplicitNull) _BigAsteroid3_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\BigAsteroid3.Material.res"); return _BigAsteroid3_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _Explo_Material;
			public static Duality.ContentRef<Duality.Resources.Material> Explo_Material { get { if (_Explo_Material.IsExplicitNull) _Explo_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\Explo.Material.res"); return _Explo_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _MediumAsteroid_Material;
			public static Duality.ContentRef<Duality.Resources.Material> MediumAsteroid_Material { get { if (_MediumAsteroid_Material.IsExplicitNull) _MediumAsteroid_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\MediumAsteroid.Material.res"); return _MediumAsteroid_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _PlayerShip_Material;
			public static Duality.ContentRef<Duality.Resources.Material> PlayerShip_Material { get { if (_PlayerShip_Material.IsExplicitNull) _PlayerShip_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\PlayerShip.Material.res"); return _PlayerShip_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _PowerupDiagonalCannon_Material;
			public static Duality.ContentRef<Duality.Resources.Material> PowerupDiagonalCannon_Material { get { if (_PowerupDiagonalCannon_Material.IsExplicitNull) _PowerupDiagonalCannon_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\PowerupDiagonalCannon.Material.res"); return _PowerupDiagonalCannon_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _PowerupFrontCannon_Material;
			public static Duality.ContentRef<Duality.Resources.Material> PowerupFrontCannon_Material { get { if (_PowerupFrontCannon_Material.IsExplicitNull) _PowerupFrontCannon_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\PowerupFrontCannon.Material.res"); return _PowerupFrontCannon_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _PowerupKillAll_Material;
			public static Duality.ContentRef<Duality.Resources.Material> PowerupKillAll_Material { get { if (_PowerupKillAll_Material.IsExplicitNull) _PowerupKillAll_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\PowerupKillAll.Material.res"); return _PowerupKillAll_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _PowerupSideCannon_Material;
			public static Duality.ContentRef<Duality.Resources.Material> PowerupSideCannon_Material { get { if (_PowerupSideCannon_Material.IsExplicitNull) _PowerupSideCannon_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\PowerupSideCannon.Material.res"); return _PowerupSideCannon_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _Projectile_Material;
			public static Duality.ContentRef<Duality.Resources.Material> Projectile_Material { get { if (_Projectile_Material.IsExplicitNull) _Projectile_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\Projectile.Material.res"); return _Projectile_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _SmallAsteroid_Material;
			public static Duality.ContentRef<Duality.Resources.Material> SmallAsteroid_Material { get { if (_SmallAsteroid_Material.IsExplicitNull) _SmallAsteroid_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\SmallAsteroid.Material.res"); return _SmallAsteroid_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _SpaceBg_Material;
			public static Duality.ContentRef<Duality.Resources.Material> SpaceBg_Material { get { if (_SpaceBg_Material.IsExplicitNull) _SpaceBg_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\SpaceBg.Material.res"); return _SpaceBg_Material; }}
			private static Duality.ContentRef<Duality.Resources.Material> _Wall_Material;
			public static Duality.ContentRef<Duality.Resources.Material> Wall_Material { get { if (_Wall_Material.IsExplicitNull) _Wall_Material = Duality.ContentProvider.RequestContent<Duality.Resources.Material>(@"Data\Materials\Wall.Material.res"); return _Wall_Material; }}
			public static void LoadAll() {
				BigAsteroid_Material.MakeAvailable();
				BigAsteroid2_Material.MakeAvailable();
				BigAsteroid3_Material.MakeAvailable();
				Explo_Material.MakeAvailable();
				MediumAsteroid_Material.MakeAvailable();
				PlayerShip_Material.MakeAvailable();
				PowerupDiagonalCannon_Material.MakeAvailable();
				PowerupFrontCannon_Material.MakeAvailable();
				PowerupKillAll_Material.MakeAvailable();
				PowerupSideCannon_Material.MakeAvailable();
				Projectile_Material.MakeAvailable();
				SmallAsteroid_Material.MakeAvailable();
				SpaceBg_Material.MakeAvailable();
				Wall_Material.MakeAvailable();
			}
		}
		public static class Pixmaps {
			private static Duality.ContentRef<Duality.Resources.Pixmap> _BigAsteroid_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> BigAsteroid_Pixmap { get { if (_BigAsteroid_Pixmap.IsExplicitNull) _BigAsteroid_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\BigAsteroid.Pixmap.res"); return _BigAsteroid_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _BigAsteroid2_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> BigAsteroid2_Pixmap { get { if (_BigAsteroid2_Pixmap.IsExplicitNull) _BigAsteroid2_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\BigAsteroid2.Pixmap.res"); return _BigAsteroid2_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _BigAsteroid3_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> BigAsteroid3_Pixmap { get { if (_BigAsteroid3_Pixmap.IsExplicitNull) _BigAsteroid3_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\BigAsteroid3.Pixmap.res"); return _BigAsteroid3_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _Explo_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> Explo_Pixmap { get { if (_Explo_Pixmap.IsExplicitNull) _Explo_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\Explo.Pixmap.res"); return _Explo_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _MediumAsteroid_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> MediumAsteroid_Pixmap { get { if (_MediumAsteroid_Pixmap.IsExplicitNull) _MediumAsteroid_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\MediumAsteroid.Pixmap.res"); return _MediumAsteroid_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _PlayerShip_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> PlayerShip_Pixmap { get { if (_PlayerShip_Pixmap.IsExplicitNull) _PlayerShip_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\PlayerShip.Pixmap.res"); return _PlayerShip_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _PowerupDiagonalCannon_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> PowerupDiagonalCannon_Pixmap { get { if (_PowerupDiagonalCannon_Pixmap.IsExplicitNull) _PowerupDiagonalCannon_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\PowerupDiagonalCannon.Pixmap.res"); return _PowerupDiagonalCannon_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _PowerupFrontCannon_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> PowerupFrontCannon_Pixmap { get { if (_PowerupFrontCannon_Pixmap.IsExplicitNull) _PowerupFrontCannon_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\PowerupFrontCannon.Pixmap.res"); return _PowerupFrontCannon_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _PowerupKillAll_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> PowerupKillAll_Pixmap { get { if (_PowerupKillAll_Pixmap.IsExplicitNull) _PowerupKillAll_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\PowerupKillAll.Pixmap.res"); return _PowerupKillAll_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _PowerupSideCannon_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> PowerupSideCannon_Pixmap { get { if (_PowerupSideCannon_Pixmap.IsExplicitNull) _PowerupSideCannon_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\PowerupSideCannon.Pixmap.res"); return _PowerupSideCannon_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _Projectile_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> Projectile_Pixmap { get { if (_Projectile_Pixmap.IsExplicitNull) _Projectile_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\Projectile.Pixmap.res"); return _Projectile_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _SmallAsteroid_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> SmallAsteroid_Pixmap { get { if (_SmallAsteroid_Pixmap.IsExplicitNull) _SmallAsteroid_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\SmallAsteroid.Pixmap.res"); return _SmallAsteroid_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _SpaceBg_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> SpaceBg_Pixmap { get { if (_SpaceBg_Pixmap.IsExplicitNull) _SpaceBg_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\SpaceBg.Pixmap.res"); return _SpaceBg_Pixmap; }}
			private static Duality.ContentRef<Duality.Resources.Pixmap> _Wall_Pixmap;
			public static Duality.ContentRef<Duality.Resources.Pixmap> Wall_Pixmap { get { if (_Wall_Pixmap.IsExplicitNull) _Wall_Pixmap = Duality.ContentProvider.RequestContent<Duality.Resources.Pixmap>(@"Data\Pixmaps\Wall.Pixmap.res"); return _Wall_Pixmap; }}
			public static void LoadAll() {
				BigAsteroid_Pixmap.MakeAvailable();
				BigAsteroid2_Pixmap.MakeAvailable();
				BigAsteroid3_Pixmap.MakeAvailable();
				Explo_Pixmap.MakeAvailable();
				MediumAsteroid_Pixmap.MakeAvailable();
				PlayerShip_Pixmap.MakeAvailable();
				PowerupDiagonalCannon_Pixmap.MakeAvailable();
				PowerupFrontCannon_Pixmap.MakeAvailable();
				PowerupKillAll_Pixmap.MakeAvailable();
				PowerupSideCannon_Pixmap.MakeAvailable();
				Projectile_Pixmap.MakeAvailable();
				SmallAsteroid_Pixmap.MakeAvailable();
				SpaceBg_Pixmap.MakeAvailable();
				Wall_Pixmap.MakeAvailable();
			}
		}
		public static class Prefabs {
			public static class Powerups {
				private static Duality.ContentRef<Duality.Resources.Prefab> _DiagonalCannon_Prefab;
				public static Duality.ContentRef<Duality.Resources.Prefab> DiagonalCannon_Prefab { get { if (_DiagonalCannon_Prefab.IsExplicitNull) _DiagonalCannon_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\Powerups\DiagonalCannon.Prefab.res"); return _DiagonalCannon_Prefab; }}
				private static Duality.ContentRef<Duality.Resources.Prefab> _FrontCannon_Prefab;
				public static Duality.ContentRef<Duality.Resources.Prefab> FrontCannon_Prefab { get { if (_FrontCannon_Prefab.IsExplicitNull) _FrontCannon_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\Powerups\FrontCannon.Prefab.res"); return _FrontCannon_Prefab; }}
				private static Duality.ContentRef<Duality.Resources.Prefab> _KillAll_Prefab;
				public static Duality.ContentRef<Duality.Resources.Prefab> KillAll_Prefab { get { if (_KillAll_Prefab.IsExplicitNull) _KillAll_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\Powerups\KillAll.Prefab.res"); return _KillAll_Prefab; }}
				private static Duality.ContentRef<Duality.Resources.Prefab> _SideCannon_Prefab;
				public static Duality.ContentRef<Duality.Resources.Prefab> SideCannon_Prefab { get { if (_SideCannon_Prefab.IsExplicitNull) _SideCannon_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\Powerups\SideCannon.Prefab.res"); return _SideCannon_Prefab; }}
				public static void LoadAll() {
					DiagonalCannon_Prefab.MakeAvailable();
					FrontCannon_Prefab.MakeAvailable();
					KillAll_Prefab.MakeAvailable();
					SideCannon_Prefab.MakeAvailable();
				}
			}
			private static Duality.ContentRef<Duality.Resources.Prefab> _AsteroidBig1_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> AsteroidBig1_Prefab { get { if (_AsteroidBig1_Prefab.IsExplicitNull) _AsteroidBig1_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\AsteroidBig1.Prefab.res"); return _AsteroidBig1_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _AsteroidBig2_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> AsteroidBig2_Prefab { get { if (_AsteroidBig2_Prefab.IsExplicitNull) _AsteroidBig2_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\AsteroidBig2.Prefab.res"); return _AsteroidBig2_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _AsteroidBig3_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> AsteroidBig3_Prefab { get { if (_AsteroidBig3_Prefab.IsExplicitNull) _AsteroidBig3_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\AsteroidBig3.Prefab.res"); return _AsteroidBig3_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _AsteroidMedium_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> AsteroidMedium_Prefab { get { if (_AsteroidMedium_Prefab.IsExplicitNull) _AsteroidMedium_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\AsteroidMedium.Prefab.res"); return _AsteroidMedium_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _AsteroidMediumBlue_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> AsteroidMediumBlue_Prefab { get { if (_AsteroidMediumBlue_Prefab.IsExplicitNull) _AsteroidMediumBlue_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\AsteroidMediumBlue.Prefab.res"); return _AsteroidMediumBlue_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _AsteroidMediumGreen_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> AsteroidMediumGreen_Prefab { get { if (_AsteroidMediumGreen_Prefab.IsExplicitNull) _AsteroidMediumGreen_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\AsteroidMediumGreen.Prefab.res"); return _AsteroidMediumGreen_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _AsteroidSmall_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> AsteroidSmall_Prefab { get { if (_AsteroidSmall_Prefab.IsExplicitNull) _AsteroidSmall_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\AsteroidSmall.Prefab.res"); return _AsteroidSmall_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _AsteroidSmallBlue_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> AsteroidSmallBlue_Prefab { get { if (_AsteroidSmallBlue_Prefab.IsExplicitNull) _AsteroidSmallBlue_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\AsteroidSmallBlue.Prefab.res"); return _AsteroidSmallBlue_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _AsteroidSmallGreen_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> AsteroidSmallGreen_Prefab { get { if (_AsteroidSmallGreen_Prefab.IsExplicitNull) _AsteroidSmallGreen_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\AsteroidSmallGreen.Prefab.res"); return _AsteroidSmallGreen_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _ExploEffect_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> ExploEffect_Prefab { get { if (_ExploEffect_Prefab.IsExplicitNull) _ExploEffect_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\ExploEffect.Prefab.res"); return _ExploEffect_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _PowerupEffect_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> PowerupEffect_Prefab { get { if (_PowerupEffect_Prefab.IsExplicitNull) _PowerupEffect_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\PowerupEffect.Prefab.res"); return _PowerupEffect_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _Projectile_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> Projectile_Prefab { get { if (_Projectile_Prefab.IsExplicitNull) _Projectile_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\Projectile.Prefab.res"); return _Projectile_Prefab; }}
			private static Duality.ContentRef<Duality.Resources.Prefab> _Wall_Prefab;
			public static Duality.ContentRef<Duality.Resources.Prefab> Wall_Prefab { get { if (_Wall_Prefab.IsExplicitNull) _Wall_Prefab = Duality.ContentProvider.RequestContent<Duality.Resources.Prefab>(@"Data\Prefabs\Wall.Prefab.res"); return _Wall_Prefab; }}
			public static void LoadAll() {
				Powerups.LoadAll();
				AsteroidBig1_Prefab.MakeAvailable();
				AsteroidBig2_Prefab.MakeAvailable();
				AsteroidBig3_Prefab.MakeAvailable();
				AsteroidMedium_Prefab.MakeAvailable();
				AsteroidMediumBlue_Prefab.MakeAvailable();
				AsteroidMediumGreen_Prefab.MakeAvailable();
				AsteroidSmall_Prefab.MakeAvailable();
				AsteroidSmallBlue_Prefab.MakeAvailable();
				AsteroidSmallGreen_Prefab.MakeAvailable();
				ExploEffect_Prefab.MakeAvailable();
				PowerupEffect_Prefab.MakeAvailable();
				Projectile_Prefab.MakeAvailable();
				Wall_Prefab.MakeAvailable();
			}
		}
		public static class Scenes {
			private static Duality.ContentRef<Duality.Resources.Scene> _GameScene_Scene;
			public static Duality.ContentRef<Duality.Resources.Scene> GameScene_Scene { get { if (_GameScene_Scene.IsExplicitNull) _GameScene_Scene = Duality.ContentProvider.RequestContent<Duality.Resources.Scene>(@"Data\Scenes\GameScene.Scene.res"); return _GameScene_Scene; }}
			private static Duality.ContentRef<Duality.Resources.Scene> _MenuScene_Scene;
			public static Duality.ContentRef<Duality.Resources.Scene> MenuScene_Scene { get { if (_MenuScene_Scene.IsExplicitNull) _MenuScene_Scene = Duality.ContentProvider.RequestContent<Duality.Resources.Scene>(@"Data\Scenes\MenuScene.Scene.res"); return _MenuScene_Scene; }}
			public static void LoadAll() {
				GameScene_Scene.MakeAvailable();
				MenuScene_Scene.MakeAvailable();
			}
		}
		public static class Sound {
			private static Duality.ContentRef<Duality.Resources.Sound> _CollectPowerup_Sound;
			public static Duality.ContentRef<Duality.Resources.Sound> CollectPowerup_Sound { get { if (_CollectPowerup_Sound.IsExplicitNull) _CollectPowerup_Sound = Duality.ContentProvider.RequestContent<Duality.Resources.Sound>(@"Data\Sound\CollectPowerup.Sound.res"); return _CollectPowerup_Sound; }}
			private static Duality.ContentRef<Duality.Resources.Sound> _Engine_Sound;
			public static Duality.ContentRef<Duality.Resources.Sound> Engine_Sound { get { if (_Engine_Sound.IsExplicitNull) _Engine_Sound = Duality.ContentProvider.RequestContent<Duality.Resources.Sound>(@"Data\Sound\Engine.Sound.res"); return _Engine_Sound; }}
			private static Duality.ContentRef<Duality.Resources.Sound> _Explo1_Sound;
			public static Duality.ContentRef<Duality.Resources.Sound> Explo1_Sound { get { if (_Explo1_Sound.IsExplicitNull) _Explo1_Sound = Duality.ContentProvider.RequestContent<Duality.Resources.Sound>(@"Data\Sound\Explo1.Sound.res"); return _Explo1_Sound; }}
			private static Duality.ContentRef<Duality.Resources.Sound> _Explo2_Sound;
			public static Duality.ContentRef<Duality.Resources.Sound> Explo2_Sound { get { if (_Explo2_Sound.IsExplicitNull) _Explo2_Sound = Duality.ContentProvider.RequestContent<Duality.Resources.Sound>(@"Data\Sound\Explo2.Sound.res"); return _Explo2_Sound; }}
			private static Duality.ContentRef<Duality.Resources.Sound> _Explo3_Sound;
			public static Duality.ContentRef<Duality.Resources.Sound> Explo3_Sound { get { if (_Explo3_Sound.IsExplicitNull) _Explo3_Sound = Duality.ContentProvider.RequestContent<Duality.Resources.Sound>(@"Data\Sound\Explo3.Sound.res"); return _Explo3_Sound; }}
			private static Duality.ContentRef<Duality.Resources.Sound> _Explo4_Sound;
			public static Duality.ContentRef<Duality.Resources.Sound> Explo4_Sound { get { if (_Explo4_Sound.IsExplicitNull) _Explo4_Sound = Duality.ContentProvider.RequestContent<Duality.Resources.Sound>(@"Data\Sound\Explo4.Sound.res"); return _Explo4_Sound; }}
			private static Duality.ContentRef<Duality.Resources.Sound> _Explo5_Sound;
			public static Duality.ContentRef<Duality.Resources.Sound> Explo5_Sound { get { if (_Explo5_Sound.IsExplicitNull) _Explo5_Sound = Duality.ContentProvider.RequestContent<Duality.Resources.Sound>(@"Data\Sound\Explo5.Sound.res"); return _Explo5_Sound; }}
			private static Duality.ContentRef<Duality.Resources.Sound> _FireWeapon_Sound;
			public static Duality.ContentRef<Duality.Resources.Sound> FireWeapon_Sound { get { if (_FireWeapon_Sound.IsExplicitNull) _FireWeapon_Sound = Duality.ContentProvider.RequestContent<Duality.Resources.Sound>(@"Data\Sound\FireWeapon.Sound.res"); return _FireWeapon_Sound; }}
			private static Duality.ContentRef<Duality.Resources.Sound> _HitAsteroid_Sound;
			public static Duality.ContentRef<Duality.Resources.Sound> HitAsteroid_Sound { get { if (_HitAsteroid_Sound.IsExplicitNull) _HitAsteroid_Sound = Duality.ContentProvider.RequestContent<Duality.Resources.Sound>(@"Data\Sound\HitAsteroid.Sound.res"); return _HitAsteroid_Sound; }}
			private static Duality.ContentRef<Duality.Resources.Sound> _HitWeapon_Sound;
			public static Duality.ContentRef<Duality.Resources.Sound> HitWeapon_Sound { get { if (_HitWeapon_Sound.IsExplicitNull) _HitWeapon_Sound = Duality.ContentProvider.RequestContent<Duality.Resources.Sound>(@"Data\Sound\HitWeapon.Sound.res"); return _HitWeapon_Sound; }}
			private static Duality.ContentRef<Duality.Resources.Sound> _Level_Sound;
			public static Duality.ContentRef<Duality.Resources.Sound> Level_Sound { get { if (_Level_Sound.IsExplicitNull) _Level_Sound = Duality.ContentProvider.RequestContent<Duality.Resources.Sound>(@"Data\Sound\Level.Sound.res"); return _Level_Sound; }}
			public static void LoadAll() {
				CollectPowerup_Sound.MakeAvailable();
				Engine_Sound.MakeAvailable();
				Explo1_Sound.MakeAvailable();
				Explo2_Sound.MakeAvailable();
				Explo3_Sound.MakeAvailable();
				Explo4_Sound.MakeAvailable();
				Explo5_Sound.MakeAvailable();
				FireWeapon_Sound.MakeAvailable();
				HitAsteroid_Sound.MakeAvailable();
				HitWeapon_Sound.MakeAvailable();
				Level_Sound.MakeAvailable();
			}
		}
		public static class Textures {
			private static Duality.ContentRef<Duality.Resources.Texture> _BigAsteroid_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> BigAsteroid_Texture { get { if (_BigAsteroid_Texture.IsExplicitNull) _BigAsteroid_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\BigAsteroid.Texture.res"); return _BigAsteroid_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _BigAsteroid2_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> BigAsteroid2_Texture { get { if (_BigAsteroid2_Texture.IsExplicitNull) _BigAsteroid2_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\BigAsteroid2.Texture.res"); return _BigAsteroid2_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _BigAsteroid3_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> BigAsteroid3_Texture { get { if (_BigAsteroid3_Texture.IsExplicitNull) _BigAsteroid3_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\BigAsteroid3.Texture.res"); return _BigAsteroid3_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _Explo_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> Explo_Texture { get { if (_Explo_Texture.IsExplicitNull) _Explo_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\Explo.Texture.res"); return _Explo_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _MediumAsteroid_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> MediumAsteroid_Texture { get { if (_MediumAsteroid_Texture.IsExplicitNull) _MediumAsteroid_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\MediumAsteroid.Texture.res"); return _MediumAsteroid_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _PlayerShip_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> PlayerShip_Texture { get { if (_PlayerShip_Texture.IsExplicitNull) _PlayerShip_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\PlayerShip.Texture.res"); return _PlayerShip_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _PowerupDiagonalCannon_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> PowerupDiagonalCannon_Texture { get { if (_PowerupDiagonalCannon_Texture.IsExplicitNull) _PowerupDiagonalCannon_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\PowerupDiagonalCannon.Texture.res"); return _PowerupDiagonalCannon_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _PowerupFrontCannon_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> PowerupFrontCannon_Texture { get { if (_PowerupFrontCannon_Texture.IsExplicitNull) _PowerupFrontCannon_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\PowerupFrontCannon.Texture.res"); return _PowerupFrontCannon_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _PowerupKillAll_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> PowerupKillAll_Texture { get { if (_PowerupKillAll_Texture.IsExplicitNull) _PowerupKillAll_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\PowerupKillAll.Texture.res"); return _PowerupKillAll_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _PowerupSideCannon_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> PowerupSideCannon_Texture { get { if (_PowerupSideCannon_Texture.IsExplicitNull) _PowerupSideCannon_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\PowerupSideCannon.Texture.res"); return _PowerupSideCannon_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _Projectile_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> Projectile_Texture { get { if (_Projectile_Texture.IsExplicitNull) _Projectile_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\Projectile.Texture.res"); return _Projectile_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _SmallAsteroid_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> SmallAsteroid_Texture { get { if (_SmallAsteroid_Texture.IsExplicitNull) _SmallAsteroid_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\SmallAsteroid.Texture.res"); return _SmallAsteroid_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _SpaceBg_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> SpaceBg_Texture { get { if (_SpaceBg_Texture.IsExplicitNull) _SpaceBg_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\SpaceBg.Texture.res"); return _SpaceBg_Texture; }}
			private static Duality.ContentRef<Duality.Resources.Texture> _Wall_Texture;
			public static Duality.ContentRef<Duality.Resources.Texture> Wall_Texture { get { if (_Wall_Texture.IsExplicitNull) _Wall_Texture = Duality.ContentProvider.RequestContent<Duality.Resources.Texture>(@"Data\Textures\Wall.Texture.res"); return _Wall_Texture; }}
			public static void LoadAll() {
				BigAsteroid_Texture.MakeAvailable();
				BigAsteroid2_Texture.MakeAvailable();
				BigAsteroid3_Texture.MakeAvailable();
				Explo_Texture.MakeAvailable();
				MediumAsteroid_Texture.MakeAvailable();
				PlayerShip_Texture.MakeAvailable();
				PowerupDiagonalCannon_Texture.MakeAvailable();
				PowerupFrontCannon_Texture.MakeAvailable();
				PowerupKillAll_Texture.MakeAvailable();
				PowerupSideCannon_Texture.MakeAvailable();
				Projectile_Texture.MakeAvailable();
				SmallAsteroid_Texture.MakeAvailable();
				SpaceBg_Texture.MakeAvailable();
				Wall_Texture.MakeAvailable();
			}
		}
		public static void LoadAll() {
			AudioData.LoadAll();
			Fonts.LoadAll();
			Materials.LoadAll();
			Pixmaps.LoadAll();
			Prefabs.LoadAll();
			Scenes.LoadAll();
			Sound.LoadAll();
			Textures.LoadAll();
		}
	}

}
