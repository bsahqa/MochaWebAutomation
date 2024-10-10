namespace MochaHomeAccounting.PageModel.BasePageModel
{
    using MochaHomeAccounting.Utilities;

    /// <summary>
    /// Base Implementation of all Model classes.
    /// </summary>
    public abstract class BaseModel : BaseLoggerLib
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseModel"/> class.
        /// </summary>
        /// <param name="baseTextContext">Instance of the BaseTestConect object.</param>
        protected BaseModel(BaseTestContext baseTextContext)
            : base(baseTextContext)
        {
        }
    }
}
