using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GerJogo : MonoBehaviour
{
	public GameObject botaoBase;

	public float distanciaPorcentagem = 0.42f;

	public int quantidadeInicial = 3;

	public RectTransform [] gradePosicoes;

    static float spawnTime = 10.0f;

    static float timeCount;

	static List<GerBotao> objetos = new List<GerBotao>();
	static GameObject botaoBaseEstatico;
	static Transform transformEstatico;
	static float distanciaJuntar = 0;
	static float ladoBotao = 90;

	static Vector3 [] grade;
	static int qtdMaxima = 1;
	static List<int> posicoesLivres = new List<int>();
	static List<int> posicoesOcupadas = new List<int>();

	void Awake()
	{
		botaoBaseEstatico = botaoBase;
		transformEstatico = transform;

		ladoBotao = botaoBase.GetComponent<RectTransform>().sizeDelta.x;

		distanciaJuntar = distanciaPorcentagem * ladoBotao;

		qtdMaxima = gradePosicoes.Length;

		grade = new Vector3[qtdMaxima];

		for (int i = 0; i < qtdMaxima; i++)
		{
			grade[i] = gradePosicoes[i].localPosition;
			posicoesLivres.Add(i);
		}

		for(int i = 0; i < quantidadeInicial; i++)
		{
			AdicionarEmPosicaoAleatoria();
		}

        Screen.orientation = ScreenOrientation.Portrait;

		//MostrarGrade();

		//Debug.Log ("Distância para juntar objetos: "+distanciaJuntar);

        GerMensagens.AdicionarMensagem("DEVELOP BUILD DEVELOP BUILD DEVELOP BUILD");
	}

    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount > spawnTime)
        {
            AdicionarEmPosicaoAleatoria();
            timeCount = 0;
        }
        Debug.Log(Dados.valorBotaoNormalMax);
    }

	static int PegarProximaPosicaoLivre()
	{
		if (posicoesLivres.Count == 0)
			return -1;

		int i = Utilidade.AleatorioLista<int>(posicoesLivres);
		posicoesOcupadas.Add(i);
		return i;
	}

	static void LiberarPosicao(int p)
	{
		posicoesOcupadas.Remove(p);
		posicoesLivres.Add(p);
	}

	static bool PosicaoOcupada(int p)
	{
		return posicoesOcupadas.Contains(p);
	}

	static void AdicionarEmPosicaoAleatoria()
	{
		int pos = PegarProximaPosicaoLivre();
		if (pos != -1)
		{
			CriarBotao(pos);
		}
		else
		{
			// Lista esta cheia!
			Debug.Log ("Não cabem mais objetos, max: "+qtdMaxima);
		}
	}

	public void AdicionarBotao()
	{
		AdicionarEmPosicaoAleatoria();
	}

	public void AdicionarBotao(int valor)
	{
		int pos = PegarProximaPosicaoLivre();
		if (pos != -1)
		{
			GameObject botao = (GameObject) Instantiate(
				botaoBaseEstatico, Vector3.zero, Quaternion.identity);
			
			botao.transform.SetParent(transformEstatico, false);
			
			botao.GetComponent<GerBotao>().Mudar(valor);

			botao.GetComponent<GerBotao>().posicaoGrade = pos;
			botao.transform.localPosition = grade[pos];
			
			objetos.Add(botao.GetComponent<GerBotao>());
		}
		else
		{
			// Lista esta cheia!
			Debug.Log ("Não cabem mais objetos, max: "+qtdMaxima);
		}
	}

	public void AdicionarBotaoMulti()
	{
		int pos = PegarProximaPosicaoLivre();
		if (pos != -1)
		{
			GameObject botao = (GameObject) Instantiate(
				botaoBaseEstatico, Vector3.zero, Quaternion.identity);
			
			botao.transform.SetParent(transformEstatico, false);
			
			botao.GetComponent<GerBotao>().tipo	=
				Tipos.Botao.Multiplicador;
			botao.GetComponent<GerBotao>().valor = 2;
			botao.GetComponent<GerBotao>().Alterar();

			botao.GetComponent<GerBotao>().posicaoGrade = pos;
			botao.transform.localPosition = grade[pos];
			
			objetos.Add(botao.GetComponent<GerBotao>());
		}
		else
		{
			// Lista esta cheia!
			Debug.Log ("Não cabem mais objetos, max: "+qtdMaxima);
		}
	}

	static void CriarBotao(int p)
	{
		GameObject botao = (GameObject) Instantiate(
			botaoBaseEstatico, Vector3.zero, Quaternion.identity);

		botao.transform.SetParent(transformEstatico, false);

		botao.GetComponent<GerBotao>().posicaoGrade = p;
		botao.transform.localPosition = grade[p];

		objetos.Add(botao.GetComponent<GerBotao>());
	}

	public static void SoltarObjeto(GerBotao ativo)
	{
		bool juntou = false;
		foreach(GerBotao obj in objetos)
		{
			if (obj != ativo)
			{
				float distancia = Vector3.Distance(
					obj.transform.localPosition, 
					ativo.transform.localPosition);

				if (distancia <= distanciaJuntar)
				{
					JuntarObjetos(obj, ativo);
					juntou = true;
					break;
				}
				else
				{
					//Debug.Log ("Distância: " + distancia);
				}
			}
		}

		if (juntou == false)
		{
			AjeitarPosicao(ativo);
		}

		//MostrarGrade();
	}

	static void AjeitarPosicao(GerBotao ativo)
	{
		LiberarPosicao(ativo.posicaoGrade);

		int p = PegarPosicaoMaisProxima(
			ativo.transform.localPosition);

		ativo.transform.localPosition = grade[p];
		ativo.posicaoGrade = p;

		posicoesOcupadas.Add(p);
		posicoesLivres.Remove(p);
	}

	static void MostrarGrade()
	{
		string saida = "";
		int i = 0;
		for (int y = 0; y < 4; y++)
		{
			for (int x = 0; x < 4; x++)
			{
				string v = "x";
				if (posicoesLivres.Contains(i)) v = "_";
				saida += " " + v;
				i++;
			}
			saida += "\n";
		}

		saida += "Livres: ";
		foreach(int l in posicoesLivres)
		{
			saida += "" + l + ", ";
		}

		saida += "\nOcups: ";
		foreach(int o in posicoesOcupadas)
		{
			saida += "" + o + ", ";
		}

		Debug.Log ("Grade: \n"+saida);

	}

	static int PegarPosicaoMaisProxima(Vector3 pos)
	{
		int retorno = -1;
		float d = float.MaxValue;

		foreach(int i in posicoesLivres)
		{
			float d2 = Vector3.Distance(pos, grade[i]);
			if (d2 < d)
			{
				retorno = i;
				d = d2;
			}
		}

		if (retorno == -1)
		{
			Debug.LogWarning("Sem posições válidas!");
		}

		return retorno;
	}

	static void JuntarObjetos(GerBotao parado, GerBotao juntado)
	{
		int novoValor = 0;
		int pontos = 0;

		int pospar = parado.posicaoGrade;
		int posjun = juntado.posicaoGrade;

		if (parado.tipo == Tipos.Botao.Multiplicador ||
		    juntado.tipo == Tipos.Botao.Multiplicador)
		{
			novoValor = parado.valor * juntado.valor;
		}
		else
		{
			novoValor = parado.valor + juntado.valor;
            if (novoValor == 0)
                Utilidade.atualizaMax(Mathf.Abs(juntado.valor));
		}

		LiberarPosicao(posjun);

		objetos.Remove(juntado);
		juntado.Destruir();

		AdicionarEmPosicaoAleatoria();

		if (novoValor == 0)
		{
			pontos = Mathf.Abs(parado.valor);
			objetos.Remove(parado);
			parado.Zerou();
			LiberarPosicao(pospar);
		}
		else
		{
			parado.Mudar(novoValor);
		}

		//Cria funçao para mostrar pontos ganhos.
		Dados.pontosAtuais += pontos;

		// Debug.Log ("Juntou objetos. Pontos: "+pontos);
	}

	public void Resetar()
	{
		for (int i = 0; i < objetos.Count; i++)
		{
			GerBotao gb = objetos[i];
			//Debug.Log ("Objeto: "+gb.posicaoGrade);
			gb.Destruir();
		}
		objetos.Clear();
		posicoesOcupadas.Clear();
		posicoesLivres.Clear();
		for (int i = 0; i < qtdMaxima; i++)
		{
			posicoesLivres.Add(i);
		}
	}
}

