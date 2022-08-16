using WazeCreditGreen.Models;

namespace WazeCreditGreen.Service {
    public interface IMarketForecaster {
        MarketResult GetMarketPrediction();
    }
}