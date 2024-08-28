namespace IndigoSoftTest {
    internal class RatesStorage {
        private Dictionary<string, Rate> rates = new();
        public object sync = new object();

        public async Task<Rate> ShowUpdatedRateAsync(NativeRate newRate) {
            await Task.Run(() => UpdateRate(newRate));
            return await Task.Run(() => GetRate(newRate.Symbol));
        }        

        public void UpdateRate(NativeRate newRate) {
            lock(sync) {
                if(!rates.ContainsKey(newRate.Symbol)) {
                    rates.Add(newRate.Symbol, new Rate(newRate.Ask, newRate.Bid, newRate.Time));
                } else {
                    var oldRate = rates[newRate.Symbol];
                    oldRate.Update(newRate.Ask, newRate.Bid, newRate.Time);
                }
            }
        }        
        
        public Rate GetRate(string symbol) {
            lock(sync) {
                if(rates.ContainsKey(symbol) == false)
                    return null;
                return rates[symbol];
                /*
                 * To avoid anti-patterns and resolve concurrency and race conditions, we can return a cloned object of `rates[symbol]` using `rates[symbol].Clone()`.
                 */
            }
        }
    }
}
