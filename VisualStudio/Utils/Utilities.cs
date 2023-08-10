using System.Diagnostics.CodeAnalysis;

namespace FuelManager
{
    internal static class Utilities
    {
        public static GearItem GetGearItemPrefab(string name) => GearItem.LoadGearItemPrefab(name).GetComponent<GearItem>();
        public static ToolsItem GetToolItemPrefab(string name) => GearItem.LoadGearItemPrefab(name).GetComponent<ToolsItem>();
        public static string? NormalizeName(string name)
        {
            if (name == null) return null;
            else return name.Replace("(Clone)", "").Trim();
        }

        [return: NotNullIfNotNull("component")]
        public static T? GetComponentSafe<T>(this Component? component) where T : Component
        {
            return component == null ? default : GetComponentSafe<T>(component.GetGameObject());
        }

        [return: NotNullIfNotNull("gameObject")]
        public static T? GetComponentSafe<T>(this GameObject? gameObject) where T : Component
        {
            return gameObject == null ? default : gameObject.GetComponent<T>();
        }

        [return: NotNullIfNotNull("component")]
        public static T? GetOrCreateComponent<T>(this Component? component) where T : Component
        {
            return component == null ? default : GetOrCreateComponent<T>(component.GetGameObject());
        }

        [return: NotNullIfNotNull("gameObject")]
        public static T? GetOrCreateComponent<T>(this GameObject? gameObject) where T : Component
        {
            if (gameObject == null) return default;

            T? result = GetComponentSafe<T>(gameObject);

            if (result == null) result = gameObject.AddComponent<T>();

            return result;
        }

        [return: NotNullIfNotNull("component")]
        internal static GameObject? GetGameObject(this Component? component)
        {
            try
            {
                return component == null ? default : component.gameObject;
            }
#if DEBUG
            catch (System.Exception exception)
            {
                Logger.LogError($"Returning null since this could not obtain a Game Object from the component. Stack trace:\n{exception.Message}", Color.red);
            }
#endif
#if !DEBUG
            catch { }
#endif
            return null;
        }

        internal static T GetItem<T>(string name, string? reference = null) where T : Component
        {
            GameObject? gameObject = AssetBundleUtils.LoadAsset<GameObject>(name);
            if (gameObject == null)
            {
                throw new ArgumentException("Could not load '" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + ".");
            }

            T targetType = GetComponentSafe<T>(gameObject);
            if (targetType == null)
            {
                throw new ArgumentException("'" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + " is not a '" + typeof(T).Name + "'.");
            }

            return targetType;
        }

        internal static T[] GetItems<T>(string[] names, string? reference = null) where T : Component
        {
            T[] result = new T[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                result[i] = GetItem<T>(names[i], reference);
            }

            return result;
        }
    }
}
