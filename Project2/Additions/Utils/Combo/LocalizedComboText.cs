using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Additions.Utils.Combo
{
    [RequireComponent(typeof(Text))]
    public class LocalizedComboText : MonoBehaviour
    {
        [SerializeField] private Text mainText;
        [SerializeField] private List<LocalizeText> localizeTexts;

        public void SetComboText(string localized = "", string addition = "", bool revert = false)
        {
            string localizedString = localizeTexts.First(x => x.key == localized).text.text;

            mainText.text = revert
                ? $"{addition} {localizedString}"
                : $"{localizedString} {addition}";
        }

        public void SetColor(Color color) =>
            mainText.color = color;

        public Tween Fade(float value, float duration) =>
            mainText.DOFade(value, duration);

        public Tween Scale(Vector3 value, float duration) =>
            mainText.transform.DOScale(value, duration);
    }

    [Serializable]
    public struct LocalizeText
    {
        public string key;
        public Text text;
    }
}