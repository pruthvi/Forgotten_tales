using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utilities
{
    class Timer
    {
        private float interval;
        public float Interval
        {
            get
            {
                return interval;
            }

            set
            {
                if (value >= 0)
                {
                    interval = value;
                }
                else
                {
                    throw new ArgumentException("Interval can not be less than 0.");
                }
            }
        }
        private float timer = 0;

        public Timer(float interval)
        {
            this.interval = interval;
        }

        public Timer(float interval, bool readyOnStart) : this(interval)
        {
            if (readyOnStart)
            {
                timer = interval;
            }
        }

        public void Tick(float deltaTime)
        {
            if(timer < interval)
            {
                timer += deltaTime;
            }
        }

        public void Reset()
        {
            timer = 0;
        }

        public bool Ready
        {
            get
            {
                return timer >= interval;
            }
        }
    }
}
