using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movimentum.Model {
    public partial class ConstVector {
        public bool Is2D() {
            return !_z.HasValue;
        }

    }
}
