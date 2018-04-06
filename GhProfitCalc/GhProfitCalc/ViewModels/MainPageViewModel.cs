using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhProfitCalc.ViewModels
{
    /// <summary>
    /// Uses ReactiveUI to enable reactive programming. Inherit from ReactiveObject to get
    /// sweetness out of the box
    /// </summary>
    public class MainPageViewModel : ReactiveObject
    {
        /// <summary>
        /// Input Properties are the properties that our Entry-fields bind to using two-way binding
        /// 
        /// In the setter methods for each property we use the inherited method from ReactiveObject
        /// 'RaiseAndSetIfChanged' which alters the referenced variable if the value passed in
        /// is different. It also raises a PropertyChanged notification which tells all listeners
        /// who are subscribing that this property has changed.
        /// 
        /// Example: Our WhenAnyValue method in the constructor subscribes to changes for all thse
        /// properties
        /// </summary>
        #region Input Properties

        private string _priceBought;
        public string PriceBought
        {
            get => _priceBought;
            set => this.RaiseAndSetIfChanged(ref _priceBought, value);
        }

        private string _amountBought;
        public string AmountBought
        {
            get => _amountBought;
            set => this.RaiseAndSetIfChanged(ref _amountBought, value);
        }

        private string _profitWanted;
        public string ProfitWanted
        {
            get => _profitWanted;
            set => this.RaiseAndSetIfChanged(ref _profitWanted, value);
        }

        private string _fee;
        public string Fee
        {
            get => _fee;
            set => this.RaiseAndSetIfChanged(ref _fee, value);
        }

        #endregion

        /// <summary>
        /// Output properties are the properties our view/page will bind to for results using one-way binding
        /// </summary>
        #region Output properties

        private string _priceToSellAt;
        public string PriceToSellAt
        {
            get => _priceToSellAt;
            set => this.RaiseAndSetIfChanged(ref _priceToSellAt, value);
        }

        private string _feeToPay;
        public string FeeToPay
        {
            get => _feeToPay;
            set => this.RaiseAndSetIfChanged(ref _feeToPay, value);
        }
        #endregion

        public MainPageViewModel()
        {
            // When any of the properties change then call our CalculateNewPrice method
            // WhenAnyValue is a method from ReactiveUI that subscribes to change notification
            this.WhenAnyValue(vm => vm.PriceBought, vm => vm.AmountBought, vm => vm.ProfitWanted, vm => vm.Fee)
                .Subscribe(_ => CalculateNewPrice());
        }

        /// <summary>
        /// Tries to calculate new price if all fields are set correctly and updates our
        /// Output properties accordingly.
        /// </summary>
        private void CalculateNewPrice()
        {
            try
            {
                var price = Convert.ToDouble(PriceBought);
                var amount = Convert.ToDouble(AmountBought);
                var orofit = Convert.ToDouble(ProfitWanted);
                var fee = Convert.ToDouble(Fee) / 100;

                var fiatUsed = price * amount;
                var fiatWanted = fiatUsed + orofit;
                var newPricePoint = (fiatWanted / fiatUsed) * price;

                if (double.IsNaN(newPricePoint)) newPricePoint = 0;

                PriceToSellAt = $"{newPricePoint:N}";
            }
            catch (Exception)
            {
                PriceToSellAt = "0";
            }
        }
    }
}
