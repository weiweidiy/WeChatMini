

using System;
using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/SceneManager/Ninja Transition")]
public class SMNinjaTransition : SMPostRenderTransition {
	
	/// <summary>
	/// The blade material.
	/// </summary>
	public Material bladeMaterial;
	
	/// <summary>
	/// size of the blade quad in pixels
	/// </summary>
	public float bladeSize = 128;
	
	/// <summary>
	/// duration of a cut
	/// </summary>
	public float cutDuration = .3f;
	
	/// <summary>
	/// delay between two cuts
	/// </summary>
	public float delayBetweenCuts = .5f;
	
	/// <summary>
	/// time for a single piece to fall down
	/// </summary>
	public float pieceFallTime = 1f;

	private Vector3 firstCutStart;
	private Vector3 firstCutEnd;
	private Vector3 secondCutStart;
	private Vector3 secondCutEnd;

	private Material material;	
	private float duration;
	private float effectTime;
	
	protected override void Prepare() {
		if (material == null) {
			material = new Material(Shader.Find("Scene Manager/Ninja Effect"));
			material.SetTexture("_Background", holdMaterial.mainTexture);
		}
		
		duration = 2 * (cutDuration + delayBetweenCuts) + pieceFallTime;
		
		firstCutStart = new Vector3(0, Screen.height * .2f, 0);
		firstCutEnd = new Vector3(Screen.width, Screen.height * .4f, 0);
		secondCutStart = new Vector3(Screen.width, Screen.height * .6f, 0);
		secondCutEnd = new Vector3(0, Screen.height * .9f, 0);
	}

	protected override bool Process(float elapsedTime) {
		effectTime = elapsedTime;
		return elapsedTime < duration;
	}

	protected override void OnRender() {
		GL.PushMatrix();
		GL.LoadPixelMatrix();
		GL.LoadIdentity();

		DrawBackground();
		DrawPieces(effectTime);
		DrawBlade(effectTime);

		GL.PopMatrix(); 
	}
	
	private void DrawBackground() {
		material.SetFloat("_BlendMode", state == SMTransitionState.Out ? 0 : 1);
		for (var i = 0; i < material.passCount; ++i) {
			material.SetPass(i);
			GL.Begin(GL.QUADS);
			GL.TexCoord3(0, 0, 0);
			GL.Vertex3(0, 0, 0);
			GL.TexCoord3(0, 1, 0);
			GL.Vertex3(0, Screen.height, 0);
			GL.TexCoord3(1, 1, 0);
			GL.Vertex3(Screen.width, Screen.height, 0);
			GL.TexCoord3(1, 0, 0);
			GL.Vertex3(Screen.width, 0, 0);			
			GL.End();
		}				
	}

	private void DrawPieces(float time) {
		material.SetFloat("_BlendMode", state == SMTransitionState.Out ? 1 : 0);
		
		for (var i = 0; i < material.passCount; ++i) {
			material.SetPass(i);
			GL.Begin(GL.QUADS);
			
			Vector3 progress = new Vector3(0, -Screen.height * SMTransitionUtils.SmoothProgress(cutDuration, pieceFallTime, time), 0);
			GL.TexCoord3(0, 0, 0);
			GL.Vertex3(0, progress.y, 0);
			GL.TexCoord3(0, .2f, 0);
			GL.Vertex(firstCutStart + progress);
			GL.TexCoord3(1, .4f, 0);
			GL.Vertex(firstCutEnd + progress);
			GL.TexCoord3(1, 0, 0);
			GL.Vertex3(Screen.width, progress.y, 0);			
						
			progress = new Vector3(0, -Screen.height * SMTransitionUtils.SmoothProgress(2 * cutDuration + delayBetweenCuts, pieceFallTime, time), 0);
			GL.TexCoord3(0, .2f, 0);
			GL.Vertex(firstCutStart + progress);
			GL.TexCoord3(0, .9f, 0);
			GL.Vertex(secondCutEnd + progress);
			GL.TexCoord3(1, .6f, 0);
			GL.Vertex(secondCutStart + progress);
			GL.TexCoord3(1, .4f, 0);
			GL.Vertex(firstCutEnd + progress);			
			
			progress = new Vector3(0, -Screen.height * SMTransitionUtils.SmoothProgress(2 * cutDuration + 2 * delayBetweenCuts, pieceFallTime, time), 0);
			GL.TexCoord3(0, .9f, 0);
			GL.Vertex(secondCutEnd + progress);
			GL.TexCoord3(0, 1, 0);
			GL.Vertex3(0, Screen.height + progress.y, 0);
			GL.TexCoord3(1, 1, 0);
			GL.Vertex3(Screen.width, Screen.height + progress.y, 0);
			GL.TexCoord3(1, .6f, 0);
			GL.Vertex(secondCutStart  + progress);			
			
			GL.End();					
		}				
	}
	
	private void DrawBlade(float time) {
		float cutProgress = SMTransitionUtils.Progress(0, cutDuration, time);
		if (cutProgress > 0 && cutProgress < 1) {
			DrawBlade(firstCutStart, firstCutEnd, cutProgress, false);
		}
	
		cutProgress = SMTransitionUtils.Progress(cutDuration + delayBetweenCuts, cutDuration, time);
		if (cutProgress > 0 && cutProgress < 1) {
			DrawBlade(secondCutEnd, secondCutStart, 1 - cutProgress, true);
		}
	}
	
	private void DrawBlade(Vector3 cutStart, Vector3 cutEnd, float progress, bool flip) {
		Vector3 direction = cutEnd - cutStart;
		
		Vector3 directionNormalized = direction.normalized;
		Vector3 normal = Vector3.Cross(direction, Vector3.back).normalized;
		Vector3 pos1 = cutStart + direction * progress;
		Vector3 pos2 = pos1 + normal * bladeSize;
		Vector3 pos3 = pos2 + directionNormalized * bladeSize;
		Vector3 pos4 = pos1 + directionNormalized * bladeSize;
		
		for (var i = 0; i < bladeMaterial.passCount; ++i) {
			bladeMaterial.SetPass(i);
			GL.Begin(GL.QUADS);
			
			GL.TexCoord3(flip ? 1 : 0,0,0);
			GL.Vertex(pos1);
			GL.TexCoord3(flip ? 1 : 0,1,0);
			GL.Vertex(pos2);
			GL.TexCoord3(flip ? 0 : 1,1,0);
			GL.Vertex(pos3);
			GL.TexCoord3(flip ? 0 : 1,0,0);
			GL.Vertex(pos4);
			
			GL.End();
		}
	}
	
}

