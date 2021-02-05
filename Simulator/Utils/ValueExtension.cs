using System;

namespace Simulator.Utils {
    public static class ValueExtension {
        internal static CpuValue ToCpuValue(this int integer, int size) {
            return CpuValue.FromInteger(integer, size);
        }

        internal static CpuValue ToCpuValueFromBin(this string text, int size) {
            var binary = text;
            try {
                var _ = Convert.ToInt32(binary, 2);
            }
            catch (FormatException) {
                throw new IncorrectFormatException();
            }

            if (binary.Length > size)
                throw new IncorrectValueSizeException();

            binary = binary.PadLeft(size, '0');

            return new CpuValue(binary, size);
        }

        internal static CpuValue ToCpuValueFromInt(this string text, int size) {
            var binary = text;
            try {
                var value = Convert.ToInt32(text);
                binary = Convert.ToString(value, 2);
            }
            catch (FormatException) {
                throw new IncorrectFormatException();
            }

            if (binary.Length > size)
                throw new IncorrectValueSizeException();

            binary = binary.PadLeft(size, '0');

            return new CpuValue(binary, size);
        }
    }
}