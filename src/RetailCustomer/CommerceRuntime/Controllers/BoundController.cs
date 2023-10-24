/**
 * SAMPLE CODE NOTICE
 * 
 * THIS SAMPLE CODE IS MADE AVAILABLE AS IS.  MICROSOFT MAKES NO WARRANTIES, WHETHER EXPRESS OR IMPLIED,
 * OF FITNESS FOR A PARTICULAR PURPOSE, OF ACCURACY OR COMPLETENESS OF RESPONSES, OF RESULTS, OR CONDITIONS OF MERCHANTABILITY.
 * THE ENTIRE RISK OF THE USE OR THE RESULTS FROM THE USE OF THIS SAMPLE CODE REMAINS WITH THE USER.
 * NO TECHNICAL SUPPORT IS PROVIDED.  YOU MAY NOT DISTRIBUTE THIS CODE UNLESS YOU HAVE A LICENSE AGREEMENT WITH MICROSOFT THAT ALLOWS YOU TO DO SO.
 */
 
namespace GSSCX.CommerceRuntime.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.DataModel;
    using Microsoft.Dynamics.Commerce.Runtime.Hosting.Contracts;

    /// <summary>
    /// An extension controller to handle requests to the StoreHours entity set.
    /// </summary>
    [RoutePrefix("BoundController")]
    [BindEntity(typeof(Entities.DataModel.RTCustomerEntity))]
    public class BoundController : IController
    {
        [HttpGet]
        [Authorization(CommerceRoles.Anonymous, CommerceRoles.Application, CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee, CommerceRoles.Storefront)]
        public async Task<PagedResult<Entities.DataModel.RTCustomerEntity>> GetAllRTCustomerEntities(IEndpointContext context)
        {
            var queryResultSettings = QueryResultSettings.SingleRecord;
            queryResultSettings.Paging = new PagingInfo(10);

            var request = new Messages.RTCustomerEntityDataRequest() { QueryResultSettings = queryResultSettings };
            var response = await context.ExecuteAsync<Messages.RTCustomerEntityDataResponse>(request).ConfigureAwait(false);
            return response.RTCustomerEntities;
        }

        [HttpPost]
        [Authorization(CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public async Task<long> CreateRTCustomerEntity(IEndpointContext context, CommerceRuntime.Entities.DataModel.RTCustomerEntity entityData)
        {
            var request = new Messages.CreateRTCustomerEntityDataRequest(entityData);
            var response = await context.ExecuteAsync<Messages.CreateRTCustomerEntityDataResponse>(request).ConfigureAwait(false);
            return response.CreatedId;
        }

        [HttpPost]
        [Authorization(CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public async Task<bool> UpdateRTCustomerEntity(IEndpointContext context, [EntityKey] long key, CommerceRuntime.Entities.DataModel.RTCustomerEntity updatedEntity)
        {
            var request = new Messages.UpdateRTCustomerEntityDataRequest(key, updatedEntity);
            var response = await context.ExecuteAsync<Messages.UpdateRTCustomerEntityDataResponse>(request).ConfigureAwait(false);
            return response.Success;
        }

        [HttpPost]
        [Authorization(CommerceRoles.Customer, CommerceRoles.Device, CommerceRoles.Employee)]
        public async Task<bool> DeleteRTCustomerEntity(IEndpointContext context, [EntityKey] long key)
        {
            var request = new Messages.DeleteRTCustomerEntityDataRequest(key);
            var response = await context.ExecuteAsync<Messages.DeleteRTCustomerEntityDataResponse>(request).ConfigureAwait(false);
            return response.Success;
        }
    }
}
