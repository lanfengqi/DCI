using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Core
{
    public class UniqueId : TValueObject<UniqueId>
    {
        #region Constructor
        public UniqueId()
            : this(Guid.NewGuid())
        { }
        public UniqueId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentNullException("UniqueId");
            }
            this.Value = value;
        }
        #endregion

        #region Public Members
        public Guid Value { get; private set; }
        #endregion

        #region public methods
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        #endregion
    }
}
