using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SelectAll))]
public class SelectAllEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SelectAll selectAllScript = (SelectAll)target;

        if (GUILayout.Button("Select Objects"))
        {
            selectAllScript.SelectObjects();
        }
    }
}

public class SelectAll : MonoBehaviour
{
    [SerializeField] string ancestorName = "1level";


    public void SelectObjects()
    {
        List<GameObject> foundObjs = new List<GameObject>();
        PolygonCollider2D[] collisionObjects = FindObjectsByType<PolygonCollider2D>(FindObjectsSortMode.None);

        foreach (PolygonCollider2D obj in collisionObjects)
        {
            Transform immediateParent = obj.transform.parent;
            if (immediateParent != null && immediateParent.parent != null && immediateParent.parent.name == ancestorName)
            {
                Debug.Log(obj.name);
                foundObjs.Add(obj.gameObject);
            }
        }
        UnityEditor.Selection.objects = foundObjs.ToArray();

    }

}
#endif
