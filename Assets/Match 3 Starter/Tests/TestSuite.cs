using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TestSuite
{
    private BoardManager boardManager;


    [UnityTest]
    public IEnumerator Test_BoardTileCounts()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(1); // Espera para asegurarte de que la escena se cargue correctamente

        // Verificar que la escena cargada sea la correcta
        Assert.AreEqual("Game", SceneManager.GetActiveScene().name, "La escena cargada no es la correcta.");

        // Obtener el objeto BoardManager en la escena
        GameObject boardPanel = GameObject.Find("BoardPanel");
        Assert.IsNotNull(boardPanel, "No se encontr� el objeto 'BoardPanel'.");

        BoardManager boardManager = boardPanel.GetComponent<BoardManager>();
        Assert.IsNotNull(boardManager, "No se encontr� el componente 'BoardManager' en 'BoardPanel'.");

        // Obtener la composici�n del tablero
        Dictionary<Sprite, int> tileCounts = boardManager.GetTileCounts();
        Assert.IsNotNull(tileCounts, "El diccionario de conteo de fichas es nulo.");

        // Verificar que haya fichas de al menos un tipo
        Assert.IsTrue(tileCounts.Count > 0, "No se encontraron fichas en el tablero.");

        // Mostrar la cantidad de cada tipo de ficha en la consola
        foreach (var kvp in tileCounts)
        {
            Debug.Log($"Ficha: {kvp.Key.name}, Cantidad: {kvp.Value}");
        }

        yield return null;
    }
    [UnityTest]
    public IEnumerator Test_StartButtonClick()
    {
        SceneManager.LoadScene("Menu");
        yield return null;

        // Verifica que la escena actual sea la del men�
        Assert.AreEqual("Menu", SceneManager.GetActiveScene().name, "La escena no es la del men�.");

        // Inicia la simulaci�n del clic en el bot�n Start
        yield return new WaitForSeconds(1f);

        // Busca el bot�n Start en la escena (aseg�rate de que el nombre sea correcto)
        GameObject startButton = GameObject.Find("PlayButton");

        // Verifica si el bot�n est� presente en la escena
        if (startButton != null)
        {
            Button button = startButton.GetComponent<Button>();
            if (button != null)
            {
                // Simula el clic en el bot�n Start
                button.onClick.Invoke();
                Debug.Log("Se ha simulado un clic en el bot�n Start.");
            }
            else
            {
                Debug.LogError("El objeto StartButton no tiene un componente Button.");
            }
        }
        else
        {
            Debug.LogError("No se encontr� el bot�n Start.");
        }

        // Espera un momento para que la escena cambie
        yield return new WaitForSeconds(1f);

        // Verifica que la escena haya cambiado a la escena del juego
        Assert.AreEqual("Game", SceneManager.GetActiveScene().name, "La escena no se cambi� a 'GameScene'.");
    }
    [UnityTest]
    public IEnumerator Test_ScoreTextStartsAtZero()
    {
        // Cargar la escena del juego
        SceneManager.LoadScene("Game");
        yield return null;

        // Buscar el objeto ScorePanel dentro de GUIManagerCanvas
        GameObject scorePanelObject = GameObject.Find("GUIManagerCanvas/ScorePanel");
        Assert.IsNotNull(scorePanelObject, "El objeto ScorePanel no se encontr� en la jerarqu�a.");

        // Buscar el objeto ScoreTxt dentro de ScorePanel
        GameObject scoreTxtObject = scorePanelObject.transform.Find("ScoreTxt").gameObject;
        Assert.IsNotNull(scoreTxtObject, "El objeto ScoreTxt no se encontr� dentro de ScorePanel.");

        // Verificar que el componente Text est� presente en el objeto ScoreTxt
        var textComponent = scoreTxtObject.GetComponent<Text>();
        Assert.IsNotNull(textComponent, "El componente Text no est� presente en el objeto ScoreTxt.");

        // Verificar que el texto inicial de ScoreTxt sea "0"
        Assert.AreEqual("0", textComponent.text, "El texto inicial de ScoreTxt no es '0'.");
    }
}