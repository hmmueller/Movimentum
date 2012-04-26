using System.Collections.Generic;
using System.Drawing;

namespace Movimentum.Model {
    public abstract partial class Thing {
        public abstract void Draw(Graphics drawingPane, IDictionary<string, ConstVector> anchorLocations);
    }
}