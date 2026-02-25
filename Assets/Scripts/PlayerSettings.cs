using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Arcade/Player Settings")]
public class PlayerSettings : ScriptableObject
{
    public float moveSpeed = 5f;
    public float smallJumpForce = 5f;
    public float bigJumpForce = 9f;
    [Tooltip("Время зажатия для большого прыжка (сек)")]
    public float longPressThreshold = 0.25f;
}