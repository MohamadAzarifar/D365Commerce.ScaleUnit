/**
 * SAMPLE CODE NOTICE
 * 
 * THIS SAMPLE CODE IS MADE AVAILABLE AS IS.  MICROSOFT MAKES NO WARRANTIES, WHETHER EXPRESS OR IMPLIED,
 * OF FITNESS FOR A PARTICULAR PURPOSE, OF ACCURACY OR COMPLETENESS OF RESPONSES, OF RESULTS, OR CONDITIONS OF MERCHANTABILITY.
 * THE ENTIRE RISK OF THE USE OR THE RESULTS FROM THE USE OF THIS SAMPLE CODE REMAINS WITH THE USER.
 * NO TECHNICAL SUPPORT IS PROVIDED.  YOU MAY NOT DISTRIBUTE THIS CODE UNLESS YOU HAVE A LICENSE AGREEMENT WITH MICROSOFT THAT ALLOWS YOU TO DO SO.
 */

namespace GSSCX.CommerceRuntime.Entities.DataModel
{
    using System.Runtime.Serialization;
    using Microsoft.Dynamics.Commerce.Runtime.ComponentModel.DataAnnotations;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using SystemAnnotations = System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines a simple class that holds information about opening and closing times for a particular day.
    /// </summary>
    public class RTCustomerEntity : CommerceEntity
    {
        private const string RTCustomerIntColumn = "RTCustomerINT";
        private const string RTCustomerStringColumn = "RTCustomerSTRING";
        private const string IdColumn = "RTCustomerID";

        /// <summary>
        /// Initializes a new instance of the <see cref="RTCustomerEntity"/> class.
        /// </summary>
        public RTCustomerEntity()
            : base("RTCustomer")
        {
        }

        /// <summary>
        /// Gets or sets a property containing an int value.
        /// </summary>
        [DataMember]
        [Column(RTCustomerIntColumn)]
        public int IntData
        {
            get { return (int)this[RTCustomerIntColumn]; }
            set { this[RTCustomerIntColumn] = value; }
        }

        /// <summary>
        /// Gets or sets a property containing a string value.
        /// </summary>
        [DataMember]
        [Column(RTCustomerStringColumn)]
        public string StringData
        {
            get { return (string)this[RTCustomerStringColumn]; }
            set { this[RTCustomerStringColumn] = value; }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <remarks>
        /// Fields named "Id" are automatically treated as the entity key.
        /// If a name other than Id is preferred, <see cref="System.ComponentModel.DataAnnotations.KeyAttribute"/>
        /// can be used like it is here to annotate a given field as the entity key.
        /// </remarks>
        [SystemAnnotations.Key]
        [DataMember]
        [Column(IdColumn)]
        public long UnusualEntityId
        {
            get { return (long)this[IdColumn]; }
            set { this[IdColumn] = value; }
        }
    }
}