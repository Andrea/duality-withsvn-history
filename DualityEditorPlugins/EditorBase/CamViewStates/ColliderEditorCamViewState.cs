using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duality;
using Duality.Components;
using Duality.Resources;
using Duality.ColorFormat;
using Duality.VertexFormat;

using DualityEditor;
using DualityEditor.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace EditorBase
{
	public class ColliderEditorCamViewState : CamViewState
	{
		public override string StateName
		{
			get { return "Collider Editor"; }
		}

		public ColliderEditorCamViewState()
		{
		}
	}
}
