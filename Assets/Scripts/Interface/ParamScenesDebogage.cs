﻿using UnityEngine;
using System.Collections;

public class ParamScenesDebogage : MonoBehaviour {
    public GameObject BoutonsCharger;
    public GameObject ParamsCarte;
    public GameObject ParamsCombat;
    private string sceneChoisie;

	// Use this for initialization
	void Start () {
        BoutonsCharger.SetActive(false);
        ParamsCarte.SetActive(false);
        ParamsCombat.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SurClique(bool transition)
    {
        if (transition)
        {
            GestScene.ProchaineSceneTransition(sceneChoisie);
        }
        else
        {
            GestScene.ProchaineScene(sceneChoisie);
        }
    }

    public void ChangerScene(string scene)
    {
        ParamsCarte.SetActive(false);
        ParamsCombat.SetActive(false);
        BoutonsCharger.SetActive(true);

        GestScene.TypeScene type = GestScene.ScenesNoms[scene];

        if (type == GestScene.TypeScene.Carte)
        {
            ParamsCarte.SetActive(true);
        }
        else if (type == GestScene.TypeScene.Combat)
        {
            ParamsCombat.SetActive(true);
        }

        sceneChoisie = scene;
    }
}
