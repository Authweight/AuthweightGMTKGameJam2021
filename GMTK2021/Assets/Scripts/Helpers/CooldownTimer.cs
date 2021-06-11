using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Helpers
{
    class CooldownTimer
    {
        private float _cooldownSeconds;
        private float _lastTime;

        public CooldownTimer(float cooldownSeconds)
        {
            _cooldownSeconds = cooldownSeconds;
            _lastTime = 0;
        }

        public void AttemptExecute(float currentTime, Action action)
        {
            if (currentTime > _lastTime + _cooldownSeconds)
            {
                _lastTime = currentTime;
                action();
            }
        }

        public bool CheckTime(float currentTime)
        {
            return currentTime > _lastTime + _cooldownSeconds;
        }

        public void StartCooldown(float currentTime)
        {
            _lastTime = currentTime;
        }
    }
}
