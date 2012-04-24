using System.Collections.Generic;
using Movimentum.Model;

namespace Movimentum {
    public class Frame {
        private double _absoluteTime;
        private double _t;
        private double _iv;
        private IEnumerable<Constraint> _constraints;
        public Frame(double absoluteTime, double t, double iv, IEnumerable<Constraint> constraints) {
            _absoluteTime = absoluteTime;
            _t = t;
            _iv = iv;
            _constraints = constraints;
        }
    }
}