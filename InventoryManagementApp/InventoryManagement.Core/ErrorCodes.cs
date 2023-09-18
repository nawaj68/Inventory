using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core
{
    public class ErrorCodes
    {
        // Error messages
        public const string BadRequest = "Invalid request message";
        public const string Unauthorized = "The request has not been applied because it lacks valid authentication credentials for the target resource.";
        public const string Forbidden = "Access denied to the requested resource.";
        public const string InternalServer = "An unexpected internal server error occurred.";

        public const string TransactionConflict = "Client transaction identifier is not unique";
        public const string ServiceConflict = "Service already exists in product catalog";

        public const string UnknownTask = "No task with the specified ID exists";
        public const string UnknownSubscription = "No subscription with the specified ID exists";
        public const string UnknownUser = "No user with the specified ID exists";

        public const string UnknownService = "No service with the specified ID exists";

        public const string UnknownTenant = "No assignments are available. Customer tenant’s aps id not found.";
        public const string InvalidToken = "No assignments are available. Invalid APS access token.";
        public const string NoLicense = "No License Found. Invalid Request.";
        public const string UnsupportedMediaType = "The request entity's media type '{0}' is not supported for this resource.";

        public const string NoSubscriptionsForCustomer = "No subscriptions registered to customer.";
        public const string NoDomain = "No domain found.";


        // Error response messages
        public static readonly ErrorResponse InternalServerErrorResponse = new ErrorResponse(InternalServer);

        public static readonly ErrorResponse UnknownTaskResponse = new ErrorResponse(UnknownTask);
        public static readonly ErrorResponse UnknownSubscriptionResponse = new ErrorResponse(UnknownSubscription);
        public static readonly ErrorResponse UnknownUserResponse = new ErrorResponse(UnknownUser);

        public static readonly ErrorResponse UnknownServiceResponse = new ErrorResponse(UnknownService);
        public static readonly ErrorResponse ServiceConflictResponse = new ErrorResponse(ServiceConflict);

        public static readonly ErrorResponse UnknownTenantResponse = new ErrorResponse(UnknownTenant);
        public static readonly ErrorResponse InvalidTokenResponse = new ErrorResponse(InvalidToken);
        public static readonly ErrorResponse NoLicenseResponse = new ErrorResponse(NoLicense);

        public static readonly ErrorResponse NoSubscriptionsForCustomerResponse = new ErrorResponse(NoSubscriptionsForCustomer);
        public static readonly ErrorResponse NoDomainResponse = new ErrorResponse(NoDomain);
    }

}
