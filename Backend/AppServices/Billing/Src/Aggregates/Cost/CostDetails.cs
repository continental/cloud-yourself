using Fancy.ResourceLinker.Models;
using System;

namespace CloudYourself.Backend.AppServices.Billing.Aggregates.Cost
{
    /// <summary>
    /// An origin independent cost details value object. 
    /// </summary>
    public class CostDetails
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public float Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the period begin.
        /// </summary>
        /// <value>
        /// The period begin.
        /// </value>
        public DateTime PeriodBegin { get; set; }

        /// <summary>
        /// Gets or sets the period end.
        /// </summary>
        /// <value>
        /// The period end.
        /// </value>
        public DateTime PeriodEnd { get; set; }

        /// <summary>
        /// Gets or sets the period identifier.
        /// </summary>
        /// <value>
        /// The period identifier.
        /// </value>
        public string PeriodId { get; set; }
    }
}
