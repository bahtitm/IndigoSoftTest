namespace IndigoSoftTest
{
    internal class RatesStorage
    {
        private Dictionary<string, Rate> rates = new();
        public object sync = new object();


        public async Task<Rate> ShowUpdatedRateAsync(NativeRate newRate)
        {
            await Task.Run(() => UpdateRate(newRate));
            return await Task.Run(() => GetRate(newRate.Symbol));
        }

        public async Task UpdateRateAsync(NativeRate newRate)
        {
            await Task.Run(() => UpdateRate( newRate));
        }

        // этим методом мы копируем котировки из нативного класса в базовый класс нашей программы, обновляем котировки много и часто, будем считать, что у нас 20+  вызовов этого метода в секунду
        public void UpdateRate(NativeRate newRate)
        {
            lock (sync)
            {
                if (rates.ContainsKey(newRate.Symbol) == false)
                {
                    rates.Add(newRate.Symbol, new Rate());
                }

                var oldRate = rates[newRate.Symbol];
                oldRate.Time = newRate.Time;
                oldRate.Bid = newRate.Bid;
                oldRate.Ask = newRate.Ask;

            }

        }

        public async Task<Rate> GetRateAsync(string symbol)
        {
            return await Task.Run(() => GetRate(symbol));

        }

        // это тоже высоконагруженный метод, вызываем 20+  раз в секунду
        public Rate GetRate(string symbol)
        {

            lock (sync)
            {
                if (rates.ContainsKey(symbol) == false)
                    return null;
                return rates[symbol];


            }


        }

    }
}
