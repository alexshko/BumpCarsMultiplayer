using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class GetMultiplayerParams : MonoBehaviour, IGetMultiplayerParams
{
    #region Refs to inputs:
    [SerializeField] public TMPro.TMP_Text loginNameRef;
    [SerializeField] public TMPro.TMP_Text sessionNameRef;
    #endregion

    public string GetLoginName(GameMode mode) => loginNameRef.text ?? "";

    public string GetSession(GameMode mode) => sessionNameRef.text ?? "";
}
