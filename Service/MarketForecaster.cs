using WazeCreditGreen.Models;

namespace WazeCreditGreen.Service {
    public class MarketForecaster : IMarketForecaster {
        public MarketResult GetMarketPrediction() {
            //Call API to do some complex calculations and current stock market forecast
            //For Course purpose we will hard code the result

            return new MarketResult {
                MarketCondition = MarketCondition.StableUp
            };
        }
 
    }

  
}
