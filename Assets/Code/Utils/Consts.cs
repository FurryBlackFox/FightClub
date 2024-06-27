namespace Code.Utils
{
    public static class Consts
    {
        public static class Path
        {
            public static string SettingsDataAssetPath = "data";
            public static string SpritesAssetPath = "Icons";
        }
        
        public static class AnimatorParams
        {
            public static string Attack = "Attack";
            public static string Health = "Health";
        }
        
        public static class Scenes
        {
            public static string BootstrapSceneName = "Boostrap";
            public static string MainSceneName = "Main";
        }
        
        public static class StatsId
        {
            public const int LifeID = 0;
            public const int ArmorID = 1;
            public const int LifeStealID = 3;
            public const int DamageID = 2;
        }
    }
}