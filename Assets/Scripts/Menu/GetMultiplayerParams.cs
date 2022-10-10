using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMultiplayerParams : MonoBehaviour, IGetMultiplayerParams
{
    #region Refs to inputs:
    [SerializeField] public Text loginNameRef;
    [SerializeField] public Text sessionNameRef;
    #endregion

    public string GetLoginName(GameMode mode) => "";

    public string GetSession(GameMode mode) => "";
}
