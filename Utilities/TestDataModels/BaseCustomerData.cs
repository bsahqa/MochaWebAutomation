namespace MochaHomeAccounting.Utilities.TestDataModels
{
    /// <summary>
    /// Model class to hold the fields related to Customer Data object.
    /// </summary>
    public class BaseCustomerData
    {
        /// <summary>
        /// Gets or sets the SQL connectionString fro Stage.
        /// </summary>
        public string ConnectionStringStage { get; set; }

        /// <summary>
        /// Gets or sets the SQL connectionString for Prod.
        /// </summary>
        public string ConnectionStringProd { get; set; }

        /// <summary>
        /// Gets or sets the usernmae field for the customer.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password field for the customer.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the SQL Query.
        /// </summary>
        public string SQLQuery { get; set; }
    }
}
