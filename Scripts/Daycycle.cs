﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daycycle : MonoBehaviour {

	// Mostly all of this script is only necessary if you want to tweak your light and your fog to make them
	// different through night and day (and that's what makes sense).  If you do not want that,
	// just use variable of line 11, line 28 of the Start method and all Update method but ManageDayNightColors().
	
	private Quaternion startRotation;
	private Light sunLight;
	private Color sunDayLightColor, fogDayColor; //
	private float lightDayIntensity;
	private bool dayArrived = false, nightArrived = false;
	public TimeManager timeManager;

	// This are the default day values, just to remind.
	// Fog Default day color = 758D65FF
	// Day Default intensity = 4.46
	// Day Default color = A9A58788

	void Start () {
		sunLight = GetComponent<Light> ();
		sunDayLightColor = sunLight.color; // This and the following store the default day values (the values the game starts with).
		fogDayColor = RenderSettings.fogColor;
		lightDayIntensity = sunLight.intensity;
		startRotation = transform.rotation;
	}

	void Update () {
		ManageDayNightColors ();
		float angleThisFrame = Time.deltaTime * timeManager.sunVelocity;
		transform.RotateAround (transform.position, Vector3.right, angleThisFrame);
	}

	void ManageDayNightColors () {
		if (timeManager.GetCurrentTime () > 0.0f && timeManager.GetCurrentTime () < 0.5) {
			if (dayArrived == false) {
				// HERE TWEAK YOUR LIGHTS AND FOG IF NEEDED.
				// THE NEXT, EXCEPT FOR THE BOOLEANS, ARE ONLY EXAMPLES: CHANGE THEM TO SUIT YOUR NEEDS.
				
				sunLight.color = sunDayLightColor;
				RenderSettings.fogColor = fogDayColor;
				sunLight.intensity = lightDayIntensity;
				dayArrived = true;
				nightArrived = false;
			}
		} else if (timeManager.GetCurrentTime () > 0.5f) {
			if (nightArrived == false) {
				fogDayColor = Color.gray;
				sunLight.color = Color.gray;
				sunLight.intensity = 1;
				nightArrived = true;
				dayArrived = false;
			}
		}
	}
}
