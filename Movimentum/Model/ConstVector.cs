namespace Movimentum.Model {
    public partial class ConstVector {
        public bool Is2D() {
            return !_z.HasValue;
        }

    }
}
