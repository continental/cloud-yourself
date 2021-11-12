using System.Collections.Generic;

namespace CloudYourself.Backend.AppServices.Azure.Dtos
{
    /// <summary>
    /// Data transfer object to transport meta data abount a deployment template parameter.
    /// </summary>
    public class DeploymentParamMetaDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the allowed values.
        /// </summary>
        /// <value>
        /// The allowed values.
        /// </value>
        public List<string> AllowedValues { get; set; }
    }
}
