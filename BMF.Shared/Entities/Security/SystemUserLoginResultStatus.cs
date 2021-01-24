namespace BMF.Shared.Entities.Security
{
    public enum SystemUserLoginResultStatus
    {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,

        /// <summary>
        /// Account does not exist (email or username)
        /// </summary>
        AccountNotExist = 2,

        /// <summary>
        /// Wrong password
        /// </summary>
        WrongPassword = 3,

        /// <summary>
        /// Account have not been activated
        /// </summary>
        NotActive = 4,

        /// <summary>
        /// Customer has been deleted 
        /// </summary>
        Deleted = 5,

        /// <summary>
        /// Customer not registered 
        /// </summary>
        NotRegistered = 6,

        /// <summary>
        /// Locked out
        /// </summary>
        LockedOut = 7
    }
}
