﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GestionCombat : MonoBehaviour {

	public GameObject curseurPrefab;
	private GameObject personnage;
	private int personnageMove = 3;
	private GameObject vilain;
	private GameObject curseur;
	private GameObject caseActuel;
	public GameObject Menu;
	private GameObject EventSystem;

	public void instantierCurseur_Click(GameObject menu){
		menu.SetActive (false);
		caseActuel = personnage.GetComponent<PersonnageMouvement> ().caseActuel;
		curseur = Instantiate(curseurPrefab,personnage.transform.position,Quaternion.identity) as GameObject;
		curseur.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		AttribMouv (caseActuel, personnageMove);
	}

	private void AttribMouv(GameObject estMov, int move) {
		if (move > 0) {
			GameObject nouvCase = estMov.GetComponent<GestionCases>().caseVoisineBas;

			AttribMouv (nouvCase, move - 1);
			if (nouvCase) {
				nouvCase.GetComponent<GestionCases> ().estMouvement = true;
			}

			nouvCase = estMov.GetComponent<GestionCases>().caseVoisineHaut;
			AttribMouv (nouvCase, move - 1);
			if (nouvCase) {
				nouvCase.GetComponent<GestionCases> ().estMouvement = true;
			}

			nouvCase = estMov.GetComponent<GestionCases> ().caseVoisineGauche;
			AttribMouv (nouvCase, move - 1);
			if (nouvCase) {
				nouvCase.GetComponent<GestionCases> ().estMouvement = true;
			}

			nouvCase = estMov.GetComponent<GestionCases>().caseVoisineDroite;
			AttribMouv (nouvCase, move - 1);
			if (nouvCase) {
				nouvCase.GetComponent<GestionCases> ().estMouvement = true;
			}
		}
	}

	private void bougeCurseur(){
		// Flèche droite
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			if (caseActuel.GetComponent<GestionCases> ().caseVoisineDroite) {
				curseur.transform.position = caseActuel.GetComponent<GestionCases> ().caseVoisineDroite.transform.position;
				caseActuel = caseActuel.GetComponent<GestionCases> ().caseVoisineDroite;
			}
		}
		// Flèche gauche
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			if (caseActuel.GetComponent<GestionCases> ().caseVoisineGauche) {
				curseur.transform.position = caseActuel.GetComponent<GestionCases> ().caseVoisineGauche.transform.position;
				caseActuel = caseActuel.GetComponent<GestionCases> ().caseVoisineGauche;
			}
		}
		// Flèche haut
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			if (caseActuel.GetComponent<GestionCases> ().caseVoisineHaut) {
				curseur.transform.position = caseActuel.GetComponent<GestionCases> ().caseVoisineHaut.transform.position;
				caseActuel = caseActuel.GetComponent<GestionCases> ().caseVoisineHaut;
			}
		}
		// Flèche bas
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			if (caseActuel.GetComponent<GestionCases> ().caseVoisineBas) {
				curseur.transform.position = caseActuel.GetComponent<GestionCases> ().caseVoisineBas.transform.position;
				caseActuel = caseActuel.GetComponent<GestionCases> ().caseVoisineBas;
			}
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (caseActuel.GetComponent<GestionCases> ().estOccupee && !caseActuel.GetComponent<GestionCases>().estMouvement) {
				curseur.GetComponent<CurseurAnim> ().ClickAccepte = false;
				} else {
				curseur.GetComponent<CurseurAnim> ().ClickAccepte = true;
			}
		}
		if(Input.GetKeyUp(KeyCode.Space) && curseur.GetComponent<CurseurAnim>().ClickAccepte){
			personnage.GetComponent<PersonnageMouvement>().caseDestination =caseActuel;
		}
		if(Input.GetKeyUp(KeyCode.LeftControl)){
			vilain.GetComponent<PersonnageMouvement> ().caseDestination = caseActuel;
		}
	}

	// Use this for initialization
	void Start () {
		personnage = GameObject.Find ("téléchargement_7");
		vilain = GameObject.Find ("téléchargement_56");
		EventSystem = GameObject.Find ("EventSystem");
		Cursor.visible = false;
		Menu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (curseur) {
			bougeCurseur ();
		}
		if (Input.GetKey(KeyCode.M)) {
			Destroy (curseur);
			Menu.SetActive (true);
			EventSystem.GetComponent<EventSystem> ().SetSelectedGameObject (Menu.transform.FindChild("Panel").GetChild(0).gameObject);
		}
	}
}