/**
 * SAMPLE CODE NOTICE
 * 
 * THIS SAMPLE CODE IS MADE AVAILABLE AS IS.  MICROSOFT MAKES NO WARRANTIES, WHETHER EXPRESS OR IMPLIED,
 * OF FITNESS FOR A PARTICULAR PURPOSE, OF ACCURACY OR COMPLETENESS OF RESPONSES, OF RESULTS, OR CONDITIONS OF MERCHANTABILITY.
 * THE ENTIRE RISK OF THE USE OR THE RESULTS FROM THE USE OF THIS SAMPLE CODE REMAINS WITH THE USER.
 * NO TECHNICAL SUPPORT IS PROVIDED.  YOU MAY NOT DISTRIBUTE THIS CODE UNLESS YOU HAVE A LICENSE AGREEMENT WITH MICROSOFT THAT ALLOWS YOU TO DO SO.
 */

namespace GSSCX.CommerceRuntime.RequestHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Dynamics.Commerce.Runtime;
    using Microsoft.Dynamics.Commerce.Runtime.Data;
    using Microsoft.Dynamics.Commerce.Runtime.DataAccess.SqlServer;
    using Microsoft.Dynamics.Commerce.Runtime.Messages;
    using GSSCX.CommerceRuntime.Entities.DataModel;
    using GSSCX.CommerceRuntime.Messages;

    /// <summary>
    /// Sample service to demonstrate managing a collection of entities.
    /// </summary>
    public class RTCustomerDataService : IRequestHandlerAsync
    {
        /// <summary>
        /// Gets the collection of supported request types by this handler.
        /// </summary>
        public IEnumerable<Type> SupportedRequestTypes
        {
            get
            {
                return new[]
                {
                    typeof(CreateRTCustomerEntityDataRequest),
                    typeof(RTCustomerEntityDataRequest),
                    typeof(UpdateRTCustomerEntityDataRequest),
                    typeof(DeleteRTCustomerEntityDataRequest),
                };
            }
        }

        /// <summary>
        /// Entry point to StoreHoursDataService service.
        /// </summary>
        /// <param name="request">The request to execute.</param>
        /// <returns>Result of executing request, or null object for void operations.</returns>
        public Task<Response> Execute(Request request)
        {
            ThrowIf.Null(request, nameof(request));

            switch (request)
            {
                case CreateRTCustomerEntityDataRequest createRTCustomerEntityDataRequest:
                    return this.CreateRTCustomerEntity(createRTCustomerEntityDataRequest);
                case RTCustomerEntityDataRequest RTCustomerEntityDataRequest:
                    return this.GetRTCustomerEntities(RTCustomerEntityDataRequest);
                case UpdateRTCustomerEntityDataRequest updateRTCustomerEntityDataRequest:
                    return this.UpdateRTCustomerEntity(updateRTCustomerEntityDataRequest);
                case DeleteRTCustomerEntityDataRequest deleteRTCustomerEntityDataRequest:
                    return this.DeleteRTCustomerEntity(deleteRTCustomerEntityDataRequest);
                default:
                    throw new NotSupportedException($"Request '{request.GetType()}' is not supported.");
            }
        }

        private async Task<Response> CreateRTCustomerEntity(CreateRTCustomerEntityDataRequest request)
        {
            ThrowIf.Null(request, nameof(request));
            ThrowIf.Null(request.EntityData, nameof(request.EntityData));

            long insertedId = 0;
            using (var databaseContext = new SqlServerDatabaseContext(request.RequestContext))
            {
                ParameterSet parameters = new ParameterSet();
                parameters["@i_RTCustomerInt"] = request.EntityData.IntData;
                parameters["@s_RTCustomerString"] = request.EntityData.StringData;
                var result = await databaseContext
                    .ExecuteStoredProcedureAsync<RTCustomerEntity>("[ext].GSSCX_INSERTRTCustomer", parameters, request.QueryResultSettings)
                    .ConfigureAwait(continueOnCapturedContext: false);
                insertedId = result.Item2.Single().UnusualEntityId;
            }

            return new CreateRTCustomerEntityDataResponse(insertedId);
        }

        private async Task<Response> GetRTCustomerEntities(RTCustomerEntityDataRequest request)
        {
            ThrowIf.Null(request, "request");

            using (DatabaseContext databaseContext = new DatabaseContext(request.RequestContext))
            {
                var query = new SqlPagedQuery(request.QueryResultSettings)
                {
                    DatabaseSchema = "ext",
                    Select = new ColumnSet("RTCustomerINT", "RTCustomerSTRING", "RTCustomerID"),
                    From = "GSSCX_RTCustomerVIEW",
                    OrderBy = "RTCustomerID",
                };

                var queryResults =
                    await databaseContext
                    .ReadEntityAsync<Entities.DataModel.RTCustomerEntity>(query)
                    .ConfigureAwait(continueOnCapturedContext: false);
                return new RTCustomerEntityDataResponse(queryResults);
            }
        }

        private async Task<Response> UpdateRTCustomerEntity(UpdateRTCustomerEntityDataRequest request)
        {
            ThrowIf.Null(request, nameof(request));
            ThrowIf.Null(request.UpdatedRTCustomerEntity, nameof(request.UpdatedRTCustomerEntity));

            if (request.RTCustomerEntityKey == 0)
            {
                throw new DataValidationException(DataValidationErrors.Microsoft_Dynamics_Commerce_Runtime_ValueOutOfRange, $"{nameof(request.RTCustomerEntityKey)} cannot be 0");
            }

            bool updateSuccess = false;
            using (var databaseContext = new SqlServerDatabaseContext(request.RequestContext))
            {
                ParameterSet parameters = new ParameterSet();
                parameters["@bi_Id"] = request.RTCustomerEntityKey;
                parameters["@i_RTCustomerInt"] = request.UpdatedRTCustomerEntity.IntData;
                parameters["@s_RTCustomerString"] = request.UpdatedRTCustomerEntity.StringData;
                int sprocErrorCode =
                    await databaseContext
                    .ExecuteStoredProcedureNonQueryAsync("[ext].GSSCX_UPDATERTCustomer", parameters, request.QueryResultSettings)
                    .ConfigureAwait(continueOnCapturedContext: false);
                updateSuccess = (sprocErrorCode == 0);
            }

            return new UpdateRTCustomerEntityDataResponse(updateSuccess);
        }

        private async Task<Response> DeleteRTCustomerEntity(DeleteRTCustomerEntityDataRequest request)
        {
            ThrowIf.Null(request, nameof(request));

            if (request.RTCustomerEntityKey == 0)
            {
                throw new DataValidationException(DataValidationErrors.Microsoft_Dynamics_Commerce_Runtime_ValueOutOfRange, $"{nameof(request.RTCustomerEntityKey)} cannot be 0");
            }

            bool deleteSuccess = false;
            using (var databaseContext = new SqlServerDatabaseContext(request.RequestContext))
            {
                ParameterSet parameters = new ParameterSet();
                parameters["@bi_Id"] = request.RTCustomerEntityKey;
                int sprocErrorCode =
                    await databaseContext
                    .ExecuteStoredProcedureNonQueryAsync("[ext].GSSCX_DELETERTCustomer", parameters, request.QueryResultSettings)
                    .ConfigureAwait(continueOnCapturedContext: false);
                deleteSuccess = sprocErrorCode == 0;
            }

            return new DeleteRTCustomerEntityDataResponse(deleteSuccess);
        }
    }
}