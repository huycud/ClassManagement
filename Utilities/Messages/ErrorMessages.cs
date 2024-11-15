namespace Utilities.Messages
{
    public static class ErrorMessages
    {
        // Status
        public const string NOT_FOUND = "{0} was not found.";

        public const string LOCKED = "{0} locked. Please back in a few minutes.";

        public const string HAS_BEEN_USED = "{0} has been used.";

        public const string WAS_FULL = "{0} was full.";

        public const string AMOUNT_INVALID = "{0} must be less or equal than class size.";

        public const string WAS_IN = "{0} was in {1}.";

        public const string NOT_RELATED = "{0} and {1} are not related.";

        public const string UNKNOWN = "Image type can not be determined.";

        public const string HANDLING_FAILURE = "{0} fail. Please check the information again.";

        public const string LOGOUT_PARTIAL_SUCCESS_MESSAGE = "Logged out, but something went wrong. Please log in again if necessary.";

        public const string UNCONFIRMED = "{0} unconfirmed.";

        public const string WAS_CONFIRMED = "{0} was confirmed.";

        public const string WAS_EXPIRED = "{0} was expired.";

        //Authorize
        public const string FORBIDDEN = "{0} forbidden.";

        //Validate
        public const string DUPLICATE_VALIDATOR = "{0} duplicate validator.";

        public const string INVALID = "{0} invalid.";

        public const string IMAGE_INVALID = "Please use image types with extensions jpg, png, jpeg.";

        public const string OVER_MAXIMUM_SIZE = "The {0} has exceeded the allowed size. Please use images with a size less than or equal to {1}.";

        public const string INVALID_PRACTICE = "Can not update {0} when class contain instance.";
    }
}
