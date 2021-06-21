using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Interface : MonoBehaviour
{
	public GameObject painelConfig;

	void Awake()
	{
		FecharConfig();
	}

	public void AbrirConfig()
	{
		painelConfig.SetActive(true);
	}

	public void FecharConfig()
	{
		painelConfig.SetActive(false);
	}
}

