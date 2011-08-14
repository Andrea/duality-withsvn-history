using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Duality;
using Duality.ColorFormat;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using GLPixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace Duality.Resources
{
	[Serializable]
	public class RenderTarget : Resource
	{
		public new const string FileExt = ".RenderTarget" + Resource.FileExt;

		public static readonly ContentRef<RenderTarget> None = ContentRef<RenderTarget>.Null;

		private static int			maxFboSamples	= -1;
		private	static RenderTarget curBound		= null;

		public static ContentRef<RenderTarget> BoundRT
		{
			get { return new ContentRef<RenderTarget>(curBound); }
		}
		public static int MaxRenderTargetSamples
		{
			get 
			{
				if (maxFboSamples == -1) GL.GetInteger(GetPName.MaxSamples, out maxFboSamples);
				return maxFboSamples;
			}
		}

		public static void Bind(ContentRef<RenderTarget> target)
		{
			RenderTarget nextBound = target.IsExplicitNull ? null : target.Res;
			if (curBound == nextBound) return;

			if (curBound != null && nextBound != curBound)
			{
				// Blit multisampled fbo
				if (curBound.Samples > 0)
				{
					GL.Ext.BindFramebuffer(FramebufferTarget.ReadFramebuffer, curBound.glFboIdMSAA);
					GL.Ext.BindFramebuffer(FramebufferTarget.DrawFramebuffer, curBound.glFboId);
					for (int i = 0; i < curBound.targetInfo.Count; i++)
					{
						GL.ReadBuffer((ReadBufferMode)((int)ReadBufferMode.ColorAttachment0 + i));
						GL.DrawBuffer((DrawBufferMode)((int)DrawBufferMode.ColorAttachment0 + i));
						GL.Ext.BlitFramebuffer(
							0, 0, curBound.targetInfo[i].target.Res.OglWidth, curBound.targetInfo[i].target.Res.OglHeight,
							0, 0, curBound.targetInfo[i].target.Res.OglWidth, curBound.targetInfo[i].target.Res.OglHeight,
							ClearBufferMask.ColorBufferBit, (ExtFramebufferBlit)(int)BlitFramebufferFilter.Nearest);
					}
					GL.ReadBuffer(ReadBufferMode.Back);
					GL.DrawBuffer(DrawBufferMode.Back);
					GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
				}

				// Generate Mipmaps for last bound
				for (int i = 0; i < curBound.targetInfo.Count; i++)
				{
					if (curBound.targetInfo[i].target.Res.Mipmaps)
					{
						GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);

						int lastTexId;
						GL.GetInteger(GetPName.TextureBinding2D, out lastTexId);

						if (lastTexId != curBound.targetInfo[i].target.Res.OglTexId) 
							GL.BindTexture(TextureTarget.Texture2D, curBound.targetInfo[i].target.Res.OglTexId);

						GL.Ext.GenerateMipmap(GenerateMipmapTarget.Texture2D);

						if (lastTexId != curBound.targetInfo[i].target.Res.OglTexId) 
							GL.BindTexture(TextureTarget.Texture2D, lastTexId);
					}
				}
			}

			// Bind new RenderTarget
			if (nextBound == null)
			{
				curBound = null;
				GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
				GL.DrawBuffer(DrawBufferMode.Back);
			}
			else
			{
				curBound = target.Res;
				GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, curBound.Samples > 0 ? curBound.glFboIdMSAA : curBound.glFboId);
				DrawBuffersEnum[] buffers = new DrawBuffersEnum[curBound.targetInfo.Count];
				for (int i = 0; i < buffers.Length; i++)
				{
					buffers[i] = (DrawBuffersEnum)((int)DrawBuffersEnum.ColorAttachment0 + i);
				}
				GL.DrawBuffers(curBound.targetInfo.Count, buffers);
			}
		}
		internal static RenderbufferStorage TexFormatToRboFormat(PixelInternalFormat format)
		{
			switch (format)
			{
				case PixelInternalFormat.Alpha:
				case PixelInternalFormat.Alpha8:
					return RenderbufferStorage.Alpha8;

				case PixelInternalFormat.R8:
				case PixelInternalFormat.Luminance:
					return RenderbufferStorage.R8;

				case PixelInternalFormat.Rg8:
				case PixelInternalFormat.LuminanceAlpha:
					return RenderbufferStorage.Rg8;

				case PixelInternalFormat.Rgb:
				case PixelInternalFormat.Rgb8:
					return RenderbufferStorage.Rgb8;

				default:
				case PixelInternalFormat.Rgba:
				case PixelInternalFormat.Rgba8:
					return RenderbufferStorage.Rgba8;

				case PixelInternalFormat.Rgba16f:
					return RenderbufferStorage.Rgba16f;

				case PixelInternalFormat.Rgba16:
					return RenderbufferStorage.Rgba16;
			}
		}


		[Serializable]
		private struct TargetInfo
		{
			[NonSerialized]	public	int	glRboIdColorMSAA;
			public	ContentRef<Texture>	target;

			public TargetInfo(ContentRef<Texture> target)
			{
				this.target = target;
				this.glRboIdColorMSAA = 0;
			}
		}

		
		private	List<TargetInfo>	targetInfo		= new List<TargetInfo>();
		private	bool				multisample		= true;
		[NonSerialized] private	int	samples	= 0;
		[NonSerialized]	private	int	glFboId;
		[NonSerialized] private	int	glRboIdDepth;
		[NonSerialized] private	int	glFboIdMSAA;

		public bool Multisampled
		{
			get { return this.multisample; }
			set
			{
				if (this.multisample != value)
				{
					this.multisample = value;
					this.FreeOpenGLRes();
					this.SetupOpenGLRes();
				}
			}
		}
		public ContentRef<Texture>[] Targets
		{
			get { return this.targetInfo.Select(i => i.target).ToArray(); }
			set
			{
				this.FreeOpenGLRes();
				this.targetInfo.Clear();
				if (value != null) foreach (var t in value) this.targetInfo.Add(new TargetInfo(t));
				this.SetupOpenGLRes();
			}
		}
		public int Width
		{
			get { return this.targetInfo.FirstOrDefault().target.IsAvailable ? this.targetInfo.FirstOrDefault().target.Res.PxWidth : 0; }
		}
		public int Height
		{
			get { return this.targetInfo.FirstOrDefault().target.IsAvailable ? this.targetInfo.FirstOrDefault().target.Res.PxHeight : 0; }
		}
		public Vector2 UVRatio
		{
			get { return this.targetInfo.FirstOrDefault().target.IsAvailable ? this.targetInfo.FirstOrDefault().target.Res.UVRatio : Vector2.One; }
		}
		public int Samples
		{
			get { return this.samples; }
		}

		public RenderTarget() : this(true, null) {}
		public RenderTarget(bool multisample, params ContentRef<Texture>[] targets)
		{
			this.multisample = multisample;
			if (targets != null) foreach (var t in targets) this.targetInfo.Add(new TargetInfo(t));
			this.SetupOpenGLRes();
		}
		public void FreeOpenGLRes()
		{
			if (this.glFboId != 0)
			{
				GL.Ext.DeleteFramebuffers(1, ref this.glFboId);
				this.glFboId = 0;
			}
			if (this.glRboIdDepth != 0)
			{
				GL.Ext.DeleteRenderbuffers(1, ref this.glRboIdDepth);
				this.glRboIdDepth = 0;
			}
			if (this.glFboIdMSAA != 0)
			{
				GL.Ext.DeleteFramebuffers(1, ref this.glFboIdMSAA);
				this.glFboIdMSAA = 0;
			}
			for (int i = 0; i < this.targetInfo.Count; i++)
			{
				TargetInfo infoTemp = this.targetInfo[i];
				if (this.targetInfo[i].glRboIdColorMSAA != 0)
				{
					GL.Ext.DeleteRenderbuffers(1, ref infoTemp.glRboIdColorMSAA);
					infoTemp.glRboIdColorMSAA = 0;
				}
				this.targetInfo[i] = infoTemp;
			}
		}
		public void SetupOpenGLRes()
		{
			if (this.multisample)
				this.samples = Math.Min(MaxRenderTargetSamples, DualityApp.TargetMode.Samples);
			else
				this.samples = 0;

			#region Setup FBO & RBO: Non-multisampled
			if (this.samples == 0)
			{
				// Generate FBO
				if (this.glFboId == 0) GL.Ext.GenFramebuffers(1, out this.glFboId);
				GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, this.glFboId);

				// Attach textures
				int oglWidth = 0;
				int oglHeight = 0;
				for (int i = 0; i < this.targetInfo.Count; i++)
				{
					if (!this.targetInfo[i].target.IsAvailable) continue;
					FramebufferAttachment attachment = (FramebufferAttachment)((int)FramebufferAttachment.ColorAttachment0Ext + i);
					GL.Ext.FramebufferTexture2D(
						FramebufferTarget.FramebufferExt, 
						attachment, 
						TextureTarget.Texture2D, 
						this.targetInfo[i].target.Res.OglTexId, 
						0);
					oglWidth = this.targetInfo[i].target.Res.OglWidth;
					oglHeight = this.targetInfo[i].target.Res.OglHeight;
				}

				// Generate Depth Renderbuffer
				if (this.glRboIdDepth == 0) GL.Ext.GenRenderbuffers(1, out this.glRboIdDepth);
				GL.Ext.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, this.glRboIdDepth);
				GL.Ext.RenderbufferStorage(RenderbufferTarget.RenderbufferExt, RenderbufferStorage.DepthComponent24, oglWidth, oglHeight);
				GL.Ext.FramebufferRenderbuffer(FramebufferTarget.FramebufferExt, FramebufferAttachment.DepthAttachmentExt, RenderbufferTarget.RenderbufferExt, this.glRboIdDepth);

				// Check status
				FramebufferErrorCode status = GL.Ext.CheckFramebufferStatus(FramebufferTarget.FramebufferExt);
				if (status != FramebufferErrorCode.FramebufferCompleteExt)
				{
					Log.Core.WriteError("Can't create RenderTarget '{0}'. Incomplete Framebuffer: {1}", this.path, status);
				}

				GL.Ext.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, 0);
				GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
			}
			#endregion

			#region Setup FBO & RBO: Multisampled
			if (this.samples > 0)
			{
				FramebufferErrorCode status;

				// Generate texture target FBO
				if (this.glFboId == 0) GL.Ext.GenFramebuffers(1, out this.glFboId);
				GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, this.glFboId);

				// Attach textures
				int oglWidth = 0;
				int oglHeight = 0;
				for (int i = 0; i < this.targetInfo.Count; i++)
				{
					if (!this.targetInfo[i].target.IsAvailable) continue;
					FramebufferAttachment attachment = (FramebufferAttachment)((int)FramebufferAttachment.ColorAttachment0Ext + i);
					GL.Ext.FramebufferTexture2D(
						FramebufferTarget.FramebufferExt, 
						attachment, 
						TextureTarget.Texture2D, 
						this.targetInfo[i].target.Res.OglTexId, 
						0);
					oglWidth = this.targetInfo[i].target.Res.OglWidth;
					oglHeight = this.targetInfo[i].target.Res.OglHeight;
				}

				// Check status
				status = GL.Ext.CheckFramebufferStatus(FramebufferTarget.FramebufferExt);
				if (status != FramebufferErrorCode.FramebufferCompleteExt)
				{
					Log.Core.WriteError("Can't create RenderTarget '{0}'. Incomplete Texture Framebuffer: {1}", this.path, status);
				}

				// Generate rendering FBO
				if (this.glFboIdMSAA == 0) GL.Ext.GenFramebuffers(1, out this.glFboIdMSAA);
				GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, this.glFboIdMSAA);

				// Attach color renderbuffers
				for (int i = 0; i < this.targetInfo.Count; i++)
				{
					if (!this.targetInfo[i].target.IsAvailable) continue;
					TargetInfo info = this.targetInfo[i];
					FramebufferAttachment attachment = (FramebufferAttachment)((int)FramebufferAttachment.ColorAttachment0Ext + i);
					RenderbufferStorage rbColorFormat = TexFormatToRboFormat(info.target.Res.PixelFormat);

					if (info.glRboIdColorMSAA == 0) GL.GenRenderbuffers(1, out info.glRboIdColorMSAA);
					GL.Ext.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, info.glRboIdColorMSAA);
					GL.Ext.RenderbufferStorageMultisample((ExtFramebufferMultisample)(int)RenderbufferTarget.RenderbufferExt, this.samples, (ExtFramebufferMultisample)(int)rbColorFormat, oglWidth, oglHeight);
					GL.Ext.FramebufferRenderbuffer(FramebufferTarget.FramebufferExt, attachment, RenderbufferTarget.RenderbufferExt, info.glRboIdColorMSAA);
					this.targetInfo[i] = info;
				}
				GL.Ext.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);

				// Attach depth renderbuffer
				if (this.glRboIdDepth == 0) GL.Ext.GenRenderbuffers(1, out this.glRboIdDepth);
				GL.Ext.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, this.glRboIdDepth);
				GL.Ext.RenderbufferStorageMultisample((ExtFramebufferMultisample)(int)RenderbufferTarget.RenderbufferExt, this.samples, (ExtFramebufferMultisample)(int)RenderbufferStorage.DepthComponent24, oglWidth, oglHeight);
				GL.Ext.FramebufferRenderbuffer(FramebufferTarget.FramebufferExt, FramebufferAttachment.DepthAttachmentExt, RenderbufferTarget.RenderbufferExt, this.glRboIdDepth);
				GL.Ext.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, 0);

				// Check status
				status = GL.Ext.CheckFramebufferStatus(FramebufferTarget.FramebufferExt);
				if (status != FramebufferErrorCode.FramebufferCompleteExt)
				{
					Log.Core.WriteError("Can't create RenderTarget '{0}'. Incomplete Multisample Framebuffer: {1}", this.path, status);
				}
				
				GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
			}
			#endregion
		}

		protected override void OnLoaded()
		{
			this.SetupOpenGLRes();
			base.OnLoaded();
		}
		protected override void OnDisposing(bool manually)
		{
			base.OnDisposing(manually);
			if (DualityApp.ExecContext != DualityApp.ExecutionContext.Terminated)
			{
				this.FreeOpenGLRes();
			}
		}
		public override void CopyTo(Resource r)
		{
			base.CopyTo(r);
			RenderTarget c = r as RenderTarget;
			c.multisample	= this.multisample;
			c.targetInfo	= new List<TargetInfo>(this.targetInfo);
			c.SetupOpenGLRes();
		}
	}
}
