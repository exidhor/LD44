using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.Callbacks;

namespace RendererEditor
{

    // source : https://www.reddit.com/r/Unity3D/comments/3yq4we/custom_icon_for_scriptableobjects/
    public class GizmoIconUtility
    {
        //[DidReloadScripts]
        //static GizmoIconUtility()
        //{
        //    EditorApplication.projectWindowItemOnGUI = ItemOnGUI;
        //}

        //static void ItemOnGUI(string guid, Rect rect)
        //{
        //    string assetPath = AssetDatabase.GUIDToAssetPath(guid);

        //    RendererData obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(RendererData)) as RendererData;

        //    if (obj != null)
        //    {
        //        rect.width = rect.height;
        //        GUI.DrawTexture(rect, obj.texture);
        //    }
        //}
    }
}