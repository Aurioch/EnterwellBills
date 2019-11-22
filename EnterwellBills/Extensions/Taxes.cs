using System;
using System.Collections;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace EnterwellBills.Extensions
{
    public interface ITaxCalculation
    {
        double Calculate(double basePrice);
    }

    public interface ITaxData
    {
        string Name { get; }
        string PercentageValue { get; }
    }

    [Export(typeof(ITaxCalculation))]
    [ExportMetadata("Name", "Hrvatska Standard")]
    [ExportMetadata("PercentageValue", "25%")]
    class TaxHRV : ITaxCalculation
    {
        public double Calculate(double basePrice)
        {
            return basePrice * 1.25;
        }
    }

    [Export(typeof(ITaxCalculation))]
    [ExportMetadata("Name", "Hrvatska Reduced")]
    [ExportMetadata("PercentageValue", "13%")]
    class TaxHRVReduced : ITaxCalculation
    {
        public double Calculate(double basePrice)
        {
            return basePrice * 1.13;
        }
    }

    [Export(typeof(ITaxCalculation))]
    [ExportMetadata("Name", "Bosna i Hercegovina")]
    [ExportMetadata("PercentageValue", "17%")]
    class TaxBIH : ITaxCalculation
    {
        public double Calculate(double basePrice)
        {
            return basePrice * 1.17;
        }
    }

    [Export(typeof(ITaxCalculation))]
    [ExportMetadata("Name", "Germany Standard")]
    [ExportMetadata("PercentageValue", "19%")]
    class TaxGER : ITaxCalculation
    {
        public double Calculate(double basePrice)
        {
            return basePrice * 1.19;
        }
    }

    [Export(typeof(ITaxCalculation))]
    [ExportMetadata("Name", "Germany Reduced")]
    [ExportMetadata("PercentageValue", "7%")]
    class TaxGERReduced : ITaxCalculation
    {
        public double Calculate(double basePrice)
        {
            return basePrice * 1.07;
        }
    }
}