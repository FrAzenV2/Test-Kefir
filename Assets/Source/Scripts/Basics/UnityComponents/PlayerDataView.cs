using System;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;

namespace Source.Scripts.Components.UnityComponents
{
    public class PlayerDataView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _positionText;
        [SerializeField] private TMP_Text _velocityText;
        [SerializeField] private TMP_Text _rotationAngleText;
        [SerializeField] private TMP_Text _lasersText;
        [SerializeField] private TMP_Text[] _totalTimeTexts;
        [SerializeField] private TMP_Text[] _totalScoreTexts;

        [SerializeField] private GameObject _deathScreen;

        private StringBuilder _stringBuilder = new();

        public void UpdatePositionText(Vector2 position)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append("Pos: X-").Append($"{position.x:F}").Append(";Y-").Append($"{position.y:F}");
            _positionText.SetText(_stringBuilder);
        }

        public void UpdateRotationText(float rotation)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append("Rotation-").Append($"{rotation:F}");
            _rotationAngleText.SetText(_stringBuilder);
        }

        public void UpdateVelocity(Vector2 velocity)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append("X-").Append($"{velocity.x:F}").Append(";Y-").Append($"{velocity.y:F}");
            _velocityText.SetText(_stringBuilder);
        }

        public void UpdateLasers(int lasers, float lasersCooldown)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append("Lasers: Ready-").Append(lasers).Append("CD-").Append($"{lasersCooldown:F}");
            _lasersText.SetText(_stringBuilder);
        }

        public void UpdateTotalTime(int time)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append($"{time / 60:D2}" + ":" + $"{time % 60:D2}");
            foreach (var totalTimeText in _totalTimeTexts) totalTimeText.SetText(_stringBuilder);
        }

        public void UpdateTotalScore(int score)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(score);
            foreach (var totalScoreText in _totalScoreTexts) totalScoreText.SetText(_stringBuilder);
        }

        public void ShowDeathScreen()
        {
            _deathScreen.SetActive(true);
        }
    }
}