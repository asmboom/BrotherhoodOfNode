﻿using UnityEngine;

/**

		This class helps zooming in an editor window
		Based on http://martinecker.com/martincodes/unity-editor-window-zooming/

**/
namespace Assets.Editor.Bon
{
	public class EditorZoomArea
	{

		private static Matrix4x4 prevGuiMatrix;
		private static Vector3 tmpScaleVector = new Vector3();
		private static Rect tmpRect = new Rect();

		public static Rect Begin(float zoomScale, Rect screenCoordsArea)
		{
			GUI.EndGroup();
			Rect clippedArea = screenCoordsArea.ScaleSizeBy(1.0f / zoomScale, screenCoordsArea.TopLeft());
			clippedArea.y += BonWindow.TopOffset;
			GUI.BeginGroup(clippedArea);
			prevGuiMatrix = GUI.matrix;
			Matrix4x4 translation = Matrix4x4.TRS(clippedArea.TopLeft(), Quaternion.identity, Vector3.one);
			tmpScaleVector.Set(zoomScale, zoomScale, 1.0f);
			Matrix4x4 scale = Matrix4x4.Scale(tmpScaleVector);
			GUI.matrix = translation * scale * translation.inverse * GUI.matrix;
			return clippedArea;
		}

		public static void End()
		{
			GUI.matrix = prevGuiMatrix;
			GUI.EndGroup();
			tmpRect.Set(0, BonWindow.TopOffset, Screen.width, Screen.height);
			GUI.BeginGroup(tmpRect);
		}
	}
}