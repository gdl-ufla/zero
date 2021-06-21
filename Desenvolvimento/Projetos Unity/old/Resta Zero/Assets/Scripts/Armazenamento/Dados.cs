using UnityEngine;
using System.Collections;

public class Dados
{
	// Pontuação
	public static int pontosAtuais = 0;

	// Chance de vir um objeto multiplicador, entre zero e um.
	// Valor padrão está em 5% (0.05f)
	public static float chanceMultiplicador = 0.05f;

	// Valores possíveis dos pontos, soma e subtração
	public static int valorBotaoNormalMin = 1;
	public static int valorBotaoNormalMax = 2;

	// Valores possíveis dos pontos, multiplicação
	public static int valorBotaoMultipMin = 2;
	public static int valorBotaoMultipMax = 2;
}
