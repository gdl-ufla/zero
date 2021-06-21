using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GerMensagens : MonoBehaviour
{
	public GameObject painelMensagemBase;
	public float tempoMostrar = 1.5f;
	public float tempoDesvanecer = 1.5f;

	static float proximoTempo = 0;
	static bool desvanecendo = false;
	static bool mostrando = false;

	static float tempoMostrarEstatico = 1;
	static float tempoDesvanecerEstatico = 1;
	static float alfaGeral = 1;
	static float alfaPainel = 1;
	static float alfaTexto = 1;
	static float alfaImagem = 1;

	static GameObject painelMensagemEstatico;
	static Image imagemPainelBaseEstatico;
	static Text textoPainelMensagemEstatico;
	static Image imagemPainelMensagemEstatico;

	static List<string> mensagens = new List<string>();
    static List<string> achievements = new List<string>();

    static string mensagensFileName = "missions.txt";
	

    void Start()
	{
		painelMensagemEstatico = painelMensagemBase;

		painelMensagemEstatico.SetActive(true);

		imagemPainelBaseEstatico = GetComponent<Image>();
		alfaPainel = imagemPainelBaseEstatico.color.a;

		textoPainelMensagemEstatico = painelMensagemEstatico
			.GetComponentInChildren<Text>();
		alfaTexto = textoPainelMensagemEstatico.color.a;

		imagemPainelMensagemEstatico = painelMensagemEstatico
			.GetComponentInChildren<Image>();
		alfaImagem = imagemPainelMensagemEstatico.color.a;

		tempoMostrarEstatico = tempoMostrar;
		tempoDesvanecerEstatico = tempoDesvanecer;

		painelMensagemEstatico.SetActive(false);

	}

	void Update()
	{
		VerificarProxima();
	}

	public static void AdicionarMensagem(string mensagem)
	{
		mensagens.Add(mensagem);
	}

	static void VerificarProxima()
	{
		if (Time.time > proximoTempo)
		{
			if (mostrando)
			{
				mostrando = false;
				desvanecendo = true;
				proximoTempo = Time.time + tempoDesvanecerEstatico;
			}
			else if (desvanecendo)
			{
				desvanecendo = false;
				painelMensagemEstatico.SetActive(false);
			}
			else if (mensagens.Count > 0)
			{
				MostrarProxima();
			}
		}
		else if (desvanecendo)
		{
			alfaGeral = (proximoTempo - Time.time) /
				tempoDesvanecerEstatico;
			AlterarAlfa();
		}
	}

	static void AlterarAlfa(float a = -1)
	{
		if (a >= 0)
		{
			alfaGeral = a;
		}

		if (alfaGeral >= 0)
		{
			imagemPainelBaseEstatico.color = new Color(
				imagemPainelBaseEstatico.color.r,
				imagemPainelBaseEstatico.color.g,
				imagemPainelBaseEstatico.color.b,
				alfaGeral * alfaPainel);
			
			textoPainelMensagemEstatico.color = new Color(
				textoPainelMensagemEstatico.color.r,
				textoPainelMensagemEstatico.color.g,
				textoPainelMensagemEstatico.color.b,
				alfaGeral * alfaTexto);
			
			imagemPainelMensagemEstatico.color = new Color(
				imagemPainelMensagemEstatico.color.r,
				imagemPainelMensagemEstatico.color.g,
				imagemPainelMensagemEstatico.color.b,
				alfaGeral * alfaImagem);
		}
	}

	static void MostrarProxima()
	{
		textoPainelMensagemEstatico.text = mensagens[0];
		mensagens.RemoveAt(0);

		AlterarAlfa(1);
		painelMensagemEstatico.SetActive(true);
		mostrando = true;
		proximoTempo = Time.time + tempoMostrarEstatico;
	}
}

