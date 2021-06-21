using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dados
{
	// Armazenamento
	public static string nomeArquivo = "dados";

	// Tempo
	public static ulong tempoTotalDeJogo = 0;
	public static float tempoAtualDeJogo = 0;

	// Pontuação
	public static int pontosAtuais = 0;
	public static int [] pontosAparecer = {
		4, 8, 16, 32, 64, 128, 256, 512
	};

	// Blocos
	public static bool blocosCriarPorTempo = true;
	public static float chanceCriarNovoBloco = 0.1f;
	public static float tempoCriarBlocos = 5.0f;

	// Chance de vir um objeto multiplicador, entre zero e um.
	// Valor padrão está em 5% (0.05f)
	public static float chanceMultiplicador = 0.05f;

	// Valores possíveis dos pontos, soma e subtração
	public static int valorBotaoNormalMin = 1;
	public static int valorBotaoNormalMax = 1;

	// Valores possíveis dos pontos, multiplicação
	public static int valorBotaoMultipMin = 2;
	public static int valorBotaoMultipMax = 2;

	// Missões
	public static string arquivoMissoes = "missoes";
	public static string textoMissaoCompleta = "Missão completa.";
	public static List<Missao> missoes = new List<Missao>();

	// Realizações
	public static string arquivoRealizacoes = "realizacoes";
	public static List<Realizacao> realizacoes = new List<Realizacao>();

	// Mensagens
	public static string [] textosMensagens = {
		"<color=#c05436ff>Não cabem mais blocos no cenário!</color>"
	};

	// Debug
	public static int totalDebug = 0;
	public static string debugMensagens =
		"<color=#ffff00ff>Debug:</color>";
}
