using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScriptA : MonoBehaviour
{
    private IEnumerator Start()
    {
        // Simulate some work
        yield return new WaitForSeconds(2);

        // Set the shared value after some operations
        int calculatedValue = 10; // Example value
        GameManager.Instance.SetSharedValue(calculatedValue);
    }
}
