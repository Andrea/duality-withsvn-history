﻿using System;
using System.Xml;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Duality;
using Duality.Resources;
using Duality.ColorFormat;
using Duality.VertexFormat;

using DualityEditor;
using DualityEditor.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace EditorBase.CamViewLayers
{
	public abstract class CamViewLayer : CamViewClient
	{
		public virtual int Priority
		{
			get { return 0; }
		}
		public virtual bool MouseTracking
		{
			get { return false; }
		}
		public abstract string LayerName { get; }
		public abstract string LayerDesc { get; }
		
		internal protected virtual void SaveUserData(XmlElement node) {}
		internal protected virtual void LoadUserData(XmlElement node) {}
		internal protected virtual void OnActivateLayer() {}
		internal protected virtual void OnDeactivateLayer() {}
		internal protected virtual void OnCollectDrawcalls(Canvas canvas) {}
		internal protected virtual void OnCollectOverlayDrawcalls(Canvas canvas) {}
		internal protected virtual void OnCollectBackgroundDrawcalls(Canvas canvas) {}
	}
}
