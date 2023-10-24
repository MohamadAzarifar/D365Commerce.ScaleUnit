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
    using Microsoft.Dynamics.Commerce.Runtime.Messages;

    /// <summary>
    /// A simple request used to delete an RTCustomer entity from the database.
    /// </summary>
    [DataContract]
    public sealed class DeleteRTCustomerEntityDataRequest : Request
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRTCustomerEntityDataRequest"/> class.
        /// </summary>
        /// <param name="entityKey">A unique key identifying an RTCustomer Entity record to delete.</param>
        public DeleteRTCustomerEntityDataRequest(long entityKey)
        {
            this.RTCustomerEntityKey = entityKey;
        }

        /// <summary>
        /// Gets the unique ID specifying the RTCustomer Entity record to delete.
        /// </summary>
        public long RTCustomerEntityKey { get; private set; }
    }
}