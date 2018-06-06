using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditorInternal;
using UnityEditor;

#endif

namespace UnityEditor
{
	[CustomGridBrush(false, false, false, "Prefab Brush")]
	public class PrefabBrush : LayerObjectBrush<Object>
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
	}
}