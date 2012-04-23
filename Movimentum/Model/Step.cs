using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movimentum.Model {
    public partial class Step {
        internal void AddConstraint(Constraint c) {
            _constraints.Add(c);
        }
    }
}
