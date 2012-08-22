using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

using Duality;
using Duality.VertexFormat;
using Duality.ColorFormat;
using Duality.Resources;
using Duality.Components.Physics;

using DualityEditor;
using DualityEditor.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace EditorBase.CamViewLayers
{
	//public class GridCamViewLayer : CamViewLayer
	//{
	//    private float gridSize	= 100.0f;

	//    public override string LayerName
	//    {
	//        get { return PluginRes.EditorBaseRes.CamViewLayer_Grid_Name; }
	//    }
	//    public override string LayerDesc
	//    {
	//        get { return PluginRes.EditorBaseRes.CamViewLayer_Grid_Desc; }
	//    }
	//    public override int Priority
	//    {
	//        get { return base.Priority - 10; }
	//    }

	//    protected internal override void OnCollectDrawcalls(Canvas canvas)
	//    {
	//        base.OnCollectDrawcalls(canvas);
	//        IDrawDevice device = canvas.DrawDevice;

	//        float scaleTemp = 1.0f;
	//        Vector3 posTemp = Vector3.Zero;
	//        device.PreprocessCoords(ref posTemp, ref scaleTemp);
	//        if (posTemp.Z <= canvas.DrawDevice.NearZ) return;

	//        float alphaTemp = 0.15f;
	//        alphaTemp *= (float)Math.Min(1.0d, ((posTemp.Z - device.NearZ) / (device.NearZ * 5.0f)));
	//        if (alphaTemp <= 0.005f) return;

	//        float stepTemp = this.gridSize * 0.5f * MathF.Max(1.0f, MathF.Pow(2.0f, -MathF.Round(1.0f - MathF.Log(1.0f / scaleTemp, 2.0f))));
	//        float scaledStep = stepTemp * scaleTemp;
	//        float viewBoundRad = device.ViewBoundingRadius;
	//        int lineCount = (int)MathF.Ceiling(viewBoundRad * 2 / scaledStep);

	//        ColorRgba gridColor = this.View.FgColor.WithAlpha(alphaTemp);
	//        VertexC1P3[] vertices = new VertexC1P3[lineCount * 2];

	//        float beginPos = posTemp.X % scaledStep - (lineCount / 2) * scaledStep;
	//        for (int x = 0; x < lineCount; x++)
	//        {
	//            float pos = posTemp.X % scaledStep + (x - lineCount / 2) * scaledStep;
	//            int lineIndex = x + MathF.RoundToInt(beginPos / (this.gridSize * 0.5f));
	//            bool primaryLine = lineIndex % 2 == 0;

	//            vertices[x * 2 + 0].Color = primaryLine ? gridColor : gridColor.WithAlpha(alphaTemp * 0.5f);

	//            vertices[x * 2 + 0].Pos.X = pos;
	//            vertices[x * 2 + 0].Pos.Y = -viewBoundRad;
	//            vertices[x * 2 + 0].Pos.Z = posTemp.Z + 0;

	//            vertices[x * 2 + 1] = vertices[x * 2 + 0];
	//            vertices[x * 2 + 1].Pos.Y = viewBoundRad;
	//        }
	//        device.AddVertices(new BatchInfo(DrawTechnique.Alpha, ColorRgba.White), VertexMode.Lines, vertices);
	//    }
	//}
}
