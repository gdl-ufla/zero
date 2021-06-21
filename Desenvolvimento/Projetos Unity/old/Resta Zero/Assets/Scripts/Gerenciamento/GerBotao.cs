using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Gerenciador do botão
public class GerBotao : MonoBehaviour 
{
	// Atributos públicos
	public Tipos.Botao	tipo 		= Tipos.Botao.Multiplicador;
	public int			valor 		= 0;
	public float 		tempoSumir	= 1f;

	// Atributos privados
	Color 			cor 	= Constantes.corZero;
	Image			imagem 	= null;
	Text			texto	= null;
	float 			tempo	= 0;
	float 			alfa	= 1;
	bool			sumir 	= false;

	// Inicialização, chamada quando o objeto é criado.
	void Awake()
	{
		tipo 	= Utilidade.GerarTipoBotao();
		valor 	= Utilidade.PegarValor(tipo);
		cor 	= Utilidade.CorPorTipo(tipo);

		Debug.Log ("Botão criado. Tipo: "+tipo+"; "+
		           "Valor: "+valor+"; "+" Cor: "+cor);

		imagem = GetComponent<Image>();
		texto = GetComponentInChildren<Text>();

		Alterar();
	}

	void Update()
	{
		if (sumir)
		{
			alfa = (tempo - Time.time) / tempoSumir;
			if (alfa < 0)
			{
				Destruir();
			}
			else
			{
				imagem.color = new Color(
					imagem.color.r,
					imagem.color.g,
					imagem.color.b,
					alfa);

				texto.color = new Color(
					texto.color.r,
					texto.color.g,
					texto.color.b,
					alfa);
			}
		}
	}

	public void Alterar()
	{
		cor 	= Utilidade.CorPorTipo(tipo);
		imagem.color = cor;
		texto.text = Utilidade.GerarTexto(tipo, valor);
	}

	public void Mudar(int novoValor)
	{
		valor = novoValor;
		if (valor > 0)
		{
			tipo = Tipos.Botao.Positivo;
		}
		else
		{
			tipo = Tipos.Botao.Negativo;
		}

		Alterar();
	}

	public void Zerou()
	{
		tipo	= Tipos.Botao.Zero;
		valor 	= 0;
		Alterar();

		sumir = true;
		tempo = Time.time + tempoSumir;
	}

	public void Destruir()
	{
		Destroy(gameObject);
	}

	public void Carregar()
	{
		//Debug.Log ("Começou a carregar");
		Transform pai = transform.parent;
		transform.SetParent(null);
		transform.SetParent(pai);
	}

	public void Mover()
	{
		transform.position = Input.mousePosition;

		// Usar este, caso o canvas esteja para a câmera
		/*
		Vector2 pos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
		myCanvas.transform as RectTransform, 
		Input.mousePosition, myCanvas.worldCamera, out pos);
		transform.position = myCanvas.transform.TransformPoint(pos);
		//*/
	}

	public void Soltar()
	{
		Debug.Log ("Soltou");
		GerJogo.SoltarObjeto(GetComponent<GerBotao>());
	}
}
