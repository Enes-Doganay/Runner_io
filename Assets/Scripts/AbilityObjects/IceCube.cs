using System.Collections;
using UnityEngine;

public class IceCube : MonoBehaviour
{
    [SerializeField]
    private SoundID iceCubeSound = SoundID.None;

    private float freezeTime = 2f;
    private void OnEnable()
    {
        AudioManager.Instance.PlayEffect(iceCubeSound);
    }
    public void Freeze(Controller target)
    {
        StartCoroutine(StartFreeze(target));
    }
    private IEnumerator StartFreeze(Controller controller)
    {
        controller.enabled = false;
        controller.SetAnimator(false);
        controller.GetComponent<AbilityManager>().enabled = false;
        yield return new WaitForSeconds(freezeTime);
        controller.enabled = true;
        controller.SetAnimator(true);
        controller.GetComponent<AbilityManager>().enabled = true;
        gameObject.SetActive(false);
    }
}
