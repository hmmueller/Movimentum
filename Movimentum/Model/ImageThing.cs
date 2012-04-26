using System;
using System.Collections.Generic;
using System.Drawing;

namespace Movimentum.Model {
    public class ImageThing : Thing {
        private readonly Image _image;
        public ImageThing(string name, Image image, IEnumerable<ConstAnchor> anchors)
            : base(name, anchors) {
            _image = image;
        }

        public override void Draw(Graphics drawingPane, IDictionary<string, ConstVector> anchorLocations) {
            throw new NotImplementedException();
        }
    }
}