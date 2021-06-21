using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MostrarPontos : MonoBehaviour 
{
	Text texto;

	void Awake()
	{
		texto = GetComponent<Text>();
	}

	void Update()
	{
		texto.text = "" + Dados.pontosAtuais;
	}
}