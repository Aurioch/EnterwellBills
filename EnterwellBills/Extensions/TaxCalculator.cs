using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace EnterwellBills.Extensions
{
    public sealed class TaxCalculator
    {
        public static TaxCalculator Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                        _instance = new TaxCalculator();

                    return _instance;
                }
            }
        }

        static TaxCalculator _instance = null;
        static readonly object _padlock = new object();

        CompositionContainer _container;

        [ImportMany]
        private IEnumerable<Lazy<ITaxCalculation, ITaxData>> _taxes;
        private TaxCalculator()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(TaxCalculator).Assembly));

            _container = new CompositionContainer(catalog);
            try
            {
                _container.ComposeParts(this);
            }
            catch (CompositionException e)
            {
                // TODO: error handling
            }
        }

        public List<string> GetAllTaxes()
        {
            var result = new List<string>();

            foreach (Lazy<ITaxCalculation, ITaxData> tax in _taxes)
                result.Add(tax.Metadata.Name + ": " + tax.Metadata.PercentageValue);

            return result;
        }

        public double Calculate(double price, string taxName)
        {
            foreach (Lazy<ITaxCalculation, ITaxData> tax in _taxes)
                if (tax.Metadata.Name == taxName)
                    return tax.Value.Calculate(price);

            return price;
        }
    }
}