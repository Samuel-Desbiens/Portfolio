// AUTO GENERATED CODE - ANY MODIFICATION WILL BE OVERRIDDEN
// GENERATED ON 2024-03-04 15:03

// Invalid names are commented out. Here are the rules :
// - Non-alphanumerical characters (like spaces) are prohibited. Underscores are allowed.
// - Per C# language rules, starting with a non alphabetic character is prohibited.
// - Per C# language rules, using the same name as it's class is prohibited. Ex : "GameObjects", "Tags" or "Layers".
// - Per C# language rules, using a keyword is prohibited. Ex : "object", "abstract" or "float".

using UnityEngine;

namespace Harmony
{
    public static partial class Finder
    {
        public static T FindWithTag<T>(string tag) where T : class
        {
            return GameObject.FindWithTag(tag)?.GetComponent<T>();
        }

    }
}