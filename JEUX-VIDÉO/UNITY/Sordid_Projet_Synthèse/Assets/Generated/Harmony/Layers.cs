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
    public static partial class Layers
    {
        public static readonly Layer Boss = new Layer(LayerMask.NameToLayer("Boss"));
        public static readonly Layer Chest = new Layer(LayerMask.NameToLayer("Chest"));
        public static readonly Layer Default = new Layer(LayerMask.NameToLayer("Default"));
        public static readonly Layer EnemyProjectile = new Layer(LayerMask.NameToLayer("EnemyProjectile"));
        public static readonly Layer Entities = new Layer(LayerMask.NameToLayer("Entities"));
        public static readonly Layer FlyingEntities = new Layer(LayerMask.NameToLayer("FlyingEntities"));
        public static readonly Layer Ground = new Layer(LayerMask.NameToLayer("Ground"));
        // "Ignore Raycast" is invalid.
        public static readonly Layer Item = new Layer(LayerMask.NameToLayer("Item"));
        public static readonly Layer Navigation = new Layer(LayerMask.NameToLayer("Navigation"));
        public static readonly Layer Player = new Layer(LayerMask.NameToLayer("Player"));
        public static readonly Layer Room = new Layer(LayerMask.NameToLayer("Room"));
        public static readonly Layer Spell = new Layer(LayerMask.NameToLayer("Spell"));
        public static readonly Layer TransparentFX = new Layer(LayerMask.NameToLayer("TransparentFX"));
        public static readonly Layer UI = new Layer(LayerMask.NameToLayer("UI"));
        public static readonly Layer Water = new Layer(LayerMask.NameToLayer("Water"));
        
        public struct Layer
        {
            public int Index;
            public int Mask;

            public Layer(int index)
            {
                this.Index = index;
                this.Mask = 1 << index;
            }
        }

    }
}