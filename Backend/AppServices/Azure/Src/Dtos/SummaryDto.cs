using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudYourself.Backend.AppServices.Azure.Dtos
{
    /// <summary>
    /// Data transfer object with summary data.
    /// </summary>
    public class SummaryDto
    {
        /// <summary>
        /// Gets or sets the managed resources count.
        /// </summary>
        /// <value>
        /// The managed resources count.
        /// </value>
        public int ManagedResourcesCount { get; set; }
    }
}
