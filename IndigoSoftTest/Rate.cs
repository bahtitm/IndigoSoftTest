namespace IndigoSoftTest {
    public class Rate {
        public object locker = new object();
        double _ask;
        double _bid;
        DateTime _time { get; set; }
        public string? Symbol { get; set; }

        public Rate(double ask, double bid, DateTime signalDateTime) {
            _ask = ask;
            _bid = bid;
            _time = signalDateTime;
        }

        public (double Ask, double Bid, DateTime Time) RateData {
            get {
                lock(locker) {
                    return (_ask, _bid, _time);
                }
            }
        }

        public void Update(double ask, double bid, DateTime signalDateTime) {
            lock(locker) {
                _ask = ask;
                _bid = bid;
                _time = signalDateTime;
            }
        }

        public Rate Clone() {
            (double ask, double bid, DateTime time) rateData = RateData;
            return new Rate(rateData.ask, rateData.bid, rateData.time);
        }
    }
}
