using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GerJogo : MonoBehaviour
{
	public GameObject botaoBase;
	public float distanciaPorcentagem = 0.42f;
	public int quantidadeInicial = 3;
	public RectTransform [] gradePosicoes;
	public Transform painelRastros;
	public float tempoSalvar = 3;

	float proximoSalvar = 0;

	public static List<GerBotao> objetos = new List<GerBotao>();
	static GameObject botaoBaseEstatico;
	static Transform transformEstatico;
	static float distanciaJuntar = 0;
	static float ladoBotao = 90;

	static Vector3 [] grade;
	static int qtdMaxima = 1;
	static List<int> posicoesLivres = new List<int>();
	static List<int> posicoesOcupadas = new List<int>();

	public float intervaloCriarBloco = 10.0f;
	float tempoProximoBloco = 0;

	static Transform painelRastrosEstatico;

	public static List<RealizacaoCompleta> realizacoesCompletas =
		new List<RealizacaoCompleta>();

	static int missaoZerar;
	static int missaoBloco;
	static int missaoPlacar;
	static int missaoMultiplicador;

	void Awake()
	{
		botaoBaseEstatico = botaoBase;
		transformEstatico = transform;
		painelRastrosEstatico = painelRastros;

		ladoBotao = botaoBase.GetComponent<RectTransform>().sizeDelta.x;

		distanciaJuntar = distanciaPorcentagem * ladoBotao;

		qtdMaxima = gradePosicoes.Length;

		grade = new Vector3[qtdMaxima];

		for (int i = 0; i < qtdMaxima; i++)
		{
			grade[i] = gradePosicoes[i].localPosition;
			posicoesLivres.Add(i);
		}

		PlayerPrefs.DeleteAll();
		if (PlayerPrefs.HasKey(Dados.nomeArquivo))
		{
			Armazenador.CarregarDados();
		}
		else
		{
			for(int i = 0; i < quantidadeInicial; i++)
			{
				AdicionarEmPosicaoAleatoria();
			}
		}

		//Dados.realizacoes = GerArquivo.CarregarRealizacoes();
		Dados.missoes = GerArquivo.CarregarMissoes();
		for(int i = 0; i < Dados.missoes.Count; i++)
		{
			switch(Dados.missoes[i].tipo){
			case Missao.Tipo.Zerar: 
				missaoZerar = i; 
				break;
			case Missao.Tipo.Bloco: 
				missaoBloco = i; 
				break;
			case Missao.Tipo.Placar: 
				missaoPlacar = i; 
				break;
			case Missao.Tipo.Multiplicador: 
				missaoMultiplicador = i; 
				break;
			}
		}

		/*
		foreach(Realizacao m in Dados.realizacoes)
		{
			GerMensagens.AdicionarMensagem(
				Mensagens.realizacao,MensagensImagens.realizacao,m.indice);
		}
		//*/

		tempoProximoBloco = Time.time + Dados.tempoCriarBlocos;
		proximoSalvar = Time.time + tempoSalvar;

		//Dados.pontosAtuais = 123456789;
	}

	void Update()
	{
		VerificarCriarBlocos();

		VerificarSalvar();
	}

	void VerificarSalvar()
	{
		if (Time.time > proximoSalvar)
		{
			proximoSalvar = Time.time + tempoSalvar;
			
			Dados.tempoAtualDeJogo = Time.time;
			Dados.tempoTotalDeJogo += (ulong)Dados.tempoAtualDeJogo;
			
			Armazenador.SalvarDados();
			//Utilidade.DebugMensagem ("Dados salvos "+Time.time);
		}
	}

	void VerificarCriarBlocos()
	{
		if (Dados.blocosCriarPorTempo &&
		    Time.time > tempoProximoBloco)
		{
			tempoProximoBloco = Time.time + Dados.tempoCriarBlocos;
			AdicionarEmPosicaoAleatoria();
		}
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
			MensagemListaCheia();
		}
	}

	public static void AdicionarNaGrade(int tipo, int valor, int pos)
	{
		if (posicoesLivres.Count > 0)
		{
			Tipos.Botao tipoBloco = Tipos.Botao.Zero;
			switch(tipo){
			case 0: tipoBloco = Tipos.Botao.Zero; break;
			case 1: tipoBloco = Tipos.Botao.Positivo; break;
			case 2: tipoBloco = Tipos.Botao.Negativo; break;
			case 3: tipoBloco = Tipos.Botao.Multiplicador; break;
			}

			GameObject botao = (GameObject) Instantiate(
				botaoBaseEstatico, Vector3.zero, Quaternion.identity);

			botao.GetComponent<GerBotao>()
				.Inicializar(transformEstatico, pos, 
				             painelRastrosEstatico);

			botao.GetComponent<GerBotao>().tipo	= tipoBloco;
			botao.GetComponent<GerBotao>().valor = valor;
			botao.GetComponent<GerBotao>().Alterar();

			if (tipoBloco == Tipos.Botao.Zero)
			{
				botao.GetComponent<GerBotao>().Zerou();
			}

			botao.transform.localPosition = grade[pos];

			posicoesLivres.Remove(pos);
			posicoesOcupadas.Add(pos);
			
			objetos.Add(botao.GetComponent<GerBotao>());
		}
	}

	static void MensagemListaCheia()
	{
		GerMensagens.AdicionarMensagem(
			Mensagens.gradeCheia, MensagensImagens.aviso);

		Utilidade.DebugMensagem ("Não cabem mais objetos, max: "+qtdMaxima);
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
			
			//botao.transform.SetParent(transformEstatico, false);
			
			botao.GetComponent<GerBotao>()
				.Inicializar(transformEstatico, pos, 
				             painelRastrosEstatico);

			botao.GetComponent<GerBotao>().Mudar(valor);
			//botao.GetComponent<GerBotao>().posicaoGrade = pos;
			botao.transform.localPosition = grade[pos];
			
			objetos.Add(botao.GetComponent<GerBotao>());
		}
		else
		{
			// Lista esta cheia!
			MensagemListaCheia();
		}
	}

	public void AdicionarBotaoMulti()
	{
		int pos = PegarProximaPosicaoLivre();
		if (pos != -1)
		{
			GameObject botao = (GameObject) Instantiate(
				botaoBaseEstatico, Vector3.zero, Quaternion.identity);
			
			//botao.transform.SetParent(transformEstatico, false);

			botao.GetComponent<GerBotao>()
				.Inicializar(transformEstatico, pos,
				             painelRastrosEstatico);
			
			botao.GetComponent<GerBotao>().tipo	=
				Tipos.Botao.Multiplicador;
			botao.GetComponent<GerBotao>().valor = 2;
			botao.GetComponent<GerBotao>().Alterar();

			//botao.GetComponent<GerBotao>().posicaoGrade = pos;

			botao.transform.localPosition = grade[pos];
			
			objetos.Add(botao.GetComponent<GerBotao>());
		}
		else
		{
			// Lista esta cheia!
			MensagemListaCheia();
		}
	}

	static void CriarBotao(int p)
	{
		GameObject botao = (GameObject) Instantiate(
			botaoBaseEstatico, Vector3.zero, Quaternion.identity);

		botao.GetComponent<GerBotao>()
			.Inicializar(transformEstatico, p, 
			             painelRastrosEstatico);

		//botao.GetComponent<GerBotao>().posicaoGrade = p;
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
					//Utilidade.DebugMensagem ("Distância: " + distancia);
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

		Utilidade.DebugMensagem ("Grade: \n"+saida);

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

		bool mul = false;

		if (parado.tipo == Tipos.Botao.Multiplicador ||
		    juntado.tipo == Tipos.Botao.Multiplicador)
		{
			novoValor = parado.valor * juntado.valor;

			if (parado.tipo == Tipos.Botao.Multiplicador &&
			    juntado.tipo == Tipos.Botao.Multiplicador)
			{
				mul = true;
			}
		}
		else
		{
			novoValor = parado.valor + juntado.valor;
		}

		LiberarPosicao(posjun);

		objetos.Remove(juntado);
		juntado.Destruir();

		if (novoValor == 0)
		{
			AdicionarEmPosicaoAleatoria();

			pontos = Mathf.Abs(parado.valor);
			objetos.Remove(parado);
			parado.Zerou();
			LiberarPosicao(pospar);

			VerificarMissao(missaoZerar, pontos);
		}
		else
		{
			if (mul)
			{
				VerificarMissao(
					missaoMultiplicador, Mathf.Abs(novoValor));
			}
			else
			{
				VerificarMissao(missaoBloco, Mathf.Abs(novoValor));
			}

			parado.Mudar(novoValor, mul);
			parado.Brilhar();

			if (Random.value <= Dados.chanceCriarNovoBloco)
			{
				AdicionarEmPosicaoAleatoria();
			}
		}

		//Cria funçao para mostrar pontos ganhos.
		Dados.pontosAtuais += pontos;
		VerificarMissao(missaoPlacar, Dados.pontosAtuais);

		//Utilidade.DebugMensagem ("Juntou objetos. Pontos: "+pontos);
	}

	static void VerificarMissao(int missao, int valor)
	{
		if (Dados.missoes[missao].Verificar(valor) > 0)
		{
			CompletouMissao(missao);
		}
	}

	static void CompletouMissao(int missao)
	{
		GerMensagens.AdicionarMensagem(
			Dados.missoes[missao].Mensagem());
	}

	public void Resetar()
	{
		for (int i = 0; i < objetos.Count; i++)
		{
			GerBotao gb = objetos[i];
			//Utilidade.DebugMensagem ("Objeto: "+gb.posicaoGrade);
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

