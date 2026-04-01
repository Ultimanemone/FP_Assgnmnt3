using Flappy_Assgnmt3.Core;
using TMPro;
using UnityEngine;

namespace Flappy_Assgnmt3.UI
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Animator _animator;

        private void Update()
        {
            _text.text = Mathf.Ceil(3f - BattleSceneManager.Instance.Timer).ToString();
            _animator.Play("finish");
            if (BattleSceneManager.Instance.Timer > 3f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
