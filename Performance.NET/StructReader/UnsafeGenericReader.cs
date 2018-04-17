using System;
using System.Reflection.Emit;

namespace Performance.NET.StructReader
{
	public class UnsafeGenericReader<T> where T : struct
	{
		private readonly Func<byte[], int, T> _implementation;

		public UnsafeGenericReader()
		{
			var method = new DynamicMethod("StructureAtOffset",
				typeof(T), new[] {typeof(byte[]), typeof(int)},
				typeof(UnsafeGenericReader<T>).Module);
			var generator = method.GetILGenerator();
			generator.DeclareLocal(typeof(byte*), true);
			generator.Emit(OpCodes.Ldarg_0);
			generator.Emit(OpCodes.Ldarg_1);
			generator.Emit(OpCodes.Ldelema, typeof(byte));
			generator.Emit(OpCodes.Stloc_0);
			generator.Emit(OpCodes.Ldloc_0);
			generator.Emit(OpCodes.Conv_I);
			generator.Emit(OpCodes.Ldobj, typeof(T));
			generator.Emit(OpCodes.Ret);
			_implementation = (Func<byte[], int, T>) method.CreateDelegate(typeof(Func<byte[], int, T>));
		}

		public T StructureAtOffset(byte[] input, int offset) => _implementation(input, offset);
	}
}