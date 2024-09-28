using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _platformPrefabs; // List of platform prefabs that can be instantiated
    [SerializeField]
    List<GameObject> _activePlatforms; // Platforms currently visible in the game
    private GameObject _currentPlatform; // The platform player is currently on
    private GameObject _nextPlatform;  // The platform that the player will jump to

    [Button("GeneratePlatform", "GeneratePlatform button")]
    public int testGeneratePlatform;

    private void Start()
    {
        _currentPlatform = _activePlatforms[0];
        _nextPlatform = GeneratePlatform();
    }

    /// <summary>
    /// Called by after the player ends the jump animation
    /// </summary>
    public void JumpLanded()
    {
        _currentPlatform = _nextPlatform;
        _nextPlatform = GeneratePlatform();
        DeinstantiateOldPlatform();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns> The world position of the next platform to jump to </returns>
    public Vector3 GetNextPlatformPosition()
    {
        if (_nextPlatform == null)
        {
            Debug.LogError("Next platform is null. Check if the next platform has been instantiated by calling GeneratePlatform and set");
        }
        return _nextPlatform.transform.position;
    }

    /// <summary>
    /// Creates a platform in either left or right of the platform
    /// </summary>
    private GameObject GeneratePlatform()
    {
        bool isGeneratingOnLeft = (UnityEngine.Random.value < 0.5);

        Vector3 newPlatformPosition = _currentPlatform.transform.position;
        float distance = 3;
        if (isGeneratingOnLeft)
        {
            newPlatformPosition += _currentPlatform.transform.forward * distance;
        }
        else
        {
            newPlatformPosition += _currentPlatform.transform.right * distance;
        }

        GameObject newPlatform = GameObject.Instantiate(_platformPrefabs[0], newPlatformPosition, Quaternion.identity);
        _activePlatforms.Add(newPlatform);

        return newPlatform;
    }

    /// <summary>s
    /// Deinstantiates the platform that is out of view
    /// </summary>
    private void DeinstantiateOldPlatform()
    {

    }
}
