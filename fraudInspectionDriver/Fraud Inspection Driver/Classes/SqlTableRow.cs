using System.Collections.Generic;

namespace RLJones.FraudInspectionDriver.Classes
{
    public class SqlTableRow
    {
        /// <summary>
        /// Stores individual column values of each row in result table.
        /// </summary>
        public readonly List<object> FieldValues = new List<object>();
    }
}
