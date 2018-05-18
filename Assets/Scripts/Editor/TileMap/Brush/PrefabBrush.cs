using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

#if UNITY_EDITOR

using UnityEditorInternal;
using UnityEditor;

#endif

namespace UnityEditor
{
    [CustomGridBrush(false, false, false, "Prefab Brush")]
    public class PrefabBrush : LayerObjectBrush<MonoBehaviour>
    {
        public override bool AlwaysCreateOnPaint { get { return true; } }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="brushTarget"></param>
        /// <param name="position"></param>
        public override void Paint(GridLayout grid, GameObject layer, Vector3Int position)
        {
            var target = GetObject(grid, position);
            if (target != null)
            {
                return;
            }

            if (ActiveObject != null)
            {
                EditorUtil.Select(BrushUtil.GetRootGrid(false).gameObject);
            }

            base.Paint(grid, layer, position);
        }

        /// <summary>
        /// けしけし
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="layer"></param>
        /// <param name="position"></param>
        public override void Erase(GridLayout grid, GameObject layer, Vector3Int position)
        {
            foreach (var obj in GetLayer().GetAllChild())
            {
                if (grid.WorldToCell(obj.transform.position) == position)
                {
                    DestroyImmediate(obj.gameObject);

                    EditorUtil.Select(BrushUtil.GetRootGrid(false).gameObject);
                    return;
                }
            }
        }
    }
}