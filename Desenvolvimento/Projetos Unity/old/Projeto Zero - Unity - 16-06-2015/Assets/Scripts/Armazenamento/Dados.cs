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

	// Blocos
	public static bool blocosCriarPorTempo = true;

	// Chance de vir um objeto multiplicador, entre zero e um.
	// Valor padrão está em 5% (0.05f)
	public static float chanceMultiplicador = 0.05f;

	// Valores possíveis dos pontos, soma e subtração
	public static int valorBotaoNormalMin = 1;
	public static int valorBotaoNormalMax = 2;

	// Valores possíveis dos pontos, multiplicação
	public static int valorBotaoMultipMin = 2;
	public static int valorBotaoMultipMax = 2;

	// Missões
	public static string arquivoMissoes = "missions";
	public static List<Missao> missoes = new List<Missao>();

	// Mensagens
	public static Color [] coresMensagens = {
		new Color(0.21f, 0.33f, 1, 1),
		new Color(1, 0.33f, 0.21f, 1),
		new Color(1,1,1,1)
	};
	public static string [] textosMensagens = {
		"Não cabem mais blocos no cenário!"
	};
}
