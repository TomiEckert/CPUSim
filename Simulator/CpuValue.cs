#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.Utils;

// ReSharper disable MemberCanBePrivate.Global

namespace Simulator {
    public struct CpuValue {
        internal CpuValue(string binary, int size) {
            Size = size;
            _bin = new string('0', size);
            Bin = binary;
        }

        private string _bin;

        public string Bin {
            get => _bin;
            set {
                CheckBin(value);
                CheckSize(value);
                _bin = value.PadLeft(Size, '0');
            }
        }

        public int Int {
            get => Convert.ToInt32(Bin, 2);
            set {
                if (value < 0)
                    throw new IncorrectFormatException();
                var binary = Convert.ToString(value, 2);
                Bin = binary;
            }
        }

        public int Size { get; }

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
        
        public static bool operator ==(CpuValue a, CpuValue b) {
            return a.Bin == b.Bin;
        }

        public static bool operator !=(CpuValue a, CpuValue b) {
            return a.Bin != b.Bin;
        }

        public override bool Equals(object? obj) {
            if (obj is CpuValue value)
                return Equals(value);
            return false;
        }

        public bool Equals(CpuValue other) {
            return Bin == other.Bin && Size == other.Size;
        }

        public override int GetHashCode() {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return HashCode.Combine(Bin, Size);
        }

        public static CpuValue FromInteger(int integer, int size) {
            var result = new CpuValue("0", size) {Int = integer};
            return result;
        }
    }
}