﻿using System.IO;
using System.Numerics;

namespace Parquet.Data.Concrete
{
   class Int96DataTypeHandler : BasicPrimitiveDataTypeHandler<BigInteger>
   {
      public Int96DataTypeHandler() : base(DataType.Int96, Thrift.Type.INT96)
      {
      }

      public override bool IsMatch(Thrift.SchemaElement tse, Options formatOptions)
      {
         return tse.Type == Thrift.Type.INT96 && !formatOptions.TreatBigIntegersAsDates;
      }

      protected override BigInteger ReadSingle(BinaryReader reader, Thrift.SchemaElement tse, int length)
      {
         byte[] data = reader.ReadBytes(12);
         var big = new BigInteger(data);
         return big;
      }

      protected override void WriteOne(BinaryWriter writer, BigInteger value)
      {
         byte[] data = value.ToByteArray();
         writer.Write(data);
      }
   }
}
