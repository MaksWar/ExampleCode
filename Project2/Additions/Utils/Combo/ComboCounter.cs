using System;
using System.Collections.Generic;
using System.Linq;
using Additions.Extensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Additions.Utils.Combo
{
    public class ComboCounter : MonoBehaviour
    {
        [SerializeField] private List<ComboSettings> comboSettings;
        [SerializeField] private LocalizedComboText comboText;
        [SerializeField] private LocalizedComboText conclusionText;
        [SerializeField] private Slider slider;
        [SerializeField] private float comboLifetime;
        [SerializeField] private AnimationCurve conclusionTextFade;

        private float _currentComboTime;
        private int _currentCombo;
        private int _bestSessionCombo;
        private Sequence _conclusionSequence;

        public event Action OnComboBreak;
        
        private void Update()
        {
            if (_currentCombo != 0)
            {
                _currentComboTime -= Time.deltaTime;

                if (_currentComboTime <= 0)
                    ResetCombo();
            }
            
            UpdateSlider();
        }

        public void AddPoint()
        {
            _currentCombo++;
            
            var settings = DefineComboSettings(_currentCombo);

            comboLifetime = settings.comboLifeTime;
            _currentComboTime = comboLifetime;

            RefreshComboText(_currentCombo, settings);
        }

        public int GetBestCombo() =>
            _bestSessionCombo;

        public void ResetCombo()
        {
            if(_currentCombo >= 1)
                DisplayConclusion(_currentCombo);

            if (_currentCombo > _bestSessionCombo)
                _bestSessionCombo = _currentCombo;

            _currentCombo = 0;
            _currentComboTime = 0;
            slider.value = 0;
            comboText.SetComboText();
            OnComboBreak?.Invoke();
        }

        [ContextMenu("DisplayCouclusion")] 
        private void DisplayConclusion(int combo)
        {
            _conclusionSequence.KillIfValid();
            _conclusionSequence = DOTween.Sequence();

            conclusionText.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            conclusionText.SetComboText("Combo", $"x{combo}!!", true);
            conclusionText.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-15, 15));

            _conclusionSequence.Join(conclusionText.Fade(1, 0.4f));
            _conclusionSequence.Join(conclusionText.Scale(Vector3.one, 0.4f));
            _conclusionSequence.Append(conclusionText.Fade(0, 1).SetEase(conclusionTextFade));
        }

        private ComboSettings DefineComboSettings(int currentCombo)
        {
            var settings = comboSettings.Last();

            foreach (var comboSetting in comboSettings.Where(comboSetting => comboSetting.maxComboValue > currentCombo))
                return comboSetting;

            return settings;
        }

        private void RefreshComboText(int comboCount, ComboSettings settings)
        {
            comboText.SetColor(settings.textColor);
            comboText.SetComboText(settings.comboName, comboCount.ToString());

            comboText.transform.DOShakeScale(0.2f, 0.1f).onComplete +=
                () => comboText.transform.DOScale(Vector3.one, 0.1f);
        }

        private void UpdateSlider()
        {
            slider.value = Mathf.InverseLerp(0, comboLifetime, _currentComboTime);
            slider.gameObject.SetActive(Math.Abs(slider.value) > 0.02f);
        }
    }


    [Serializable]
    public struct ComboSettings
    {
        public string comboName;
        public Color textColor;
        public int maxComboValue;
        public float comboLifeTime;
    }
}