namespace TrafficSimTM
{
    class Semaphore
    {
        public bool _color { get; set; }
        private int _greenWaitTime { get; set; }
        private int _redWaitTime { get; set; }
        private int _delay { get; set; }

        public Semaphore(bool color = false, int greenWaitTime = 20, int redWaitTime = 20, int delay = 0)
        {
            _color = color;
            _greenWaitTime = greenWaitTime;
            _redWaitTime = redWaitTime;
            _delay = delay;
        }

        public void increaseGreenTime()
        {
            this._greenWaitTime += 10;
        }

        public bool isGreen()
        {
            if (_color)
            {
                return true;
            }

            return false;
        }

        public bool isRed()
        {
            if (!_color)
            {
                return true;
            }

            return false;
        }
    }
}
