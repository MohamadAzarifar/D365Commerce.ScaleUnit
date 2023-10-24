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
    using Microsoft.Dynamics.Commerce.Runtime.Messages;

    /// <summary>
    /// A simple request class to update the values on an RTCustomer entity. 
    /// </summary>
    [DataContract]
    public sealed class UpdateRTCustomerEntityDataRequest : Request
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRTCustomerEntityDataRequest"/> class.
        /// </summary>
        /// <param name="entityKey">A unique key identifying an RTCustomer Entity record to update.</param>
        /// <param name="updatedEntity">An RTCustomer entity with update fields.</param>
        public UpdateRTCustomerEntityDataRequest(long entityKey, RTCustomerEntity updatedEntity)
        {
            this.RTCustomerEntityKey = entityKey;
            this.UpdatedRTCustomerEntity = updatedEntity;
        }

        /// <summary>
        /// Gets the unique ID specifying the RTCustomer Entity record to update.
        /// </summary>
        public long RTCustomerEntityKey { get; private set; }

        /// <summary>
        /// Gets an RTCustomer Entity instance with any updates applied to it.
        /// </summary>
        [DataMember]
        public RTCustomerEntity UpdatedRTCustomerEntity { get; private set; }
    }
}