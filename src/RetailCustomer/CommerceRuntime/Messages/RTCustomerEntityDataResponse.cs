/**
 * SAMPLE CODE NOTICE
 * 
 * THIS SAMPLE CODE IS MADE AVAILABLE AS IS.  MICROSOFT MAKES NO WARRANTIES, WHETHER EXPRESS OR IMPLIED,
 * OF FITNESS FOR A PARTICULAR PURPOSE, OF ACCURACY OR COMPLETENESS OF RESPONSES, OF RESULTS, OR CONDITIONS OF MERCHANTABILITY.
 * THE ENTIRE RISK OF THE USE OR THE RESULTS FROM THE USE OF THIS SAMPLE CODE REMAINS WITH THE USER.
 * NO TECHNICAL SUPPORT IS PROVIDED.  YOU MAY NOT DISTRIBUTE THIS CODE UNLESS YOU HAVE A LICENSE AGREEMENT WITH MICROSOFT THAT ALLOWS YOU TO DO SO.
 */

namespace GSSCX.CommerceRuntime.Messages
{
    using System.Runtime.Serialization;
    using GSSCX.CommerceRuntime.Entities.DataModel;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.Messages;

    /// <summary>
    /// Defines a simple response class that holds a collection of RTCustomer Entities.
    /// </summary>
    [DataContract]
    public sealed class RTCustomerEntityDataResponse : Response
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RTCustomerEntityDataResponse"/> class.
        /// </summary>
        /// <param name="RTCustomerEntities">The collection of RTCustomer Entities.</param>
        public RTCustomerEntityDataResponse(PagedResult<RTCustomerEntity> RTCustomerEntities)
        {
            this.RTCustomerEntities = RTCustomerEntities;
        }

        /// <summary>
        /// Gets the retrieved RTCustomer Entities as a paged result.
        /// </summary>
        [DataMember]
        public PagedResult<RTCustomerEntity> RTCustomerEntities { get; private set; }
    }
}