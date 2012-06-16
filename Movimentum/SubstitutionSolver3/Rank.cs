//using System;

//namespace Movimentum.SubstitutionSolver3 {
//    public class Rank : IComparable<Rank> {
//        private readonly int _clazz;
//        private readonly int _orderInClass;

//        public Rank(int clazz, int orderInClass) {
//            _clazz = clazz;
//            _orderInClass = orderInClass;
//        }

//        public int CompareTo(Rank other) {
//            if (_clazz != other._clazz) {
//                return _clazz.CompareTo(other._clazz);
//            }
//            return _orderInClass.CompareTo(other._orderInClass);
//        }

//        public override string ToString() {
//            return "/" + _clazz + "." + _orderInClass + "/";
//        }

//        public static readonly Rank NULL = new Rank(0, 0);
//    }
//}