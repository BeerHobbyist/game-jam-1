using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once InconsistentNaming
[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannelSo : ScriptableObject
{
    public UnityAction onEventRaised;
    
    public void RaiseEvent()
    {
        onEventRaised?.Invoke();
    }
}
