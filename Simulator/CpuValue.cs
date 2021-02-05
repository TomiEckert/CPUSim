#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.Utils;

// ReSharper disable MemberCanBePrivate.Global

namespace Simulator {
    public class CpuValue {
        internal CpuValue(string binary, int size) {
            Size = size;
            Bin = new string('0', size);
            CheckBin(binary);
            AssignBinary(binary);
        }

        public string Bin { get; private set; }

        public int Int => Convert.ToInt32(Bin, 2);

        public int Size { get; }

        public void SetBinary(string binary) {
            CheckBin(binary);
            AssignBinary(binary);
        }

        public void SetInteger(int integer) {
            if (integer < 0)
                throw new IncorrectFormatException();
            var binary = Convert.ToString(integer, 2);
            AssignBinary(binary);
        }

        private void AssignBinary(string binary) {
            CheckSize(binary);
            Bin = binary.PadLeft(Size, '0');
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void CheckSize(string binary) {
            if (binary.Length > Size)
                throw new IncorrectValueSizeException();
        }

        private void CheckBin(string binary) {
            try {
                var _ = Convert.ToInt32(Bin, 2);
            }
            catch (FormatException) {
                throw new IncorrectFormatException("CpuValue constructor value not binary: " + binary);
            }
        }

        internal static CpuValue FromBinary(IEnumerable<CpuBinary> binary) {
            var cpuBinaries = binary as CpuBinary[] ?? binary.ToArray();
            var bin = cpuBinaries.Aggregate(
                "", (current, cpuBinary) => current + (cpuBinary == CpuBinary.One ? "1" : "0"));
            return new CpuValue(bin, cpuBinaries.Length);
        }

        public static CpuValue FromInteger(int integer, int size) {
            if (integer < 0)
                throw new IncorrectFormatException();
            var bin = Convert.ToString(integer, 2);

            if (bin.Length > size)
                throw new IncorrectValueSizeException();

            bin = bin.PadLeft(size, '0');

            return new CpuValue(bin, size);
        }

        public static bool operator ==(CpuValue a, CpuValue b) {
            return a.Int == b.Int;
        }

        public static bool operator !=(CpuValue a, CpuValue b) {
            return a.Int != b.Int;
        }

        public override bool Equals(object? obj) {
            if (obj != null && obj.GetType() == typeof(CpuValue))
                return Equals((CpuValue) obj);
            return false;
        }

        protected bool Equals(CpuValue other) {
            return Bin == other.Bin && Size == other.Size;
        }

        public override int GetHashCode() {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return HashCode.Combine(Bin, Size);
        }
    }
}